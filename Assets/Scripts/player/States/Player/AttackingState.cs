using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : PlayerState
{
    //REFERENCIA AL RIGIDBODY
    protected Rigidbody2D rb2D;
    //REFERENCIA AL ANIMATOR
    protected Animator anim;
    private bool transition = false;

    private float h;

    public AttackingState(PlayerController pc, float h, float cd = 0) : base(pc, cd)
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
        anim.SetBool("Attacking", true);
        h = Input.GetAxis("Horizontal"); 
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void Execute()
    {
        base.Execute();

        //SI H ES SUPERIOR A 0, EL PERSONAJE ESTÁ MIRANDO A LA DERECHA
        if (h > 0)
            rb2D.transform.rotation = Quaternion.AngleAxis(0, new Vector2(0, 1));
        //SI H ES MENOR QUE 0, EL PERSONAJE SE GIRA HACIA LA IZQUIERDA
        if (h < 0)
            rb2D.transform.rotation = Quaternion.AngleAxis(180, new Vector2(0, 1));
    }

    public override void FixedExecute()
    {
        
    }

    public override void OnTriggerEnter(Collider2D collision)
    {

    }

    public override void OnTriggerExit(Collider2D collision)
    {
        base.OnTriggerExit(collision);
    }

    public override void EndAnimation()
    {
        transition = true;
        anim.SetBool("Attacking", false);
    }

    public override void CheckTransitions()
    {
        if(transition)
            playerCharacter.ChangeState(new OnGroundState(playerCharacter, dashCoolDown));       
    }
}
