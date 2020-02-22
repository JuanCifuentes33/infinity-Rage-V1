using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlowerState
    
{
    protected static FlowerController flowerController;

    public FlowerState(FlowerController fc)
    {
        flowerController = fc;
    }

    public abstract void Execute();
    public abstract void FixedExecute();
    public abstract void CheckTransitions();

    public virtual void OnInit() { }
    public virtual void OnExit() { }
    public virtual void OnTriggerEnter2D(Collider2D collision) { }
    public virtual void OnTriggerExit2D(Collider2D collision) { }
    public virtual void OnDrawGizmosSelected() { }

}
