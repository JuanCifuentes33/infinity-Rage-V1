using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoca : MonoBehaviour
{
    public GameObject[] puntosToMove;

    public float speed;
    private int currentPunto = 0;
    private float movePRadius = 0.5f;

    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
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
        transform.position = Vector3.MoveTowards(transform.position, puntosToMove[currentPunto].transform.position, speed * Time.deltaTime);
    }
}
