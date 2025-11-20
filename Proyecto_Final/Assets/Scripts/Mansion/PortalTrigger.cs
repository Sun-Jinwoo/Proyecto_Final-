using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    [Header("Referencia obligatoria")]
    public LevelManager levelManager;   // Arrástralo desde el Inspector (el GameObject con LevelManager)

    [Header("Opcional: Efectos al entrar")]
    public ParticleSystem enterEffect;  // Partículas al pasar por el portal
    public AudioClip enterSound;        // Sonido al entrar

    private AudioSource audioSource;

    private void Awake()
    {
        // Crear AudioSource si hay sonido
        if (enterSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.clip = enterSound;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Solo reaccionar si es el Player y la misión está completada
        if (other.CompareTag("Player") && levelManager != null)
        {
            // Llamar al método de victoria en LevelManager
            levelManager.OnPlayerEnterPortal();

            // Efectos opcionales
            if (enterEffect != null)
                Instantiate(enterEffect, transform.position, Quaternion.identity);

            if (audioSource != null)
                audioSource.Play();
        }
    }
}