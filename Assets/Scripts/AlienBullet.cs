using UnityEngine;

public class AlienBullet : MonoBehaviour {
    public float speed = 5f;
    public int damage = 20;
    public float maxDistance = 15f;  // Maximální vzdálenost, po které se kulka sama zničí

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
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null) {
            player.DecrementHealth(damage);
            Destroy(gameObject);
        } else {
            Destroy(gameObject);  // Zničíme objekt také při jakékoliv kolizi
        }
    }
}