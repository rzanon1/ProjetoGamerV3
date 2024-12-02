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
        gameSpeed = 0f;
        enabled = false;

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

        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (var obstacle in obstacles) {

            Destroy(obstacle.gameObject);

        }

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

        UpdateHighScore();
    }

    public void HardGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (var obstacle in obstacles)
        {

            Destroy(obstacle.gameObject);

        }

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

    public void GameOver() {

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
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }


    private void UpdateHighScore() {

        float HighScore = PlayerPrefs.GetFloat("HighScore", 0);

        if (score > HighScore) { 
        
            HighScore = score;
            PlayerPrefs.SetFloat("HighScore", HighScore);
        
        }

        HighScoreText.text = Mathf.FloorToInt(HighScore).ToString("D5");

    }
}
