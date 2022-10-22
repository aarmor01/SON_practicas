using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Chatter : MonoBehaviour
{
    [SerializeField][Range(0, 1)] float IChatter;
    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    [SerializeField] float radius;

    [SerializeField] AudioClip chatter_pad;
    [SerializeField] AudioClip[] chatter;

    AudioSource chatter_pad_source;
    AudioSource[] chatter_source;

    private void Awake()
    {
        chatter_pad_source = gameObject.AddComponent<AudioSource>();
        chatter_pad_source.clip = chatter_pad;
        chatter_pad_source.volume = IChatter;
        chatter_pad_source.playOnAwake = true;
        chatter_pad_source.loop = true;
        chatter_pad_source.Play();

        chatter_source = new AudioSource[chatter.Length];
        for (int i = 0; i < chatter.Length; i++)
        {
            GameObject go = new GameObject($"Chatter: {i}");

            chatter_source[i] = go.AddComponent<AudioSource>();
            chatter_source[i].transform.SetParent(transform);
            chatter_source[i].clip = chatter[i];
            chatter_source[i].volume = IChatter;
            chatter_source[i].playOnAwake = false;
        }

        StartCoroutine(PlayChatterEveryTime());
    }

    IEnumerator PlayChatterEveryTime()
    {
        float time = Random.Range(minTime, maxTime);
        while (true)
        {
            yield return new WaitForSeconds(time);

            if (IChatter >= 0.5f)
            {
                foreach (var chatter in chatter_source)
                {
                    if (!chatter.isPlaying && Random.Range(0, 1) <= IChatter)
                    {
                        chatter.gameObject.transform.position = Random.insideUnitCircle * radius;
                        chatter.volume = IChatter;
                        chatter.pitch = 1 + Random.Range(-0.05f, 0.05f);
                        chatter.Play();
                    }
                }
            }
        }
    }

    private void Update()
    {
        chatter_pad_source.volume = IChatter;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
