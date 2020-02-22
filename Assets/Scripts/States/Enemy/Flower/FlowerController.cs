using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : MonoBehaviour
{
    //REFERENCIA - MODELO DEL ENEMIGO DERIVADO DE ENEMYMODEL
    public EnemyModel fModel;
    public float maxSpeed = 10f;
    public float visionRange = 10f;
    [HideInInspector]public Transform player;
    [Range(1, 3)] public float heightRange = 1f;
    public GameObject ball;
    public Transform ballSpawn;
    public float initialTimeEntreShoots=2;
    [HideInInspector] public float timeEntreShoots;
    


    private FlowerState fState;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //INSTANCIA EL MODELO DEL PLAYER
        fModel = Instantiate(fModel);
        //PONE SU ESTADO INICIAL EN ONGROUND
        ChangeState(new FlowerIdle(this));
    }

    private void Update()
    {
        //EJECUTA EL CÓDIGO DEL ESTADO EN EL QUE SE ENCUENTRE EL PLAYER EN ESE MOMENTO
        fState.Execute();

    }

    private void FixedUpdate()
    {
        //EJECUTA EL CÓDIGO DEL ESTADO EN EL QUE SE ENCUENTRE EL PLAYER EN ESE MOMENTO
        //ORIENTADO A LOS MÉTODOS QUE TENGAN QUE VER CON MOVER EL RIGIDBODY DEL PLAYER (PARA EVITAR EL EFECTO TUNEL, ETC.)
        fState.FixedExecute();
    }

    private void LateUpdate()
    {
        //DESPUÉS DE CADA FRAME DEL JUEGO, SE EJECUTA EL LATE UPDATE PARA COMPROBAR SI SE HA CAMBIADO DE ESTADO DURANTE ESE FRAME
        fState.CheckTransitions();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //CÓDIGO QUE SE EJECUTA EN EL CASO QUE ALGO INTERACTÚE CON LOS TRIGGERS DEL JUGADOR
        fState.OnTriggerEnter2D(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //CÓDIGO QUE SE EJECUTA EN EL CASO QUE ALGO SALGA DE LOS TRIGGERS DEL JUGADOR
        fState.OnTriggerExit2D(collision);
    }

    public void ChangeState(FlowerState newState)
    {
        //CODIGO QUE SE EJECUTA PARA CAMBIAR DE ESTADO
        //SI NEWSTATE NO ES NULO
        if (newState != null)
        {
            //COMPRUEBA QUE EL STADO ACTUAL DEL PLAYER TAMPOCO LO SEA
            if (fState != null)
                //SI LO ES, SALE DEL ESTADO ACTUAL
                fState.OnExit();
            //IGUALA EL ESTADO ACTUAL AL NUEVO ESTADO
            fState = newState;
            //INICIA EL ESTADO NUEVO
            fState.OnInit();
        }
    }

    public void OnDrawGizmosSelected()
    {
       
    }
   
}
