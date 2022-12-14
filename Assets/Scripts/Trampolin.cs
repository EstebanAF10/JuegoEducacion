using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour
{

    public float jumpforce = 2f;
    private Animator animator;
    [SerializeField] private AudioSource jumpSoundEffect;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            jumpSoundEffect.Play();
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = (Vector2.up * jumpforce);
            //agregar animaci[on] aca
            animator.Play("Shroompoline");
        }
    }
}
