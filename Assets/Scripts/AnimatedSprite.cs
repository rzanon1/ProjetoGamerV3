using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites; // Array de sprites que ser�o usados na anima��o.

    private SpriteRenderer spriteRenderer; // Refer�ncia ao componente SpriteRenderer.
    private int frame; // �ndice atual do sprite exibido na anima��o.

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Inicializa o SpriteRenderer pegando o componente no GameObject.
    }

    private void OnEnable()
    {
        Invoke(nameof(Animate), 0f); // Inicia a anima��o imediatamente ao ativar o GameObject.
    }
    // Aguarda o pr�ximo frame para come�ar a anima��o.

    private void OnDisable()
    {
        CancelInvoke(); // Para a anima��o caso o GameObject seja desativado.
    }

    private void Animate()
    {
        frame++; // Avan�a para o pr�ximo frame da anima��o.

        if (frame >= sprites.Length)
        {
            frame = 0; // Reinicia a anima��o quando chega ao �ltimo frame.
        }

        spriteRenderer.sprite = sprites[frame]; // Atualiza o sprite exibido no SpriteRenderer.

        // Agenda a pr�xima chamada do m�todo Animate com base na velocidade do jogo.
        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);
    }
}
