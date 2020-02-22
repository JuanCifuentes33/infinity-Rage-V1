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
        //pc = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            pc.takeDamage(damage);
    }
}
