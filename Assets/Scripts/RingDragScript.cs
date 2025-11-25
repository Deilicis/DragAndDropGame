using UnityEngine;
using UnityEngine.EventSystems;

public class RingDragScript : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rect;
    private Canvas canvas;
    private Vector2 offset;

    private HanoiRingScript ring;
    private HanoiTowerScript originalTower;

    public RectTransform towerAPos;
    public RectTransform towerBPos;
    public RectTransform towerCPos;

    public HanoiTowerScript towerA;
    public HanoiTowerScript towerB;
    public HanoiTowerScript towerC;
    private HanoiGameManager gm;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        ring = GetComponent<HanoiRingScript>();

        gm = FindObjectOfType<HanoiGameManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (gm.gameWon) return;  // <- BLOCK input

        if (ring.currentTower.rings.Peek() != ring)
            return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out var localMousePos);

        offset = rect.anchoredPosition - localMousePos;
        originalTower = ring.currentTower;
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (gm.gameWon) return;  // <- BLOCK input

        if (ring.currentTower.rings.Peek() != ring)
            return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out var localMousePos);

        rect.anchoredPosition = localMousePos + offset;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if (gm.gameWon) return;  // <- BLOCK input

        HanoiTowerScript nearest = GetNearestTower();

        if (nearest != null && nearest.CanPlaceRing(ring))
            PlaceOnTower(nearest);
        else
            PlaceOnTower(originalTower);
    }


    private HanoiTowerScript GetNearestTower()
    {
        Vector3 ringWorldPos = rect.position;

        float distA = Vector3.Distance(ringWorldPos, towerAPos.position);
        float distB = Vector3.Distance(ringWorldPos, towerBPos.position);
        float distC = Vector3.Distance(ringWorldPos, towerCPos.position);

        if (distA < distB && distA < distC) return towerA;
        if (distB < distC) return towerB;
        return towerC;
    }


    private void PlaceOnTower(HanoiTowerScript target)
    {
        if (ring.currentTower != target)
        {
            ring.currentTower.rings.Pop();
            target.rings.Push(ring);
            ring.currentTower = target;

            // Count the move
            HanoiGameManager gm = FindObjectOfType<HanoiGameManager>();

            gm.moves++;
            gm.movesText.text = "Moves: " + gm.moves;
            gm.movesTextScene.text = "Moves: " + gm.moves;


        }


        // Make ring a child of the tower BEFORE setting position
        rect.SetParent(target.transform, false);

        // Now the localPosition aligns with the tower properly
        rect.anchoredPosition = target.GetNextRingLocalPosition();
    }

}
