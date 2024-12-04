using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance { get; private set; } // Singleton para acesso global.

    private AudioSource audioSource; // Refer�ncia ao componente AudioSource que controla a m�sica de fundo.

    private void Awake()
    {
        // Configura o Singleton.
        if (Instance == null)
        {
            Instance = this; // Define esta inst�ncia como a �nica.
        }
        else
        {
            Destroy(gameObject); // Remove duplicatas do Singleton.
        }

        // Obt�m o componente AudioSource.
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
