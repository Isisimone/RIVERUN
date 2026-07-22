using UnityEngine;
using System.Collections;

public class RiverController : MonoBehaviour
{
    public Transform player;

    // Animator del jugador
    public Animator playerAnimator;


    // Distancia normal del río
    public float maxDistance = 25f;

    // Distancia peligrosa
    public float minDistance = 5f;


    // Distancia actual
    private float riverDistance;


    // Cuánto se acerca por golpe
    public float hitAmount = 5f;


    // Cuánto baja si el jugador avanza bien
    public float recoverAmount = 3f;


    // Tiempo para recuperar
    public float recoverTime = 5f;


    private float timer;


    // Evita activar la muerte varias veces
    private bool gameOver = false;



    void Start()
    {
        riverDistance = maxDistance;
    }



    void Update()
    {
        if (player == null)
            return;



        Vector3 targetPosition = new Vector3(
            transform.position.x,
            transform.position.y,
            player.position.z - riverDistance
        );



        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * 5f
        );



        // Recuperación automática
        timer += Time.deltaTime;


        if (timer >= recoverTime)
        {
            MoveRiverBack();

            timer = 0;
        }
    }




    // Llamado cuando el jugador golpea un tronco
    public void PlayerHitObstacle()
    {
        riverDistance -= hitAmount;


        if (riverDistance < minDistance)
        {
            riverDistance = minDistance;
        }


        timer = 0;


        Debug.Log("Río se acerca: " + riverDistance);
    }





    void MoveRiverBack()
    {
        riverDistance += recoverAmount;


        if (riverDistance > maxDistance)
        {
            riverDistance = maxDistance;
        }


        Debug.Log("Río baja: " + riverDistance);
    }





    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !gameOver)
        {
            gameOver = true;


            Debug.Log("El río alcanzó al jugador");



            // Animación de muerte
            if (playerAnimator != null)
            {
                playerAnimator.SetTrigger("Death");
            }



            StartCoroutine(GameOverDelay());
        }
    }





    IEnumerator GameOverDelay()
    {
        // Tiempo para ver la animación
        yield return new WaitForSeconds(2f);



        // Después congelamos el juego
        Time.timeScale = 0;
    }
}