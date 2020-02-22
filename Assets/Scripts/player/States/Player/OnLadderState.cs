using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLadderState : OnGroundState
{
    public enum ladderPosition
    {
        TOP_LADDER, BOTTOM_LADDER, MIDDLE_LADDER, NO_STATE
    }

    private float v;

    public OnLadderState(PlayerController playerCharacter, ladderPosition lpos, float initialJump, float cd = 0) : base(playerCharacter)
    {
        this.lpos = lpos;
        this.initialJump = initialJump;
    }
    public override void OnInit()
    {
        Debug.Log("ONLADDER");
    }
    public override void OnExit()
    {
        //ator.SetBool("ladder", false);
    }
    public override void Execute()
    {
        v = Input.GetAxis("Vertical");

        switch (lpos)
        {
            case ladderPosition.TOP_LADDER:
                if (v < 0)
                    rb2D.gravityScale = 0;
                else
                    v = 0;
                break;
            case ladderPosition.BOTTOM_LADDER:
                if (v > 0)
                    rb2D.gravityScale = 0;
                else
                    v = 0;
                break;
            case ladderPosition.MIDDLE_LADDER:
                //ator.SetFloat("speedFactor", v);
                break;
        }

        if (rb2D.gravityScale != 0)
            base.Execute();

        else
            if (dashCoolDown > 0)
                dashCoolDown -= Time.deltaTime;
    }
    public override void FixedExecute()
    {
        if (rb2D.gravityScale != 0)
            base.FixedExecute();
        else
        {
            if (lpos == ladderPosition.BOTTOM_LADDER || lpos == ladderPosition.TOP_LADDER)
            {
                rb2D.position += new Vector2(0, Mathf.Sign(v) * initialJump);
                lpos = ladderPosition.MIDDLE_LADDER;
                //ator.SetBool("ladder", true);
            }
            if (lpos == ladderPosition.MIDDLE_LADDER)
                rb2D.velocity = new Vector2(0, playerCharacter.pModel.ladderSpeed * v);
        }
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
        if (collision.tag == "BottomLadder")
        {
            rb2D.gravityScale = 4;
            //ator.SetBool("ladder", false);
            lpos = ladderPosition.BOTTOM_LADDER;
        }

        if (collision.tag == "TopLadder")
        {
            rb2D.position += new Vector2(0, initialJump);
            rb2D.gravityScale = 4;
            lpos = ladderPosition.TOP_LADDER;
            //ator.SetBool("ladder", false);
        }
    }

    public override void OnTriggerExit(Collider2D collision)
    {
        if (collision.tag == "BottomLadder" && lpos == ladderPosition.BOTTOM_LADDER)
        {
            Debug.Log("NO STATE");
            lpos = ladderPosition.NO_STATE;
        }
        if (collision.tag == "TopLadder" && lpos == ladderPosition.TOP_LADDER)
        {
            Debug.Log("NO STATE");
            lpos = ladderPosition.NO_STATE;
        }
    }

    public override void CheckTransitions()
    {
        if (lpos == ladderPosition.NO_STATE)
            playerCharacter.ChangeState(new OnGroundState(playerCharacter, dashCoolDown));
    }
}

