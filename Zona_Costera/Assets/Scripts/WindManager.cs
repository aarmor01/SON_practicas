using FMODUnity;
using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class WindManager : MonoBehaviour
{
    [SerializeField] StudioEventEmitter windEvent;
    [SerializeField] Transform character;

    const string parameter = "Height";
    const float maxHeight = 12;

    float ogHeight;

    private void Awake()
    {
        ogHeight = transform.transform.position.y;
    }

    private void Update()
    {
        float height = character.position.y - ogHeight;

        height = Mathf.Max(height, 0);
        height = Mathf.Min(height, maxHeight);

        windEvent.EventInstance.setParameterByName(parameter, height);
    }
}
