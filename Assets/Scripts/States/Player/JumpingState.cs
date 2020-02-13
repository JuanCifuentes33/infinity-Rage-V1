using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : PlayerState
{
    //VALOR DEL MOVIMIENTO HORIZONTAL
    private float h;

    private Rigidbody2D rb2D;
    private Animator anim;
    

    public JumpingState(PlayerController pc) : base(pc)
    {
        //INICIALIZACIÓN DEL RIGIDBODY
        rb2D = playerCharacter.GetComponent<Rigidbody2D>();
        //INICIALIZACIÓN DEL ANIMATOR
        anim = playerCharacter.GetComponent<Animator>();
    }

    public override void OnInit()
    {
        base.OnInit();
    }

    public override void Execute()
    {
        if(rb2D.velocity.y > 0.01f)
            anim.SetTrigger("onAir");

        if (rb2D.velocity.y < -0.1f)
            anim.SetTrigger("falling");

        //EL MOVIMIENTO HORIZONTAL ES EL RESULTADO DE H
        h = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftShift))
            playerCharacter.ChangeState(new DashingState(playerCharacter, h));

    }

    public override void FixedExecute()
    {
        //CUANDO ESTÁ EN EL AIRE, MOVIMINTO LATERAL DEL JUGADOR AL PULSAR "A" O "D". MULTIPLICA H POR LA VELOCIDAD HORIZONTAL PASADA A TRAVÉS DEL PLAYERCHARACTER (DEL PLAYERSTATE.CS)
        rb2D.velocity = new Vector2(h * playerCharacter.pModel.onAirHorizontalSpeed * playerCharacter.pModel.onAirHorizontalSpeed, rb2D.velocity.y);

        //Debug.Log("ONAIR");
    }
    public void OnCollisionENter2D(Collider2D other)
    {
        if (other.CompareTag("WAll"))
        {
            h = 0;
        }
            
    }

    public override void CheckTransitions()
    {
        //ARRAY DE RAYCAST
        RaycastHit2D[] hitResults = new RaycastHit2D[2];
        //HACE UN CASTING TIRANDO UNA LÍNEA HACIA ABAJO (VECTOR2 DIRECCIÓN, RAYCASTHIT2D[] RESULTADOS, FLOAT DISTANCIA
        if (rb2D.Cast(new Vector2(0, -1), hitResults, 0.1f) > 0)
        {
            Debug.Log("HA CHOCADO");
            //SI EL RESULTADO DE LA DISTANCIA DEL ARRAY DE RAYCAST QUE APUNTA HACIA ABAJO ES MAYOR QUE 0
            //CAMBIA AL ESTADO DE SUELO
            playerCharacter.ChangeState(new OnGroundState(playerCharacter));
        }
          
    }
}
