using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BossState
{
    protected Rigidbody2D rb2D;
    protected Animator anim;

    public IdleState(BossController bc) : base(bc)
    {
        //INICIALIZACIÓN DEL RIGIDBODY
        rb2D = bossCharacter.GetComponent<Rigidbody2D>();
        //INICIALIZACIÓN DEL ANIMATOR
        anim = bossCharacter.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

    public override void Execute()
    {
        anim.SetTrigger("DejarInvocar");
    }


    public override void CheckTransitions()
    {
        
    }

}
