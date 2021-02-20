using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PuppetMaster master;
    public Transform topLeftCorner;         //These two corners are meant to sense if the player is grounded
    public Transform bottomRightCorner;
    public Transform[] rightRays;
    public Transform[] leftRays;
    public LayerMask ground;
    public float t;
    public float horizontal = 0;
    public bool grounded;
    private float velocity;
    private Rigidbody2D rb;
    private Animator anim;
    private bool running = false;
    private bool right = true;
    private SpriteRenderer sprite;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Debug.DrawLine(topLeftCorner.position, bottomRightCorner.position);
        if (horizontal != 0 && grounded) //checks if the absolute value of the key conversion is > 0, meaning a key is pressed (and not cancelled out)
        {
            t += horizontal * Time.deltaTime * (master.speed * master.accelerationSpeed); //updates interpolation, causes acceleration
            if (t > 1) t = 1;
            else if (t < -1) t = -1;   //if t is already at 1 or -1, stop it.
        }
        else if(horizontal == 0 && grounded)
        {
            if (velocity < 0)
            {
                t += master.speed * master.accelerationSpeed * Time.deltaTime;
                if (t > 0) t = 0;
            }
            if (velocity > 0)
            {
                t -= master.speed * master.accelerationSpeed * Time.deltaTime; //start interpolating to 0 acceleration.
                if (t < 0) t = 0;
            }
        }
        else if (horizontal != 0 && !grounded)
        {
            t += horizontal * Time.deltaTime * (master.speed * master.accelerationSpeed) * master.aerialMultiplier; //updates interpolation, causes acceleration
            if (t > 1) t = 1;
            else if (t < -1) t = -1;   //if t is already at 1 or -1, stop it.
        }
        velocity = master.speed * t;
        velocity = (float)((int)(velocity * 100)) / 100; //rounds to nearest 100th.
        float move = velocity * Time.deltaTime;
        if (move > 0)
        {
            foreach (Transform p in rightRays)
            {
                RaycastHit2D hit = Physics2D.Raycast(p.position, p.TransformDirection(Vector3.right), Mathf.Abs(move));
                Debug.DrawRay(p.position, p.TransformDirection(Vector2.right), Color.red, Mathf.Abs(move));
                if (hit.collider != null)
                {
                    move = hit.point.x - p.position.x;
                    if (Mathf.Abs(move) <= 0.01) t = 0;
                }
            }
        }
        else if (move < 0)
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
        if (!running && horizontal != 0)
        {
            running = true;
            anim.SetBool("Running", true);
        }
        else if (running && horizontal == 0)
        {
            running = false;
            anim.SetBool("Running", false);
        }
        if(horizontal > 0 && !right)
        {
            right = true;
            sprite.flipX = false;
        } else if (horizontal < 0 && right)
        {
            right = false;
            sprite.flipX = true;
        }
        transform.Translate(Vector2.right * move); //move the object, Vector2.right = (1, 0). If move is negative, the object will go left.
    }

    public void updateHorizontal(float h)
    {
        horizontal += h;
        anim.SetFloat("h", horizontal);
        
    }
    public void setHorizontal(float h)
    {
        horizontal = h;
    }
    public void Jump(float force)
    {
        if(isGrounded())
        {
            anim.SetBool("Grounded", false);
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse); //create an impulse force on the rigidbody (physics body) upwards
        }
       
    }
    public bool isGrounded()
    {
        grounded = Physics2D.OverlapArea(topLeftCorner.position, bottomRightCorner.position, ground); //checks if there is a collider in the overlap area (below the cube) with the ground layer.
        return grounded;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isGrounded()) anim.SetBool("Grounded", true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded();
    }
}
