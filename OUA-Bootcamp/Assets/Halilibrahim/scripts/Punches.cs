using UnityEngine;

public class Punches : MonoBehaviour
{
    public Animator animator;
    public AudioSource[] audioSources; // Boþ objenin altýndaki Audio Source bileþenlerini tutan dizi
    private bool leftPunch;
    private bool rightPunch;
    private bool Run;
    private bool canPlayAudio = true;

    private void Start()
    {
        // Boþ objenin altýndaki Audio Source bileþenlerini alýn
        audioSources = GetComponentsInChildren<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            leftPunch = true;
            animator.SetBool("LeftPunch", leftPunch);
            PlayRandomAudioSource();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            leftPunch = false;
            animator.SetBool("LeftPunch", leftPunch);
        }

        if (Input.GetMouseButtonDown(1))
        {
            rightPunch = true;
            animator.SetBool("RightPunch", rightPunch);
            PlayRandomAudioSource();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            rightPunch = false;
            animator.SetBool("RightPunch", rightPunch);
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            Run = true;
            animator.SetBool("Run", Run);
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            Run = false;
            animator.SetBool("Run", Run);
        }
    }

    private void PlayRandomAudioSource()
    {
        if (canPlayAudio)
        {
            // Rastgele bir indeks seçin (0. ve 3. arasýnda)
            int randomIndex = Random.Range(0, 3);

            // Seçilen indeksteki Audio Source'u oynatýn
            audioSources[randomIndex].Play();

            canPlayAudio = false;
            Invoke("ResetAudioPlaying", 0.5f);
        }
    }

    private void ResetAudioPlaying()
    {
        canPlayAudio = true;
    }
}
