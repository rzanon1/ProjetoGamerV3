using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        // Obtém o componente MeshRenderer
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        // Calcula a velocidade do scroll com base na escala do objeto
        float speed = GameManager.Instance.gameSpeed / transform.localScale.x;

        // Atualiza o deslocamento da textura do material
        meshRenderer.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
    }
}
