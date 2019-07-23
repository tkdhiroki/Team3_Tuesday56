using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    //イベントの種類
    private enum EventList
    {
        Explosion,
        CadaverMeet,
        Water,

    }

    private EventList eventList;

    //イベントフラグ達
    public static bool explosion = false;       //爆発
    public static bool cadaverMeet = false;     //死体発見
    public static bool water = false;           //水位の近く


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EventAction();
    }

    private void EventAction()
    {
        //イベントごとにオンにするフラグを変える
        switch (eventList)
        {
            case EventList.Explosion:

                explosion = true;

                break;
            case EventList.CadaverMeet:

                cadaverMeet = true;

                break;
            case EventList.Water:

                water = true;

                break;
        }
    }

}
