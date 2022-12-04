using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public enum PokemonCry { Axew, Buizel, Froakie, Noibat, Scorbunny, Phantump, Eevee }

public class CrySound : MonoBehaviour
{
    [SerializeField] EventReference cryEvent;
    private EventInstance cryInstance;

    const string parameter = "Cry";
    [SerializeField] PokemonCry pokemon;
    [SerializeField] float minTime, maxTime;
    float timer = 0;
    float duration = 0;

    private void Start()
    {
        duration = Random.Range(minTime, maxTime);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= duration)
        {
            timer -= duration;
            duration = Random.Range(minTime, maxTime);
            PlayCry();
        }
    }

    private void Awake()
    {
        if (!cryEvent.IsNull)
            cryInstance = RuntimeManager.CreateInstance(cryEvent);
    }

    public void PlayCry()
    {
        if (cryInstance.isValid())
        {
            RuntimeManager.AttachInstanceToGameObject(cryInstance, transform);
            cryInstance.setParameterByNameWithLabel(parameter, pokemon.ToString());
            cryInstance.start();
            //Debug.Log("Playing " + pokemon.ToString());
        }
    }
}
