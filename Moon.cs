using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public Sprite[] phases;
    public SpriteRenderer moon;
    private static float initialTime;
    private static float time;
    private static float interval;
    private static int phasesAmount;
    private static int phase;
    private void Start()
    {
        phasesAmount = phases.Length;
    }
    public void Update()
    {
        time -= Time.deltaTime;
        if(initialTime - interval * phase >= time)
        {
            phase++;
            moon.sprite = phases[phase - 1];
        }
        if(time <= 0)
        {
            //GAME OVER CODE
        }
    }
    public static void startTime(float t)
    {
        time = t;
        initialTime = t;
        phase = 1;
        interval = t / phasesAmount;
    }
}
