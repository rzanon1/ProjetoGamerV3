using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource; // Referência ao componente AudioSource que controla a música de fundo.

    void Awake()
    {
        // Obtém o componente AudioSource do GameObject ao qual o script está anexado.
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
