using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayAudio : MonoBehaviour
{

    // [SerializeField] private AudioSource backgroundAudio;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        if(SceneManager.GetActiveScene().name == "Perdida"){
            Destroy(gameObject);
        }
    }

    // public void PlayBGAudio(){
    //     backgroundAudio = GetComponent<AudioSource>();
    //     backgroundAudio.Play();
    //     DontDestroyOnLoad(gameObject);
    // }

    
}
