using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform playerTransform; // Tento public Transform přiřadíte k hráči v Unity editoru.

    private Vector3 cameraOffset; // Offset pro kameru, pokud chcete, aby kamera nebyla přímo ve středu nad hráčem

    void Start() {
        // Nastavte počáteční offset podle aktuální pozice kamery a hráče.
        cameraOffset = transform.position - playerTransform.position;
    }

    void LateUpdate() {
        // Nastavíme pozici kamery tak, aby sledovala hráče s daným offsetem.
        transform.position = playerTransform.position + cameraOffset;
    }
}