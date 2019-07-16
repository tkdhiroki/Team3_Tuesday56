using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWaterIN : MonoBehaviour
{
    public float oxygen;
    [SerializeField]
    private float maxOx;

    [SerializeField]
    private Vector2 moveSpeed;
    public float changeSpeed;
    public float breaking;

    [SerializeField]
    private float accelalation;
    [SerializeField]
    private float accelNum;

    private float x, y;

    private enum KeyNum
    {
        Stop,
        Left,
        Right,
        Up,
        Down
    }

    private KeyNum kNum;

    private Rigidbody2D rigid;

    //[HideInInspector]
    public bool waterInCheack = false;

    [SerializeField]
    private float time;

    [SerializeField]
    private float aTime;
    [SerializeField]
    private float aTimeInterval;


    public bool moveCheack()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        if (x != 0 || y != 0)
        {
            return true;
        }

        kNum = KeyNum.Stop;
        aTime = 0;
        accelalation = 0;
        return false;
    }
    public void WaterInPlayerMove()
    {
        if (moveCheack())
        {
            aTime += Time.deltaTime;
            AccelalationPlus(1);
        }
        else if (!moveCheack())
        {
            moveSpeed.x *= breaking;
            moveSpeed.y *= breaking;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            kNum = KeyNum.Left;
        }

        time += Time.deltaTime;
        if (time > 1)
        {
            OxygenUse();
            time = time - time;
        }
    }

    public void WIPMove()
    {
        if (kNum == KeyNum.Left)
        {
            if (moveSpeed.x > -0.03)
            {
                moveSpeed.x -= accelalation / changeSpeed;
            }
            gameObject.transform.localPosition = new Vector2(gameObject.transform.localPosition.x + moveSpeed.x, gameObject.transform.localPosition.y + moveSpeed.y);
        }
        else if (kNum == KeyNum.Stop)
        {
            gameObject.transform.localPosition = new Vector2(gameObject.transform.localPosition.x + moveSpeed.x, gameObject.transform.localPosition.y + moveSpeed.y);
        }
    }

    public void AccelalationPlus(int i)
    {
        if (aTime > aTimeInterval)
        {
            accelalation += accelNum;
            aTime = aTime - aTime;
        }
    }

    public void OxygenUse()
    {
        if (oxygen <= 0) { return; }
        oxygen--;
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
