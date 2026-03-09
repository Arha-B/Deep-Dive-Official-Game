using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    public Transform player; // Drag your Player here

    [Header("Settings")]
    public float smoothSpeed = 0.125f; // Higher = slower/smoother
    public Vector3 offset = new Vector3(0, 0, -10); // Keep Z at -10 to see 2D objects

    [Header("Map Boundaries")]
    // You will set these numbers in the Inspector
    public Vector2 minLimit; // The Bottom-Left corner of the map
    public Vector2 maxLimit; // The Top-Right corner of the map

    // We use LateUpdate because the Player moves in Update/FixedUpdate.
    // The camera should move AFTER the player has finished moving to prevent jitter.
    void LateUpdate()
    {
        if (player == null) return;

        // 1. Calculate where we WANT to be
        Vector3 desiredPosition = player.position + offset;

        // 2. Clamp that position to stay inside the Map Boundaries
        // We use Mathf.Clamp(value, min, max)
        float clampedX = Mathf.Clamp(desiredPosition.x, minLimit.x, maxLimit.x);
        float clampedY = Mathf.Clamp(desiredPosition.y, minLimit.y, maxLimit.y);

        Vector3 finalPosition = new Vector3(clampedX, clampedY, offset.z);

        // 3. Smoothly move from current position to final position
        transform.position = Vector3.Lerp(transform.position, finalPosition, smoothSpeed);
    }
}
