using UnityEngine;

public class FihController : MonoBehaviour
{
// This defines what the options ARE
    private enum FihType 
    { 
        NormalFih, 
        BigFih, 
        DartFih
    }

    // This creates the actual dropdown variable in the Inspector
    [Header("Fih Type")]
    [SerializeField] private FihType selectedFih;
    [Header("Fih Stats")]

    [SerializeField] private float speed = 2f;
    [SerializeField] private float aggroSpeed = 4.5f;
    [SerializeField] public int collisionDamage = 1;

    [Header("Patrol Settings")]
    public float moveDistance = 3f; // How far it swims left/right
    private Vector3 startPos;

    // Optional: Reference to player to chase them
    private Transform playerTransform;

    void Start()
    {
        if (selectedFih == FihType.NormalFih)
    {
        speed = 2f;
        aggroSpeed = 4.5f;
        collisionDamage = 1;
    }
    else if (selectedFih == FihType.BigFih)
    {
        speed = 1.5f;
        aggroSpeed = 3.5f;
        collisionDamage = 2;
    }
    else if (selectedFih == FihType.DartFih)
    {
        speed = 3f;
        aggroSpeed = 6f;
        collisionDamage = 1;
    }
    
        startPos = transform.position;
        // Find player to chase later
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player) playerTransform = player.transform;
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        // Check the Global State
        bool isAngry = GameManager.Instance.hasTreasure;
        float currentSpeed = isAngry ? aggroSpeed : speed;

        if (isAngry && playerTransform != null)
        {
            // BEHAVIOR A: CHASE PLAYER (Aggro Mode)
            // Move specifically toward the player
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, currentSpeed * Time.deltaTime);
            
            // Flip sprite to face player
            if (playerTransform.position.x > transform.position.x) transform.localScale = new Vector3(-1, 1, 1);
            else transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            // BEHAVIOR B: PATROL (Calm Mode)
            PerformPatrol(currentSpeed);
        }
    }

    void PerformPatrol(float speed)
    {
        // Simple Ping-Pong movement
        float distCovered = Mathf.PingPong(Time.time * speed, moveDistance);
        transform.position = new Vector3(startPos.x + distCovered, startPos.y, startPos.z);
        
        // Note: For perfect flipping during patrol, you'd track the direction change 
        // separately, but this is the simplest movement logic.
    }
}
