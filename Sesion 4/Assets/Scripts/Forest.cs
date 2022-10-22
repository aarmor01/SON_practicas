using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Forest : MonoBehaviour
{
    [SerializeField][Range(0, 1)] float IBirds;
    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    [SerializeField] float radius;

    [SerializeField] AudioClip forest_loop;
    [SerializeField] AudioClip[] birds;

    AudioSource forest_loop_source;
    AudioSource[] birds_source;

    private void Awake()
    {
        forest_loop_source = gameObject.AddComponent<AudioSource>();
        forest_loop_source.clip = forest_loop;
        forest_loop_source.volume = IBirds;
        forest_loop_source.playOnAwake = true;
        forest_loop_source.loop = true;
        forest_loop_source.Play();

        birds_source = new AudioSource[birds.Length];
        for (int i = 0; i < birds.Length; i++)
        {
            GameObject go = new GameObject($"Chatter: {i}");

            birds_source[i] = go.AddComponent<AudioSource>();
            birds_source[i].transform.SetParent(transform);
            birds_source[i].clip = birds[i];
            birds_source[i].volume = IBirds;
            birds_source[i].playOnAwake = false;
        }

        StartCoroutine(PlayBirdsEveryTime());
    }

    IEnumerator PlayBirdsEveryTime()
    {
        float time = Random.Range(minTime, maxTime);
        while (true)
        {
            yield return new WaitForSeconds(time);

            foreach (var bird
                in birds_source)
            {
                if (!bird.isPlaying && Random.Range(0, 1) <= IBirds)
                {
                    bird.gameObject.transform.position = Random.insideUnitCircle * (radius / Mathf.Max(IBirds, 0.01f));
                    bird.volume = IBirds;
                    bird.Play();
                }
            }
        }
    }

    private void Update()
    {
        forest_loop_source.volume = IBirds;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
