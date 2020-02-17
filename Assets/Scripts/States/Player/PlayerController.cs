using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //REFERENCIA - MODELO DEL PLAYER DERIVADO DE CHARACTER MODEL
    public PlayerModel pModel;
    public float maxSpeed = 10f;
    //REFERENCIA A PLAYERSTATE (DONDE ESTÁN LAS FUNCIONES DE EJECUCIÓN DEL PLAYER)
    private PlayerState pState;
    
    private void Start()
    {
        //INSTANCIA EL MODELO DEL PLAYER
        pModel = Instantiate(pModel);
        //PONE SU ESTADO INICIAL EN ONGROUND
        ChangeState(new OnGroundState(this));
    }

    private void Update()
    {
        //EJECUTA EL CÓDIGO DEL ESTADO EN EL QUE SE ENCUENTRE EL PLAYER EN ESE MOMENTO
        pState.Execute();
        
    }

    private void FixedUpdate()
    {
        //EJECUTA EL CÓDIGO DEL ESTADO EN EL QUE SE ENCUENTRE EL PLAYER EN ESE MOMENTO
        //ORIENTADO A LOS MÉTODOS QUE TENGAN QUE VER CON MOVER EL RIGIDBODY DEL PLAYER (PARA EVITAR EL EFECTO TUNEL, ETC.)
        pState.FixedExecute();
    }

    private void LateUpdate()
    {
        //DESPUÉS DE CADA FRAME DEL JUEGO, SE EJECUTA EL LATE UPDATE PARA COMPROBAR SI SE HA CAMBIADO DE ESTADO DURANTE ESE FRAME
        pState.CheckTransitions();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //CÓDIGO QUE SE EJECUTA EN EL CASO QUE ALGO INTERACTÚE CON LOS TRIGGERS DEL JUGADOR
        pState.OnTriggerEnter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //CÓDIGO QUE SE EJECUTA EN EL CASO QUE ALGO SALGA DE LOS TRIGGERS DEL JUGADOR
        pState.OnTriggerExit(collision);
    }

    public void ChangeState(PlayerState newState)
    {
        //CODIGO QUE SE EJECUTA PARA CAMBIAR DE ESTADO
        //SI NEWSTATE NO ES NULO
        if (newState != null)
        {
            //COMPRUEBA QUE EL STADO ACTUAL DEL PLAYER TAMPOCO LO SEA
            if (pState != null)
                //SI LO ES, SALE DEL ESTADO ACTUAL
                pState.OnExit();
            //IGUALA EL ESTADO ACTUAL AL NUEVO ESTADO
            pState = newState;
            //INICIA EL ESTADO NUEVO
            pState.OnInit();
        }
    }

    //FUNCIÓN PARA RECIBIR DAÑO
    public void takeDamage(float damage)
    {
        //LLAMA AL GAMEMANAGER PARA RESTAR EL DAÑO QUE SE LE PASA A LA FUNCIÓN
        GameManager.Instance.playerHealth -= damage;

        //SI LA VIDA DEL JUGADOR ES INFERIOR A 0
        if (GameManager.Instance.playerHealth < 0)
        {
            //SE FIJA LA VIDA A 0
            GameManager.Instance.playerHealth = 0;
        }
    }
}
