using UnityEngine;

public class DashOnSwordSwing : MonoBehaviour
{
    public float dashDistance = 5f; // Distance to dash
    public float dashDuration = 0.2f; // Duration of the dash
    public Animator animator; // Reference to the Animator component

    private bool isDashing = false;
    private Vector3 dashDirection;

    void Update()
    {
        // Check for sword swing input (e.g., left mouse button)
        if (Input.GetMouseButtonDown(0) && !isDashing)
        {
            // Trigger the sword swing animation
            if (animator != null)
            {
                animator.SetTrigger("SwingSword");
            }

            // Start the dash
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;

        // Calculate the dash direction based on the player's facing direction
        dashDirection = transform.forward * dashDistance;

        // Move the player
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + dashDirection;

        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / dashDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the player ends up exactly at the target position
        transform.position = targetPosition;

        isDashing = false;
    }
}