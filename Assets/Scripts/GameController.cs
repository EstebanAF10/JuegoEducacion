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

    // [SerializeField] InputHandler inputHandler;
    //[SerializeField] string playerName;

    public int level;

    public bool isFinal;

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

    //Timer
    public TextMeshProUGUI timerText;
    public float timeLeft;
    public bool timerOn = false;
    //Timer

    // [SerializeField] private AudioSource nextLevelSoundEffect;


    
    // Start is called before the first frame update
    void Start()
    {
        timerOn = true;
        timeLeft = 30; //Tiempo de cada nivel. Se resetea cada nuevo nivel

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
        } else if(playerCollider.IsTouching(respuestaBCollider))
        {
            Debug.Log("Seleccionando respuesta B");
            respuestaSeleccionada = 1;
            seleccionandoRespuesta = true;
        }


    if(seleccionandoRespuesta){ 
        evaluarPregunta(respuestaSeleccionada);
    }

    if(timerOn){
        if(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            displayTime(timeLeft);
        }else
        {
            Debug.Log("Time is up"); //Aqui se agrega la funcionalidad si se le acaba el timepo
            timeLeft = 0;
            timerOn = false;
            SceneManager.LoadScene("Perdida");
        }
    }

   }

   public void displayTime(float currentTime){
        if(currentTime < 0)
        {
            currentTime = 0;
        }

        currentTime += 1;
        int minutes = (int) Mathf.FloorToInt(currentTime / 60);
        int seconds = (int) Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
        StartCoroutine(EvaluarPreguntaDelay(respuestaSeleccionada));
    }

    IEnumerator EvaluarPreguntaDelay(int respuestaSeleccionada){
        if (respuestaSeleccionada == preguntaActual.respuestaCorrecta) 
        {
            // nextLevelSoundEffect.Play();
            Debug.Log("Has pasado de nivel");
            if(isFinal){

                //inputHandler.AddHighscoreIfPossible (new InputEntry(playerName, level));
                SceneManager.LoadScene("Gane"); //Para cargar la escena del ultimo nivel
                Debug.Log("HAS GANADO EL JUEGO!");

            }else{
                
                yield return new WaitForSecondsRealtime(0.1f);
                SceneManager.LoadScene("Nivel" + (level + 1));
            }
        }
        else
        {
            Debug.Log("Respuesta Incorrecta");
            //inputHandler.AddHighscoreIfPossible (new InputEntry(playerName, level));
            SceneManager.LoadScene("Perdida");
        }
    }


    

}
