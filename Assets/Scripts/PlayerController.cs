using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController Instance { get; private set; }
    
    public float rotationSpeed = 800f;
    public GameObject playerBulletPrefab;
    public Transform firePoint;

    private float fireRate = 0.1f;
    private float timeSinceLastShot = 0f;

    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    private Vector2 moveDirection;

    public int health;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public UIManager uiManager;

    void Update() {
        ProcessInputs();
        HandleRotation();
        HandleShooting();

        // Volání UpdateHealth na UIManager
        if (uiManager != null) {
            uiManager.UpdateHealth(health); // předpokládá, že máte definovanou proměnnou 'health'
        }
    }

    void FixedUpdate() {
        Move();
    }

    void ProcessInputs() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move() {
        rb.velocity = moveDirection * moveSpeed;
    }

    void HandleRotation() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        if (direction.magnitude > 0.1f) {
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    void HandleShooting() {
        timeSinceLastShot += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && timeSinceLastShot >= fireRate) {
            Shoot();
            timeSinceLastShot = 0f;
        }
    }

    void Shoot() {
        Instantiate(playerBulletPrefab, firePoint.position, firePoint.rotation);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("AlienBullet")) {
            DecrementHealth(other.GetComponent<AlienBullet>().damage);
            Destroy(other.gameObject);
        }
    }

    public void DecrementHealth(int amount)
    {
        health -= amount;
    }
}
