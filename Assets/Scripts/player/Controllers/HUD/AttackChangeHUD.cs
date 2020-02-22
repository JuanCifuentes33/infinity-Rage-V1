using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackChangeHUD : MonoBehaviour
{
    public Image[] panel = new Image[3];
    //private Color initialColor;
    //private Color selectedColor;


    // Start is called before the first frame update
    void Start()
    {
        //asc = GetComponent<AttackSelectorController>();

         
        //initialColor = new Color(255, 255, 255, 100);
        //selectedColor = new Color(255, 0, 0, 100);
    }
   

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < panel.Length; i++)
        {
            if (AttackSelectorController.selectedAttack == i)
                panel[i].color = new Color(255f, 0f, 0f, 100f);
            else
                panel[i].color = new Color(255f, 255f, 255f, 100f);
        }
    }
}
