using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerIdle : FlowerState
{
    private Rigidbody2D rb2D;
    private Animator ator;
    private Transform flowerTransform;
    private bool detectPlayer;
    RaycastHit2D hitInfo;
    public FlowerIdle(FlowerController fc) : base(fc)
    {
        rb2D = flowerController.GetComponent<Rigidbody2D>();
        ator = flowerController.GetComponent<Animator>();
        flowerTransform = flowerController.GetComponent<Transform>();
        
    }
    public override void OnInit()
    {
        base.OnInit();
        ator.SetBool("Idle", true);
        ator.SetBool("Attacking", false);
        detectPlayer = false;
        base.OnInit();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
    public override void Execute()
    {

        //hitInfo = Physics2D.Raycast(flowerTransform.transform.position, flowerTransform.transform.position - flowerTransform.transform.right, flowerController.visionRange);
        //if (hitInfo.collider != null)
        //{
        //    detectPlayer = true;
        //    Debug.Log("si choco");

        //    Debug.DrawLine(flowerTransform.transform.position, hitInfo.point, Color.red);


        //}
        //else 
        //{
        //    Debug.Log("no choco");

        //    Debug.DrawLine(flowerTransform.transform.position, flowerTransform.transform.position - flowerTransform.transform.right * flowerController.visionRange, Color.green);

        //}

        float target_dist = (flowerTransform.transform.position - flowerController.player.transform.position).sqrMagnitude;
        if (Vector2.Distance(flowerTransform.transform.position, flowerController.player.transform.position) < flowerController.visionRange && flowerTransform.transform.position.y <= flowerController.player.transform.position.y + flowerController.heightRange)
        {
            Debug.Log("dentroo");
            detectPlayer = true;
        }
        


    }
    public override void FixedExecute()
    {
       
    }
    
    public override void CheckTransitions()
    {
        if (detectPlayer==true)
        {
            flowerController.ChangeState(new FlowerAttack(flowerController));
        }
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ator.SetTrigger("Hit");
        }
    }
    public override void OnDrawGizmosSelected()
    {
        //if (hitInfo.collider != null)
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawLine(flowerTransform.transform.position, hitInfo.point);

        //}
        //else
        //{

        //    Gizmos.color = Color.green;
        //    Gizmos.DrawLine(flowerTransform.transform.position, flowerTransform.transform.position - flowerTransform.transform.right * flowerController.visionRange);


        //}

        Gizmos.color = Color.green;
        if (flowerController.player.transform.localRotation.y > 0)
        {
            Gizmos.DrawLine(flowerController.player.transform.position, new Vector2(flowerController.player.transform.position.x + flowerController.visionRange, flowerController.player.transform.position.y));

        }
        else
        {
            Gizmos.DrawLine(flowerController.player.transform.position, new Vector2(flowerController.player.transform.position.x - flowerController.visionRange, flowerController.player.transform.position.y));

        }


        Gizmos.color = Color.green;
        Gizmos.DrawLine(flowerController.player.transform.position, new Vector2(flowerController.player.transform.position.x, flowerController.player.transform.position.y + flowerController.heightRange));

    }
}
