using System;
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

    public int maxHealth = 100;
    private int health;
    
    public float acceleration = 10f; // Nastavte vyšší hodnotu pro rychlejší rozjezd
    public float deceleration = 10f; // Záporné zrychlení pro zpomalení
    
    private AudioSource blastSound;
    
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 2f;
    private float reloadTimer;
    
    public GameObject pauseMenu; // Přidejte toto do vašeho PlayerController skriptu


    private void Start()
    {
        health = maxHealth;
        blastSound = GetComponent<AudioSource>();
        currentAmmo = maxAmmo; // inicializace střel
        UIManager.Instance.UpdateAmmo(currentAmmo, maxAmmo);
    }
    
    void CheckPauseMenu() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!pauseMenu.activeInHierarchy) {
                pauseMenu.SetActive(true);
                Time.timeScale = 0; // Zastaví čas ve hře
            } else {
                pauseMenu.SetActive(false);
                Time.timeScale = 1; // Obnoví běh času ve hře
            }
        }
    }

    private GameOverScreen gameOverScreen;
    void Awake() {
        gameOverScreen = FindObjectOfType<GameOverScreen>();
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
        CheckPauseMenu();
        HandleReload();
        // Volání UpdateHealth na UIManager
        if (uiManager != null) {
            UIManager.Instance.UpdateHealth(health, maxHealth); // předpokládá, že máte definovanou proměnnou 'health'
            UIManager.Instance.UpdateAmmo(currentAmmo, maxAmmo);
        }
        
        
    }

    void FixedUpdate() {
        Move();
    }
    
    // Fields to store direction and smoothed velocity
    private Vector2 currentVelocity = Vector2.zero;
    private Vector2 desiredDirection = Vector2.zero;

    // Process player inputs
    void ProcessInputs() {
        // Gather key states
        bool isUp = Input.GetKey(KeyCode.W);
        bool isDown = Input.GetKey(KeyCode.S);
        bool isLeft = Input.GetKey(KeyCode.A);
        bool isRight = Input.GetKey(KeyCode.D);

        // Determine the desired movement direction
        desiredDirection = Vector2.zero;
        if (isUp) desiredDirection.y += 1;
        if (isDown) desiredDirection.y -= 1;
        if (isLeft) desiredDirection.x -= 1;
        if (isRight) desiredDirection.x += 1;

        // Normalize to prevent diagonal boost
        if (desiredDirection.magnitude > 1) {
            desiredDirection.Normalize();
        }
    }

    void Move() {
        // Smoothly transition to the desired direction with acceleration
        currentVelocity = Vector2.Lerp(currentVelocity, desiredDirection * moveSpeed, acceleration * Time.fixedDeltaTime);

        // Apply gradual deceleration when no input is given
        if (desiredDirection == Vector2.zero && currentVelocity.magnitude > 0) {
            currentVelocity = Vector2.Lerp(currentVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }

        // Update rigidbody velocity with current velocity
        rb.velocity = currentVelocity;
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
        if (Input.GetKey(KeyCode.Space) && timeSinceLastShot >= fireRate && currentAmmo > 0) {
            Shoot();
            timeSinceLastShot = 0f;
        }
    }

    void Shoot() {
        Instantiate(playerBulletPrefab, firePoint.position, firePoint.rotation);
        currentAmmo--;
        if (blastSound != null) {
            Debug.Log("hraju");
            blastSound.Play();
        }
    }
    
    void HandleReload() {
        if (currentAmmo < maxAmmo) {
            reloadTimer += Time.deltaTime;
            if (reloadTimer >= reloadTime) {
                currentAmmo++;
                reloadTimer = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("AlienBullet")) {
            DecrementHealth(other.GetComponent<AlienBullet>().damage);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Alien"))
        {
            health = 0; // Vynulujeme zdraví hráče
            gameOverScreen.ShowGameOverScreen();
            //GameOver();
        }
    }

    
    public void DecrementHealth(int amount)
    {
        health -= amount;
        if (health <= 0)
            gameOverScreen.ShowGameOverScreen();
    }
    
    public void IncrementHealth(int amount) {
        health = Mathf.Min(health + amount, maxHealth); // Přidání životů s maximálním omezením
    }

}
