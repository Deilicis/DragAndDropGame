using UnityEngine;
using UnityEngine.EventSystems;

public class DropPlaceScript : MonoBehaviour, IDropHandler
{
    private float placeZRot, vehicleZRot, rotDiff;
    private Vector3 placeSiz, vehicleSiz;
    private float xSizeDiff, ySizeDiff;

    public ObjectScript objScript;
    private CarAndPlaceRandomizer randomizer;  

    private void Start()
    {
        randomizer = FindAnyObjectByType<CarAndPlaceRandomizer>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if ((eventData.pointerDrag != null) &&
            Input.GetMouseButtonUp(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
        {
            if (eventData.pointerDrag.tag.Equals(tag))
            {
                placeZRot = eventData.pointerDrag.GetComponent<RectTransform>().transform.eulerAngles.z;
                vehicleZRot = GetComponent<RectTransform>().transform.eulerAngles.z;
                rotDiff = Mathf.Abs(placeZRot - vehicleZRot);
                Debug.Log("Rotation difference: " + rotDiff);

                placeSiz = eventData.pointerDrag.GetComponent<RectTransform>().localScale;
                vehicleSiz = GetComponent<RectTransform>().localScale;
                xSizeDiff = Mathf.Abs(placeSiz.x - vehicleSiz.x);
                ySizeDiff = Mathf.Abs(placeSiz.y - vehicleSiz.y);
                Debug.Log("X size difference: " + xSizeDiff);
                Debug.Log("Y size difference: " + ySizeDiff);

                if ((rotDiff <= 5 || (rotDiff >= 355 && rotDiff <= 360)) &&
                    (xSizeDiff <= 0.10 && ySizeDiff <= 0.1))
                {
                    Debug.Log("Correct place");
                    objScript.rightPlace = true;
                    ScoreScript.instance.AddScore(1);
                    Debug.Log("Score: " + ScoreScript.instance.score);

                    var rect = eventData.pointerDrag.GetComponent<RectTransform>();
                    var targetRect = GetComponent<RectTransform>();
                    rect.anchoredPosition = targetRect.anchoredPosition;
                    rect.localRotation = targetRect.localRotation;
                    rect.localScale = targetRect.localScale;

                    PlayPlacementSound(eventData.pointerDrag.tag);
                }
            }
            else
            {
   
                objScript.rightPlace = false;
                objScript.effects.PlayOneShot(objScript.audioCli[1]);

                if (randomizer != null)
                {

                    randomizer.ResetCarToSpawn(eventData.pointerDrag.transform);
                }

            }
        }
    }

    private void PlayPlacementSound(string tag)
    {
        switch (tag)
        {
            case "Garbage": objScript.effects.PlayOneShot(objScript.audioCli[2]); break;
            case "Medicine": objScript.effects.PlayOneShot(objScript.audioCli[3]); break;
            case "Fire": objScript.effects.PlayOneShot(objScript.audioCli[4]); break;
            case "Traktor": objScript.effects.PlayOneShot(objScript.audioCli[5]); break;
            case "Ekscavator": objScript.effects.PlayOneShot(objScript.audioCli[6]); break;
            case "Police": objScript.effects.PlayOneShot(objScript.audioCli[7]); break;
            case "School": objScript.effects.PlayOneShot(objScript.audioCli[8]); break;
            case "Ekscavator2": objScript.effects.PlayOneShot(objScript.audioCli[9]); break;
            case "Cement": objScript.effects.PlayOneShot(objScript.audioCli[10]); break;
            case "B2": objScript.effects.PlayOneShot(objScript.audioCli[11]); break;
            case "E46": objScript.effects.PlayOneShot(objScript.audioCli[12]); break;
            case "E61": objScript.effects.PlayOneShot(objScript.audioCli[13]); break;
            default: Debug.Log("Unknown tag"); break;
        }
    }
}
