using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Storm : MonoBehaviour
{
    [SerializeField][Range(0, 1)] float IStorm;
    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    [SerializeField] float radius;

    [SerializeField] AudioClip[] rain;
    [SerializeField] AudioClip[] wind;
    [SerializeField] AudioClip[] thunderstorm;
    [SerializeField] AudioClip[] drop;

    AudioSource[] rain_source;
    AudioSource[] wind_source;
    AudioSource[] thunderstorm_source;
    AudioSource[] drop_source;

    private void Awake()
    {
        rain_source = new AudioSource[rain.Length];
        for (int i = 0; i < rain.Length; i++)
        {
            float lambda = 1.0f / rain.Length;
            GameObject go = new GameObject($"Chatter: {i}");

            rain_source[i] = go.AddComponent<AudioSource>();
            rain_source[i].transform.SetParent(transform);
            rain_source[i].clip = rain[i];
            rain_source[i].volume = Mathf.InverseLerp(lambda * i, lambda * (i + 1), Mathf.Min(IStorm * (lambda * (i + 1)), lambda * (i + 1)));
            rain_source[i].playOnAwake = true;
        }

        wind_source = new AudioSource[wind.Length];
        for (int i = 0; i < wind.Length; i++)
        {
            float lambda = 1.0f / wind.Length;
            GameObject go = new GameObject($"Chatter: {i}");

            wind_source[i] = go.AddComponent<AudioSource>();
            wind_source[i].transform.SetParent(transform);
            wind_source[i].clip = wind[i];
            wind_source[i].volume = Mathf.InverseLerp(lambda * i, lambda * (i + 1), Mathf.Min(IStorm * (lambda * (i + 1)), lambda * (i + 1)));
            wind_source[i].playOnAwake = true;
        }

        thunderstorm_source = new AudioSource[thunderstorm.Length];
        for (int i = 0; i < thunderstorm.Length; i++)
        {
            GameObject go = new GameObject($"Chatter: {i}");

            thunderstorm_source[i] = go.AddComponent<AudioSource>();
            thunderstorm_source[i].transform.SetParent(transform);
            thunderstorm_source[i].clip = thunderstorm[i];
            thunderstorm_source[i].volume = IStorm;
            thunderstorm_source[i].playOnAwake = false;
        }

        drop_source = new AudioSource[drop.Length];
        for (int i = 0; i < drop.Length; i++)
        {
            GameObject go = new GameObject($"Chatter: {i}");

            drop_source[i] = go.AddComponent<AudioSource>();
            drop_source[i].transform.SetParent(transform);
            drop_source[i].clip = drop[i];
            drop_source[i].volume = IStorm;
            drop_source[i].playOnAwake = false;
        }

        StartCoroutine(PlayStormEveryTime());
    }

    IEnumerator PlayStormEveryTime()
    {
        float time = Random.Range(minTime, maxTime);
        while (true)
        {
            yield return new WaitForSeconds(time);

            for (int i = 0; i < rain_source.Length; i++)
            {
                float lambda = 1.0f / rain_source.Length;
                rain_source[i].volume = Mathf.InverseLerp(lambda * i, lambda * (i + 1),
                                            Mathf.Min(IStorm * (lambda * (i + 1)), lambda * (i + 1)));
            }

            for (int i = 0; i < wind_source.Length; i++)
            {
                float lambda = 1.0f / wind_source.Length;
                wind_source[i].volume = Mathf.InverseLerp(lambda * i, lambda * (i + 1),
                                            Mathf.Min(IStorm * (lambda * (i + 1)), lambda * (i + 1)));
            }

            foreach (var thunderstorm in thunderstorm_source)
            {
                if (!thunderstorm.isPlaying && Random.Range(0, 1) <= IStorm)
                {
                    thunderstorm.gameObject.transform.position = Random.insideUnitCircle * radius;
                    thunderstorm.volume = IStorm;
                    thunderstorm.Play();
                }
            }

            foreach (var drop in drop_source)
            {
                if (!drop.isPlaying && Random.Range(0, 1) <= IStorm)
                {
                    drop.gameObject.transform.position = Random.insideUnitCircle * radius;
                    drop.volume = IStorm;
                    drop.Play();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
