using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameObject character; // Karakter objesini tutmak için deðiþken
    public GameObject otherObject; // Diðer objeyi tutmak için deðiþken
    public float collisionDelay = 2f;
    public float collisionDelay2;// Tetiklemeden sonra sesin çalacaðý gecikme süresi
    public AudioSource audioSource; // Dýþarýdan atanacak olan AudioSource bileþeni

    private bool collided;

    private void Start()
    {
        collided = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == character)
        {
            collided = true;
            
            Invoke("PlayAudio", collisionDelay);
            Invoke("Destroy", collisionDelay2);


        }
    }

    private void PlayAudio()
    {
        if (collided && audioSource != null)
        {
            audioSource.Play();
        }
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
