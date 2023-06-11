using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMotor : MonoBehaviour
{
    [SerializeField] protected CharacterMotorConfig Config;

    protected Rigidbody LinkedRB;
    protected Collider LinkedCollider;
    protected float OriginalDrag;
    protected float CurrentCameraPitch = 0f;
    protected float CurrentCameraHorizontalPitch = 0f;

    public bool IsJumping { get; private set; } = false;
    public int JumpCount { get; private set; } = 0;
    public bool IsRunning { get; protected set; } = false;
    public bool IsGrounded { get; protected set; } = true;

    public float CurrentMaxSpeed
    {
        get
        {
            if (IsGrounded)
                return IsRunning ? Config.RunSpeed : Config.WalkSpeed;
            return Config.CanAirControl ? Config.AirControlMaxSpeed : 0f;
        }
    }

    private void Awake()
    {
        defaultYPos = LinkedCamera.transform.localPosition.y;

        LinkedRB = GetComponent<Rigidbody>();
        LinkedCollider = GetComponent<Collider>();
    }
    private void Start()
    {
        LinkedCollider.material = Config.Material_Default;
    }
    private void FixedUpdate()
    {
        RaycastHit groundCheckResult = UpdateIsGrounded();
        UpdateMovement(groundCheckResult);
    }

    private void LateUpdate()
    {
        UpdateCamera();
    }

    protected RaycastHit UpdateIsGrounded()
    {
        RaycastHit hitResult;

        if (JumpTimeRemaining > 0)// double jump için bu if
        {
            IsGrounded = false;
            return new RaycastHit();
        }

        Vector3 startPos = LinkedRB.position + Vector3.up * Config.Height * 0.5f;
        float groundCheckDistance = (Config.Height * 0.5f) + Config.GroundedCheckBuffer;
        //Debug.DrawLine(startPos, startPos + Vector3.down * currentHitDistance, Color.yellow);
        
        if (Physics.Raycast(startPos, Vector3.down, out hitResult, groundCheckDistance,
                            Config.GroundedLayerMask, QueryTriggerInteraction.Ignore))
        {
            IsGrounded = true;
            JumpCount = 0;
            JumpTimeRemaining = 0f;
        }
        else
        {
            IsGrounded = false;
        }

        return hitResult;
    }
    protected void UpdateMovement(RaycastHit groundCheckResult)
    {
        Vector3 movementVector = (transform.forward * _Input_Move.y + transform.right * _Input_Move.x);
        movementVector *= CurrentMaxSpeed;

        //are we on the ground
        if (IsGrounded)
        {
            //project onto the current surface
            movementVector = Vector3.ProjectOnPlane(movementVector, groundCheckResult.normal);

            //trying to move up a too steep slope
            if (movementVector.y > 0 && Vector3.Angle(Vector3.up, groundCheckResult.normal) > Config.SlopeLimit)
                movementVector = Vector3.zero;
        }
        else
        {
            movementVector += Vector3.down * Config.FallVelocity;
        }

        UpdateJumping(ref movementVector);

        LinkedRB.velocity = Vector3.MoveTowards(LinkedRB.velocity, movementVector, Config.Acceleration);
    }


    [SerializeField] Transform LinkedCamera;
    protected void UpdateCamera()
    {
        float cameraYawDelta = _Input_Look.x * Config.Camera_HorizontalSensitivity * Time.deltaTime;
        float cameraPitchDelta = _Input_Look.y * Config.Camera_VerticalSensitivity * Time.deltaTime * (Config.Camera_InvertY ? 1f : -1f); //time delta time olmasa nolurdu acaba burada

        // rotate the character 
        transform.localRotation = transform.localRotation * Quaternion.Euler(0f, cameraYawDelta, 0f);//quaternionlarda ekleme yapmak için çarpmak. çýkarma yapmak için bölmen lazýmmýþ. transform.localRotation adlý quaterniona ekleme yaptýk. bi saðýndaki quaternionla çarparak

        HandleHeadbobAndFootstep();

        // tilt the camera
        CurrentCameraPitch = Mathf.Clamp(CurrentCameraPitch + cameraPitchDelta, Config.Camera_MinPitch, Config.Camera_MaxPitch);
        LinkedCamera.transform.localRotation = Quaternion.Euler(CurrentCameraPitch, 0, 0);
    }


    private float timer;
    private float defaultYPos;
    private float headbobTimerMultiplier = 1.3f;
    protected void HandleHeadbobAndFootstep()
    {
        if (!IsGrounded) return;

        float currentSpeed = LinkedRB.velocity.magnitude;
        if (currentSpeed >= Config.Headbob_MinSpeedToBob)
        {
            var old_value = Mathf.Sin(timer);
            timer += Time.deltaTime * CurrentMaxSpeed * headbobTimerMultiplier;
            var current_value = Mathf.Sin(timer);
            
            //if (old_value >= 0 && current_value <= 0)
                //LinkedSource.PlayOneShot(FootstepSounds[UnityEngine.Random.Range(0, FootstepSounds.Count)], LinkedRB.velocity.magnitude * 0.1f);


            LinkedCamera.transform.localPosition = new Vector3(
              LinkedCamera.transform.localPosition.x,
              defaultYPos + Mathf.Sin(timer) * /*CurrentMaxSpeed **/ (IsRunning ? Config.Headbob_RunBobAmount : Config.Headbob_WalkBobAmount),
              LinkedCamera.transform.localPosition.z
              );
        }
    }



    protected float JumpTimeRemaining = 0f;
    protected void UpdateJumping(ref Vector3 movementVector)
    {
        bool triggeredJumpThisFrame = false;
        if (_Input_Jump)//press and release yapýnca boolean resetleniyo
        {
            _Input_Jump = false;
            bool triggerJump = true;

            int numJumpsPermitted = Config.CanDoubleJump ? 2 : 1;

            if (JumpCount >= numJumpsPermitted)
                triggerJump = false;
            if (!IsGrounded && !IsJumping)
                triggerJump = false;

            if (triggerJump)
            {
                if (JumpCount == 0)
                    triggeredJumpThisFrame = true;

                LinkedCollider.material = Config.Material_Jumping;

                LinkedRB.drag = 0;
                JumpTimeRemaining += Config.JumpTime;

                IsJumping = true;
                ++JumpCount;
            }
        }
        if (IsJumping)
        {
            if (!triggeredJumpThisFrame)
                JumpTimeRemaining -= Time.deltaTime;

            if (JumpTimeRemaining <= 0)
                IsJumping = false;
            else
            {
                Vector3 startPos = LinkedRB.position + Vector3.up * Config.Height * 0.5f;
                float ceilingCheckRadius = Config.Radius + Config.CeilingCheckRadiusBuffer;
                float ceilingCheckDistance = (Config.Height * 0.5f) - Config.Radius + Config.CeilingCheckRangeBuffer;

                RaycastHit ceilingHitResult;
                if (Physics.SphereCast
                    (
                    startPos,
                    ceilingCheckRadius,
                    Vector3.up,
                    out ceilingHitResult,
                    ceilingCheckDistance,
                    Config.CeilingCheckLayerMask,
                    QueryTriggerInteraction.Ignore)
                    )
                {
                    Debug.Log("ceiling");
                    IsJumping = false;
                    JumpTimeRemaining = 0f;
                    movementVector.y = 0f;
                }
                else
                {
                    movementVector.y = Config.JumpVelocity;
                }
            }
        }
        
    }
    protected Vector2 _Input_Move;
    private void OnMove(InputValue value)
    {
        _Input_Move = value.Get<Vector2>();
    }

    protected Vector2 _Input_Look;
    private void OnLook(InputValue value)
    {
        _Input_Look = value.Get<Vector2>();
    }
    protected bool _Input_Jump;
    protected void OnJump(InputValue value)
    {
        _Input_Jump = value.isPressed;
    }
}
