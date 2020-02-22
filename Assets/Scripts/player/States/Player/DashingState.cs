using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingState : PlayerState
{
    //REFERENCIA AL RIGIDBODY
    protected Rigidbody2D rb2D;
    //REFERENCIA AL ANIMATOR
    protected Animator anim;
    private float h;



    private bool isDashing;
    private float dashSpeed;

    

    public DashingState(PlayerController pc, float h, float cd = 0) : base(pc, cd)
    {
        //DIRECCIÓN HACIA LA QUE SE MUEVE EL PLAYER AL INICIAR EL ESTADO
        this.h = h;
        //INICIALIZACIÓN DEL RIGIDBODY
        rb2D = playerCharacter.GetComponent<Rigidbody2D>();
        //INICIALIZACIÓN DEL ANIMATOR
        anim = playerCharacter.GetComponent<Animator>();
    }

    public override void OnInit()
    {
        base.OnInit();
        playerCharacter.canDash = false;
        isDashing = true;
        playerCharacter.InstantiateDashParticles();
        dashCoolDown = playerCharacter.pModel.dashMaxTime;
        dashSpeed = playerCharacter.pModel.dashSpeed;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void Execute()
    {
        base.Execute();
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
        if (!isDashing)
            playerCharacter.ChangeState(new OnGroundState(playerCharacter, dashCoolDown));
    } 
}

    

