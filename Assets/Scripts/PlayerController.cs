using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float rotationSpeed = 800f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private float fireRate = 0.1f;
    private float timeSinceLastShot = 0f;

    void Update() {
        HandleMovement();
        HandleRotation();
        HandleShooting();
    }

    void HandleMovement() {
        // Sem můžete přidat logiku pro pohyb, pokud je třeba.
    }

    void HandleRotation() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        if (direction.magnitude > 0.1f) { // Deadzone
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // Odečteme 90°, protože v Unity je 0° na východ
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
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // Vytvoříme kopii prefabu bullet na pozici a s rotací firePoint
    }
}

