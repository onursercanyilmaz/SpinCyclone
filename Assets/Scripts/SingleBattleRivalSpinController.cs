using System;
using UnityEngine;

public class SingleBattleRivalSpinController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int damageAmount = 10; // Damage amount when the rival collides with the player
    public int scoreValue = 10; // Score value awarded when the rival successfully attacks the player

    public float spinDuration = 2f; // Duration of the spin attack
    public float spinForce = 100f; // Force applied during the spin attack

    private Rigidbody rb;
    private bool isSpinning = false; // Flag to indicate if the rival is currently performing a spin attack

    private SingleBattlePlayerSpinController playerController; // Reference to the player controller

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

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

             // Calculate relative speed between rival and player
             float relativeSpeed = rb.velocity.magnitude - playerController.GetRigidbody().velocity.magnitude;

             if (playerController.IsSpinning())
             {
                 // Player is spinning, attempt to defend

                 // Defense was unsuccessful, take damage

             }
             else
             {
                 // Player is not spinning, initiate spin attack
                 SpinAttack();

                 // Award score to the player or rival based on relative speed
                 SingleBattleGameSceneManager gameSceneManager = FindObjectOfType<SingleBattleGameSceneManager>();
                 gameSceneManager.GainScore(relativeSpeed < 0);
             }
         }
     }

     internal int GetDamageAmount()
     {
         return damageAmount;
     }

     internal bool IsSpinning()
     {
         return isSpinning;
     }

     public Rigidbody GetRigidbody()
     {
         return GetComponent<Rigidbody>();
     }
}
