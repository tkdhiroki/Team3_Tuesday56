using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterIN : MonoBehaviour
{
    public static float oxygen;
    [SerializeField]
    private float maxOx;

    public void WaterInPlayer()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {

        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {

        }
    }

    public void OxygenUse()
    {
        if(oxygen <= 0) { return; }
        oxygen--;
    }

    public void OxygenRevive()
    {
        oxygen = maxOx;
    }
}
