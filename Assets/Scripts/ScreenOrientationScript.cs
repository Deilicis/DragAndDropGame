using UnityEngine;

public class SceneOrientationManager : MonoBehaviour
{
    private ScreenOrientation previousOrientation;

    private void Start()
    {
        // Save current orientation to restore later
        previousOrientation = Screen.orientation;

        // Force landscape
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        // Recommended for Android
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;

        Screen.orientation = ScreenOrientation.AutoRotation;
    }

    private void OnDestroy()
    {
        // Restore original orientation
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;

        Screen.orientation = previousOrientation;
    }
}
