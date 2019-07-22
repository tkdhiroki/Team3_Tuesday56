using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayerControl.Instance.PlayerStatePhase++;
        }

        if (Input.GetMouseButtonDown(1))
        {
            PlayerControl.Instance.PlayerStatePhase--;
        }
    }
}
