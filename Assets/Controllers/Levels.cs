using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{

    public void Easy()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
        SnakeController.moveSpeed = 4f; 
    }

    public void Medium()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
        SnakeController.moveSpeed = 5.5f; 
    }

    public void Hard()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
        SnakeController.moveSpeed = 6.5f; 
    }
}
