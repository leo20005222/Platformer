using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public float spawnInterval = 1f; // Time interval between spawns
    public float spawnHeightMin = -2f; // Minimum height to spawn projectiles
    public float spawnHeightMax = 2f; // Maximum height to spawn projectiles

    private void Start()
    {
        // Start spawning projectiles
        InvokeRepeating("SpawnProjectile", 0f, spawnInterval);
    }

    private void SpawnProjectile()
    {
        // Generate a random position within specified height
        float randomY = Random.Range(spawnHeightMin, spawnHeightMax);
        Vector3 spawnPosition = new Vector3(10f, randomY, 0); // Change 10f to your right boundary of gameplay

        // Instantiate the projectile
        Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
    }
}
