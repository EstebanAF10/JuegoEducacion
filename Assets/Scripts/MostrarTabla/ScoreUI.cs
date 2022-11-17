using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{

    public ScoreManager scoreManager;
    public RowUI rowUI;

    public bool actualizar = true;


    void Start()
    {
        /*
        if (actualizar)
        {
            var scores = scoreManager.GetHighScores().ToArray();
            for (int i = 0; i < scores.Length; i++)
            {
                var row = Instantiate(rowUI, transform).GetComponent<RowUI>();
                row.rank.text = (i + 1).ToString();
                row.nombre.text = scores[i].nombre;
                row.nivel.text = scores[i].nivel.ToString();
            }
            actualizar = false;
        }*/
    }
    private void Update()
    {
        if (actualizar)
        {
            var scores = scoreManager.GetHighScores().ToArray();
            int maxPlayers = 5;
            if(scores.Length < 5)
            {
                maxPlayers = scores.Length;
            }
            for (int i = 0; i < maxPlayers; i++)
            {
                var row = Instantiate(rowUI, transform).GetComponent<RowUI>();
                row.rank.text = (i + 1).ToString();
                row.nombre.text = scores[i].nombre;
                row.nivel.text = scores[i].nivel.ToString();
            }
            actualizar = false;
        }
    }


}
