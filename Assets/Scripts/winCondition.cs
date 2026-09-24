using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winCondition : MonoBehaviour
{
    //call scene manager
    Scene scene;
    //assign a string variable to current scene
    string sceneName;

    //when player collides with win coin load win screen
    private void OnTriggerEnter2D(Collider2D collision)
    {
        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;

        //if player collides with coin, player wins
        if (collision.gameObject.CompareTag("Player") && sceneName == "SampleScene")
        {
            //load win screen
            SceneManager.LoadScene("CodeTesting");
        }
        else if (collision.gameObject.CompareTag("Player") && sceneName == "CodeTesting")
        {
            //load win screen
            SceneManager.LoadScene("WinScreen");
        }
    }
}
