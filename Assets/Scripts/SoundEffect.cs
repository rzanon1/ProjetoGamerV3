using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioSource audioSource; 

    void Update()
    {
        // Toca o som ao pressionar a barra de espaço
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.Play();
        }
    }
}
