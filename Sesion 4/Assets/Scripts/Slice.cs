using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Autores
// Aarón Nauzet Moreno Sosa
// Nicolás Rosa Caballero

public class Slice : MonoBehaviour
{
    private int frequency = 44100;
    private int numSamples;

    private AudioSource head;
    private AudioSource tail;

    public float overlapTime = 0.02f;
    public AudioClip[] pcmDataHeads, pcmDataTails;
    private int nHeads, nTails;


    void Start()
    {
        nHeads = pcmDataHeads.Length;
        nTails = pcmDataTails.Length;
        head = gameObject.AddComponent<AudioSource>();
        tail = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int h = Random.Range(0, nHeads), t = Random.Range(0, nTails);
            head.clip = pcmDataHeads[h];
            tail.clip = pcmDataTails[t];

            numSamples = (int)(overlapTime * frequency);

            FadeOut(head.clip);
            FadeIn(tail.clip);

            double clipLength = (head.clip.samples / head.pitch);
            Debug.Log($"head {h} length {clipLength}  p tail {t}");
            head.Play();
            tail.PlayScheduled(AudioSettings.dspTime + (clipLength / 44100) - overlapTime);
        }
    }

    void FadeOut(AudioClip head)
    {
        if (numSamples > head.samples)
            Debug.LogWarning("Overlap time too long, fade out applied to the whole audio.");

        float[] samplesHead = new float[head.samples];

        head.GetData(samplesHead, 0);

        for (int i = Mathf.Max(samplesHead.Length - numSamples, 0); i < samplesHead.Length; ++i)
            samplesHead[i] = Mathf.Sqrt((numSamples - samplesHead[i]) / numSamples);

        head.SetData(samplesHead, 0);
    }

    void FadeIn(AudioClip tail)
    {
        if (numSamples > tail.samples)
            Debug.LogWarning("Overlap time too long, fade in applied to the whole audio.");

        float[] samplesTail = new float[tail.samples];

        tail.GetData(samplesTail, 0);

        for (int i = 0; i < Mathf.Min(numSamples, samplesTail.Length); ++i)
            samplesTail[i] = Mathf.Sqrt(samplesTail[i] / numSamples);
        tail.SetData(samplesTail, 0);
    }
}
