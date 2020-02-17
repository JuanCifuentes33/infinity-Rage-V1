using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{

    [SerializeField]
    public GameObject[] puntosToMove;

    
    public float speed;

    private int currentPunto = 0;
    private float movePRadius = 0.5f;


    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, puntosToMove[currentPunto].transform.position) < movePRadius)
        {

            currentPunto++;

            if (currentPunto >= puntosToMove.Length)
            {
                currentPunto = 0;
            }
        }
    }
    private void FixedUpdate()
    {
        rb2D.transform.position = Vector3.MoveTowards(transform.position, puntosToMove[currentPunto].transform.position, speed * Time.deltaTime);
    }
}
