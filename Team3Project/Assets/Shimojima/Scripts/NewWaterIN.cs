using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWaterIN : MonoBehaviour
{
    [Header("-酸素-")]
    public float oxygen; //100
    [SerializeField]
    private float maxOx; //100

    [Header("-水中移動-")]

    [SerializeField]
    private Vector2 moveSpeed;
    public float changeSpeed; //初期値 0.9
    public float breaking;  //減衰係数 0.97

    [Header("横方向")]
    [SerializeField]
    private float accelalationX; //加速値
    [SerializeField]
    private float accelNumX; //加速値に追加する値 0.0001

    [Header("縦方向")]
    [SerializeField]
    private float accelalationY; //加速値
    [SerializeField]
    private float accelNumY; //加速値に追加する値 0.0001

    private float x, y; //キー入力の判定用

    private enum KeyNum
    {
        Left,
        Right,
        Up,
        Down
    }

    private KeyNum kNum;

    private Rigidbody2D rigid;

    //[HideInInspector]
    public bool waterInCheack = false;

    [Header("-時間-")]
    [SerializeField]
    private float time;

    [SerializeField]
    private float aTimeX;
    [SerializeField]
    private float aTimeY;
    [SerializeField]
    private float aTimeInterval;


    /// <summary>
    /// キー入力の有無の判定
    /// </summary>
    /// <returns></returns>
    public int MoveCheack()
    {
        if (!SanMaterPlus.madFlag)
        {
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");
        }
        if (x != 0 && y == 0)
        {
            return 1;
        }
        else if (y != 0 && x == 0)
        {
            return 2;
        }
        else if (x != 0 && y != 0)
        {
            return 3;
        }
        
        aTimeX = 0;
        aTimeY = 0;
        accelalationX = 0;
        accelalationY = 0;
        return 0;
    }

    /// <summary>
    /// 水中操作
    /// </summary>
    public void WaterInPlayerMove()
    {
        if (!SanMaterPlus.madFlag)
        {
            if (MoveCheack() == 1 || MoveCheack() == 2)
            {
                if (MoveCheack() == 1)
                {
                    aTimeX += Time.deltaTime;
                    AccelalationPlus(1);
                    moveSpeed.y *= breaking;
                }
                else if (MoveCheack() == 2)
                {
                    aTimeY += Time.deltaTime;
                    AccelalationPlus(2);
                    moveSpeed.x *= breaking;
                }
            }
            else if (MoveCheack() == 3)
            {
                aTimeX += Time.deltaTime;
                aTimeY += Time.deltaTime;
                AccelalationPlus(1);
                AccelalationPlus(2);
            }
            else if (MoveCheack() == 0)
            {
                moveSpeed.x *= breaking;
                moveSpeed.y *= breaking;
            }

            //左方向の入力
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.localScale = new Vector3(-0.1f, 0.1f, 1);
                accelalationX = 0;
                kNum = KeyNum.Left;
            }

            //右方向の入力
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.localScale = new Vector3(0.1f, 0.1f, 1);
                accelalationX = 0;
                kNum = KeyNum.Right;
            }

            //上方向の入力
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                accelalationY = 0;
                kNum = KeyNum.Up;
            }

            //下方向の入力
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                accelalationY = 0;
                kNum = KeyNum.Down;
            }
        }

        time += Time.deltaTime;
        if (time > 1)
        {
            OxygenUse();
            time = time - time;
        }
    }

    /// <summary>
    /// 移動値の加算
    /// </summary>
    public void WIPMove()
    {
        if (kNum == KeyNum.Left)
        {
            if (moveSpeed.x > -0.05)
            {
                moveSpeed.x -= accelalationX / changeSpeed;
            }
        }
        else if (kNum == KeyNum.Right)
        {
            if (moveSpeed.x < 0.05)
            {
                moveSpeed.x += accelalationX / changeSpeed;
            }
        }
        else if (kNum == KeyNum.Up)
        {
            if (moveSpeed.y < 0.03)
            {
                moveSpeed.y += accelalationY / changeSpeed;
            }
        }
        else if (kNum == KeyNum.Down)
        {
            if (moveSpeed.y > -0.03)
            {
                moveSpeed.y -= accelalationY / changeSpeed;
            }
        }
        
        gameObject.transform.localPosition = new Vector2(gameObject.transform.localPosition.x + moveSpeed.x, gameObject.transform.localPosition.y + moveSpeed.y);
    }

    public void MoveStop()
    {
        moveSpeed = new Vector2(0,0);
    }

    /// <summary>
    /// 加速値の変更
    /// </summary>
    /// <param name="i">縦か横<para>1なら縦、2なら横</para></param>
    public void AccelalationPlus(int i)
    {
        if (i == 1)
        {
            if (aTimeX > aTimeInterval)
            {
                accelalationX += accelNumX;
                aTimeX = aTimeX - aTimeX;
            }
        }
        else if (i == 2)
        {
            if (aTimeY > aTimeInterval)
            {
                accelalationY += accelNumY;
                aTimeY = aTimeY - aTimeY;
            }
        }
    }

    /// <summary>
    /// 酸素の使用
    /// </summary>
    public void OxygenUse()
    {
        if (oxygen <= 0) { return; }
        oxygen--;
    }

    /// <summary>
    /// 酸素の回復
    /// </summary>
    public void OxygenRevive()
    {
        oxygen = maxOx;
    }

    /// <summary>
    /// rigidbodyの取得
    /// </summary>
    /// <param name="rigidB">プレイヤーのrigidbody</param>
    public void RigidSet(Rigidbody2D rigidB)
    {
        rigid = rigidB;
    }
}
