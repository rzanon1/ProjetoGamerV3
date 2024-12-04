using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance { get; private set; } // Singleton para acesso global.

    private AudioSource audioSource; // Referência ao componente AudioSource que controla a música de fundo.

    private void Awake()
    {
        // Configura o Singleton.
        if (Instance == null)
        {
            Instance = this; // Define esta instância como a única.
        }
        else
        {
            Destroy(gameObject); // Remove duplicatas do Singleton.
        }

        // Obtém o componente AudioSource.
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        // Toca a música apenas se ela não estiver tocando atualmente.
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        // Para a música apenas se ela estiver tocando.
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void SetVolume(float volume)
    {
        // Define o volume do áudio, garantindo que ele esteja no intervalo válido (entre 0 e 1).
        audioSource.volume = Mathf.Clamp01(volume);
    }
}
