using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchController : MonoBehaviour
{
    public NormalPunchModel npm;
    private float damage;
    private float lifeSpan;

    // Start is called before the first frame update
    void Start()
    {
        npm = Instantiate(npm);
        damage = npm.damage;
        lifeSpan = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifeSpan);
    }
}
