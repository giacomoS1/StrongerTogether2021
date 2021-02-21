using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
public class PulsatingLight : MonoBehaviour
{
    private float time;
    private float n = 0;
    private UnityEngine.Experimental.Rendering.Universal.Light2D light;
    private void Start()
    {
        light = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        time = Random.Range(0.04f, 0.08f);
    }
    private void FixedUpdate()
    {
        n += time;
        light.intensity = Mathf.Abs(Mathf.Sin(n));
    }
}

