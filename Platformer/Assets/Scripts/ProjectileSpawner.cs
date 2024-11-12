using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public Transform spawnPoint; // Reference to the spawner's transform or a specific spawn point
    public float spawnInterval = 1f; // Time interval between spawns
    public float spawnHeightMin = -2f; // Minimum height offset
    public float spawnHeightMax = 2f; // Maximum height offset
    public float spawnXOffset = 1f; // Horizontal offset (if you want to spawn projectiles ahead of the spawner)

    private void Start()
    {
        // Start spawning projectiles at a set interval
        InvokeRepeating("SpawnProjectile", 0f, spawnInterval);
    }

    private void SpawnProjectile()
    {
        // Generate a random Y offset based on the min and max height
        float randomYOffset = Random.Range(spawnHeightMin, spawnHeightMax);

        // Determine the spawn position relative to the spawner's position
        Vector3 spawnPosition = new Vector3(spawnPoint.position.x + spawnXOffset, spawnPoint.position.y + randomYOffset, spawnPoint.position.z);

        // Instantiate the projectile at the calculated position
        Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
    }
}
