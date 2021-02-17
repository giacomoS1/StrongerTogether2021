using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float padding;
    public Transform[] players;
    public float speed;
    void Update()
    {
        float averageY = 0;
        float averageX = 0;
        foreach(Transform p in players)
        {
            averageY += p.position.y;
            averageX += p.position.x;
        }
        averageY /= players.Length;
        averageX /= players.Length;
        transform.position = Vector3.Lerp(transform.position, new Vector3(averageX, averageY, -10), speed);
    }

}
