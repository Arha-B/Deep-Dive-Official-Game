using UnityEngine;

public class PlayerController : MonoBehaviour
{
[Header("Movement Settings")]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Header("Health Settings")]
    public int maxHealth = 5;
    public int currentHealth;
    // Reference to your UI script from earlier (optional)
    // public HeartManager heartManager; 

    [Header("Damage Settings")]
    public int mineDamage = 2; // Mines hurt a lot!
    public float bounceForce = 5f;  // Knockback when hit

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        // 1. Movement Input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        // 2. Win Condition Check (Surface)
        // Assuming Surface is at Y = 0 or higher
        if (transform.position.y > 0 && GameManager.Instance.hasTreasure)
        {
            GameManager.Instance.WinGame();
        }
    }

    void FixedUpdate()
    {
        // Physics Movement
        rb.linearVelocity = moveInput * moveSpeed;
    }

    // ─────────────────────────────────────────────────────────────
    //  COLLISION & DAMAGE LOGIC
    // ─────────────────────────────────────────────────────────────
    private void OnTriggerEnter2D(Collider2D other)
    {
        // A. HIT BY MINE
        if (other.CompareTag("Mine"))
        {
            TakeDamage(mineDamage);
            Destroy(other.gameObject); // Mine explodes (disappears)
            Debug.Log("Boom! Hit a mine.");
            
            // Optional: Add explosion particle effect here
        }

        // B. HIT BY FIH (Fish)
        else if (other.CompareTag("Fih"))
        {
            // Get damage amount from the specific Fih we hit
            FihController fih = other.GetComponent<FihController>();
            if (fih != null)
            {
                TakeDamage(fih.collisionDamage);
                
                // Knockback effect
                Vector2 pushDirection = (transform.position - other.transform.position).normalized;
                transform.position += (Vector3)pushDirection * 0.5f;
            }
        }

        // C. FOUND TREASURE
        else if (other.CompareTag("Treasure"))
        {
            GameManager.Instance.GrabTreasure();
            Destroy(other.gameObject);
        }
    }

    void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        
        // Update UI if you have it
        // heartManager.UpdateHearts(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            GameManager.Instance.GameOver();
            gameObject.SetActive(false); // Hide player
        }
    }
}
