using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton para fácil acesso ao GameManager.

    public float initialGameSpeed = 5f; // Velocidade inicial do jogo.
    public float gameSpeedIncrease = 0.1f; // Incremento na velocidade do jogo ao longo do tempo.
    public float gameSpeed { get; private set; } // Velocidade atual do jogo.

    // Referências para UI e botões.
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI HardText;
    public TextMeshProUGUI NormalText;
    public TextMeshProUGUI StartText;
    public Button retryButton;
    public Button hardButton;

    private Player player; // Referência ao jogador.
    private Spawner spawner; // Referência ao controlador de obstáculos.

    private float score; // Pontuação atual do jogador.

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Define a instância única do GameManager.
        }
        else
        {
            DestroyImmediate(gameObject); // Garante que apenas uma instância existe.
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null; // Limpa a instância ao destruir o GameManager.
        }
    }

    private void Start()
    {
        gameSpeed = 0f; // Começa com velocidade zero para aguardar o início do jogo.
        enabled = false; // Desabilita a lógica do GameManager até o jogo começar.

        // Inicializa referências e configurações.
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();
        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(true);
        hardButton.gameObject.SetActive(true);
        HardText.gameObject.SetActive(true);
        NormalText.gameObject.SetActive(true);
        StartText.gameObject.SetActive(true);
    }

    public void NewGame()
    {
        // Remove obstáculos existentes.
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        // Configurações iniciais para um novo jogo.
        gameSpeed = initialGameSpeed;
        score = 0f;
        enabled = true;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        hardButton.gameObject.SetActive(false);
        HardText.gameObject.SetActive(false);
        NormalText.gameObject.SetActive(false);
        StartText.gameObject.SetActive(false);

        UpdateHighScore(); // Atualiza a pontuação mais alta exibida.
    }

    public void HardGame()
    {
        // Configuração para o modo difícil, semelhante ao novo jogo.
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        // Ajusta a velocidade e o incremento para o modo difícil.
        gameSpeed = initialGameSpeed * 3;
        gameSpeedIncrease = 0.2f;
        score = 0f;
        enabled = true;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        hardButton.gameObject.SetActive(false);
        HardText.gameObject.SetActive(false);
        NormalText.gameObject.SetActive(false);
        StartText.gameObject.SetActive(false);

        UpdateHighScore();
    }

    public void GameOver()
    {
        // Configurações para o estado de Game Over.
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        hardButton.gameObject.SetActive(true);
        HardText.gameObject.SetActive(true);
        NormalText.gameObject.SetActive(true);

        UpdateHighScore();
    }

    private void Update()
    {
        // Atualiza a velocidade e a pontuação com base no tempo.
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;

        // Exibe a pontuação atual, arredondada para baixo, no formato de 5 dígitos.
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }

    private void UpdateHighScore()
    {
        // Verifica e atualiza a pontuação mais alta no PlayerPrefs.
        float HighScore = PlayerPrefs.GetFloat("HighScore", 0);

        if (score > HighScore)
        {
            HighScore = score;
            PlayerPrefs.SetFloat("HighScore", HighScore); // Salva a nova pontuação mais alta.
        }

        // Atualiza o texto exibido com a pontuação mais alta.
        HighScoreText.text = Mathf.FloorToInt(HighScore).ToString("D5");
    }
}
