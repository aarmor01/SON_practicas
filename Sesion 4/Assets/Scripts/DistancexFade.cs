    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;

    public class DistancexFade: MonoBehaviour
    {
        [SerializeField] AudioSource sound;    
        public float minDistance, maxDistance;
        public float dist;
        [SerializeField] GameObject listener;


        //private AudioLowPassFilter filter;
        //private AudioReverbFilter reverb;

        private float freq;

        void Start() {               
            minDistance = 1; 
            maxDistance = 50;
            sound = GetComponent<AudioSource>();
            //filter = GetComponent<AudioLowPassFilter>();   
            //reverb = GetComponent<AudioReverbFilter>();  // 

            sound.Play();
        }


        void Update() {
            // distancia entre listener y source
            dist = Vector3.Distance(listener.transform.position, sound.transform.position);
            dist = Mathf.Clamp(dist,0.00000001f,maxDistance);
            //Debug.Log("dist: " + dist);
            sound.spatialBlend = dist/maxDistance;  // 2d-3d
            sound.spread = 360f/dist;  // spread stereo
            
            //filter.cutoffFrequency = 3000*maxDistance/dist;  // corte del filtro
            //reverb.dryLevel = -1000f*(dist/maxDistance);
            //reverb.room = -10000*(1-dist/(1f*maxDistance));
        }
    }

