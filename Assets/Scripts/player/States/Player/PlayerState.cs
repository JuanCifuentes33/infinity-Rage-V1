using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected static PlayerController playerCharacter;
    //TIEMPO QUE HA DE PASAR ENTRE DASH Y DASH
    protected float dashCoolDown;

    //ESTO LE PASA UN 0 Y EN EL CASO QUE NO SE IGUALE A NINÚN PARÁMETRO LE PONE 0 
    public PlayerState(PlayerController pc, float cd = 0)
    {
        playerCharacter = pc;
        dashCoolDown = cd;
    }

    public virtual void Execute()
    {
        if (dashCoolDown > 0)
            dashCoolDown -= Time.deltaTime;
    }

    public abstract void FixedExecute();
    public abstract void CheckTransitions();

    public virtual void OnInit() { }
    public virtual void OnExit() { }
    public virtual void OnTriggerEnter(Collider2D collision) { }
    public virtual void OnTriggerExit(Collider2D collision) { }
    public virtual void OnCollisionEnter(Collision2D collision) { }
    public virtual void EndAnimation() { }
}
