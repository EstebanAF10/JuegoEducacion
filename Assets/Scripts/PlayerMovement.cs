using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using static UnityEngine.Physics;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;   
    private bool Grounded;
    public float walkSpeed = 3f;
    public float JumpForce;
    private SkeletonAnimation skeletonAnimation;
    private string previousState, currentState;
    public GameObject[] waterSurface;
    public GameObject[] lavaSurface;
    private float xAxis;
    private float yAxis;
    private bool climbingAllowed = false;
    private Vector2 initialPosition;

    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource spikeSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //Eje X
        xAxis = Input.GetAxis("Horizontal");

         if(xAxis < 0.0f) //Si va a la izquierda
        {
            transform.localScale = new Vector3(-0.6f, 0.6f, 0.6f); //Se pone el eje "y" en negativo y se gira el personaje
        }
        else if(xAxis > 0.0f) //Si va a la derecha
        {
            transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }

        currentState = xAxis == 0 ? "Idle" : "Run"; //Si el hAxis es 0, el estado es Idle, si no, es Run
        //Eje X

        //------Eje Y------
        yAxis = Input.GetAxis("Vertical");
        
        if(Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else{
            currentState = "jump_static"; 
            Grounded = false;
            }

        if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && Grounded)
        {
            jumpSoundEffect.Play();
            Rigidbody2D.AddForce(Vector2.up * JumpForce); //El .up signofica que el eje "x=0" y "y=1"
        }

        if(climbingAllowed){
            yAxis *= walkSpeed;
        }
        
        //-----Eje Y------


        // Cambio de animaciones
        if(previousState != currentState)
        {
            skeletonAnimation.state.SetAnimation(0, currentState, true);
        }

        previousState = currentState;
        // Cambio de animaciones
        

    }

    private void FixedUpdate()  //FixedUpdate se usa siempre que trabajemos con fisicas ya que se tienen que actualizar con mucha frecuencia
    {
        //velocity espera un vector2 = dos elementos indican la "x" y "y" del mundo
        Rigidbody2D.velocity = new Vector2(xAxis * walkSpeed, Rigidbody2D.velocity.y);

        if(climbingAllowed){
            Rigidbody2D.isKinematic = true;
            Rigidbody2D.velocity = new Vector2(xAxis, yAxis);
        }else{
            Rigidbody2D.isKinematic = false;
            Rigidbody2D.velocity = new Vector2(xAxis * walkSpeed, Rigidbody2D.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {

        //Water collider
        if(collider.name == "WaterSurface") walkSpeed = 0.5f;
        //Water collider vvvvvv

        //Lava collider
        if(collider.name == "LavaSurface")
        {
            walkSpeed = 0.1f;
            SceneManager.LoadScene("Perdida");
        }
        //Lava collider

        if(collider.CompareTag("Ladder")){
            climbingAllowed = true;
        }

        if (collider.name == "PointA")
        {
            SceneManager.LoadScene("EasterEgg");
        }
        if (collider.name == "PointB")
        {
            SceneManager.LoadScene("Nivel4");
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {

        //Al salir de cualquier collider
        walkSpeed = 3f;

        if(collider.CompareTag("Ladder")){
            climbingAllowed = false;
        }
    }

    public void Hit(GameObject spike)
    {
  
        if (spike)
        {
            spikeSoundEffect.Play();
            transform.position = initialPosition;
                
        }

    }


}
