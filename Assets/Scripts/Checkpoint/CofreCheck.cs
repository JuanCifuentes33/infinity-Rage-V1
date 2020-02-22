using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CofreCheck : MonoBehaviour
{
    private Animator ator;
    // Start is called before the first frame update
    void Start()
    {
        ator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ator.SetTrigger("open");
            GameManager.Instance.ultimoCheckpoint = transform;
        }

    }
}
