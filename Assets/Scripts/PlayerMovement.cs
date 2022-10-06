using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    public GameObject BulletPrefab;

    public float JumpForce;
    public float Speed;
    private float LastShoot;

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
            transform.localScale = new Vector3(-4.0f, 4.0f, 4.0f); //Se pone el eje "y" en negativo y se gira el personaje
        }
        else if(Horizontal > 0.0f) //Si va a la derecha
        {
            transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
        }

        Animator.SetBool("running", Horizontal != 0.0f); //Si horizontal =! 0 es true, si es != 0 es que nos estamos moviendo 

        //Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if(Physics2D.Raycast(transform.position, Vector3.down, 0.4f))
        {
            Grounded = true;
        }
        else Grounded = false;

        if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && Grounded)
        {
            Jump();
        }

        // if(Input.GetKeyDown(KeyCode.Space))//&& Time.time > LastShoot + 0.25f
        // {
        //     Shoot();
        //     //LastShoot = Time.time; //Esto es para rgular la velocidad entre cada disparo
        // }
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce); //El .up signofica que el eje "x=0" y "y=1"
    }

    // private void Shoot()
    // {
    //     Vector3 direction;
    //     if(transform.localScale.x == 4.0f) direction = Vector2.right;
    //     else direction = Vector2.left;

    //     GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity); //Esta funcion agarra un prefab y lo duplica en algun lugar del mundo
    //     bullet.GetComponent<BulletScript>().SetDirection(direction);
    // }

    private void FixedUpdate()  //FixedUpdate se usa siempre que trabajemos con fisicas ya que se tienen que actualizar con mucha frecuencia
    {
        //velocity espera un vector2 = dos elementos indican la "x" y "y" del mundo
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }


}
