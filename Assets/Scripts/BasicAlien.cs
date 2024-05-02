using UnityEngine;

public class BasicAlien : Alien {
    public float speed = 2f; // Rychlost pohybu mimozemšťana
    public GameObject alienBulletPrefab; // Prefab střely mimozemšťana

    private float fireRate = 1f; // Interval střelby
    private float timeSinceLastShot = 0f; // Čas od poslední střelby

    void Update() {
        UpdateBehavior();
    }

    public override void UpdateBehavior() {
        MoveTowardsPlayer();
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= fireRate) {
            ShootAtPlayer();
            timeSinceLastShot = 0f;
        }
    }

    void MoveTowardsPlayer() {
        Vector3 direction = (PlayerController.Instance.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    void ShootAtPlayer() {
        Vector3 direction = (PlayerController.Instance.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // Přidáme 90 stupňů
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        Instantiate(alienBulletPrefab, transform.position, rotation);
    }
}