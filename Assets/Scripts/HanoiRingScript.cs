using UnityEngine;

public class HanoiRingScript : MonoBehaviour
{
    public int size; // 1 = smallest, 4 = largest
    [HideInInspector] public HanoiTowerScript currentTower;
}
