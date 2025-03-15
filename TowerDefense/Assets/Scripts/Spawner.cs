using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class Spawner : MonoBehaviour
{
    [Header("Addressable Monstres Configurations")]
    [Tooltip("List of Addressable keys for monstres prefabs.")]
    public List<string> monstresKeys;

    [Header("Spawn Settings")]
    [Tooltip("Time interval between enemy spawns in seconds.")]
    public int spawnInterval = 3;

    public int nbMonstresSpawn;
    public int nbMonstresToSpawn;

    private List<GameObject> loadedMonstres = new List<GameObject>();

    void Start()
    {
        StartCoroutine(LoadMonstresAndStartSpawning());
        nbMonstresToSpawn = 15;
        nbMonstresSpawn = 0;
    }

    private IEnumerator LoadMonstresAndStartSpawning()
    {
        // Load all enemies from the Addressable system
        foreach (var key in monstresKeys)
        {
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(key);
            yield return handle;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                loadedMonstres.Add(handle.Result);
            }
        }

        // Start spawning after all enemies are loaded
        StartCoroutine(SpawnMonstres());
    }

    public IEnumerator SpawnMonstres()
    {
        while (true)
        {
            if (loadedMonstres.Count > 0 && nbMonstresSpawn > 0)
            {
                GameObject monstrePrefab;

                float randomValue = Random.Range(0f, 1f);
                //Debug.Log(randomValue);
                if (randomValue < 0.55f)
                {
                    // gobelin
                    // Debug.Log("gobelin");
                    monstrePrefab = loadedMonstres[1];
                }
                else if (randomValue < 0.85f)
                {
                    // troll
                    // Debug.Log("troll");
                    monstrePrefab = loadedMonstres[2];
                }
                else
                {
                    // dragon
                    // Debug.Log("dragon");
                    monstrePrefab = loadedMonstres[0];
                }
                // Select a random enemy from the loaded list
                // GameObject monstrePrefab = loadedMonstres[Random.Range(0, loadedMonstres.Count)];

                // Instantiate the enemy at the spawner's position and rotation
                Instantiate(monstrePrefab, this.transform.position, this.transform.rotation);
                nbMonstresSpawn -= 1;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void OnDestroy()
    {
        // Release Addressable assets when the spawner is destroyed
        foreach (var monstre in loadedMonstres)
        {
            Addressables.Release(monstre);
        }
    }
}
