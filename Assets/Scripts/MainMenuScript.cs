using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public AudioSource effects;
    public AudioClip[] audioCli;
    public void StartGame1()
    {
        Debug.Log("Pressed 1");
        effects.PlayOneShot(audioCli[0]);
        SceneManager.LoadScene(1);
    }
    public void StartGame2()
    {
        effects.PlayOneShot(audioCli[0]);
       
    }
    public void Quit()
    {
        effects.PlayOneShot(audioCli[0]);
        Application.Quit();
    }
}
