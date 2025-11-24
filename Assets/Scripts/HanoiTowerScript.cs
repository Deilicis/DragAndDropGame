using System.Collections.Generic;
using UnityEngine;

public class HanoiTowerScript : MonoBehaviour
{
    public Stack<HanoiRingScript> rings = new Stack<HanoiRingScript>();
    public Transform ringAnchor; // bottom position where rings stack

    public Vector2 GetNextRingLocalPosition()
    {
        float yOffset = rings.Count * 100f; // adjust for your UI ring height
        return ((RectTransform)ringAnchor).anchoredPosition + new Vector2(0, yOffset);
    }



    // Can a ring be placed here?
    public bool CanPlaceRing(HanoiRingScript ring)
    {
        if (rings.Count == 0) return true;
        return rings.Peek().size > ring.size;
    }
}
