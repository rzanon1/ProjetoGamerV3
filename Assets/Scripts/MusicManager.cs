using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource; // Refer�ncia ao componente AudioSource que controla a m�sica de fundo.

    void Awake()
    {
        // Obt�m o componente AudioSource do GameObject ao qual o script est� anexado.
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        // Toca a m�sica apenas se ela n�o estiver tocando atualmente.
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        // Para a m�sica apenas se ela estiver tocando.
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void SetVolume(float volume)
    {
        // Define o volume do �udio, garantindo que ele esteja no intervalo v�lido (entre 0 e 1).
        audioSource.volume = Mathf.Clamp01(volume);
    }
}
