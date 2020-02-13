using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InestablePlatform : MonoBehaviour
{
    private Vector3 initialPos;
    private Rigidbody2D rb2D;
    private float timeGravity = 4f;

    public float vibrarDuration = 1f;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
