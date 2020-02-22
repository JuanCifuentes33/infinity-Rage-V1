using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerAttack : FlowerState
{

    private Animator ator;
    private Transform flowerTransform;
    RaycastHit2D hitInfo;
    private bool detectPlayer;
    //

    //

    public FlowerAttack(FlowerController fc) : base(fc)
    {
        
        ator = flowerController.GetComponent<Animator>();
        flowerTransform = flowerController.GetComponent<Transform>();
    }
    public override void OnInit()
    {
        base.OnInit();
        ator.SetBool("Idle", false);
        ator.SetBool("Attacking", true);
        detectPlayer = true;
    }
    public override void Execute()
    {
        if (Vector2.Distance(flowerTransform.transform.position, flowerController.player.transform.position) > flowerController.visionRange|| flowerTransform.transform.position.y > flowerController.player.transform.position.y + flowerController.heightRange)
        {
            Debug.Log("fueraa");
            detectPlayer = false;
        }
        if (flowerController.player.transform.position.x < flowerController.transform.position.x)
        {
            if (flowerController.timeEntreShoots <= 0)
            {
                ator.SetBool("Idle", false);
                ator.SetBool("Attacking", true);


                MonoBehaviour.Instantiate(flowerController.ball, flowerController.ballSpawn.position, Quaternion.identity);
                flowerController.timeEntreShoots = flowerController.initialTimeEntreShoots;
            }
            else
            {
                ator.SetBool("Idle", true);
                ator.SetBool("Attacking", false);

                flowerController.timeEntreShoots -= Time.deltaTime;
            }
        }
    }
    public override void FixedExecute()
    {
      
    }
    public override void CheckTransitions()
    {
       if(detectPlayer==false)
            flowerController.ChangeState(new FlowerIdle(flowerController));
        
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ator.SetTrigger("Hit");
            MonoBehaviour.Destroy(flowerController.gameObject, 0.420f);
            
        }
    }
    
}
