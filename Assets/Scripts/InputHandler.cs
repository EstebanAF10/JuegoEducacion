using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputHandler : MonoBehaviour {
    [SerializeField] TMP_InputField nameInput;
    // [SerializeField] int currentLevel; 
    [SerializeField] string filename;
    [SerializeField] int maxCount = 5;

    List<InputEntry> highScoreList = new List<InputEntry> ();

    

    private void Start () {
        LoadHighscores();
    }

    public void LoadHighscores(){
        highScoreList = FileHandler.ReadListFromJSON<InputEntry> (filename);

        while(highScoreList.Count > maxCount){
            highScoreList.RemoveAt(maxCount); //Esto permite tener solo 5 highscores
        }
    }

    public void AddNameToList () { //Esto es igual a SaveHighscore()
        highScoreList.Add (new InputEntry (nameInput.text, 0)); //Revisar esto --------
        nameInput.text = "";

        FileHandler.SaveToJSON<InputEntry> (highScoreList, filename);
    }

    public void AddHighscoreIfPossible(InputEntry element){
        for (int i = 0; i < maxCount; i++)
        {
            if( i >= highScoreList.Count || element.maxLevel > highScoreList[i].maxLevel){
                highScoreList.Insert (i, element);

                while(highScoreList.Count > maxCount){
                    highScoreList.RemoveAt(maxCount); //Esto permite tener solo 5 highscores
                }  

                AddNameToList();

                break;
            }
        }
    }


}
