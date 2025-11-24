using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    void Start()
    {
        // Sort largest -> smallest
        System.Array.Sort(rings, (a, b) => b.size.CompareTo(a.size));

        PlaceRingsOnStart();
        totalRings = rings.Length;
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
            Debug.Log("You Win!");
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
