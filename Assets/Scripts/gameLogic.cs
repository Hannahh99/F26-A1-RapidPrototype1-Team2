using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameLogic : MonoBehaviour
{

   //make game object variable to tie block to in unity
    public GameObject blockPrefab;
    //update lives if time
    
    //when player collides with spikes
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // store current pos for blockPrefab
        Vector3 currentPosition = transform.position;
        Vector3 blockAdjust = new Vector3(-.2f, .1f, 0);

        if (collision.gameObject.CompareTag("Player"))
        {
                //if the player collides with the spike, instantiate a block, destroy the spike, run updateLife

                //Spawn blockPrefab
                Instantiate(blockPrefab, currentPosition, Quaternion.identity);
                Instantiate(blockPrefab, currentPosition + blockAdjust , Quaternion.identity);
                //Destroy spike
                Destroy(gameObject);
        }
    }
}
