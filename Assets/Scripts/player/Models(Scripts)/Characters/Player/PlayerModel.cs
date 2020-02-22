using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerModel", menuName = "Characters/Player Model")]
public class PlayerModel : CharacterModel
{
    //IMPULSO VERTICAL PARA EL SALTO
    public float verticalImpulse = 1f;
    //VELOCIDAD DEL DASH
    public float dashSpeed;
    //VELOCIDAD AL SUBIR ESCALERAS
    public float ladderSpeed = 0.2f;
    //VELOCIDAD DE MOVIMIENTO HORIZONTAL EN EL AIRE
    public float onAirHorizontalSpeed = 0.3f;
    //TIEMPO QUE SE VA A GUARDAR PARA REINICIALIZAR EL CONTADOR
    public float dashMaxTime;
    //PUNTOS DE MANA DEL JUGADOR
    public float mana;
}
