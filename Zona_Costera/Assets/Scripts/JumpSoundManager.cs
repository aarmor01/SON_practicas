using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSoundManager : MonoBehaviour
{
    [SerializeField] EventReference jumpEvent;
    [SerializeField] EventReference fallEvent;
    private EventInstance jumpInstance;
    private EventInstance fallInstance;

    [SerializeField] StarterAssets.FirstPersonController controller;
    SurfaceDetector detector;

    const string parameter = "Fall";
    const string defaultLabel = "Dirt";
    bool groundedPrevFrame = false;

    private void Awake()
    {
        detector = GetComponent<SurfaceDetector>();

        if (!jumpEvent.IsNull)
            jumpInstance = RuntimeManager.CreateInstance(jumpEvent);

        if(!fallEvent.IsNull)
            fallInstance = RuntimeManager.CreateInstance(fallEvent);
    }

    private void LateUpdate()
    {
        if (controller.Grounded && !groundedPrevFrame)
            PlayFall();
        else if (!controller.Grounded && groundedPrevFrame)
            PlayJump();

        groundedPrevFrame = controller.Grounded;
    }

    public void PlayJump()
    {
        if(jumpInstance.isValid())
        {
            RuntimeManager.AttachInstanceToGameObject(jumpInstance, transform);
            jumpInstance.start();
        }
    }

    public void PlayFall()
    {
        if (fallInstance.isValid())
        {
            RuntimeManager.AttachInstanceToGameObject(fallInstance, transform);
            fallInstance.setParameterByNameWithLabel(parameter, detector.Surface?.SurfaceName ?? defaultLabel);
            fallInstance.start();
        }
    }
}
