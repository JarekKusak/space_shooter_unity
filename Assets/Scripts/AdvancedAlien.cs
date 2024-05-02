using UnityEngine;

public class AdvancedAlien : Alien {
    public float speed = 3f; // Rychlost pohybu mimozemšťana

    private float fireRate = 0.5f; // Interval střelby
    private float timeSinceLastShot = 0f; // Čas od poslední střelby

    private float shieldDuration = 3f; // Doba trvání štítu
    private float shieldCooldown = 4f; // Interval pro aktivaci štítu
    private float timeSinceLastShield = 0f; // Čas od poslední aktivace štítu

    private bool shieldOn = false; // Stav štítu

    void Update() {
        UpdateBehavior();
    }

    public override void UpdateBehavior() {
        MoveTowardsPlayer();

        timeSinceLastShot += Time.deltaTime;
        timeSinceLastShield += Time.deltaTime;

        if (timeSinceLastShot >= fireRate) {
            ShootAtPlayer();
            timeSinceLastShot = 0f;
        }

        if (timeSinceLastShield >= shieldCooldown) {
            ActivateShield(shieldDuration); // Aktivujeme štít
            shieldOn = true; // Zaznamenáme, že štít je zapnutý
            timeSinceLastShield = 0f; // Restartujeme časovač pro další aktivaci štítu
        }

        if (shieldOn && timeSinceLastShield >= shieldDuration) {
            DeactivateShield(); // Deaktivujeme štít
            shieldOn = false; // Zaznamenáme, že štít je vypnutý
        }
    }

    void MoveTowardsPlayer() {
        Vector3 direction = (PlayerController.Instance.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}
