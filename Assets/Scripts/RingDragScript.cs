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

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        ring = GetComponent<HanoiRingScript>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Only top ring can be dragged
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
        HanoiTowerScript nearest = GetNearestTower();

        if (nearest != null && nearest.CanPlaceRing(ring))
            PlaceOnTower(nearest);
        else
            PlaceOnTower(originalTower);
    }

    private HanoiTowerScript GetNearestTower()
    {
        // Compare canvas-space positions
        float distA = Vector2.Distance(rect.anchoredPosition, towerAPos.anchoredPosition);
        float distB = Vector2.Distance(rect.anchoredPosition, towerBPos.anchoredPosition);
        float distC = Vector2.Distance(rect.anchoredPosition, towerCPos.anchoredPosition);

        if (distA < distB && distA < distC) return towerA;
        if (distB < distC) return towerB;
        return towerC;
    }

    private void PlaceOnTower(HanoiTowerScript target)
    {
        // Remove from previous
        if (ring.currentTower != target)
        {
            ring.currentTower.rings.Pop();
            target.rings.Push(ring);
            ring.currentTower = target;
        }

        // Snap correctly to anchor
        rect.anchoredPosition = target.GetNextRingLocalPosition();
    }
}
