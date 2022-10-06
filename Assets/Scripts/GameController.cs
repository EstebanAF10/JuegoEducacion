using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public int level = 0;

    // private bool selectingAnswer = false;
    private bool seleccionandoRespuesta = false;
    private int respuestaSeleccionada;

    public GameObject respuestaA;
    public GameObject respuestaB;

    private BoxCollider2D respuestaACollider;
    private BoxCollider2D respuestaBCollider;

    public GameObject player;

    private CapsuleCollider2D playerCollider;

    // Seccion de preguntas
    public Dificultad[] bancoPreguntas;
    public TextMeshProUGUI enunciado;
    public TextMeshProUGUI[] respuestas;
    public Pregunta preguntaActual;
    //public PanelComplementario panelComplementario;
    //public Button[] btnRespuesta;
    // Seccion de preguntas

    
    // Start is called before the first frame update
    void Start()
    {
        level = 0; //Nivel del juego
        seleccionandoRespuesta = false;

        cargarBancoPreguntas();
        setPregunta();

        playerCollider = player.GetComponent<CapsuleCollider2D>();
        
        respuestaACollider = respuestaA.GetComponent<BoxCollider2D>();
        respuestaBCollider = respuestaB.GetComponent<BoxCollider2D>();

    }

   private void Update(){
    if (playerCollider.IsTouching(respuestaACollider))
        {
            Debug.Log("Seleccionando respuesta A");
            respuestaSeleccionada = 0;
            seleccionandoRespuesta = true;
        } else {
            seleccionandoRespuesta = false;
        }

    if (playerCollider.IsTouching(respuestaBCollider))
        {
            Debug.Log("Seleccionando respuesta B");
            respuestaSeleccionada = 1;
            seleccionandoRespuesta = true;
        }   
         else {
            seleccionandoRespuesta = false;
        } 

    if(seleccionandoRespuesta){ //Esto solo es de prueba, evaluarPregunta se tiene que hacer solo despues de que termine el timer. 
        evaluarPregunta(respuestaSeleccionada);
    }
   }


    public void setPregunta(){
        int preguntaRandom = Random.Range(0,bancoPreguntas[level].preguntas.Length);
        preguntaActual = bancoPreguntas[level].preguntas[preguntaRandom];
        enunciado.text = preguntaActual.enunciado;

        for(int i = 0; i < respuestas.Length; i++)
        {
            respuestas[i].text = preguntaActual.respuestas[i].texto;
        }
    }

    public void cargarBancoPreguntas(){
        try{

            bancoPreguntas = JsonConvert.DeserializeObject<Dificultad[]>(File.ReadAllText(Application.streamingAssetsPath + "/QuestionBank.json"));

        }catch(System.Exception ex)
        {
            Debug.Log(ex.Message);
            enunciado.text = ex.Message;
        }
    }

    public void evaluarPregunta(int respuestaSeleccionada)
    {
        if (respuestaSeleccionada == preguntaActual.respuestaCorrecta) 
        {
            Debug.Log("Has pasado de nivel");
            // level++;
            if (level == bancoPreguntas.Length)
            {
                Debug.Log("Has Ganado");
                //SceneManager.LoadScene("Gane");
            }
            // else
            // {
            //     try
            //     {
            //         panelComplementario.desplegar();
            //     }
            //     catch (System.Exception ex)
            //     {
            //         Debug
            //             .Log("Olvidaste configurar el panel complementario: " +
            //             ex.Message);
            //     }
            // }
        }
        else
        {
            Debug.Log("Respuesta Incorrecta");
            //SceneManager.LoadScene("Perdida");
        }
    }

}
