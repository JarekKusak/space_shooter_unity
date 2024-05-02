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
        Destroy(gameObject); // Zničíme mimozemšťana, pokud jeho zdraví klesne na nulu nebo pod ní
    }
}