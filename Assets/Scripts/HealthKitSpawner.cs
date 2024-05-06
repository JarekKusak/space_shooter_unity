using System.Collections;
using UnityEngine;

public class HealthKitSpawner : MonoBehaviour {
    public GameObject healthKitPrefab; // Prefab lékárničky
    public float spawnDistance = 10f; // Pevná vzdálenost od hráče
    public float spawnInterval = 2f; // Interval generování
    public float spawnChance = 1f; // Pravděpodobnost generování lékárničky (10 %)

    void Start() {
        // Spustit periodický generátor
        StartCoroutine(SpawnHealthKit());
    }

    IEnumerator SpawnHealthKit() {
        while (true) {
            yield return new WaitForSeconds(spawnInterval);

            // Náhodná pravděpodobnost generování lékárničky
            if (Random.value < spawnChance) {
                // Vygenerování náhodného úhlu a výpočet pozice vzhledem k hráči
                float angle = Random.Range(0f, 360f);
                Vector3 spawnPosition = PlayerController.Instance.transform.position + Quaternion.Euler(0, 0, angle) * Vector3.up * spawnDistance;

                // Vytvoření lékárničky na vypočítané pozici
                Instantiate(healthKitPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}