using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class StepSoundManager : MonoBehaviour
{
    [SerializeField] EventReference footstepEventReference;
    private EventInstance footsteps;

    private void Awake()
    {
        if (footstepEventReference.IsNull)
            footsteps = RuntimeManager.CreateInstance(footstepEventReference);
    }

    public void PlayFootsteps()
    {
        if (footsteps.isValid())
            footsteps.start();
    }
}
