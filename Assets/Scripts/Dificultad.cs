using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //Se pone esto y se quita el monoBehaviour por que no es un GameObject
public class Dificultad
{
    public string nombreDificultad;

    public Pregunta[] preguntas;
}