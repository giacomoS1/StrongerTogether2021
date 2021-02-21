using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
public class PulsatingLight : MonoBehaviour
{
    public float time = 0;
    private float n = 0;
    private UnityEngine.Experimental.Rendering.Universal.Light2D l;
    private void Start()
    {
        if(time == 0)
        {
            time = Random.Range(0.04f, 0.08f);
        }

        l = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        
    }
    private void FixedUpdate()
    {
        n += time;
        l.intensity = Mathf.Abs(Mathf.Sin(n));
    }
}

