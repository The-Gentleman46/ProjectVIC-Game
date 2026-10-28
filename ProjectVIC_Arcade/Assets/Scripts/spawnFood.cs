using UnityEngine;

public class spawnFood : MonoBehaviour
{
    public GameObject[] collectables;

    public float minX = -5f;
    public float maxX = 5f;
    public float minY = -5f;
    public float maxY = 5f;

    public int numberOfCollectablesToSpawn = 5;
    public int numberOfSpawned = 0;

    void Start()
    {
        spawnRandomCollectables();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (numberOfSpawned <= numberOfCollectablesToSpawn)
        {
            spawnRandomCollectables();
        }
        else
        {
            //Debug.Log("All collectables spawned");
        }
    }

    void spawnRandomCollectables()
    {     
        int randomIndex = Random.Range(0, collectables.Length);
        GameObject collectableToSpawn = collectables[randomIndex];
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector2 spawnPosition = new Vector2(randomX, randomY);
        Instantiate(collectableToSpawn, spawnPosition, Quaternion.identity);
        numberOfSpawned++;
    }
}
