using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject Player;//PlayerGameObjectとセットしてください
                             //Hpメーターとして作りましたが　san値メーターとして使ってください

    //-----
    public float HPMAX = 10;//HP总量
    public float PlayerHP = 10;//HP残量
    //-----
    float HPDown ;//HP残量計算用変数
    public Image HPimage; //HPイメージport
    public Image HPimageRed; //HPイメージport
    public float HPDownSpeed;
    public float HPPercentage;//
    public float ImagePercentage;//HPPercentage
    //--HPUIここまで
  

    // Start is called before the first frame update
    void Start()
    {
        PlayerHP = Player.GetComponent<Player>().HP;//ここをプレイヤーのSan数値とセットしてください。
        HPMAX = PlayerHP;
    }
    void Update()
    {
        HPUIBOX();//UI
        PlayerHP = Player.GetComponent<Player>().HP;

        if (PlayerHP <= 0)
        {
            GameObject.Find("UIBOX").SendMessage("DIEEND");

        }

    }
 

    void HPUIBOX() {
        //残りHP
        HPDown = 10;//HP残量計算用変数
        HPDownSpeed = (HPDown - PlayerHP) *0.25f;
        HPDown = PlayerHP;

        //PlayerHPUI.gameObject.GetComponent<Text>().text = ("HP:" + HP);//HPUI
        if (PlayerHP > HPMAX) { PlayerHP = HPMAX; }

        HPPercentage = PlayerHP / HPMAX;//HPPercentage

        ImagePercentage = HPPercentage;

        HPimage.fillAmount = HPPercentage;
        if(HPimageRed.fillAmount > HPPercentage) {
            HPimageRed.fillAmount -= Time.deltaTime* HPDownSpeed;
        }
        if (HPimageRed.fillAmount < HPPercentage) { HPimageRed.fillAmount = HPPercentage; }


    }
}
