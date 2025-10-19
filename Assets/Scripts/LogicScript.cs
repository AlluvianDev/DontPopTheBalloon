using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class LogicScript : MonoBehaviour
{   
    public int playerScore;
    public int playerTapeCount = 0;
    public AudioSource gameOverSound;
    public AudioSource gameOverMenuSound;
    public AudioSource tapeObtainedSound;
    public AudioSource windSound;
    public bool gameEnded = false;
    public GameObject gameOverScreen;
    public Text scoreText;
    public Text finalScoreText;
    public Text recordScoreText;
    public Text tapeText;
    public float timer;

    public string mainMenuScene = "MainMenuScene";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        addScore(1);
    }

    [ContextMenu("Add Score")]
    public void addScore(int score)
    {
        if (!gameEnded)
        {
            if (timer >= 0.5)
            {
                playerScore += score;
                scoreText.text = $"{playerScore}";
                timer = 0;
                windSound.Play();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    public bool addTape(int tapeCount)
    {
         // Check if game is already ended
        if (gameEnded)
        {
            return false;
        }
        // Add score and update UI
        playerTapeCount += tapeCount;
        tapeText.text = $"{playerTapeCount}";

        // Play sound effect
        tapeObtainedSound.Play();

        return true;
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        scoreText.gameObject.SetActive(true);
        tapeText.gameObject.SetActive(true);
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
    public void gameOver()
    {
        scoreText.gameObject.SetActive(false);
        tapeText.gameObject.SetActive(false);
        gameOverSound.Play();
        gameOverMenuSound.Play();
        gameEnded = true;
        gameOverScreen.SetActive(true);
        finalScoreText.text = "Final Score: " + scoreText.text;

        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (playerScore > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", playerScore);
            PlayerPrefs.Save();
            currentHighScore = playerScore;
        }
        recordScoreText.text = "Record: " + currentHighScore;
    }
}
