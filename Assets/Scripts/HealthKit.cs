using UnityEngine;

public class HealthKit : MonoBehaviour {
    public int healthAmount = 10; // Kolik životů lékárnička hráči přidá

    void OnTriggerEnter2D(Collider2D other) {
        // Pokud narazí hráč, přidejte mu životy
        if (other.CompareTag("Player")) {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null) {
                player.IncrementHealth(healthAmount); // Přidáme hráči životy
                Destroy(gameObject); // Zničíme lékárničku
            }
        }
    }
}