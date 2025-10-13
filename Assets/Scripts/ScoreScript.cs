using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript instance;

    [Header("Score & Timer")]
    public int score = 0;
    public float timer = 0f;

    [Header("Car Tracking")]
    public int totalCars = 12;     // Total cars in the level
    public int placedCars = 0;     // Successfully placed cars
    public int destroyedCars = 0;  // Cars lost due to hazards

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public Image[] stars;
    public GameObject victoryMenu;  // Assign your Victory Menu panel here

    [Header("Buttons")]
    public Button[] restartButtons;
    public Button quitButton;
    public Button mainMenuButton;

    private bool gameRunning = true;
    private bool gameEnded = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (restartButtons != null && restartButtons.Length > 0)
        {
            foreach (Button btn in restartButtons)
            {
                if (btn != null)
                    btn.onClick.AddListener(RestartLevel);
            }
        }
        if(quitButton)
            quitButton.onClick.AddListener(Application.Quit);
        if (mainMenuButton)
            mainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    void Update()
    {
        if (gameRunning)
        {
            timer += Time.deltaTime;
        
        UpdateUI();

            // Check for win condition
            if (placedCars + destroyedCars >= totalCars && !gameEnded)
            {
                EndGame();
            }
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        placedCars += 1;
        UpdateUI();
    }

    public void AddDestroyedCar()
    {
        destroyedCars += 1;
        UpdateUI();
    }

    private void EndGame()
    {
        gameRunning = false;
        gameEnded = true;
        Time.timeScale = 0f;

        CalculateStars();
        if (victoryMenu) victoryMenu.SetActive(true);
    }


    private void UpdateUI()
    {
        int hours = Mathf.FloorToInt(timer / 3600);
        int minutes = Mathf.FloorToInt((timer % 3600) / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        if (scoreText) scoreText.text = $"{score}/{totalCars}";
        if (timerText) timerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    private void CalculateStars()
    {
        int starsEarned = 1;

        // Timer thresholds
        if (timer < 120) starsEarned = 3;
        else if (timer < 180) starsEarned = 2;

        // Penalize for missing or destroyed cars
        float completionRatio = (float)placedCars / totalCars;

        if (completionRatio < 1f)
        {
            if (completionRatio >= 0.75f)
                starsEarned = Mathf.Min(starsEarned, 2);
            else
                starsEarned = Mathf.Min(starsEarned, 1);
        }

        // Update UI
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].enabled = i < starsEarned;
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");
    }

}
