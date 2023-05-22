using UnityEngine;
using UnityEngine.UI;

public class SingleBattleSpinSelectManager : MonoBehaviour
{
    public GameObject[] playerSpheres; // Array of player's sphere game objects
    public GameObject[] rivalSpheres; // Array of rival's sphere game objects
    public Button changeButton; // Button to change the player's sphere
    public Button rivalChangeButton; // Button to change the rival's sphere
    public Text playerSphereNameText; // Text component to display the player's sphere name
    public Text rivalSphereNameText; // Text component to display the rival's sphere name

    private int currentPlayerSphereIndex; // Index of the player's current sphere
    private int currentRivalSphereIndex; // Index of the rival's current sphere

    // Nested class for storing spin data
    public static class SpinData
    {
        public static int currentPlayerIndex;
        public static int currentRivalIndex;
    }

    private void Start()
    {
        // Initialize the current sphere indices to 0
        currentPlayerSphereIndex = 0;
        currentRivalSphereIndex = 0;

        // Set the initial spheres active, and others inactive
        SetSpheresVisibility(playerSpheres, currentPlayerSphereIndex);
        SetSpheresVisibility(rivalSpheres, currentRivalSphereIndex);

        // Update the sphere name texts
        UpdateSphereName(playerSphereNameText, playerSpheres, currentPlayerSphereIndex);
        UpdateSphereName(rivalSphereNameText, rivalSpheres, currentRivalSphereIndex);

        // Add listeners to the button click events
        changeButton.onClick.AddListener(ChangePlayerSphere);
        rivalChangeButton.onClick.AddListener(ChangeRivalSphere);
    }

    private void ChangePlayerSphere()
    {
        // Disable the current player's sphere
        playerSpheres[currentPlayerSphereIndex].SetActive(false);

        // Increment the player's sphere index
        currentPlayerSphereIndex++;

        // Wrap around to the first sphere if the index exceeds the array length
        if (currentPlayerSphereIndex >= playerSpheres.Length)
        {
            currentPlayerSphereIndex = 0;
        }

        // Enable the new player's sphere
        playerSpheres[currentPlayerSphereIndex].SetActive(true);

        // Update the sphere name text for the player
        UpdateSphereName(playerSphereNameText, playerSpheres, currentPlayerSphereIndex);
    }

    private void ChangeRivalSphere()
    {
        // Disable the current rival's sphere
        rivalSpheres[currentRivalSphereIndex].SetActive(false);

        // Increment the rival's sphere index
        currentRivalSphereIndex++;

        // Wrap around to the first sphere if the index exceeds the array length
        if (currentRivalSphereIndex >= rivalSpheres.Length)
        {
            currentRivalSphereIndex = 0;
        }

        // Enable the new rival's sphere
        rivalSpheres[currentRivalSphereIndex].SetActive(true);

        // Update the sphere name text for the rival
        UpdateSphereName(rivalSphereNameText, rivalSpheres, currentRivalSphereIndex);
    }

    private void SetSpheresVisibility(GameObject[] spheres, int activeIndex)
    {
        // Loop through all the spheres and set their visibility based on the active index
        for (int i = 0; i < spheres.Length; i++)
        {
            spheres[i].SetActive(i == activeIndex);
        }
    }

    private void UpdateSphereName(Text sphereNameText, GameObject[] spheres, int activeIndex)
    {
        // Get the name of the current sphere and set it as the text
        string sphereName = spheres[activeIndex].name;
        sphereNameText.text = sphereName;
    }

    public void StartBattle()
    {
        // Store the selected indexes in the SpinData class
        SpinData.currentPlayerIndex = currentPlayerSphereIndex;
        SpinData.currentRivalIndex = currentRivalSphereIndex;

        // Load the GameScene
        UnityEngine.SceneManagement.SceneManager.LoadScene("SingleBattleScene");
    }
}
