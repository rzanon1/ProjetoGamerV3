using System.Threading; // Biblioteca para funcionalidades de threading (n�o usada diretamente neste script).
using UnityEngine;

public class Ground : MonoBehaviour
{
    private MeshRenderer meshRenderer; // Refer�ncia ao componente MeshRenderer do ch�o.

    private void Awake()
    {
        // Inicializa a refer�ncia ao MeshRenderer anexado ao objeto.
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        // Calcula a velocidade do movimento do ch�o baseada na velocidade do jogo,
        // ajustada pela escala local no eixo X.
        float speed = GameManager.Instance.gameSpeed / transform.localScale.x;

        // Move a textura do material para criar a ilus�o de movimento no ch�o.
        // O deslocamento � no eixo X, proporcional � velocidade e ao tempo decorrido entre frames.
        meshRenderer.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
    }
}
