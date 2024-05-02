using UnityEngine;

public class BasicAlien : Alien {
    public float speed = 2f; // Rychlost pohybu mimozemšťana
    //public GameObject alienBulletPrefab; // Prefab střely mimozemšťana

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

    void MoveTowardsPlayer()
    {
        Vector3 direction = (PlayerController.Instance.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}