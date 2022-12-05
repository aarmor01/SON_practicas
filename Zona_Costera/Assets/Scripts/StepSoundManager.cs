using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;

[RequireComponent(typeof(SurfaceDetector))]
public class StepSoundManager : MonoBehaviour
{
    [SerializeField] EventReference footstepsEvent;
    private EventInstance footstepsInstance;

    [SerializeField] StarterAssets.FirstPersonController controller;
    SurfaceDetector detector;

    const string parameter = "Footsteps";
    const string defaultLabel = "Dirt";

    private void Awake()
    {
        detector = GetComponent<SurfaceDetector>();

        if (!footstepsEvent.IsNull)
            footstepsInstance = RuntimeManager.CreateInstance(footstepsEvent);
    }

    public void PlayFootstepsEvent()
    {
        if (footstepsInstance.isValid() && controller != null ? controller.Grounded : true)
        {
            RuntimeManager.AttachInstanceToGameObject(footstepsInstance, transform);
            footstepsInstance.setParameterByNameWithLabel(parameter, detector.Surface?.SurfaceName ?? defaultLabel);
            footstepsInstance.start();
        }
    }
}
