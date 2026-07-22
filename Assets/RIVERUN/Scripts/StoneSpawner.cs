using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    public GameObject stonePrefab;
    public Transform player;

    public float spawnTime = 2f;
    public float spawnDistance = 40f;

    private float timer;


    void Update()
    {
        timer += Time.deltaTime;


        if (timer >= spawnTime)
        {
            SpawnPattern();
            timer = 0f;
        }
    }



    void SpawnPattern()
    {
        // 0 = una piedra
        // 1 = cinco piedras juntas

        int pattern = Random.Range(0, 2);


        if (pattern == 0)
        {
            SpawnSingleStone();
        }
        else
        {
            SpawnFiveStones();
        }
    }



    void SpawnSingleStone()
    {
        int lane = Random.Range(0, 3);

        float xPosition = GetLanePosition(lane);


        Vector3 spawnPosition = new Vector3(
            xPosition,
            0.5f,
            player.position.z + spawnDistance
        );


        Instantiate(
            stonePrefab,
            spawnPosition,
            Quaternion.identity
        );
    }



    void SpawnFiveStones()
    {
        int lane = Random.Range(0, 3);

        float xPosition = GetLanePosition(lane);


        for (int i = 0; i < 5; i++)
        {
            Vector3 spawnPosition = new Vector3(
                xPosition,
                0.5f,
                player.position.z + spawnDistance + (i * 4)
            );


            Instantiate(
                stonePrefab,
                spawnPosition,
                Quaternion.identity
            );
        }
    }



    float GetLanePosition(int lane)
    {
        // Carril izquierdo
        if (lane == 0)
        {
            return -12f;
        }


        // Carril central
        if (lane == 1)
        {
            return 0f;
        }


        // Carril derecho
        return 12f;
    }
}