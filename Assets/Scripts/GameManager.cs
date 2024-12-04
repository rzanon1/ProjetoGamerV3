using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI HardText;
    public TextMeshProUGUI NormalText;
    public TextMeshProUGUI StartText;
    public Button retryButton;
    public Button hardButton;

    private Player player;
    private Spawner spawner;

    private float score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        // Inicializa o jogo sem velocidade para aguardar o in�cio.
        gameSpeed = 0f;
        enabled = false;

        // Obt�m refer�ncias ao jogador e ao spawner.
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        // Desativa o jogador, o spawner e os elementos de interface.
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
        // Reinicia a m�sica ao come�ar um novo jogo.
        BackgroundMusic.Instance.PlayMusic();

        // Remove obst�culos existentes da cena.
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        // Configura��es iniciais para um novo jogo.
        gameSpeed = initialGameSpeed;
        score = 0f;
        enabled = true;

        // Ativa o jogador e o spawner.
        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);

        // Atualiza a interface para o estado do jogo.
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
        // Para a m�sica quando o jogador morre.
        BackgroundMusic.Instance.StopMusic();

        // Configura��es para o estado de Game Over.
        gameSpeed = 0f;
        enabled = false;

        // Desativa o jogador e o spawner.
        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);

        // Atualiza a interface para o estado de Game Over.
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        hardButton.gameObject.SetActive(true);
        HardText.gameObject.SetActive(true);
        NormalText.gameObject.SetActive(true);

        UpdateHighScore();
    }

    private void Update()
    {
        // Aumenta a velocidade do jogo e acumula a pontua��o ao longo do tempo.
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;

        // Atualiza o texto da pontua��o com 5 d�gitos.
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }

    private void UpdateHighScore()
    {
        // Verifica e atualiza a pontua��o mais alta no PlayerPrefs.
        float HighScore = PlayerPrefs.GetFloat("HighScore", 0);

        if (score > HighScore)
        {
            HighScore = score;
            PlayerPrefs.SetFloat("HighScore", HighScore);
        }

        // Atualiza o texto exibido com a pontua��o mais alta.
        HighScoreText.text = Mathf.FloorToInt(HighScore).ToString("D5");
    }
}
