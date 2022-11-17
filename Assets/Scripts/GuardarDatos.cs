using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GuardarDatos : MonoBehaviour
{
    //datos para crear la tabla
    public bool derrotaVictoria = false;
    public bool final = true;
    public ScoreManager scoreManager;

    //nombre del input
    private string namePlayer;
    private int nivel;
    public bool subirNivel = true;

    private string finalPrefsName = "final";
    private string nivelPrefsName = "nivelMax";
    private string namePrefsName = "nombreJugador";


    private void Awake()
    {
        LoadData();

        if (subirNivel)
        {
            nivel++;
        }

        
    }

    private void Start()
    {
        if (derrotaVictoria && final)
        {
            scoreManager.AddScore(new Score(namePlayer, nivel));
        }
    }

    private void OnDestroy()
    {
            SaveData();
    }

    //****** Leer nombre del string y guardar nombre y nivel
    public void LeerStringInput(string inputName)
    {
        namePlayer = inputName;
    }
    public void reiniciarNiveles()
    {
        nivel = 0;
        final = true;
    }

    public void ActualizarTabla(string sceneName)
    {
        final = false;
        SceneManager.LoadScene(sceneName);
    }

    private void SaveData()
    {
        PlayerPrefs.SetString(namePrefsName, namePlayer);
        PlayerPrefs.SetInt(nivelPrefsName, nivel);
        PlayerPrefs.SetInt(finalPrefsName, Convert.ToInt32 (final));

        print("guardando nombre: " + namePlayer + "\n");
        print("guardando nivel: " + nivel);
        print("guardando final: " + derrotaVictoria);
    }
    private void LoadData()
    {
        namePlayer = PlayerPrefs.GetString(namePrefsName, "NoName");
        nivel = PlayerPrefs.GetInt(nivelPrefsName);
        final = Convert.ToBoolean(PlayerPrefs.GetInt(finalPrefsName));

        print("cargando nombre: " + namePlayer + "\n");
        print("cargando nivel: " + nivel);
        print("cargando final: " + final);
    }
}
