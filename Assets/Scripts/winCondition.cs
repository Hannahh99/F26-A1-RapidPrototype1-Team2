using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winCondition : MonoBehaviour
{
    //when player collides with win coin load win screen
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if player collides with coin, player wins

        if (collision.gameObject.CompareTag("Player"))
        {
            //load win screen
            SceneManager.LoadScene("WinScreen");
        }
    }
}
