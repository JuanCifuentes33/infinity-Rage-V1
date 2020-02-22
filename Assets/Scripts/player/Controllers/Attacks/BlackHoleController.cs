using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleController : MonoBehaviour
{
    //REFERENCIA AL MODELO DE ATAQUES A DISTANCIA
    public BlackHoleModel bhm;
    //REFERENCIA AL RIGIDBODY2D
    private Rigidbody2D rb2D;
    //REFERENCIA AL ANIMATOR
    private Animator anim;
    //SE LE VA A PASAR UN POINT EFFECTOR (PARA SIMULAR LA GRAVEDAD) DESDE EL EDITOR
    public GameObject effector;
    ////SE LE VA A PASAR UN SISTEMA DE PARTÍCULAS DESDE EL EDITOR
    public GameObject particles;
    //TIEMPO QUE EL AGUJERO NEGRO VA A ESTAR ABSORVIENDO ANTES DE DESAPARCER 

    public PlayerController pc;

    public float lifeSpan;
    private int timesCanInstantiate;
    private float growValue;
    private float speed;
    public float maxGrow;
    public float moveTime;

    // Start is called before the first frame update
    void Start()
    {
        bhm = Instantiate(bhm);
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        transform.localScale = new Vector3(1, 1, 1);
        timesCanInstantiate = 0;
        growValue = 1.5f;
        maxGrow = 5f;
        speed = bhm.speed;
        moveTime = bhm.moveTime;
        lifeSpan = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        moveTime -= Time.deltaTime;
        Debug.Log(moveTime);
        //SI LA POSICIÓN X DEL RIGIDBODY DEL ATAQUE ES MAYOR O IGUAL QUE LA DISTANCIA A LA QUE TIENE QUE LLEGAR
        if (moveTime < 0  )
        {
            speed = 0;
            //Y LA ESCALA DEL ATAQUE ES INFERIOR AL MÁXIMO ESCALADO INDICADO
            if (transform.localScale.x < maxGrow)
            {
                //ACTIVA LA FUNCIÓN QUE HACE QUE EL AGUJERO NEGRO CREZCA
                Grow();
            }
            else
            {
                if (timesCanInstantiate == 0)
                {
                    Instantiate(effector, transform);
                    Instantiate(particles, transform);
                    timesCanInstantiate++;
                }
            }    
        }
        ScaleInstantiations();
        StartCoroutine("WaitAndDestroy");
    }

    private void FixedUpdate()
    {
        //LO DESENGANCHA DE SU GAMEOBJECT PARA QUE NO SE MUEVA CON SU PARENT (EL JUGADOR)
        transform.parent = null;
        Move();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            Destroy(other.gameObject);
    }

    void Move()
    {
        //MUEVE EL AGUJERO NEGRO HASTA SU DESTINO
        rb2D.velocity = new Vector2(transform.right.x, transform.right.y) * speed;
    }

    //ESCALA EL AGUJERO NEGRO Y LO HACE CRECER
    void Grow()
    {
        transform.localScale = new Vector3(transform.localScale.x + growValue * Time.deltaTime, transform.localScale.y + growValue * Time.deltaTime, transform.localScale.z + growValue * Time.deltaTime);
    }

    //ESCALA LOS OBJETOS QUE SE HAN INSTANCIADO (EFFECTOR Y PARTÍCULAS)
    void ScaleInstantiations()
    {
        Vector3 scale = new Vector3(1, 1, 1);
        effector.transform.localScale = scale;
        particles.transform.localScale = scale;
    }

    //ESPERA EL TIEMPO DE VIDA ESPECIFICADO Y DESTRUYE EL AGUJERO NEGRO
    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
        //Destroy(particles);
        //Destroy(effector);
    }
}
