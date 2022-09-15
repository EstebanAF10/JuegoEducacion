using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;

    public float JumpForce;
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>(); //Siempre hay que poner los componentes que creamos en Unity en Start para poder usarlos en Update
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal"); //Aqui obtenemos valores de -1 a 1 con respecto a lo que presione el jugador. "a" es -1, "d" es 1, nada es 0

        if(Horizontal < 0.0f) //Si va a la izquierda
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f); //Se pone el eje "y" en negativo y se gira el personaje
        }
        else if(Horizontal > 0.0f) //Si va a la derecha
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        Animator.SetBool("running", Horizontal != 0.0f); //Si horizontal =! 0 es true, si es != 0 es que nos estamos moviendo 

        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if(Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else Grounded = false;

        if(Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()  //FixedUpdate se usa siempre que trabajemos con fisicas ya que se tienen que actualizar con mucha frecuencia
    {
        //velocity espera un vector2 = dos elementos indican la "x" y "y" del mundo
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }


     private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce); //El .up signofica que el eje "x=0" y "y=1"
    }

}
