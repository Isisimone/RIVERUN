using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public Transform player;

    public float spawnTime = 2f;
    public float spawnDistance = 40f;

    private float timer;


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTime)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }


    void SpawnObstacle()
    {
        // Elegir carril aleatorio
        int lane = Random.Range(0, 3);


        float xPosition = 0f;


        // Carril izquierdo
        if (lane == 0)
        {
            xPosition = -12f;
        }

        // Carril centro
        if (lane == 1)
        {
            xPosition = 0f;
        }

        // Carril derecho
        if (lane == 2)
        {
            xPosition = 12f;
        }


        Vector3 spawnPosition = new Vector3(
            xPosition,
            0.5f,
            player.position.z + spawnDistance
        );


        Debug.Log("LOG CREADO EN X: " + xPosition);


        Instantiate(
            obstaclePrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}