using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public Button playButton;
    public Button settingsButton;

    private void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonClick);
        settingsButton.onClick.AddListener(OnSettingsButtonClick);
    }

    private void OnPlayButtonClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void OnSettingsButtonClick()
    {
        // Add code here to open the settings menu
    }
}
