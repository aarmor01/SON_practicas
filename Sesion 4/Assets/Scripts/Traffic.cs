using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Traffic : MonoBehaviour
{
    [SerializeField] [Range(0, 1)]float ITraffic;
    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    [SerializeField] float radius;

    [SerializeField] AudioClip traffic_pad;
    [SerializeField] AudioClip[] passing;
    [SerializeField] AudioClip[] train;
    [SerializeField] AudioClip[] horn;

    AudioSource traffic_pad_source;
    AudioSource[] passing_source;
    AudioSource[] train_source;
    AudioSource[] horn_source;

    private void Awake()
    {
        traffic_pad_source = gameObject.AddComponent<AudioSource>();
        traffic_pad_source.clip = traffic_pad;
        traffic_pad_source.volume = ITraffic;
        traffic_pad_source.playOnAwake = true;
        traffic_pad_source.loop = true;
        traffic_pad_source.Play();

        passing_source = new AudioSource[passing.Length];
        for (int i = 0; i < passing.Length; i++)
        {
            passing_source[i] = gameObject.AddComponent<AudioSource>();
            passing_source[i].clip = passing[i];
            passing_source[i].volume = ITraffic;
            passing_source[i].playOnAwake = false;
        }

        train_source = new AudioSource[train.Length];
        for (int i = 0; i < train.Length; i++)
        {
            train_source[i] = gameObject.AddComponent<AudioSource>();
            train_source[i].clip = train[i];
            train_source[i].volume = ITraffic;
            train_source[i].playOnAwake = false;
        }

        horn_source = new AudioSource[horn.Length];
        for (int i = 0; i < horn.Length; i++)
        {
            GameObject go = new GameObject($"Horn: {i}");

            horn_source[i] = go.AddComponent<AudioSource>();
            horn_source[i].transform.SetParent(transform);
            horn_source[i].clip = horn[i];
            horn_source[i].volume = ITraffic;
            horn_source[i].playOnAwake = false;
        }

        StartCoroutine(PlayPassingEveryTime());
    }

    IEnumerator PlayPassingEveryTime()
    {
        float time = Random.Range(minTime, maxTime);
        while (true)
        {
            yield return new WaitForSeconds(time);

            if (ITraffic >= 0.2f)
            {
                foreach (var passing  in passing_source)
                {
                    if (!passing.isPlaying &&  Random.Range(0,1) <= ITraffic)
                    {
                        passing.volume = ITraffic;
                        passing.pitch = 1 + Random.Range(-0.05f, 0.05f);
                        passing.Play();
                    }
                }

                foreach (var train in train_source)
                {
                    if (!train.isPlaying && Random.Range(0, 1) <= ITraffic/2.0f)
                    {
                        train.volume = ITraffic;
                        train.pitch = 1 + Random.Range(-0.05f, 0.05f);
                        train.Play();
                    }
                }
            }
            if(ITraffic >= 0.5f)
            {
                foreach (var horns in horn_source)
                {
                    if (!horns.isPlaying &&  Random.Range(0, 1) <= ITraffic / 2.0f)
                    {
                        horns.gameObject.transform.position = Random.insideUnitCircle * radius;
                        horns.volume = ITraffic;
                        horns.Play();
                    }
                }
            }
        }
    }

    private void Update()
    {
        traffic_pad_source.volume = ITraffic;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

