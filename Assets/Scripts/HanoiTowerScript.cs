using System.Collections.Generic;
using UnityEngine;

public class HanoiTowerScript : MonoBehaviour
{
    public Stack<HanoiRingScript> rings = new Stack<HanoiRingScript>();

    public Transform ringAnchor; // bottom position

    public Vector3 GetNextRingPosition()
    {
        float yOffset = rings.Count * 0.75f; // adjust to your ring height
        return ringAnchor.position + new Vector3(0, yOffset, 0);
    }

    public bool CanPlaceRing(HanoiRingScript ring)
    {
        if (rings.Count == 0) return true;
        return rings.Peek().size > ring.size;
    }
}
