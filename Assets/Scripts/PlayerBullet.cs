using UnityEngine;

public class PlayerBullet : MonoBehaviour {
    public float speed = 10f;
    public int damage = 10;

    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Alien alien = collision.gameObject.GetComponent<Alien>();
        if (alien != null) {
            alien.TakeDamage(damage);
            Destroy(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}