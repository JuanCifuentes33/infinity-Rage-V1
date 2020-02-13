using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingState : PlayerState
{
    //REFERENCIA AL RIGIDBODY
    protected Rigidbody2D rb2D;
    //REFERENCIA AL ANIMATOR
    protected Animator anim;
    private bool isDashing;
    private float h;



    public DashingState(PlayerController pc, float h) : base(pc)
    {
        this.h = h;
        //INICIALIZACIÓN DEL RIGIDBODY
        rb2D = playerCharacter.GetComponent<Rigidbody2D>();
        //INICIALIZACIÓN DEL ANIMATOR
        anim = playerCharacter.GetComponent<Animator>();

    }

    public override void OnInit()
    {
        isDashing = true;
        base.OnInit();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void Execute()
    {
        
    }

    public override void FixedExecute()
    {
        
        rb2D.velocity = new Vector2(h * playerCharacter.pModel.dashSpeed, rb2D.velocity.y);
       

        isDashing = false;
    }

    public override void OnTriggerEnter(Collider2D collision)
    {

    }

    public override void OnTriggerExit(Collider2D collision)
    {
        base.OnTriggerExit(collision);
    }

    public override void CheckTransitions()
    {
        if (!isDashing)
        {

            //ARRAY DE RAYCAST
            RaycastHit2D[] rh2D = new RaycastHit2D[2];
            //HACE UN CASTING TIRANDO UNA LÍNEA HACIA ABAJO (VECTOR2 DIRECCIÓN, RAYCASTHIT2D[] RESULTADOS, FLOAT DISTANCIA
            if (rb2D.Cast(new Vector2(0, -1), rh2D, 0.1f) == 0)
                //SI EL RESULTADO DE LA DISTANCIA DEL ARRAY DE RAYCAST QUE APUNTA HACIA ABAJO DA 0
                //CAMBIA AL ESTADO DE SALTAR
                playerCharacter.ChangeState(new JumpingState(playerCharacter));

            if (rb2D.Cast(new Vector2(0, -1), rh2D, 0.1f) > 0)
                //SI EL RESULTADO DE LA DISTANCIA DEL ARRAY DE RAYCAST QUE APUNTA HACIA ABAJO ES MAYOR QUE 0
                //CAMBIA AL ESTADO DE SUELO
                playerCharacter.ChangeState(new OnGroundState(playerCharacter));
        } 
    }
}

    

