using UnityEngine;
using UnityEngine.UI;

public class MainMenuControls : MonoBehaviour
{
    public GameObject mainTab;
    public GameObject singleBattleModeTab;

    public Button singleBattleModeButton;
    public Button quitButton;

    private void Start()
    {
        singleBattleModeButton.onClick.AddListener(OnSingleBattleModeButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    private void OnSingleBattleModeButtonClick()
    {
        mainTab.SetActive(false);
        singleBattleModeTab.SetActive(true);
    }

    private void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
