using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuardarDatos : MonoBehaviour
{
    //datos para crear la tabla
    private TextMeshProUGUI textMesh;
    public bool derrotaVictoria = false;

    //nombre del input
    private string namePlayer;
    private int nivel;
    public bool subirNivel = true;

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
        if (derrotaVictoria)
        {
            //iniciar textMesh para la tabla
            textMesh = GetComponent<TextMeshProUGUI>();
        }
        
    }
    private void Update()
    {
        if (derrotaVictoria)
        {
            //actualizar tabla
            textMesh.text = namePlayer+"    nivel: "+nivel.ToString("0");
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
    }

    private void SaveData()
    {
        PlayerPrefs.SetString(namePrefsName, namePlayer);
        PlayerPrefs.SetInt(nivelPrefsName, nivel);

        print("guardando nombre: " + namePlayer + "\n");
        print("guardando nivel: " + nivel);

    }
    private void LoadData()
    {
        namePlayer = PlayerPrefs.GetString(namePrefsName, "NoName");
        nivel = PlayerPrefs.GetInt(nivelPrefsName);

        print("cargando nombre: " + namePlayer + "\n");
        print("cargando nivel: " + nivel);
    }
}
