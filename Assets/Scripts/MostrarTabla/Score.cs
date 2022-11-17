using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Score
{
    public string nombre;
    public int nivel;

    public Score(string nombre, int nivel)
    {
        this.nombre = nombre;
        this.nivel = nivel;
    }
}
