using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FPSCounter : MonoBehaviour
{

    Text text;
    int secondFrames;
    int frames;
    int lastSecond = 0;
    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {

        if (Time.realtimeSinceStartup >= lastSecond + 1)
        {
            secondFrames = frames;
            frames = 0;
            lastSecond = (int)Mathf.Round(Time.realtimeSinceStartup);
        }
        else frames++;
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        text.text = "Guessed FPS:\t" + Mathf.Round(1f / Time.deltaTime) + "\n Actual FPS:\t" + secondFrames + "\nRestart: [r]";
    }
}
