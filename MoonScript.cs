using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour
{
    public Transform spriteMask;
    public Transform startPoint;
    public Transform endPoint;
    private float t;
    private float time;
    private float speed;
    // Start is called before the first frame update
    private void Start()
    {
        startTimer(60);
    }
    void Update()
    {
        if (t <= time)
        {
            t += Time.deltaTime;
            spriteMask.Translate(Vector2.right * Time.deltaTime * speed);
        } else
        {
            //GAME OVER
        }
    }
    public void startTimer(float time)
    {
        this.time = time;
        t = 0;
        speed = (endPoint.position.x - startPoint.position.x) / time;
    }
}
