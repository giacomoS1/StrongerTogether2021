using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCalculator : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float maxJump;
    public float jumpForce;
    public float divider;
    public RectTransform jumpForceUI;
    private void Update()
    {
        updateJump();   //update how powerful the jump is based on distance.
        
    }
    public void updateJump()
    {
        jumpForce = Mathf.Max(0, maxJump - distance() / divider);
        jumpForceUI.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (300 / maxJump) * jumpForce); //update the meter on the ui
    }
    float distance()
    {
        return Mathf.Pow(player1.position.x - player2.position.x, 2) + Mathf.Pow(player1.position.y - player2.position.y, 2);
    }
}
