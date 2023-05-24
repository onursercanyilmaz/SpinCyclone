using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGameHandler : MonoBehaviour
{
    private void OnMouseDown()
    {
        // Check if the cube is clicked
        if (gameObject.CompareTag("QuitButton"))
        {
            // Quit the game and load the main screen
            QuitGame();
        }
    }

    private void QuitGame()
    {
        // Add any necessary cleanup or save operations here

        // Load the main screen scene
        SceneManager.LoadScene("MainMenu");
    }
}
