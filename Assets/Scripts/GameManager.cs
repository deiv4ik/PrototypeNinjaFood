using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    private float spawnRate = 1f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI volumeText;
    public Slider volumeSlider;
    public GameObject titleScreen;
    public GameObject panel;
    public bool isGameActive;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public Button restartButton;
    private bool paused;
    private int score;
    private int lives;
    public List<GameObject> target;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckForPaused();
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, target.Count);
            Instantiate(target[index]);   
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;

        StartCoroutine(SpawnTarget());
        score = 0;
    
        spawnRate /= difficulty;

        UpdateScore(0); 
        UpdateLives(3);

        titleScreen.gameObject.SetActive(false);
        volumeText.gameObject.SetActive(false);
        volumeSlider.gameObject.SetActive(false);
    }

    public void UpdateLives(int liveValue)
    {
        lives += liveValue;

        if (lives < 0)
            lives = 0;

        livesText.text = "Lives: " + lives;

        if (lives == 0)
            GameOver();
    }

    private void CheckForPaused()
    {
        if (!paused)
        {
            paused = true;
            panel.SetActive(true);
            Time.timeScale = 0;
        } else
        {
            paused = false;
            panel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
