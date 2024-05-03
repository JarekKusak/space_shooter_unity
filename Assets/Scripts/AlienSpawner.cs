using UnityEngine;

public class AlienSpawner : MonoBehaviour {
    public GameObject basicAlienPrefab; // Prefab pro BasicAlien
    public GameObject advancedAlienPrefab; // Prefab pro AdvancedAlien
    public float spawnDistance = 10f; // Vzdálenost od hráče, kde se Alieni spawnují
    public float spawnInterval = 5f; // Interval spawnu v sekundách

    private float timeSinceLastSpawn = 0f;

    void Update() {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval) {
            SpawnAlien();
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnAlien() {
        float angle = Random.Range(0f, 360f);
        Vector3 spawnPosition = PlayerController.Instance.transform.position + Quaternion.Euler(0, 0, angle) * Vector3.up * spawnDistance;

        float chance = Random.Range(0f, 100f);
        GameObject alienPrefab;
        Alien.AlienType type;

        if (chance < 70f) { // 70 % šance na BasicAliena
            alienPrefab = basicAlienPrefab;
            type = Alien.AlienType.Basic;
        } else { // 30 % šance na AdvancedAliena
            alienPrefab = advancedAlienPrefab;
            type = Alien.AlienType.Advanced;
        }

        GameObject newAlien = Instantiate(alienPrefab, spawnPosition, Quaternion.identity);
        newAlien.GetComponent<Alien>().type = type; // Nastavíme typ mimozemšťana
    }
}