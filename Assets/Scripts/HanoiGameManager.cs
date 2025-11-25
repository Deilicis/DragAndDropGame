using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class HanoiGameManager : MonoBehaviour
{
    public HanoiTowerScript towerA;
    public HanoiTowerScript towerB;
    public HanoiTowerScript towerC;

    [Header("Rings")]
    public HanoiRingScript[] rings;
    public Button restartButton;
    public Button mainMenuButton;
    private int totalRings;

    public int moves = 0;
    public GameObject victoryScreen;
    public TMP_Text movesText;
    public TMP_Text movesTextScene;

    public Image[] stars;
    public bool gameWon = false;


    void Start()
    {
        // Sort largest -> smallest
        System.Array.Sort(rings, (a, b) => b.size.CompareTo(a.size));

        PlaceRingsOnStart();
        totalRings = rings.Length;

        AdsRewarded ads = FindFirstObjectByType<AdsRewarded>();
        if (ads != null)
            SubscribeToRewardedAd(ads);
    }

    void PlaceRingsOnStart()
    {
        towerA.rings.Clear();

        for (int i = 0; i < rings.Length; i++)
        {
            HanoiRingScript ring = rings[i];

            // Make ring a child of the tower
            ring.transform.SetParent(towerA.transform, false);

            // Assign correct local position
            ring.transform.localPosition = towerA.GetNextRingLocalPosition();

            towerA.rings.Push(ring);
            ring.currentTower = towerA;
        }
    }
    void Update()
    {
        if (towerC.rings.Count == totalRings)
        {
            WinGame();
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
    public void WinGame()
    {
        gameWon = true;

        victoryScreen.SetActive(true);
        movesText.text = "Moves: " + moves;

        int earnedStars = CalculateStars(moves);

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].enabled = (i < earnedStars);
        }
    }
    public void RemoveFiveMoves()
    {
        moves = Mathf.Max(0, moves - 5);
        movesText.text = "Moves: " + moves;
        movesTextScene.text = "Moves: " + moves;
        Debug.Log("Hanoi Tower: -5 moves reward applied.");
    }

    public void SubscribeToRewardedAd(AdsRewarded adsRewarded)
    {
        if (adsRewarded != null)
        {
            adsRewarded.OnRewardedHanoi += () =>
            {
                if (!gameWon)
                    RemoveFiveMoves();
            };
        }
    }

    private int CalculateStars(int moveCount)
    {
        // 3 stars = 31 or fewer moves
        // 2 stars = 32–45 moves
        // 1 star = 46+

        if (moveCount <= 31) return 3;
        if (moveCount <= 45) return 2;
        return 1;
    }

}
