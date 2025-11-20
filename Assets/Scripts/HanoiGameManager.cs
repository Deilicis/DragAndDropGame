using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HanoiTowerScript towerA;
    public HanoiTowerScript towerB;
    public HanoiTowerScript towerC;

    [Header("Rings")]
    public HanoiRingScript[] rings; // assign all rings here in Inspector

    private int totalRings;

    void Start()
    {
        InitializeRings();
        totalRings = rings.Length;

        // Sort rings by size (largest first)
        System.Array.Sort(rings, (a, b) => b.size.CompareTo(a.size));

        foreach (var ring in rings)
        {
            towerA.rings.Push(ring);
            ring.currentTower = towerA;
            ring.transform.position = towerA.GetNextRingPosition();
        }
    }

    void Update()
    {
        // Check win: all rings on TowerC
        if (towerC.rings.Count == totalRings)
        {
            Debug.Log("You Win!");
            // TODO: Show victory UI
        }
    }
    void InitializeRings()
    {
        foreach (var ring in rings)
        {
            float yOffset = towerA.rings.Count * 0.5f;
            ring.transform.position = towerA.ringAnchor.position + new Vector3(0, yOffset, 0);

            towerA.rings.Push(ring);
            ring.currentTower = towerA;
        }
    }
}
