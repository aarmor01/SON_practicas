using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    private StudioEventEmitter windEvent;

    const string parameter = "Height";
    const float maxHeight = 10;

    float ogHeight;

    private void Awake()
    {
        windEvent = GetComponent<StudioEventEmitter>();
        ogHeight = player.transform.position.y;
        Debug.Log("Og Height: " + ogHeight);

    }

    private void Update()
    {
        float height = player.transform.position.y - ogHeight;

        Debug.Log("Height: " + height);
        height = Mathf.Max(height, 0);
        height = Mathf.Min(height, maxHeight);
        Debug.Log("Height: " + height);

        windEvent.EventInstance.setParameterByName(parameter, height);
        windEvent.EventInstance.start();

    }
}
