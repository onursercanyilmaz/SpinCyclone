using UnityEngine;
using UnityEngine.UI;

public class GoBackToMainMenuButton : MonoBehaviour
{
    public GameObject mainTab;
    public GameObject singleBattleModeTab;



    private void OnBackToMenuButtonClick()
    {
        singleBattleModeTab.SetActive(false);
        mainTab.SetActive(true);
    }


      private void OnMouseDown()
    {
        // Check if the cube is clicked
        if (gameObject.CompareTag("GoBackToMainMenu"))
        {
            // Quit the game and load the main screen
            OnBackToMenuButtonClick();
        }
    }

    
}
