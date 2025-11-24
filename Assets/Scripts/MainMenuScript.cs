using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{
    public Button Game1Button;
    public Button Game2Button;
    public Button QuitButton;
    public AudioSource effects;
    public AudioClip[] audioCli;

    public void Game1()
    {
        effects.PlayOneShot(audioCli[0]);
        SceneManager.LoadScene("CityScene");
    }
    public void Game2()
    {
        effects.PlayOneShot(audioCli[0]);
        SceneManager.LoadScene("HanoiScene");
    }
    public void Quit()
    {
        effects.PlayOneShot(audioCli[0]);
        Application.Quit();
    }
}
