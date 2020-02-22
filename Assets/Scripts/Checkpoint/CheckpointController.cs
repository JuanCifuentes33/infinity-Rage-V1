using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ( collision.CompareTag("Player"))
        {
            player.transform.position = GameManager.Instance.ultimoCheckpoint.position;
        }
    }
}
