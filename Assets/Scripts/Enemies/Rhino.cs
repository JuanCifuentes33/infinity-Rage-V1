using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Transform player;
    [Range(3,20)]public float range = 1f;
    [Range(1, 3)] public float heightRange = 2f;
    [Range(1, 4)] public float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
         player= GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CompruebaFlipeo();
    }
    private void FixedUpdate()
    {
        float target_dist = (transform.position - player.position).sqrMagnitude;
        if (Vector2.Distance(transform.position, player.position)<range && transform.position.y <= player.transform.position.y + heightRange)
        {
            Debug.Log("dentroo");
            rb2D.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        }

    }

    private void CompruebaFlipeo()
    {
        if ( transform.position.x - player.position.x  < 0)
        {
            rb2D.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            rb2D.transform.rotation = new Quaternion(0, 0, 0, 0); ;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if (transform.localRotation.y > 0)
        {
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + range, transform.position.y));

        }
        else
        {
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x - range, transform.position.y));

        }


        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + heightRange));
    }
}
