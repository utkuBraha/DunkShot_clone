using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObjects;
    public float minX = -2.2f;
    public float maxX = 2.2f;
    public Transform spawnPoint;
    public BallScript ballScript;
    private int _previousScore;
    
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
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(3, 5);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);
        Instantiate(spawnObjects, spawnPosition, Quaternion.identity);
    }
}
