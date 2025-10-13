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

    void Start()
    {
        // Assign button listeners
        if (Game1Button)
            Game1Button.onClick.AddListener(Game1);
        if (Game1Button)
            Game2Button.onClick.AddListener(Game2);
        if (QuitButton)
            QuitButton.onClick.AddListener(Quit);
    }
    public void Game1()
    {
        effects.PlayOneShot(audioCli[0]);
        SceneManager.LoadScene("CityScene");
    }
    public void Game2()
    {
        effects.PlayOneShot(audioCli[0]);
    }
    public void Quit()
    {
        effects.PlayOneShot(audioCli[0]);
        Application.Quit();
    }
}
