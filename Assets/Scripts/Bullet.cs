using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed = 20f;

    void Update() {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo) {
        // Zde přidejte kód pro detekci kolize s nepřítelem a následnou reakci.
        Destroy(gameObject);
    }
}