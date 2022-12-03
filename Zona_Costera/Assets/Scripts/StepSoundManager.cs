using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;

public class StepSoundManager : MonoBehaviour
{
    [SerializeField] EventReference footstepsEvent;
    private EventInstance footstepsInstance;
    [SerializeField] StarterAssets.FirstPersonController controller;
    [SerializeField]SurfaceDetector detector;
    const string parameter = "Footsteps";
    const string defaultLabel = "Dirt";

    private void Awake()
    {
        if (!footstepsEvent.IsNull)
            footstepsInstance = RuntimeManager.CreateInstance(footstepsEvent);
    }

    public void PlayFootstepsEvent()
    {
        if (footstepsInstance.isValid() && controller.Grounded)
        {
            RuntimeManager.AttachInstanceToGameObject(footstepsInstance, transform);
            footstepsInstance.setParameterByNameWithLabel(parameter, detector.Surface?.SurfaceName ?? defaultLabel);
            footstepsInstance.start();
        }
    }
}
