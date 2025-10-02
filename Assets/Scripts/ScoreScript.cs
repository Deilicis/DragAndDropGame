using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript instance;  // Singleton

    [Header("Score & Timer")]
    public int score = 0;
    public float timer = 0f;

    [Header("UI")]
    public Text scoreText;
    public Text timerText;
    public Image[] stars;   // Assign 3 star images in the inspector

    private bool gameRunning = true;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (gameRunning)
        {
            timer += Time.deltaTime;
            UpdateUI();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public void StopGame()
    {
        gameRunning = false;
        CalculateStars();
    }

    private void UpdateUI()
    {
        if (scoreText) scoreText.text = "Score: " + score;
        if (timerText) timerText.text = "Time: " + timer.ToString("F1") + "s";
    }

    private void CalculateStars()
    {
        // Example star logic:
        // <30s = 3 stars
        // 30–60s = 2 stars
        // >60s = 1 star

        int starsEarned = 1;
        if (timer < 30) starsEarned = 3;
        else if (timer < 60) starsEarned = 2;

        // Update UI stars
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].enabled = i < starsEarned;
        }
    }
}