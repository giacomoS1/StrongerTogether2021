using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PuppetMaster : MonoBehaviour
{

    public float speed;
    public float aerialMultiplier;
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
    AudioSource switchSound;
    bool rightDown;
    bool leftDown;
    // Update is called once per frame
    private void Start()
    {
        player = players[currentPlayer];
        switchSound = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(right))
        {
            player.updateHorizontal(1);
            rightDown = true;
        }
        if (Input.GetKeyDown(left))
        {
            player.updateHorizontal(-1);
            leftDown = true;
        }
        if (Input.GetKeyUp(right) && rightDown)
        {
            player.updateHorizontal(-1);
            rightDown = false;
        }
        if (Input.GetKeyUp(left) && leftDown)
        {
            player.updateHorizontal(1);
            leftDown = false;
        }

        if (Input.GetKeyDown(switchPlayer))
        {
            switchSound.Play();
            player.setHorizontal(0);
            currentPlayer = (currentPlayer + 1) % 2;
            player = players[currentPlayer];
            rightDown = false;
            leftDown = false;

        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(currentPlayer == 1) //1 is god, so if the current player is god then do a god's jump.
            {
                Debug.Log("God!");
                player.Jump(jumping.godJump);
            } else
            {
                player.Jump(jumping.jumpForce);
            }

        }
        
    }
    
    
}
