using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterIN : MonoBehaviour
{
    public float oxygen;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float moveMultiply;

    private float x, y;

    [SerializeField]
    private float maxOx;

    private Rigidbody2D rigid;

    //[HideInInspector]
    public bool waterInCheack = false;

    [SerializeField]
    private float time;
    

    public void WaterInPlayerSetInput()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
    }
    public void WaterInPlayerMove()
    {
        Vector2 move = Vector2.zero;
        move.x = moveSpeed * x;
        if (gameObject.transform.localPosition.y <= -4.2)
        {
            move.y = moveSpeed * 0;
        }
        else
        {
            move.y = moveSpeed * y;
        }

        rigid.AddForce(moveMultiply * (move - rigid.velocity));
    }

    public void Fall()
    {
        if (!Input.anyKey)
        {
            if (gameObject.transform.localPosition.y >= -4.2)
            {
                rigid.AddForce(new Vector2(0, -0.01f));
            }
        }
    }

    public void OxygenUse()
    {
        time += Time.deltaTime;
        if(oxygen <= 0) { return; }
        if (time > 1)
        {
            oxygen--;
            time = time - time;
        }
    }

    public void OxygenRevive()
    {
        oxygen = maxOx;
    }

    public void RigidSet(Rigidbody2D rigidB)
    {
        rigid = rigidB;
    }
}
