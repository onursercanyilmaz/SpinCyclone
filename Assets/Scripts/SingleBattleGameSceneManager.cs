


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public List<GameObject> collisionEffects; // List of collision effect prefabs
    public float effectDuration = 2f; // Duration of the collision effect

    public List<AudioClip> collisionSounds; // List of collision sound effects
    private AudioSource audioSource; // Audio source component
    public AudioClip winSound; // Sound effect for winning
    public AudioClip loseSound; // Sound effect for losing


    private bool isGameEnded = false; // Flag to check if the game has ended

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

        // Initialize the audio source
        audioSource = GetComponent<AudioSource>();

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
        // Increment the score for the corresponding player
        if (isPlayer)
        {
            playerScore++;
        }
        else
        {
            rivalScore++;
        }

        // Generate a random index within the range of the collisionEffects list
        int randomEffectIndex = UnityEngine.Random.Range(0, collisionEffects.Count);

        // Get the randomly selected effect prefab
        GameObject randomEffectPrefab = collisionEffects[randomEffectIndex];

        // Instantiate the random effect prefab at the desired position and rotation
        GameObject effectInstance = Instantiate(randomEffectPrefab, transform.position, Quaternion.identity);

        // Start the coroutine to destroy the effect after a certain duration
        StartCoroutine(DestroyEffect(effectInstance));

        // Generate a random index within the range of the collisionSounds list
        int randomSoundIndex = UnityEngine.Random.Range(0, collisionSounds.Count);

        // Get the randomly selected audio clip
        AudioClip randomClip = collisionSounds[randomSoundIndex];

        // Play the random audio clip on the AudioSource
        audioSource.PlayOneShot(randomClip);

        // Update the score display
        UpdateScoreDisplay();

        // Check if the game has ended
        if (!isGameEnded)
        {
            // Check if the player has reached the maximum score
            if (playerScore >= 3)
            {
                  AudioSource.PlayClipAtPoint(winSound, transform.position);
            Destroy(rivalSpheres[currentRivalIndex]);
                // Player wins
                EndGame(true);

            }
            // Check if the rival has reached the maximum score
            else if (rivalScore >= 3)
            {
                 AudioSource.PlayClipAtPoint(loseSound, transform.position);
            Destroy(playerSpheres[currentPlayerIndex]);
                // Rival wins
                EndGame(false);
            }
        }
    }

    private IEnumerator DestroyEffect(GameObject effect)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(effectDuration);

        // Destroy the effect game object
        Destroy(effect);
    }


    private void EndGame(bool isPlayerWinner)
    {
        isGameEnded = true;

        // Activate the corresponding win or lose screen based on the winner
        if (isPlayerWinner)
        {
             StartCoroutine(LoadWinScreen());
        }
        else
        {
             StartCoroutine(LoadLoseScreen());
        }

        Debug.Log("Game ended. Player wins: " + isPlayerWinner);

      
    }

    // Coroutine to load the main menu scene after a delay
    private IEnumerator LoadWinScreen()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("WinScreen");
    }
      private IEnumerator LoadLoseScreen()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("LoseScreen");
    }
    // Function to end the game
   
}
