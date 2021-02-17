using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1 : MonoBehaviour
{
    public float speed;
    [Range(0.0f, 1.0f)]
    public float accelerationSpeed;         //in seconds to reach max speed
    public Transform topLeftCorner;         //These two corners are meant to sense if the player is grounded
    public Transform bottomRightCorner;
    public LayerMask ground;
    public Rigidbody2D rb;
    public JumpCalculator jumping;
    public Transform[] rightRays;
    public Transform[] leftRays;
    [Header("Settings")]
    public KeyCode right;
    public KeyCode left;
    public KeyCode jump;

    [Header("Don't change")]
    [SerializeField] private float velocity = 0f;
    private float t = 0; //controls lerp between speeds, goes -1 to 1.


    // Update is called once per frame
    void Update()
    {
        float horizontal = (Input.GetKey(right) ? 1 : 0) + (Input.GetKey(left) ? -1 : 0); //checks if left or right keys are pressed, converts right to 1 and left to -1.

        if (Mathf.Abs(horizontal) >= 0) //checks if the absolute value of the key conversion is > 0, meaning a key is pressed (and not cancelled out)
        {
            t += horizontal * Time.deltaTime * (speed * accelerationSpeed); //updates interpolation, causes acceleration
            if (t > 1)          t = 1;
            else if (t < -1)    t = -1;   //if t is already at 1 or -1, stop it.
        }
        if (horizontal == 0)
        {
            if (velocity < 0)
            {
                t += speed * accelerationSpeed * Time.deltaTime;
                if (t > 0) t = 0;
            }
            if (velocity > 0)
            {
                t -= speed * accelerationSpeed * Time.deltaTime; //start interpolating to 0 acceleration.
                if (t < 0) t = 0;
            }
        }
        velocity = speed * t;
        velocity = (float)((int)(velocity * 100)) / 100; //rounds to nearest 100th.
        float move = velocity * Time.deltaTime;
        if(move > 0)
        {
            foreach(Transform p in rightRays)
            {
                RaycastHit2D hit = Physics2D.Raycast(p.position, p.TransformDirection(Vector3.right), Mathf.Abs(move));
                Debug.DrawRay(p.position, p.TransformDirection(Vector2.right), Color.red, Mathf.Abs(move));
                if (hit.collider != null)
                {
                    Debug.Log("ahhhh");
                    move = hit.point.x - p.position.x;
                    if (Mathf.Abs(move) <= 0.01) t = 0;
                }
            }
        } else if(move < 0)
        {
            foreach (Transform p in leftRays)
            {
                RaycastHit2D hit = Physics2D.Raycast(p.position, p.TransformDirection(Vector2.left), Mathf.Abs(move));
                Debug.DrawRay(p.position, p.TransformDirection(Vector2.left), Color.red, Mathf.Abs(move));
                if (hit.collider != null)
                {
                    Debug.Log(hit.transform.gameObject.name);
                    move = p.position.x - hit.point.x;
                }
                if (hit.collider != null)
                {
                    Debug.Log(hit.transform.gameObject.name);
                    move = hit.point.x - p.position.x;
                    if (Mathf.Abs(move) <= 0.01) t = 0;
                }
            }
        }


        transform.Translate(Vector2.right * move); //move the object, Vector2.right = (1, 0). If move is negative, the object will go left.
        if (Input.GetKeyDown(jump) && isGrounded())
        {
            //jumping.updateJump();
            rb.AddForce(Vector2.up * jumping.jumpForce, ForceMode2D.Impulse); //create an impulse force on the rigidbody (physics body) upwards
        }
        
    }
    public bool isGrounded()
    {
        bool g = Physics2D.OverlapArea(topLeftCorner.position, bottomRightCorner.position, ground); //checks if there is a collider in the overlap area (below the cube) with the ground layer.
        Debug.Log(g);
        return g;
    }
    
}
