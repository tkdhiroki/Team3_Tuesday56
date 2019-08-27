using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    //イベントの番号
    int eventNum = 0;

    //イベントの種類
    /*
    private enum  EventList
    {
        none,
        Explosion,
        CadaverMeet,
        Water,

    }*/

    //private EventList eventList;

    //イベントフラグ達
    public static bool explosion = false;       //爆発
    public static bool cadaverMeet = false;     //死体発見
    public static bool water = false;           //水位の近く
    public static bool audioFlag = false;       //爆発音

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EventAction();
        AudioCheck();
    }

    private void EventAction()
    {
        //イベントごとにオンにするフラグを変える
        switch (eventNum)
        {
            case 1:

                explosion = true;

                break;
            case 2:

                cadaverMeet = true;

                break;
            case 3:

                water = true;

                break;
        }
    }

    void AudioCheck()
    {
        //爆発音が聞こえたら
        if (audioFlag == true)
        {
            eventNum = 1;
            StartCoroutine(EventReset(5f));
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //死体を見つけたら
        if (col.gameObject.tag == "CadaverMeet")
        {
            eventNum = 2;
            StartCoroutine(EventReset(5f));
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        //スーツを着ないで水に触れ続けている間
        if (col.gameObject.tag == "Water")
        {
            eventNum = 3;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        //水から上がった時
        if(col.gameObject.tag == "Water")
        {
            eventNum = 0;
        }
    }

    //イベントのリセット
    private IEnumerator EventReset(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);

        eventNum = 0;

        yield break;

    }

}
