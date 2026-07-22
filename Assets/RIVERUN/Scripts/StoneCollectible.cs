using UnityEngine;

public class StoneCollectible : MonoBehaviour
{
    public int points = 1;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Piedra recogida");

            Destroy(gameObject);
        }
    }
}