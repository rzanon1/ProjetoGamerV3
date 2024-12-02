using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioSource audioSource; // Arraste o Audio Source aqui no Inspector

    void Update()
    {
        // Exemplo: Toca o som ao pressionar a barra de espaço
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.Play();
        }
    }
}
