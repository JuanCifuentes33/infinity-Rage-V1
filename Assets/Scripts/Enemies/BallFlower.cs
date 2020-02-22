using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFlower : MonoBehaviour
{
    public float speed = 2f;
    private Transform player;
    private Vector3 target;
    public float damage = 10f;
    Vector3 normalizedToPlayer;
    private Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector3(player.position.x, player.position.y, player.position.z);
        rb2D = GetComponent<Rigidbody2D>();
        normalizedToPlayer = (target - transform.position);
        normalizedToPlayer.Normalize();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(speed * normalizedToPlayer.x, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.playerHealth -= damage;
            Destroy(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }


    }
  
}
