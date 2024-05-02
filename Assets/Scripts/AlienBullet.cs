using UnityEngine;

public class AlienBullet : MonoBehaviour {
    public float speed = 5f;
    public int damage = 20;

    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null) {
            player.DecrementHealth(damage); // Přidáme tuto metodu do PlayerController
            Destroy(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}