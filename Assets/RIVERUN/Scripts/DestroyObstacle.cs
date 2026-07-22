using UnityEngine;

public class DestroyObstacle : MonoBehaviour
{
    public float destroyDistance = 20f;

    private Transform player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        if (transform.position.z < player.position.z - destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}