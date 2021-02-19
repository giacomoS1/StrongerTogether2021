using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float padding;
    public float yOffset;
    public Transform[] players;
    public float speed;
    private Camera cam;
    private void Start()
    {
        cam = GetComponent<Camera>();
    }
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

        //5 camera size is 20 units in the world
        cam.orthographicSize = cam.orthographicSize + speed * ((padding + 0.5f + Mathf.Abs(players[0].position.x - players[1].position.x) / 2) - cam.orthographicSize);
        transform.position = Vector3.Lerp(transform.position, new Vector3(averageX, averageY + yOffset, -10), speed);
    }

}
