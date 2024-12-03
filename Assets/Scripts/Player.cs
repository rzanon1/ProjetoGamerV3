using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController character; // Referência ao componente CharacterController para gerenciar o movimento do jogador.
    private Vector3 direction; // Vetor que controla a direção e a velocidade do jogador.

    public float gravity = 9.81f * 2f; // Força gravitacional aplicada ao jogador (dobrada para maior impacto no jogo).
    public float jumpForce = 8f; // Força do pulo aplicada ao jogador.

    private void Awake()
    {
        // Obtém o componente CharacterController anexado ao mesmo GameObject.
        character = GetComponent<CharacterController>();
    }
    // Unity chama Awake automaticamente para inicializar variáveis e referências antes do jogo começar.

    private void OnEnable()
    {
        // Reseta a direção ao ativar o jogador.
        direction = Vector3.zero;
    }

    private void Update()
    {
        // Aplica gravidade ao jogador continuamente.
        direction += Vector3.down * gravity * Time.deltaTime;

        // Verifica se o jogador está no chão usando a propriedade `isGrounded` do CharacterController.
        if (character.isGrounded)
        {
            // Reseta a direção para não aplicar a gravidade enquanto tocar o chão.
            direction = Vector3.down;

            // Verifica se o botão "Jump" foi pressionado.
            if (Input.GetButton("Jump"))
            {
                // Aplica a força de pulo para o jogador.
                direction = Vector3.up * jumpForce;
            }
        }

        // Move o jogador usando o vetor de direção ajustado, multiplicado por `Time.deltaTime` para suavizar o movimento.
        character.Move(direction * Time.deltaTime);
    }
    // deltaTime é o tempo decorrido desde o último frame, garantindo movimentos suaves e consistentes.

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o jogador colidiu com um objeto marcado com a tag "Obstacle".
        if (other.CompareTag("Obstacle"))
        {
            // Chama o método GameOver no GameManager para encerrar o jogo.
            GameManager.Instance.GameOver();
        }
    }
}
