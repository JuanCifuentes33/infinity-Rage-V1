using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchoController : MonoBehaviour
{
    float damage = 20;
    public PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            pc.takeDamage(damage);
    }
}
