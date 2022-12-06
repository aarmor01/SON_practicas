using FMODUnity;
using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class WindManager : MonoBehaviour
{
    [SerializeField] EventReference windEvent;
    private EventInstance windInstance;

    const string parameter = "Height";
    const float maxHeight = 10;

    float ogHeight;

    private void Awake()
    {
        if (!windEvent.IsNull)
            windInstance = RuntimeManager.CreateInstance(windEvent);

        ogHeight = transform.position.y;
        Debug.Log("Og Height: " + ogHeight);

    }

    private void Update()
    {
        float height = transform.position.y - ogHeight;

        height = Mathf.Max(height, 0);
        height = Mathf.Min(height, maxHeight);

        if (windInstance.isValid())
        {
            windInstance.setParameterByName(parameter, 5f);
            windInstance.start();
        }
    }
}
