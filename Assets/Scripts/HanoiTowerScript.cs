using System.Collections.Generic;
using UnityEngine;

public class HanoiTowerScript : MonoBehaviour
{
    public Stack<HanoiRingScript> rings = new Stack<HanoiRingScript>();
    public Transform ringAnchor; // bottom position where rings stack

    // Position for the next ring
    public Vector3 GetNextRingLocalPosition()
    {
        float yOffset = rings.Count * 100f; // Adjust 50f depending on your ring height in UI units
        return ringAnchor.localPosition + new Vector3(0, yOffset, 0);
    }


    // Can a ring be placed here?
    public bool CanPlaceRing(HanoiRingScript ring)
    {
        if (rings.Count == 0) return true;
        return rings.Peek().size > ring.size;
    }
}
