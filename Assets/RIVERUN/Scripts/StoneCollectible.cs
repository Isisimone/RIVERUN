using UnityEngine;

public class StoneCollectible : MonoBehaviour
{
    public int points = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Piedra recogida");

            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddGem(points);
            }

            Destroy(gameObject);
        }
    }
}