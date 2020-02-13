using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class probadorr : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float speed = 2f;
    private float h, v;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

    }
    private void FixedUpdate()
    {
        rb2D.velocity =new Vector2 (h * speed, rb2D.velocity.y);
    }
}
