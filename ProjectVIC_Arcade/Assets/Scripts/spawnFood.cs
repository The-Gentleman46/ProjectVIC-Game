using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
public class spawnFood : MonoBehaviour
{
    [SerializeField] GameObject[] collectables;
    // Set of locations fruit already exists at
    public HashSet<Vector2> OccPosition;
    [SerializeField] snakeMovement snakeScript;
    [SerializeField] float minX = -4.0f;
    [SerializeField] float maxX = 4.0f;
    [SerializeField] float minY = -4.0f;
    [SerializeField] float maxY = 4.0f;

    public int numberOfCollectablesToSpawn = 5;
    public int numberOfSpawned = 0;

    void Start()
    {
        OccPosition = new HashSet<Vector2>();
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

        float randomX = Mathf.Floor(Random.Range(minX, maxX));
        float randomY = Mathf.Floor(Random.Range(minY, maxY));
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        while (isOccupied(spawnPosition))
        {
            randomX = Mathf.Floor(Random.Range(minX, maxX));
            randomY = Mathf.Floor(Random.Range(minY, maxY));
            spawnPosition = new Vector2(randomX, randomY);
        }

        OccPosition.Add(spawnPosition);

        Instantiate(collectableToSpawn, spawnPosition, Quaternion.identity);
        numberOfSpawned++;
    }

    bool isOccupied(Vector2 pos)
    {
        bool isOccupied = false;
        Vector3 checkPos = new Vector3(pos.x, pos.y, 0.0f);

        for (int i = 0; i < snakeScript.segments.Count; i++)
        {
            if (Vector3.Distance(snakeScript.segments[i].position, checkPos) < 0.01f || OccPosition.Contains(pos))
            {
                isOccupied = true;
            }
            
        }
        return isOccupied;
    }
}
