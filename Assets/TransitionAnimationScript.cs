using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionAnimationScript : MonoBehaviour
{
    public Animator panel;
    public AudioSource entrance;
    public AudioSource exit;
    public void fadeOut()
    {
        panel.ResetTrigger("Fade Out");
        panel.SetTrigger("Fade Out");
    }
    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void playSound()
    {
        entrance.Play();
    }
    public void playExitSound()
    {
        exit.Play();
    }
}
