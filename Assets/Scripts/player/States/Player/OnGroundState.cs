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
    private PlayerController pC;
    private bool isDashing;
    private bool isAttacking;
    protected float initialGravityScale;

    public OnGroundState(PlayerController pc, float cd = 0) : base(pc, cd)
    {
        //INICIALIZACIÓN DEL RIGIDBODY
        rb2D = playerCharacter.GetComponent<Rigidbody2D>();
        //INICIALIZACIÓN DEL ANIMATOR
        anim = playerCharacter.GetComponent<Animator>();
    }

    public override void OnInit()
    {
        initialGravityScale = rb2D.gravityScale;
        lpos = ladderPosition.NO_STATE;
        isDashing = false;
        base.OnInit();
        //isAttacking = false;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void Execute()
    {
        base.Execute();
        anim.SetBool("OnGround", true);
        float temp_speed = Mathf.Max(Mathf.Abs(h));
        anim.SetFloat("Speed", temp_speed);

        //EL MOVIMIENTO HORIZONTAL ES EL RESULTADO DE H
        h = Input.GetAxis("Horizontal");

        //SI SE PULSA LA TECLA "ESPACIO", SE AÑADE UN IMPULSO VERTICAL AL RIGIDBODY USANDO EL PLAYERCHARACTER (DEL PLAYERSTATE.CS)
        if (Input.GetButtonDown("Jump"))
        {
            rb2D.AddForce(new Vector2(0, 1) * (playerCharacter.pModel.verticalImpulse + rb2D.velocity.y * -1), ForceMode2D.Impulse);
            Debug.Log("Salta");
        }

        if (Input.GetButtonDown("Dash"))
        {
            if (dashCoolDown <= 0)
                isDashing = true;   
        }

        if (Input.GetButtonDown("Attack"))
            isAttacking = true;
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
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
        base.OnTriggerEnter(collision);
        if (collision.gameObject.tag == "BottomLadder")
        {
            initialJump = (collision as CircleCollider2D).radius * 2 * 1f;
            lpos = ladderPosition.BOTTOM_LADDER;
        }

        if (collision.gameObject.tag == "TopLadder")
        {
            initialJump = (collision as CircleCollider2D).radius * 2 * 1f;
            lpos = ladderPosition.TOP_LADDER;
        }
    }

    public override void OnTriggerExit(Collider2D collision)
    {
        
        base.OnTriggerExit(collision);
    }

    public override void OnCollisionEnter(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            rb2D.velocity = Vector3.zero;
            isDashing = false;
        }
    }

    public override void CheckTransitions()
    {
        if (lpos != ladderPosition.NO_STATE)
            playerCharacter.ChangeState(new OnLadderState(playerCharacter, lpos, initialJump, dashCoolDown));
        
        //ARRAY DE RAYCAST
        RaycastHit2D[] rh2D = new RaycastHit2D[2];
        //HACE UN CASTING TIRANDO UNA LÍNEA HACIA ABAJO (VECTOR2 DIRECCIÓN, RAYCASTHIT2D[] RESULTADOS, FLOAT DISTANCIA
        if (rb2D.Cast(new Vector2(0, -1), rh2D, 0.1f) == 0)
        {
            //SI EL RESULTADO DE LA DISTANCIA DEL ARRAY DE RAYCAST QUE APUNTA HACIA ABAJO DA 0
            //CAMBIA AL ESTADO DE SALTAR
            playerCharacter.ChangeState(new JumpingState(playerCharacter, dashCoolDown));
        }

        if (isDashing)
        {
            playerCharacter.ChangeState(new DashingState(playerCharacter, h, dashCoolDown));
        }

        if (isAttacking)
        {
            playerCharacter.ChangeState(new AttackingState(playerCharacter, h, dashCoolDown));
        }     
    }
}
