using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanMaterPlus : MonoBehaviour
{

    private Slider SanGauge;   //SAN値ゲージ
    float MaxValue = 100;   //最高値
    float NowValue = 0;     //現在値

    public static bool madFlag = false; //発狂しているかどうか

    // Start is called before the first frame update
    private void Start()
    {

        SanGauge = GetComponent<Slider>();

        //SAN値ゲージの最高値と現在値を取得
        SanGauge.maxValue = MaxValue;
        SanGauge.value = NowValue;

    }

    // Update is called once per frame
    private void Update()
    {
        EventCheck();
        SanCheck();
        ValueCheck();
    }

    public void ValueCheck()
    {
        SanGauge.value = NowValue;
    }

    //SAN値が最大値になったかどうか
    private void SanCheck()
    {
        //最大値になったら発狂して10秒後に解除しSAN値を0にする
        if(NowValue == MaxValue)
        {
            madFlag = true;
            StartCoroutine(SanReset(5f));
        }
    }

    //何かイベントが起きたかどうかの判断
    public void EventCheck()
    {
        //各イベントが発生したらSAN値を上昇させフラグをオフにする
        if (Event.explosion == true)
        {
            NowValue += 20;
            Event.explosion = false;
        }
        else if (Event.cadaverMeet == true)
        {
            NowValue += 100;
            Event.cadaverMeet = false;
        }
        else if (Event.water == true)
        {
            NowValue += 1;
            Event.water = false;
        }
    }

    //発狂後にSAN値のリセット
    private IEnumerator SanReset(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);

        NowValue = 0;
        madFlag = false;

        yield break;

    }

}
