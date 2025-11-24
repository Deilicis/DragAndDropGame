using UnityEngine;

public class HanoiGameManager : MonoBehaviour
{
    public HanoiTowerScript towerA;
    public HanoiTowerScript towerB;
    public HanoiTowerScript towerC;

    [Header("Rings")]
    public HanoiRingScript[] rings;

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
        towerA.rings.Clear();  // IMPORTANT!

        for (int i = 0; i < rings.Length; i++)
        {
            HanoiRingScript ring = rings[i];

            // Get correct Y position based on stack count
            Vector3 pos = towerA.GetNextRingPosition();

            ring.transform.position = pos;

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
}
