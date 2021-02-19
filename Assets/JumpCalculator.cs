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
    public RectTransform jumpBar;
    public RectTransform jumpForceUILeft;
    public RectTransform jumpForceUIRight;
    public RectTransform jumpForceDiamondUILeft;
    public RectTransform jumpForceDiamondUIRight;
    public float max;
    public float min = 1.218238f;
    private float jumpUIYPos;
    private float jumpbarXPos;
    private float minXDiamonds;
    private void Update()
    {
        updateJump();   //update how powerful the jump is based on distance.
        jumpUIYPos = jumpForceDiamondUILeft.position.y;
        jumpbarXPos = jumpBar.position.x;
        minXDiamonds = jumpForceDiamondUIRight.position.x - jumpbarXPos;
    }
    public void updateJump()
    {
        jumpForce = Mathf.Max(0, maxJump - distance() / divider);
        float size = min +((max / maxJump) * jumpForce);
        jumpForceUILeft.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size); //update the meter on the ui
        jumpForceUIRight.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size); //update the meter on the ui

    }
    float distance()
    {
        return Mathf.Pow(player1.position.x - player2.position.x, 2) + Mathf.Pow(player1.position.y - player2.position.y, 2);
    }
}
