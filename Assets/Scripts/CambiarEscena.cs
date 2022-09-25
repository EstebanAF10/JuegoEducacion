using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public void CambiarEscenaClick(string sceneName){
        Debug.Log("Cambiando de escena " + sceneName);
        SceneManager.LoadScene(sceneName);
        //StartCoroutine(retrasoEscena(sceneName));
    }

    public void SalirJuego(){
        Debug.Log("Sailiendo del juego");
        Application.Quit();
    }

    // IEnumerator retrasoEscena(string sceneName){
    //     yield return new WaitForSecondsRealtime(1f);
    //     SceneManager.LoadScene(sceneName);
    // }


}