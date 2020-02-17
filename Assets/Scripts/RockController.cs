using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    public float speed=-300f;
    Rigidbody2D rb2D;
    private float posIni;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            rb2D.velocity = new Vector2(0, speed * Time.deltaTime);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Invoke("subir", 3f);

    }
    private void subir()
    {
        speed *= -1;
        
    }
   
}
