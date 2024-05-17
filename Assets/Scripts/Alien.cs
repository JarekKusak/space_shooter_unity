using System.Collections;
using UnityEngine;

public abstract class Alien : MonoBehaviour {
    public int health; // Zdraví mimozemšťana
    public int damage; // Poškození způsobené mimozemšťanem
    public GameObject alienBulletPrefab; // Prefab střely mimozemšťana
    private bool shieldActive = false; // Zda je štít aktivní
    public GameObject shieldVisual; // Prefab vizuálu štítu (může být například kruh)
    private GameObject activeShield; // Aktivní instance vizuálu štítu

    private SpriteRenderer spriteRenderer;
    //public UIManager uiManager;
    public abstract void UpdateBehavior(); // Abstraktní metoda pro chování mimozemšťana
    public enum AlienType {
        Basic,
        Advanced
        // Další typy mohou být přidány zde
    }
    
    // Tato metoda se volá při inicializaci každého objektu odvozeného od třídy Alien
    void Awake()
    {
        gameObject.tag = "Alien";
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private IEnumerator FlashCoroutine(Color color)
    {
        spriteRenderer.color = color;  // Změna barvy na červenou
        yield return new WaitForSeconds(0.1f);  // Krátké zpoždění
        spriteRenderer.color = Color.white;  // Vrátit zpět na původní barvu
    }

    public AlienType type; // Přidáme typ mimozemšťana
    public void TakeDamage(int amount) {
        if (!shieldActive) { // Pokud není štít aktivní
            StartCoroutine(FlashCoroutine(Color.red));
            health -= amount; // Odečteme poškození od zdraví
            if (health <= 0) {
                Die();
            }
        }
    }
    
    public GameObject explosionEffectPrefab;

    public void Die() {
        
        if (explosionEffectPrefab != null)
        {
            // Vytvořte efekt na pozici mimozemšťana
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }
        UIManager.Instance.AlienKilled(type == AlienType.Advanced);
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
        if (other.CompareTag("Player") && PlayerController.Instance.IsShieldActive())
        {
            Die();
        }
        else if (other.CompareTag("PlayerBullet"))
        {
            TakeDamage(other.GetComponent<PlayerBullet>().damage); // Nebo použij jiný způsob snižování zdraví
            Destroy(other.gameObject);
        }
    }
    
    public void ShootAtPlayer() {
        Vector3 direction = (PlayerController.Instance.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // Přidáme 90 stupňů
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        Instantiate(alienBulletPrefab, transform.position, rotation);
    }
}