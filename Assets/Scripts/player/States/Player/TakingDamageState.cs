using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingDamageState : PlayerState
{
    //REFERENCIA AL RIGIDBODY
    protected Rigidbody2D rb2D;
    //REFERENCIA AL ANIMATOR
    protected Animator anim;
    private bool transition = false;
    private SpriteRenderer spriteRdr;
    private Color playerColor;
    private SpriteRenderer playerRdr;
    

    private float h;

    public TakingDamageState(PlayerController pc, float cd = 0) : base(pc, cd)
    {
        //INICIALIZACIÓN DEL RIGIDBODY
        rb2D = playerCharacter.GetComponent<Rigidbody2D>();
        //INICIALIZACIÓN DEL ANIMATOR
        anim = playerCharacter.GetComponent<Animator>();
    }

    public override void OnInit()
    {
        base.OnInit();
        anim.SetBool("Hurt", true);
        Debug.Log(GameManager.Instance.playerHealth);
        //playerColor = playerCharacter.spriteRdr.color;
        playerRdr = PlayerController.FindObjectOfType<SpriteRenderer>();
        playerColor = playerRdr.color;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void Execute()
    {
        base.Execute();
        h = Input.GetAxis("Horizontal");
        rb2D.velocity = new Vector2(h * playerCharacter.pModel.speedHorizontal, rb2D.velocity.y);

        //SI H ES SUPERIOR A 0, EL PERSONAJE ESTÁ MIRANDO A LA DERECHA
        if (h > 0)
            rb2D.transform.rotation = Quaternion.AngleAxis(0, new Vector2(0, 1));
        //SI H ES MENOR QUE 0, EL PERSONAJE SE GIRA HACIA LA IZQUIERDA
        if (h < 0)
            rb2D.transform.rotation = Quaternion.AngleAxis(180, new Vector2(0, 1));

        if (Time.deltaTime % 2 == 0)
        {
            playerColor.a = 0.5f;
        }
        else
            playerColor.a = 1f;

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
        Debug.Log("ENTRA FUNCIÓN");
        transition = true;
        anim.SetBool("Hurt", false);
    }

    public override void CheckTransitions()
    {
        if (transition)
            playerCharacter.ChangeState(new OnGroundState(playerCharacter, dashCoolDown));    
    }
}
