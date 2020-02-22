using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : PlayerState
{
    //VALOR DEL MOVIMIENTO HORIZONTAL
    private float h;

    private Rigidbody2D rb2D;
    private Animator anim;
    private PlayerController pc;
    private bool isDashing;
    private bool isAttacking;


    public JumpingState(PlayerController pc, float cd = 0) : base(pc, cd)
    {
        //INICIALIZACIÓN DEL RIGIDBODY
        rb2D = playerCharacter.GetComponent<Rigidbody2D>();
        //INICIALIZACIÓN DEL ANIMATOR
        anim = playerCharacter.GetComponent<Animator>();
    }

    public override void OnInit()
    {
        isDashing = false;
        base.OnInit();
    }

    public override void Execute()
    {
        base.Execute();

        if (rb2D.velocity.y > 0.01f)
            anim.SetBool("OnGround", false);

        h = Input.GetAxis("Horizontal");

        //SI H ES SUPERIOR A 0, EL PERSONAJE ESTÁ MIRANDO A LA DERECHA
        if (h > 0)
            rb2D.transform.rotation = Quaternion.AngleAxis(0, new Vector2(0, 1));
        //SI H ES MENOR QUE 0, EL PERSONAJE SE GIRA HACIA LA IZQUIERDA
        if (h < 0)
            rb2D.transform.rotation = Quaternion.AngleAxis(180, new Vector2(0, 1));

        //EL MOVIMIENTO HORIZONTAL ES EL RESULTADO DE H
        

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
        //CUANDO ESTÁ EN EL AIRE, MOVIMINTO LATERAL DEL JUGADOR AL PULSAR "A" O "D". MULTIPLICA H POR LA VELOCIDAD HORIZONTAL PASADA A TRAVÉS DEL PLAYERCHARACTER (DEL PLAYERSTATE.CS)
        rb2D.velocity = new Vector2(h * playerCharacter.pModel.onAirHorizontalSpeed * playerCharacter.pModel.onAirHorizontalSpeed, rb2D.velocity.y);

        //Debug.Log("ONAIR");
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
        //ARRAY DE RAYCAST
        RaycastHit2D[] hitResults = new RaycastHit2D[2];
        //HACE UN CASTING TIRANDO UNA LÍNEA HACIA ABAJO (VECTOR2 DIRECCIÓN, RAYCASTHIT2D[] RESULTADOS, FLOAT DISTANCIA
        if (rb2D.Cast(new Vector2(0, -1), hitResults, 0.1f) > 0)
        {
            //SI EL RESULTADO DE LA DISTANCIA DEL ARRAY DE RAYCAST QUE APUNTA HACIA ABAJO ES MAYOR QUE 0
            //CAMBIA AL ESTADO DE SUELO
            playerCharacter.ChangeState(new OnGroundState(playerCharacter, dashCoolDown));
        }

        if (isDashing)
        {
            playerCharacter.ChangeState(new DashingState(playerCharacter, h));
        }

        if (isAttacking)
        {
            playerCharacter.ChangeState(new AttackingState(playerCharacter, h, dashCoolDown));
        }

    }
}
