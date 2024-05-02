using UnityEngine;

public abstract class Alien : MonoBehaviour {
    public int health; // Zdraví mimozemšťana
    public int damage; // Poškození způsobené mimozemšťanem

    public abstract void UpdateBehavior(); // Abstraktní metoda pro chování mimozemšťana

    public void TakeDamage(int amount) {
        health -= amount; // Odečteme poškození od zdraví
        if (health <= 0) {
            Die();
        }
    }

    public void Die() {
        Destroy(gameObject); // Zničíme mimozemšťana
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("PlayerBullet")) {
            TakeDamage(other.GetComponent<PlayerBullet>().damage); // Nebo použij jiný způsob snižování zdraví
            Destroy(other.gameObject);
        }
    }
}