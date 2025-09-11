using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public TextMeshProUGUI chronometerText;
    private float elapsedTime = 0f;
    private bool isRunning = true;

    void Update()
    {
        if (!isRunning) return;

        elapsedTime += Time.deltaTime;

        int hours = Mathf.FloorToInt(elapsedTime / 3600);
        int minutes = Mathf.FloorToInt((elapsedTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        chronometerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    // Optional: Methods to start/stop/reset the timer
    public void StartChronometer() => isRunning = true;
    public void StopChronometer() => isRunning = false;
    public void ResetChronometer()
    {
        elapsedTime = 0f;
        chronometerText.text = "00:00:00";
    }
}
