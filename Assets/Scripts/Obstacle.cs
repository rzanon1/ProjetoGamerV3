using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float leftEdge; // Posi��o do limite esquerdo da tela onde o obst�culo ser� destru�do.

    private void Start()
    {
        // Calcula a posi��o do limite esquerdo da tela em coordenadas do mundo.
        // `Vector3.zero` representa o canto inferior esquerdo da tela, e `ScreenToWorldPoint` converte para coordenadas do mundo.
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
        // O valor `-2f` adiciona uma margem para garantir que o obst�culo seja removido um pouco depois de sair da tela.
    }

    private void Update()
    {
        // Move o obst�culo para a esquerda com base na velocidade do jogo.
        // A velocidade � ajustada por `Time.deltaTime` para garantir um movimento suave e consistente.
        transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime;

        // Verifica se o obst�culo saiu da tela (al�m do limite esquerdo).
        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject); // Destroi o GameObject para liberar mem�ria e manter o jogo eficiente.
        }
    }
}
