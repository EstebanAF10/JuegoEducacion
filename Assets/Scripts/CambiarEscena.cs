using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{

    public Animator transition;

    public void CambiarEscenaClick(string sceneName){
        Debug.Log("Cambiando de escena " + sceneName);
        StartCoroutine(CargarEscena(sceneName));
    }

    public void SalirJuego(){
        Debug.Log("Sailiendo del juego");
        Application.Quit();
    }

    IEnumerator CargarEscena(string sceneName){

        //yield return new WaitForSecondsRealtime(2f);
        transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(sceneName);
    }


}