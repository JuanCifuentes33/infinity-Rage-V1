using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    public Transform toTeleportPoint;
    private Transform player;
    public GameObject backgroundtoHide;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.CompareTag("Player"))
        {
            player.position = toTeleportPoint.position;
            backgroundtoHide.SetActive(false);
            Invoke("BossTrue", 1f);

        }
    }
    //poner a true la variable del boss, esto significa que ya esta en la pantalla del boss
    void BossTrue()
    {
        GameManager.Instance.isWithBoss = true;
    }
}
