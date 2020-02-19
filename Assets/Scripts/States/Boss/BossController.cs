using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public BossModel bModel;
    private BossState bState;
    public GameObject Roca;
    
    // Start is called before the first frame update
    void Start()
    {
        bModel = Instantiate(bModel);
        ChangeState(new IdleState(this));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        bState.CheckTransitions();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bState.OnTriggerEnter(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        bState.OnTriggerExit(collision);
    }
    public void ChangeState(BossState newState)
    {
        if (newState != null)
        {
            
            if (bState != null)
                
                bState.OnExit();
            
            bState = newState;
            
            bState.OnInit();
        }
    }
    public void Invocar()
    {
        Invoke("Roca", 3f);
    }
}
