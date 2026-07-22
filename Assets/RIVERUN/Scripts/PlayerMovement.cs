using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    // Velocidad de carrera
    public float forwardSpeed = 8f;
    private float normalSpeed;


    // Referencia al río
    public RiverController river;


    // Carriles
    public float laneDistance = 8f;
    public float laneChangeSpeed = 10f;

    private int currentLane = 1;


    // Salto
    public float jumpForce = 8f;
    public float fallMultiplier = 3.5f;


    private Rigidbody rb;
    public Animator animator;

    private bool isGrounded = true;
    private bool slowed = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (animator != null)
        {
            Debug.Log("Animator encontrado: " + animator.gameObject.name);
        }
        else
        {
            Debug.Log("NO HAY ANIMATOR");
        }

        normalSpeed = forwardSpeed;
    }



    void Update()
    {
        // Correr hacia delante
        transform.Translate(
            Vector3.forward * forwardSpeed * Time.deltaTime,
            Space.World
        );


        // Ir izquierda
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            currentLane--;

            currentLane = Mathf.Clamp(currentLane, 0, 2);
        }



        // Ir derecha
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            currentLane++;

            currentLane = Mathf.Clamp(currentLane, 0, 2);
        }



        // Posición del carril
        float targetX = (currentLane - 1) * laneDistance;


        Vector3 targetPosition = new Vector3(
            targetX,
            transform.position.y,
            transform.position.z
        );


        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            laneChangeSpeed * Time.deltaTime
        );



        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(
                rb.linearVelocity.x,
                jumpForce,
                rb.linearVelocity.z
            );

            isGrounded = false;


            // Animación de salto
            if (animator != null)
            {
                animator.SetTrigger("Jump");
            }
        }



        // Caída más rápida
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up *
                Physics.gravity.y *
                fallMultiplier *
                Time.deltaTime;
        }
    }





    void OnCollisionEnter(Collision collision)
    {

        // Suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }



        // Tronco
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            HitObstacle();
        }
    }





    void HitObstacle()
    {
        Debug.Log("Golpe con tronco");


        if (animator != null)
        {
            Debug.Log("Enviando Stumble");

            animator.ResetTrigger("Stumble");
            animator.SetTrigger("Stumble");
        }


        if (river != null)
        {
            river.PlayerHitObstacle();
        }


        if (!slowed)
        {
            StartCoroutine(Stumble());
        }
    }





    IEnumerator Stumble()
    {
        slowed = true;


        // Baja velocidad
        forwardSpeed = 2f;


        yield return new WaitForSeconds(1f);



        // Recupera velocidad
        forwardSpeed = normalSpeed;


        slowed = false;
    }
}