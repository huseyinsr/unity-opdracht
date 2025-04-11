using UnityEngine;

public class CharacterGravity : MonoBehaviour
{
    // This multiplier will control the gravity for the character
    public float gravityMultiplier = 1.0f;

    // You can store the original gravity scale so you can reset it if needed
    private float originalGravity;

    // Reference to the CharacterController
    private CharacterController characterController;

    void Start()
    {
        // Get the CharacterController component attached to the player
        characterController = GetComponent<CharacterController>();

        // Store the original gravity for resetting later if needed
        originalGravity = Physics.gravity.y;
    }

    void Update()
    {
        // Set gravity based on the multiplier
        SetCharacterGravity(gravityMultiplier);
    }

    // Method to change the gravity for the character
    private void SetCharacterGravity(float multiplier)
    {
        // Modify the gravity according to the multiplier
        Physics.gravity = new Vector3(0, originalGravity * multiplier, 0);
    }

    // You can use this method to reset gravity back to default if needed
    public void ResetGravity()
    {
        gravityMultiplier = 1.0f;
        SetCharacterGravity(gravityMultiplier);
    }

    // Optionally, you can implement a method to change gravity dynamically during gameplay
    public void ChangeGravity(float newGravityMultiplier)
    {
        gravityMultiplier = newGravityMultiplier;
        SetCharacterGravity(gravityMultiplier);
    }
}
