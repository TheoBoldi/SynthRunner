using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class NoFogOnCamera : MonoBehaviour
{
    public bool AllowFog = false;

    private bool FogOn;

    private void OnPreRender()
    {
        FogOn = RenderSettings.fog;
        RenderSettings.fog = AllowFog;
    }

    private void OnPostRender()
    {
        RenderSettings.fog = FogOn;
    }

}