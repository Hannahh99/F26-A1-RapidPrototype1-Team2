using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameLogic : MonoBehaviour
{

    public GameObject blockPrefab;
    //update lives
    //put player at spawn

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Spawn blockPrefab
    //Destroy spike
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //if the player collides with the spike, instantiate a block, destroy the spike, run updateLife
            //From the RandomObjectSpawnerScript using spawner.(nameOfFunction)
            Instantiate(blockPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
