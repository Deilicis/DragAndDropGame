using UnityEngine;

public class RingDragScript : MonoBehaviour
{
    private Camera cam;
    private bool dragging = false;
    private Vector3 offset;

    private HanoiRingScript ring;
    private HanoiTowerScript originalTower;

    [Header("Towers")]
    public HanoiTowerScript towerA;
    public HanoiTowerScript towerB;
    public HanoiTowerScript towerC;

    private void Start()
    {
        cam = Camera.main;
        ring = GetComponent<HanoiRingScript>();
    }

    void OnMouseDown()
    {
        if (ring.currentTower.rings.Peek() != ring)
            return;

        dragging = true;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;

        offset = transform.position - mouseWorld;
        originalTower = ring.currentTower;
    }


    void OnMouseUp()
    {
        dragging = false;

        HanoiTowerScript nearest = GetNearestTower();

        if (nearest != null && nearest.CanPlaceRing(ring))
        {
            PlaceOnTower(nearest);
        }
        else
        {
            PlaceOnTower(originalTower); // snap back
        }
    }

    void Update()
    {
        if (!dragging) return;

        Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos + offset;
    }

    void PlaceOnTower(HanoiTowerScript target)
    {
        if (ring.currentTower != target)
        {
            ring.currentTower.rings.Pop();
            target.rings.Push(ring);
            ring.currentTower = target;
        }

        transform.position = target.GetNextRingLocalPosition();
    }

    HanoiTowerScript GetNearestTower()
    {
        HanoiTowerScript nearest = null;
        float minDist = float.MaxValue;

        HanoiTowerScript[] towers = new HanoiTowerScript[] { towerA, towerB, towerC };

        foreach (var t in towers)
        {
            float dist = Vector2.Distance(transform.position, t.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = t;
            }
        }

        return nearest;
    }
}
