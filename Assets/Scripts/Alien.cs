using UnityEngine;

public abstract class Alien : MonoBehaviour {
    public int health; // Zdraví mimozemšťana
    public int damage; // Poškození způsobené mimozemšťanem

    private bool shieldActive = false; // Zda je štít aktivní
    public GameObject shieldVisual; // Prefab vizuálu štítu (může být například kruh)
    private GameObject activeShield; // Aktivní instance vizuálu štítu

    public abstract void UpdateBehavior(); // Abstraktní metoda pro chování mimozemšťana

    public void TakeDamage(int amount) {
        if (!shieldActive) { // Pokud není štít aktivní
            health -= amount; // Odečteme poškození od zdraví
            if (health <= 0) {
                Die();
            }
        }
    }

    public void Die() {
        Destroy(gameObject); // Zničíme mimozemšťana, pokud jeho zdraví klesne na nulu nebo pod ní
    }

    public void ActivateShield(float duration) {
        shieldActive = true; // Aktivujeme štít
        if (shieldVisual != null && activeShield == null) {
            activeShield = Instantiate(shieldVisual, transform); // Vytvoříme vizuální zobrazení štítu kolem mimozemšťana
        }

        Invoke("DeactivateShield", duration); // Nastavíme časovač na deaktivaci štítu
    }

    public void DeactivateShield() {
        shieldActive = false; // Deaktivujeme štít
        if (activeShield != null) {
            Destroy(activeShield); // Zničíme vizuální zobrazení štítu
            activeShield = null;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("PlayerBullet"))
        {
            TakeDamage(other.GetComponent<PlayerBullet>().damage); // Nebo použij jiný způsob snižování zdraví
            Destroy(other.gameObject);
        }
    }
}