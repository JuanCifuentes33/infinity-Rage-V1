using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashParticlesScrip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //LO DESENGANCHA DE SU GAMEOBJECT PARA QUE NO SE MUEVA CON SU PARENT (EL JUGADOR)
        transform.parent = null;
        //LO BORRA AL ACABAR EL EFECTO
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
    }
}
