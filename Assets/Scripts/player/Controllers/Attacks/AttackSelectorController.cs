using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSelectorController : MonoBehaviour
{
    [HideInInspector] public static int selectedAttack;

    public enum attackSelector
    {
        FIRE,
        BLACKHOLE,
        MELEE
    }

    private attackSelector attS;

    //public GameObject normalPunch;
    public GameObject fireBall;
    public GameObject blackHole;
    public GameObject punch;

    public FireBallModel fbm;
    public BlackHoleModel bhm;
    public NormalPunchModel npm;

    // Start is called before the first frame update
    void Start()
    {
        selectedAttack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            selectedAttack++;
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            selectedAttack--;

        if (selectedAttack > 2)
            selectedAttack = 0;

        if (selectedAttack < 0)
            selectedAttack = 2;

            switch (selectedAttack)
        {
            case 0:
                Debug.Log("MELEE");
                attS = attackSelector.MELEE;
                break;
            case 1:
                Debug.Log("FIRE");
                attS = attackSelector.FIRE;
                break;
            case 2:
                Debug.Log("BLACKHOLE");
                attS = attackSelector.BLACKHOLE;
                break;
            default:
                Debug.Log("MELEE");
                attS = attackSelector.MELEE;
                break;
        }

        if (Input.GetButtonDown("Attack")){
            switch (attS)
            {
                case attackSelector.MELEE:
                    Instantiate(punch, transform);
                    break;
                case attackSelector.FIRE:
                        Instantiate(fireBall, transform);
                    break;
                case attackSelector.BLACKHOLE:
                        Instantiate(blackHole, transform);
                    break;
            }
        }
    }
}
