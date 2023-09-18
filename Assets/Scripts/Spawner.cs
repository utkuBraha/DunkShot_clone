using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObjects;
    public float minX = -2.2f;
    public float maxX = 2.2f;
    public BallScript ballScript;
    private int _previousScore;
    private List<GameObject> _spawnedObjects = new List<GameObject>();
    private Vector3 _lastSpawnedPosition;
    void Start()
    {
      ballScript = GameObject.Find("Ball").GetComponent<BallScript>();
      _previousScore = ballScript.GetScore();
    }
    public void Update()
    {
        int currentScore = ballScript.GetScore();
        if (currentScore > _previousScore)
        {
            _previousScore = currentScore;
            SpawnObject();
        }
    }
    void SpawnObject()
    {
        float randomX;
        float randomY = Random.Range(1.7f, 2.3f);
        Vector3 spawnPosition;
        if (_spawnedObjects.Count >= 2)
        {
            Destroy(_spawnedObjects[0]);
            _spawnedObjects.RemoveAt(0);
        }
        if (_spawnedObjects.Count > 0)
        {
            float minXClamped = Mathf.Max(minX, _lastSpawnedPosition.x - 1.5f);
            float maxXClamped = Mathf.Min(maxX, _lastSpawnedPosition.x + 1.5f);
            if (_lastSpawnedPosition.x < 0)
            {
                randomX = Random.Range(1.3f, maxXClamped);
            }
            else
            {
                randomX = Random.Range(minXClamped, -1.3f);
            }
            randomY = _lastSpawnedPosition.y + 1.5f;
            spawnPosition = new Vector3(randomX, randomY, 0);
        }
        else
        {
            randomX = Random.Range(minX, -1.3f);
            spawnPosition = new Vector3(randomX, randomY, 0);
        }
        GameObject newHoop = Instantiate(spawnObjects, spawnPosition, Quaternion.identity);
        _spawnedObjects.Add(newHoop);
        _lastSpawnedPosition = spawnPosition;
    }
}


// void SpawnObject()
// {
//     float randomX = Random.Range(minX, maxX);
//     float randomY = Random.Range(1,4);
//     Vector3 spawnPosition = new Vector3(randomX, randomY, 0);
//     if (spawnedObjects.Count >= 2)
//     {
//         Destroy(spawnedObjects[0]);
//         spawnedObjects.RemoveAt(0);
//     }
//     GameObject newHoop= Instantiate(spawnObjects, spawnPosition, Quaternion.identity);
//     spawnedObjects.Add(newHoop);
//     if (spawnedObjects.Count > 1)
//     {
//         float previousY = spawnedObjects[spawnedObjects.Count - 2].transform.position.y;
//         if (spawnPosition.y <= previousY)
//         {
//             float newY = previousY + 1f; 
//             spawnPosition.y = newY;
//             newHoop.transform.position = spawnPosition;
//         }
//     }
// }