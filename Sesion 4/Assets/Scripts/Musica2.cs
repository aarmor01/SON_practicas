using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musica2 : MonoBehaviour{
    [SerializeField]
    private float maxVolume;
    [SerializeField]
    private float fadeTime;

    AudioSource musica;
    private int fade; // 0 nada, -1 fadeOut, 1 fadeIn

    void Start(){
        maxVolume = 0.8f;
        fadeTime = 10f;
        fade = 0;
        musica = GetComponent<AudioSource>();
        musica.loop = true;
        musica.volume = 0.2f;
        musica.Play(); 
    }
    void Update() { // automata controlado por fade
        if (Input.GetKey(KeyCode.O)) fade = -1;
        else if (Input.GetKey(KeyCode.I)) fade = 1;
        if (fade==-1) {
            Debug.Log($"vol {musica.volume}");
            if (musica.volume>0) {
                musica.volume -= maxVolume*Time.deltaTime/fadeTime;
                musica.volume = Mathf.Clamp(musica.volume,0,maxVolume);
            } else fade = 0;
        } else if (fade==1) {
            Debug.Log($"vol {musica.volume}");
            if (musica.volume<maxVolume) {
                musica.volume += maxVolume*Time.deltaTime/fadeTime;
                musica.volume = Mathf.Clamp(musica.volume,0,maxVolume);
            } else fade = 0;
        }        
    }
}
