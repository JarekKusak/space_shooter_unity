using UnityEngine;

public class PlayerBullet : MonoBehaviour {
    public float speed = 10f;
    public int damage = 10;
    public float maxDistance = 50f;  // Maximální vzdálenost, po které se kulka sama zničí

    private Rigidbody2D rb;
    private Vector3 startPosition;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
        startPosition = transform.position;  // Uložení počáteční pozice
    }

    void Update() {
        if (Vector3.Distance(startPosition, transform.position) > maxDistance) {
            Destroy(gameObject);  // Zničíme objekt, pokud je příliš daleko
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Alien alien = collision.gameObject.GetComponent<Alien>();
        if (alien != null) {
            alien.TakeDamage(damage);
            Destroy(gameObject);
        } else {
            Destroy(gameObject);  // Zničíme objekt také při jakékoliv kolizi
        }
    }
}