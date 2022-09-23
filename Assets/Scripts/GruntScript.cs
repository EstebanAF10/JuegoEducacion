using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntScript : MonoBehaviour
{
    public GameObject John;

    private void Update() {

        //Esto es para que el enemigo vuelva a ver al jugador
        Vector3 direction = John.transform.position - transform.position; //transform.position es la posicion del enemigo
        if(direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); //derecha
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f); //izquierda
    }
}
