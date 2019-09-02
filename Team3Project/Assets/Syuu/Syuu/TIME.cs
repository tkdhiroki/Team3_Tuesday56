using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class TIME : MonoBehaviour
{
    public Text TIMETEXT; // Textオブジェクト
    public float TIMEOUT;
    // 初期化
    void Start()
    {
    }

    // 更新
    void Update()
    {
        TIMEOUT = TIMEOUT - 1 * Time.deltaTime;
        if (TIMEOUT > 0)
        {
            TIMETEXT.text = " " + (int)TIMEOUT;
        }
        if (TIMEOUT < 0)
        {
          //  GameObject.Find("UIBOX").SendMessage("DIEEND");
        }
       

    }
}
