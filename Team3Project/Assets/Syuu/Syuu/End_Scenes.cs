using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン転移　には必要

public class End_Scenes : MonoBehaviour
{//プレイヤーの結局
    void HAPPYEND()
    {
        SceneManager.LoadScene("HAPPYEND");//シーンの切り替え    }

        // GameObject.Find("UIBOX").SendMessage("HAPPYEND");//呼び出しは必要ので　クリア処理のところにいれてください

    }

    void DIEEND()//死亡結局
    {
        SceneManager.LoadScene("DIEEND");//シーンの切り替え    }
        // SAN値か酸素が0より低い場合は自動的に呼び出し
    }

}
