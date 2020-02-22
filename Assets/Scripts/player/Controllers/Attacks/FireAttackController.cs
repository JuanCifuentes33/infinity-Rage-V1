using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttackController : MonoBehaviour
{
    //REFERENCIA AL MODELO DE ATAQUES A DISTANCIA
    public FireBallModel fbm;

    //REFERENCIA AL RIGIDBODY2D
    private Rigidbody2D rb2D;
    //REFERENCIA AL ANIMATOR
    private Animator anim;
   
    //VELOCIDAD DEL ATAQUE 
    private float speed;
    //DAÑO QUE HACE EL ATAQUE
    private float damage;
   

    // Start is called before the first frame update
    void Start()
    {
        //SE INSTANCIA EL MODELO DE ATAQUE QUE SE LE PASA EN EL INSPECTOR
        fbm = Instantiate(fbm);
        //INSTANCIA LA REFERENCIA AL RIGIDBODY2D CON EL RIGIDBODY DEL OBJETO
        rb2D = GetComponent<Rigidbody2D>();
        //INSTANCIA LA REFERENCIA AL ANIMATOR CON EL ANIMATOR DEL OBJETO
        anim = GetComponent<Animator>();
        //SE INSTANCIA LA VELOCIDAD DEL ATAQUE IGUALÁNDOLA CON LA VELOCIDAD QUE SE PASA DESDE EL MODELO
        speed = fbm.speed;

        //LO DESENGANCHA DE SU GAMEOBJECT PARA QUE NO SE MUEVA CON SU PARENT (EL JUGADOR)
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //MUEVE EL HECHIZO HACIA LA DERECHA EN SU EJE X E Y, YA QUE EL SPRITE DEL FUEGO MIRA HACIA LA DERECHA (ESTO SE HACE ASÍ PARA QUE NO SALGA RARO SI SE DISPARA AL REVÉS)
        rb2D.velocity = new Vector2(transform.right.x, transform.right.y) * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //SI CHOCA CON UN ENEMIGO
        if (other.CompareTag("Enemy"))
            //EL HECHIZO SE DESTRUYE
            Destroy(gameObject);
    }

    private void DestroyOnExitTime()
    {
        Destroy(gameObject);
    }
}
