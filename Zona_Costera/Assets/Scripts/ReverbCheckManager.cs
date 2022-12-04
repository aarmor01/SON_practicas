using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverbCheckManager : MonoBehaviour
{
    [SerializeField]ReverbCheck[] reverbPoints;

    [ContextMenu("Fill")]
    void Fill()
    {
        reverbPoints = GetComponentsInChildren<ReverbCheck>();
    }

    public void EnableMe(ReverbCheck check)
    {
        foreach (var item in reverbPoints)
        {
            item.enabled = false;
        }

        check.enabled = true;
    }
}
