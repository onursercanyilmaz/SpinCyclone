using System;
using UnityEngine;

public class SingleBattleRivalSpinController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int maxHealth = 100; // Maximum health value for the rival
    private int currentHealth; // Current health value for the rival

    public int damageAmount = 10; // Damage amount when the rival collides with the player
    public int scoreValue = 10; // Score value awarded when the rival successfully attacks the player
    public int defenseDamageReduction = 5; // Amount of damage reduced when the rival successfully defends against the player's spin

    public float spinDuration = 2f; // Duration of the spin attack
    public float spinForce = 100f; // Force applied during the spin attack

    private Rigidbody rb;
    private bool isSpinning = false; // Flag to indicate if the rival is currently performing a spin attack

    private SingleBattlePlayerSpinController playerController; // Reference to the player controller

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth; // Initialize the current health to the maximum health value

        playerController = FindObjectOfType<SingleBattlePlayerSpinController>(); // Find the player controller
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
            // Move towards the player
            MoveTowardsPlayer();
        }
    }

    // Function to handle damage to the rival
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Check if the rival's health has reached 0
        if (currentHealth <= 0)
        {
            // Rival is defeated
            // Perform necessary actions such as awarding points, spawning a new rival, etc.
            SingleBattleGameSceneManager gameSceneManager = FindObjectOfType<SingleBattleGameSceneManager>();
            gameSceneManager.GainScore(false); // Award score to the rival instead of the player
            // ...perform other actions...
        }
    }

    // Function to perform the rival's spin attack
    private void SpinAttack()
    {
        isSpinning = true;
        Invoke(nameof(StopSpinAttack), spinDuration);
    }

    // Function to stop the spin attack
    private void StopSpinAttack()
    {
        isSpinning = false;
    }

    // Function to move the rival towards the player
    private void MoveTowardsPlayer()
    {
        if (playerController != null)
        {
            Vector3 direction = (playerController.transform.position - transform.position).normalized;
            rb.AddForce(direction * moveSpeed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Handle collision with the player
            SingleBattlePlayerSpinController playerController = collision.gameObject.GetComponent<SingleBattlePlayerSpinController>();

            if (playerController.IsSpinning())
            {
                // Player is spinning, attempt to defend
                bool successfulDefense = DefendAgainstSpin(playerController.GetSpinForce());
                if (!successfulDefense)
                {
                    // Defense was unsuccessful, take damage
                    playerController.TakeDamage(damageAmount);
                }
            }
            else
            {
                // Player is not spinning, initiate spin attack
                SpinAttack();
                playerController.TakeDamage(damageAmount);
            }
        }
        else if (collision.gameObject.CompareTag("Rival"))
        {
            // Handle collision with another rival
            SingleBattleRivalSpinController rivalController = collision.gameObject.GetComponent<SingleBattleRivalSpinController>();
            rivalController.TakeDamage(damageAmount); // Deal damageAmount damage to the rival
        }
    }

    // Function to defend against the player's spin
    private bool DefendAgainstSpin(float playerSpinForce)
    {
        // Calculate the total force by combining the rival's spin force and the player's spin force
        float totalForce = spinForce + playerSpinForce;

        // Compare the forces to determine if the rival successfully defends
        if (totalForce > spinForce)
        {
            // Successful defense, reduce damage by defenseDamageReduction
            playerController.TakeDamage(damageAmount - defenseDamageReduction);
            return true;
        }
        else
        {
            // Defense failed
            return false;
        }
    }

    // Function to get the damage amount
    internal int GetDamageAmount()
    {
        return damageAmount;
    }

    // Function to check if the rival is spinning
    internal bool IsSpinning()
    {
        return isSpinning;
    }
}
