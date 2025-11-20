using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public float openAngle = 90f;           // Cambia a -90 si abre al otro lado
    public AudioClip openSound;             // Opcional
    public AudioClip closeSound;            // Opcional

    private bool isOpen = false;
    private bool playerInRange = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private AudioSource audioSource;

    private void Awake()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(0f, openAngle, 0f) * closedRotation;

        if (openSound || closeSound)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            transform.rotation = isOpen ? openRotation : closedRotation;

            if (audioSource)
            {
                audioSource.clip = isOpen ? openSound : closeSound;
                if (audioSource.clip) audioSource.Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}