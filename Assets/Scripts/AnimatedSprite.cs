using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites; // Array de sprites que serão usados na animação.

    private SpriteRenderer spriteRenderer; // Referência ao componente SpriteRenderer.
    private int frame; // Índice atual do sprite exibido na animação.

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Inicializa o SpriteRenderer pegando o componente no GameObject.
    }

    private void OnEnable()
    {
        Invoke(nameof(Animate), 0f); // Inicia a animação imediatamente ao ativar o GameObject.
    }
    // Aguarda o próximo frame para começar a animação.

    private void OnDisable()
    {
        CancelInvoke(); // Para a animação caso o GameObject seja desativado.
    }

    private void Animate()
    {
        frame++; // Avança para o próximo frame da animação.

        if (frame >= sprites.Length)
        {
            frame = 0; // Reinicia a animação quando chega ao último frame.
        }

        spriteRenderer.sprite = sprites[frame]; // Atualiza o sprite exibido no SpriteRenderer.

        // Agenda a próxima chamada do método Animate com base na velocidade do jogo.
        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);
    }
}
