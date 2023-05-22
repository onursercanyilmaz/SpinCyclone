using System;
using UnityEngine;

public class SingleBattlePlayerSpinController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int maxHealth = 100; // Maximum health value for the player
    private int currentHealth; // Current health value for the player

    public int damageAmount = 10; // Damage amount when the player collides with the rival
    public float scoreValue = 10f; // Score value awarded when the player successfully attacks the rival

    public float spinForce = 100f; // Force applied during the spin attack

    private Rigidbody rb;
    private bool isSpinning = false; // Flag to indicate if the player is currently performing a spin attack

    private SingleBattleRivalSpinController rivalController; // Reference to the rival controller

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth; // Initialize the current health to the maximum health value

        rivalController = FindObjectOfType<SingleBattleRivalSpinController>(); // Find the rival controller
    }

    private void FixedUpdate()
    {
        if (isSpinning)
        {
            // Apply spin force during the spin attack
            rb.AddTorque(transform.up * spinForce);
        }
        else
        {
            // Move the player based on input
            MovePlayer();
        }
    }

    // Function to handle damage to the player
public void TakeDamage(int damageAmount)
{
    currentHealth -= damageAmount;

    // Check if the player's health has reached 0
    if (currentHealth <= 0)
    {
        // Player is defeated
        // Perform necessary actions such as ending the game, displaying a game over screen, etc.
        SingleBattleGameSceneManager gameSceneManager = FindObjectOfType<SingleBattleGameSceneManager>();
        gameSceneManager.EndGame();
        // ...perform other actions...
    }
    else
    {
        // Player successfully attacked rival
        SingleBattleGameSceneManager gameSceneManager = FindObjectOfType<SingleBattleGameSceneManager>();
        gameSceneManager.GainScore(true); // Award score to player
    }
}

    // Function to initiate the player's spin attack
    public void StartSpinAttack()
    {
        isSpinning = true;
    }

    // Function to stop the player's spin attack
    public void StopSpinAttack()
    {
        isSpinning = false;
    }

    // Function to move the player based on input
    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        rb.AddForce(movement * moveSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rival"))
        {
            // Handle collision with the rival
            SingleBattleRivalSpinController rivalController = collision.gameObject.GetComponent<SingleBattleRivalSpinController>();

            if (isSpinning)
            {
                // Player is spinning, attempt to damage the rival
                rivalController.TakeDamage(damageAmount);

                if (!rivalController.IsSpinning())
                {
                    // Rival is not spinning, stop the player's spin attack
                    StopSpinAttack();
                }
            }
            else
            {
                // Player is not spinning, take damage from the rival's spin
                int rivalSpinDamageAmount = rivalController.GetDamageAmount();
                TakeDamage(rivalSpinDamageAmount);
            }
        }
    }

    internal float GetSpinForce()
    {
        return spinForce;
    }

    public bool IsSpinning()
    {
        return isSpinning;
    }

    public int GetDamageAmount()
    {
        return damageAmount;
    }
}
