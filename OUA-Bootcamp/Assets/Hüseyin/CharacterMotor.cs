using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            if (IsGrounded || IsWallRunning || IsSliding)
                return IsSliding ? Config.SlideSpeed : IsWallRunning ? Config.RunSpeed : Config.WalkSpeed;
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
        //Debug.Log(LinkedRB.velocity.magnitude + " veloctiy");
    }

    private void LateUpdate()
    {
        UpdateCamera();
    }

    protected RaycastHit UpdateIsGrounded()
    {
        RaycastHit hitResult;

        if (JumpTimeRemaining > 0)// 
        {
            IsGrounded = false;
            return new RaycastHit();
        }

        Vector3 startPos = LinkedRB.position + Vector3.up * Config.Height * 0.5f;
        //Debug.DrawLine(startPos, startPos + Vector3.down * currentHitDistance, Color.yellow);
        float groundCheckDistance = (Config.Height * 0.5f) + Config.GroundedCheckBuffer;

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
        //if (IsSliding)
        //{
        //    _Input_Move = new Vector2(SlidingVector.x, SlidingVector.y);
        //}

        Vector3 movementVector = (transform.forward * _Input_Move.y + transform.right * _Input_Move.x);
        if (IsSliding)
            movementVector = transform.forward * SlidingVector.y + transform.right * SlidingVector.x;
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
        UpdateWallrun(ref movementVector);
        UpdateSliding();
        LinkedRB.velocity = Vector3.MoveTowards(LinkedRB.velocity, movementVector, Config.Acceleration);
    }
    [SerializeField] Transform LinkedCamera;
    protected void UpdateCamera()
    {
        float cameraYawDelta = _Input_Look.x * Config.Camera_HorizontalSensitivity * Time.deltaTime;
        float cameraPitchDelta = _Input_Look.y * Config.Camera_VerticalSensitivity * Time.deltaTime * (Config.Camera_InvertY ? 1f : -1f);

        // rotate the character 
        if (!IsSliding)
            transform.localRotation = transform.localRotation * Quaternion.Euler(0f, cameraYawDelta, 0f);

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
        if (!IsGrounded && !IsWallRunning || IsSliding) return;

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
              defaultYPos + Mathf.Sin(timer) * /*CurrentMaxSpeed **/ (IsWallRunning ? Config.Headbob_RunBobAmount : Config.Headbob_WalkBobAmount),
              LinkedCamera.transform.localPosition.z
              );
        }
    }
    protected float JumpTimeRemaining = 0f;
    protected void UpdateJumping(ref Vector3 movementVector)
    {
        bool triggeredJumpThisFrame = false;
        if (_Input_Jump)
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
        else
        {
            wallRight = false; wallLeft = false; IsWallRunning = false;
            transform.localRotation = Quaternion.Lerp(Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, 0), transform.localRotation, Mathf.Pow(.01f, Time.deltaTime));
        }

    }

    protected void UpdateWallrun(ref Vector3 movementVector)
    {
        if (!IsGrounded)
        {
            if (Mathf.Abs(_Input_Move.x) > 0 && _Input_Move.y > 0)
            {
                Vector3 start = LinkedRB.position + Vector3.down * Config.Height * 0.5f;
                if (_Input_Move.x > 0)
                    wallRight = Physics.Raycast(start, transform.right, out rightWallHit, Config.WallCheckDistance, Config.WallLayer);
                else if (_Input_Move.x < 0)
                    wallLeft = Physics.Raycast(start, -transform.right, out leftWallHit, Config.WallCheckDistance, Config.WallLayer);

                if (wallLeft || wallRight)
                {
                    IsWallRunning = true;
                    Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;
                    Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

                    if ((transform.forward - wallForward).magnitude > (transform.forward - -wallForward).magnitude)
                        wallForward = -wallForward;

                    //currentAngle = Mathf.SmoothStep(currentAngle, wallLeft ? -1 : 1, angleTimer);
                    currentAngle = wallLeft ? -25 : 25;
                    //transform.RotateAround(start, transform.forward, currentAngle);
                    transform.localRotation = Quaternion.Lerp(Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, currentAngle), transform.localRotation, Mathf.Pow(.05f, Time.deltaTime));


                    Debug.DrawLine(start, start + (wallRight ? transform.right : -transform.right) * Config.WallCheckDistance, Color.yellow);
                    LinkedRB.velocity = new Vector3(LinkedRB.velocity.x, 0f, LinkedRB.velocity.z);
                    LinkedRB.useGravity = false;
                    movementVector = wallForward * CurrentMaxSpeed;
                    movementVector.y = 0;
                    LinkedRB.AddForce(-wallNormal * 100, ForceMode.Force);
                    //JumpTimeRemaining = 0.1f;
                }
                else
                {
                    IsWallRunning = false;
                    //transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
                }
            }
        }
    }
    private float currentAngle = 0;
    private float angleTimer = 0;
    public bool wallLeft;
    public bool wallRight;
    public RaycastHit leftWallHit;
    public RaycastHit rightWallHit;
    public bool IsWallRunning = false;
    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, transform.right, out rightWallHit, Config.WallCheckDistance, Config.WallLayer);
        wallLeft = Physics.Raycast(transform.position, -transform.right, out leftWallHit, Config.WallCheckDistance, Config.WallLayer);
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

    protected bool IsSliding = false;
    protected float SlideTimeRemaining = 0f;
    protected bool _Input_Slide;
    protected Vector2 SlidingVector;
    protected void OnSlide(InputValue value)
    {
        _Input_Slide = value.isPressed;
    }
    protected void UpdateSliding()
    {
        bool triggeredSlideThisFrame = false;
        if (_Input_Slide)
        {
            _Input_Slide = false;
            bool shouldWeTryToSlide = true;

            if (!IsGrounded)
                shouldWeTryToSlide = false;
            if (IsWallRunning)
                shouldWeTryToSlide = false;
            if (IsSliding)
                shouldWeTryToSlide = false;
            if (Config.VelocityThresholdForSliding > LinkedRB.velocity.magnitude)
                shouldWeTryToSlide = false;
            if (_Input_Move.y <= 0)
                shouldWeTryToSlide = false;
            if (Mathf.Abs(_Input_Move.x) > 0.7f)
                shouldWeTryToSlide = false;

            if (shouldWeTryToSlide)
            {
                triggeredSlideThisFrame = true;
                SlidingVector = new Vector2(_Input_Move.x, _Input_Move.y);
                SlideTimeRemaining += Config.MaxSlideTime;
                IsSliding = true;
                currentAngle = -50;
            }
        }
        if (IsSliding)
        {
            if (!triggeredSlideThisFrame)
                SlideTimeRemaining -= Time.deltaTime;

            if (SlideTimeRemaining <= 0)
            {
                Vector3 startPos = LinkedRB.position;
                RaycastHit ceilingHitResult;

                if (Physics.Raycast(startPos, Vector3.up, out ceilingHitResult, Config.UnderWallCheckDistance))
                {
                    Debug.Log("true ??km?yor mu");
                    IsSliding = true;
                }
                else
                {
                    SlidingVector = Vector2.zero;
                    SlideTimeRemaining = 0f;
                    IsSliding = false;
                }

            }

            //transform.localRotation = Quaternion.Lerp(Quaternion.Euler(currentAngle, transform.localEulerAngles.y, transform.localEulerAngles.z), transform.localRotation, Mathf.Pow(.01f, Time.deltaTime));
            transform.localRotation = Quaternion.Lerp(Quaternion.Euler(currentAngle, transform.localEulerAngles.y, -15), transform.localRotation, Mathf.Pow(.01f, Time.deltaTime));
        }
        else
        {
            transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0, transform.localEulerAngles.y, transform.localEulerAngles.z), transform.localRotation, Mathf.Pow(.01f, Time.deltaTime));
        }
    }
}
