using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OnLadderState;

public class OnGroundState : PlayerState
{
    //VALOR DEL MOVIMIENTO HORIZONTAL
    public float h;
    //REFERENCIA AL RIGIDBODY
    protected Rigidbody2D rb2D;
    //REFERENCIA AL ANIMATOR
    protected Animator anim;
    protected float initialJump;
    protected ladderPosition lpos;

    public OnGroundState(PlayerController pc) : base(pc)
    {
        //INICIALIZACIÓN DEL RIGIDBODY
        rb2D = playerCharacter.GetComponent<Rigidbody2D>();
        //INICIALIZACIÓN DEL ANIMATOR
        anim = playerCharacter.GetComponent<Animator>();
    }

    public override void OnInit()
    {
        lpos = ladderPosition.NO_STATE;
        

        base.OnInit();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void Execute()
    {
        anim.SetTrigger("onGround");
        float temp_speed = Mathf.Max(Mathf.Abs(h));
        anim.SetFloat("Speed", temp_speed);

        //EL MOVIMIENTO HORIZONTAL ES EL RESULTADO DE H
        h = Input.GetAxis("Horizontal");

        //SI SE PULSA LA TECLA "ESPACIO", SE AÑADE UN IMPULSO VERTICAL AL RIGIDBODY USANDO EL PLAYERCHARACTER (DEL PLAYERSTATE.CS)
        if (Input.GetButtonDown("Jump"))
            rb2D.AddForce(new Vector2(0, 1) * playerCharacter.pModel.verticalImpulse, ForceMode2D.Impulse);
            

        if (Input.GetKeyDown(KeyCode.LeftShift))
            playerCharacter.ChangeState(new DashingState(playerCharacter, h));
    }

    public override void FixedExecute()
    {
        //MOVIMINTO LATERAL DEL JUGADOR AL PULSAR "A" O "D". MULTIPLICA H POR LA VELOCIDAD HORIZONTAL PASADA A TRAVÉS DEL PLAYERCHARACTER (DEL PLAYERSTATE.CS)
        rb2D.velocity = new Vector2(h * playerCharacter.pModel.speedHorizontal, rb2D.velocity.y);

        //SI H ES SUPERIOR A 0, EL PERSONAJE ESTÁ MIRANDO A LA DERECHA
        if (h > 0)
            rb2D.transform.rotation = Quaternion.AngleAxis(0, new Vector2(0, 1));
        //SI H ES MENOR QUE 0, EL PERSONAJE SE GIRA HACIA LA IZQUIERDA
        if (h < 0)
            rb2D.transform.rotation = Quaternion.AngleAxis(180, new Vector2(0, 1));

        //Debug.Log("ONGROUND");
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
        base.OnTriggerEnter(collision);
        if (collision.gameObject.tag == "BottomLadder")
        {
            initialJump = (collision as CircleCollider2D).radius * 2 * 1.05f;
            lpos = ladderPosition.BOTTOM_LADDER;
        }

        if (collision.gameObject.tag == "TopLadder")
        {
            initialJump = (collision as CircleCollider2D).radius * 2 * 1.05f;
            lpos = ladderPosition.TOP_LADDER;
        }
    }

    public override void OnTriggerExit(Collider2D collision)
    {
        
        base.OnTriggerExit(collision);
    }



    public override void CheckTransitions()
    {
        if (lpos != ladderPosition.NO_STATE)
            playerCharacter.ChangeState(new OnLadderState(playerCharacter, lpos, initialJump));
        
        //ARRAY DE RAYCAST
        RaycastHit2D[] rh2D = new RaycastHit2D[2];
        //HACE UN CASTING TIRANDO UNA LÍNEA HACIA ABAJO (VECTOR2 DIRECCIÓN, RAYCASTHIT2D[] RESULTADOS, FLOAT DISTANCIA
        if (rb2D.Cast(new Vector2(0, -1), rh2D, 0.1f) == 0)
        {
            Debug.Log("NO HA CHOCADO");
            //SI EL RESULTADO DE LA DISTANCIA DEL ARRAY DE RAYCAST QUE APUNTA HACIA ABAJO DA 0
            //CAMBIA AL ESTADO DE SALTAR
            playerCharacter.ChangeState(new JumpingState(playerCharacter));
        }
           
    }
}
