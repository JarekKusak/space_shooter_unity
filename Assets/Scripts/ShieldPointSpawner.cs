using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPointSpawner : MonoBehaviour
{
    public GameObject shieldPointPrefab;
    public float spawnRate = 5f; // doba mezi spawnováním
    public float spawnDistance = 5f; // vzdálenost od hráče
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            Vector3 spawnPosition = PlayerController.Instance.transform.position + Random.insideUnitSphere * spawnDistance;
            spawnPosition.z = 0; // ujistěte se, že zůstane ve 2D rovině
            Instantiate(shieldPointPrefab, spawnPosition, Quaternion.identity);
            timer = 0f;
        }
    }
}