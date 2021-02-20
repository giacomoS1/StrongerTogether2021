using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFall : MonoBehaviour
{
    public float targetRotation;
    public float speed;
    public BoxCollider2D treeCollider;
    public GameObject sparkle;
    bool triggered = false;
    Transform tree;
    private float t;

    private void Start()
    {
        tree = transform.parent;
    }
    private void Update()
    {
        if(triggered && t <= 1)
        {
            t += speed * Time.deltaTime;
            Debug.Log(t);
            if(t > 1)
            {
                t = 1;
                tree.rotation = Quaternion.Euler(0, 0, targetRotation);
                treeCollider.enabled = true;
                GetComponent<TreeFall>().enabled = false;
            } else
            {
                tree.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, targetRotation), t);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        triggered = true;
        sparkle.SetActive(false);
    }
}
