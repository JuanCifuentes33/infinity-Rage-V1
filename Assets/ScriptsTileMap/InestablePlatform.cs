using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InestablePlatform : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private float timeGravity = 4f;

    private Transform initPos;
    // Start is called before the first frame update
    void Start()
    {
        initPos = GetComponent<Transform>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckReinicio();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("GravityOn", timeGravity);

    }

    private void GravityOn()
    {
        
        rb2D.gravityScale = 1;

    }

    private void GravityOff()
    {

        rb2D.gravityScale = 0;

    }
    private void CheckReinicio()
    {
        if(GameManager.Instance.firstCollision== true)
        {
            rb2D.gravityScale = 0;
          //  rb2D.transform.position = initPos.position;
            rb2D.MovePosition(new Vector2(initPos.position.x, initPos.position.y));
            print("me muevo");
        }
    }
}
