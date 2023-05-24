using System;
using UnityEngine;
using UnityEngine.UI;

public class SingleBattleGameSceneManager : MonoBehaviour
{
    public GameObject[] playerSpheres; // Array of player's sphere game objects
    public GameObject[] rivalSpheres; // Array of rival's sphere game objects

    public SingleBattlePlayerSpinController playerSphereController; // Player's sphere controller
    public SingleBattleRivalSpinController rivalSphereController; // Rival's sphere controller

    public int currentPlayerIndex; // Index of the player's current sphere
    public int currentRivalIndex; // Index of the rival's current sphere

    public int playerScore = 0; // Player's score
    public int rivalScore = 0; // Rival's score

    public TextMesh playerScoreText; // Reference to the player score Text component
    public TextMesh rivalScoreText; // Reference to the rival score Text component

    private void Start()
    {
        // Get the selected indexes from PlayerPrefs
        currentPlayerIndex = PlayerPrefs.GetInt("CurrentPlayerIndex");
        currentRivalIndex = PlayerPrefs.GetInt("CurrentRivalIndex");

        // Set up the spins based on the selected indexes
        SetSpins(playerSpheres, currentPlayerIndex);
        SetSpins(rivalSpheres, currentRivalIndex);

        // Activate the corresponding sphere controllers
        playerSphereController.enabled = (currentPlayerIndex == 0);
        rivalSphereController.enabled = (currentRivalIndex == 0);

        // Get the Rigidbody component from the player sphere
        Rigidbody playerSphereRB = playerSpheres[currentPlayerIndex].GetComponent<Rigidbody>();
        Rigidbody rivalSphereRB = rivalSpheres[currentRivalIndex].GetComponent<Rigidbody>();

        // If the Rigidbody component doesn't exist, add it
        if (playerSphereRB == null)
        {
            playerSphereRB = playerSpheres[currentPlayerIndex].AddComponent<Rigidbody>();
        }
        if (rivalSphereRB == null)
        {
            rivalSphereRB = rivalSpheres[currentRivalIndex].AddComponent<Rigidbody>();
        }

        // Initialize the scores to 0
        playerScore = 0;
        rivalScore = 0;

        playerSpheres[currentPlayerIndex].tag = "Player";
        rivalSpheres[currentRivalIndex].tag = "Rival";

        // Update the score display
        UpdateScoreDisplay();

        Debug.Log("Game started. Player: " + playerSpheres[currentPlayerIndex].name + ", Rival: " + rivalSpheres[currentRivalIndex].name);
    }

    private void SetSpins(GameObject[] spins, int activeIndex)
    {
        // Loop through all the spins
        for (int i = 0; i < spins.Length; i++)
        {
            // Set the visibility of the spin based on the active index
            bool isVisible = (i == activeIndex);
            spins[i].SetActive(isVisible);

            // If the spin is not active, make it invisible
            if (!isVisible)
            {
                // Check if the spin has a renderer component
                Renderer spinRenderer = spins[i].GetComponent<Renderer>();
                if (spinRenderer != null)
                {
                    // Disable the renderer to make the spin invisible
                    spinRenderer.enabled = false;
                }
            }
        }
    }

    // Function to update the score display
    private void UpdateScoreDisplay()
    {
        playerScoreText.text = "Player Score: " + playerScore.ToString();
        rivalScoreText.text = "Rival Score: " + rivalScore.ToString();
    }

    // Function to handle scoring when a player or rival gains a point
    public void GainScore(bool isPlayer)
    {
        if (isPlayer)
        {
            playerScore++;
            Debug.Log("Player gained a point. Player score: " + playerScore);
        }
        else
        {
            rivalScore++;
            Debug.Log("Rival gained a point. Rival score: " + rivalScore);
        }

        // Update the score display
        UpdateScoreDisplay();

        // Check if either the player or rival has reached a score of 10
        if (playerScore >= 10 || rivalScore >= 10)
        {
            // End the game
            EndGame();
        }
    }

    internal void EndGame()
    {
        Debug.Log("Game ended. Player score: " + playerScore + ", Rival score: " + rivalScore);

        // Implement the necessary actions for ending the game
        // This can include displaying a game over screen, stopping the game flow, etc.
    }
}
