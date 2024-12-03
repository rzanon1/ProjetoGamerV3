using System.Threading; // Biblioteca para funcionalidades de threading (não usada diretamente neste script).
using UnityEngine;

public class Ground : MonoBehaviour
{
    private MeshRenderer meshRenderer; // Referência ao componente MeshRenderer do chão.

    private void Awake()
    {
        // Inicializa a referência ao MeshRenderer anexado ao objeto.
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        // Calcula a velocidade do movimento do chão baseada na velocidade do jogo,
        // ajustada pela escala local no eixo X.
        float speed = GameManager.Instance.gameSpeed / transform.localScale.x;

        // Move a textura do material para criar a ilusão de movimento no chão.
        // O deslocamento é no eixo X, proporcional à velocidade e ao tempo decorrido entre frames.
        meshRenderer.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
    }
}
