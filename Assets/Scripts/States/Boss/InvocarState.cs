using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvocarState : BossState
{
    Rigidbody2D rb2D;
    Animator anim;
    

    public InvocarState(BossController bc) : base(bc)
    {
        //INICIALIZACIÓN DEL RIGIDBODY
        rb2D = bossCharacter.GetComponent<Rigidbody2D>();
        //INICIALIZACIÓN DEL ANIMATOR
        anim = bossCharacter.GetComponent<Animator>();
    }
    public override void Execute()
    {
        anim.SetTrigger("Invocar");


    }
    public override void OnInit()
    {
        base.OnInit();
    }
    public override void OnExit()
    {
        base.OnExit();
    }
    public override void FixedExecute()
    {
        
    }
    public override void CheckTransitions()
    {
        
    }
    public override void OnTriggerEnter(Collider2D collision)
    {
        Debug.Log("FJANF");
    }



}
