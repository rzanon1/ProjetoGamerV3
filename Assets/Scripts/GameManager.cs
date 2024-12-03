using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton para f�cil acesso ao GameManager.

    public float initialGameSpeed = 5f; // Velocidade inicial do jogo.
    public float gameSpeedIncrease = 0.1f; // Incremento na velocidade do jogo ao longo do tempo.
    public float gameSpeed { get; private set; } // Velocidade atual do jogo.

    // Refer�ncias para UI e bot�es.
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI HardText;
    public TextMeshProUGUI NormalText;
    public TextMeshProUGUI StartText;
    public Button retryButton;
    public Button hardButton;

    private Player player; // Refer�ncia ao jogador.
    private Spawner spawner; // Refer�ncia ao controlador de obst�culos.

    private float score; // Pontua��o atual do jogador.

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Define a inst�ncia �nica do GameManager.
        }
        else
        {
            DestroyImmediate(gameObject); // Garante que apenas uma inst�ncia existe.
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null; // Limpa a inst�ncia ao destruir o GameManager.
        }
    }

    private void Start()
    {
        gameSpeed = 0f; // Come�a com velocidade zero para aguardar o in�cio do jogo.
        enabled = false; // Desabilita a l�gica do GameManager at� o jogo come�ar.

        // Inicializa refer�ncias e configura��es.
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
        // Remove obst�culos existentes.
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        // Configura��es iniciais para um novo jogo.
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

        UpdateHighScore(); // Atualiza a pontua��o mais alta exibida.
    }

    public void HardGame()
    {
        // Configura��o para o modo dif�cil, semelhante ao novo jogo.
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        // Ajusta a velocidade e o incremento para o modo dif�cil.
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
        // Configura��es para o estado de Game Over.
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
        // Atualiza a velocidade e a pontua��o com base no tempo.
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;

        // Exibe a pontua��o atual, arredondada para baixo, no formato de 5 d�gitos.
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }

    private void UpdateHighScore()
    {
        // Verifica e atualiza a pontua��o mais alta no PlayerPrefs.
        float HighScore = PlayerPrefs.GetFloat("HighScore", 0);

        if (score > HighScore)
        {
            HighScore = score;
            PlayerPrefs.SetFloat("HighScore", HighScore); // Salva a nova pontua��o mais alta.
        }

        // Atualiza o texto exibido com a pontua��o mais alta.
        HighScoreText.text = Mathf.FloorToInt(HighScore).ToString("D5");
    }
}
