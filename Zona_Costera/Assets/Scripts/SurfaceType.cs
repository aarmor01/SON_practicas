using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Surface { Grass, Stones, Dirt, Wood, Water, Sand }

public class SurfaceType : MonoBehaviour
{
    [SerializeField] private Surface surface;
    
    public string SurfaceName => surface.ToString();
}
