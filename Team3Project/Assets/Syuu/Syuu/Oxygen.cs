using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oxygen : MonoBehaviour
{
    // Start is called before the first frame update
    //HPUI
    
    //-----
    public float OxygenMAX = 100;//Oxygen总量
    public float PlayerOxygen = 100;//Oxygen残量
    //-----
    public GameObject Player;
    
    public Image OxygenImege;
    public float  OxygenDown;//Oxygen残量計算用変数

    //Image
    public float OxygenDownSpeed;
    public float OxygenPercentage;//
    public float ImagePercentage;//OxygenPercentage
                                 //--HPUIここまで


    // Start is called before the first frame update
    void Start()
    {
        OxygenMAX = Player.GetComponent<Player>().Oxygen;
        PlayerOxygen = Player.GetComponent<Player>().Oxygen;
    }
    void Update()
    {
        OxygenUIBOX();
        PlayerOxygen = Player.GetComponent<Player>().Oxygen;
    }


    void OxygenUIBOX()
    {
        //残りOxygen
        OxygenDown = 100;//Oxygen残量計算用変数
        OxygenDownSpeed = (OxygenDown - PlayerOxygen) * 0.25f;
        OxygenDown = PlayerOxygen;

       

        OxygenPercentage = PlayerOxygen / OxygenMAX;//HPPercentage

        ImagePercentage = OxygenPercentage;

       // OxygenImege.fillAmount = OxygenPercentage;
        if (OxygenImege.fillAmount > OxygenPercentage)
        {
            OxygenImege.fillAmount -= Time.deltaTime * OxygenDownSpeed;
        }
        if (OxygenImege.fillAmount < OxygenPercentage) { OxygenImege.fillAmount = OxygenPercentage; }


    }
}
