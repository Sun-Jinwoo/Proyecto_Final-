using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Collectible : MonoBehaviour
{
    [Header("Configuración")]
    public int pointsValue = 1;                    // Cuánto suma al progreso (normalmente 1)
    public LevelManager levelManager;              // Referencia al LevelManager

    [Header("Efectos opcionales")]
    public ParticleSystem collectEffect;           // Partículas al recolectar (opcional)
    public AudioClip collectSound;                 // Sonido al recolectar (opcional)
    private AudioSource audioSource;

    private void Awake()
    {
        // Buscar automáticamente el LevelManager si no está asignado
        if (levelManager == null)
            levelManager = FindObjectOfType<LevelManager>();

        // Añadir AudioSource si hay sonido
        if (collectSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.clip = collectSound;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    private void Collect()
    {
        // Sumar al progreso
        levelManager.AddProgress(pointsValue);

        // Efectos visuales y sonoros
        if (collectEffect != null)
            Instantiate(collectEffect, transform.position, Quaternion.identity);

        if (audioSource != null)
            audioSource.Play();

        // Desactivar o destruir el objeto (recomendado desactivar para poder reutilizar en pools)
        gameObject.SetActive(false);
        // O bien: Destroy(gameObject);
    }

    // Método para reiniciar el objeto si reinicias el nivel
    public void ResetCollectible()
    {
        gameObject.SetActive(true);
    }
}