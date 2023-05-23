using System;
using UnityEngine;

public class SingleBattlePlayerSpinController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int damageAmount = 10; // Damage amount when the player collides with the rival
    public float scoreValue = 10f; // Score value awarded when the player successfully attacks the rival

    public float spinForce = 100f; // Force applied during the spin attack

    private Rigidbody rb;
    private bool isSpinning = false; // Flag to indicate if the player is currently performing a spin attack
    private bool canGainScore = true; // Flag to indicate if the player can gain score
    private bool gameStarted = false; // Flag to indicate if the game has started

    private SingleBattleRivalSpinController rivalController; // Reference to the rival controller

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

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

             // Calculate relative speed between player and rival
             float relativeSpeed = rb.velocity.magnitude - rivalController.GetRigidbody().velocity.magnitude;

             if (isSpinning && !rivalController.IsSpinning())
             {
                 // Player is spinning and the rival is not spinning, attempt to damage the rival

                 // Stop the player's spin attack
                 StopSpinAttack();

                 // Award score to the player or rival based on relative speed
                 if (gameStarted)
                 {
                     SingleBattleGameSceneManager gameSceneManager = FindObjectOfType<SingleBattleGameSceneManager>();
                     gameSceneManager.GainScore(relativeSpeed > 0);
                 }
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

     public void SetCanGainScore(bool canGain)
     {
         canGainScore = canGain;
     }

     public Rigidbody GetRigidbody()
     {
         return GetComponent<Rigidbody>();
     }

     public void StartGame()
     {
         gameStarted = true;
     }
}
