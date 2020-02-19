using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState
{
    protected static BossController bossCharacter;
    // Start is called before the first frame update
    public BossState(BossController bc)
    {
        bossCharacter = bc;
    }

    public abstract void Execute();
    public abstract void FixedExecute();
    public abstract void CheckTransitions();

    public virtual void OnInit() { }
    public virtual void OnExit() { }
    public virtual void OnTriggerEnter(Collider2D collision) { }
    public virtual void OnTriggerExit(Collider2D collision) { }
}
