using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController character; // Refer�ncia ao componente CharacterController para gerenciar o movimento do jogador.
    private Vector3 direction; // Vetor que controla a dire��o e a velocidade do jogador.

    public float gravity = 9.81f * 2f; // For�a gravitacional aplicada ao jogador (dobrada para maior impacto no jogo).
    public float jumpForce = 8f; // For�a do pulo aplicada ao jogador.

    private void Awake()
    {
        // Obt�m o componente CharacterController anexado ao mesmo GameObject.
        character = GetComponent<CharacterController>();
    }
    // Unity chama Awake automaticamente para inicializar vari�veis e refer�ncias antes do jogo come�ar.

    private void OnEnable()
    {
        // Reseta a dire��o ao ativar o jogador.
        direction = Vector3.zero;
    }

    private void Update()
    {
        // Aplica gravidade ao jogador continuamente.
        direction += Vector3.down * gravity * Time.deltaTime;

        // Verifica se o jogador est� no ch�o usando a propriedade `isGrounded` do CharacterController.
        if (character.isGrounded)
        {
            // Reseta a dire��o para n�o aplicar a gravidade enquanto tocar o ch�o.
            direction = Vector3.down;

            // Verifica se o bot�o "Jump" foi pressionado.
            if (Input.GetButton("Jump"))
            {
                // Aplica a for�a de pulo para o jogador.
                direction = Vector3.up * jumpForce;
            }
        }

        // Move o jogador usando o vetor de dire��o ajustado, multiplicado por `Time.deltaTime` para suavizar o movimento.
        character.Move(direction * Time.deltaTime);
    }
    // deltaTime � o tempo decorrido desde o �ltimo frame, garantindo movimentos suaves e consistentes.

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o jogador colidiu com um objeto marcado com a tag "Obstacle".
        if (other.CompareTag("Obstacle"))
        {
            // Chama o m�todo GameOver no GameManager para encerrar o jogo.
            GameManager.Instance.GameOver();
        }
    }
}
