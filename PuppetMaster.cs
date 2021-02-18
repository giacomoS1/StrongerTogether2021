using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuppetMaster : MonoBehaviour
{
    public float speed;
    [Range(0.0f, 1.0f)]
    public float accelerationSpeed;         //in seconds to reach max speed
    public JumpCalculator jumping;
    [Header("Settings")]
    public KeyCode switchPlayer;
    public KeyCode right;
    public KeyCode left;
    public PlayerController[] players;
    public int currentPlayer;
    public PlayerController player;
    // Update is called once per frame
    private void Start()
    {
        player = players[currentPlayer];
    }
    void Update()
    {
        if (Input.GetKeyDown(switchPlayer))
        {
            player.setHorizontal(0);
            currentPlayer = (currentPlayer + 1) % 2;
            player = players[currentPlayer];
        }

        if (Input.GetKeyDown(right)) player.updateHorizontal(1);
        if (Input.GetKeyDown(left)) player.updateHorizontal(-1);
        if (Input.GetKeyUp(right)) player.updateHorizontal(-1);
        if (Input.GetKeyUp(left)) player.updateHorizontal(1);

        if (Input.GetKeyDown(KeyCode.Space)) player.Jump(jumping.jumpForce);
        
    }
    
    
}
