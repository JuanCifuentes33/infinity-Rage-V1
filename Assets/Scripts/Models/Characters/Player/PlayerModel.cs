using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerModel", menuName = "Characters/Player Model")]
public class PlayerModel : CharacterModel
{
    //
    public float verticalImpulse = 1f;
    public float dashSpeed;
    public float ladderSpeed = 0.2f;
    public float onAirHorizontalSpeed = 0.3f;
}
