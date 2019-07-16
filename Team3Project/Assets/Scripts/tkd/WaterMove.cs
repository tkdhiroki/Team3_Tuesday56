using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/*
 * Timerが低くなるのと比例して水位があがる
 * Timerが一定の値を超えると上昇スピードが上がる  
 */
public class WaterMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;    // 移動速度
    private float defaultSpeed = 0f;
    private int nowIntTime = 0;
    private int maxTime = 0;    // 制限時間の最初

    private List<int> waterSpeed = new List<int>(3);

    void Start()
    {
        maxTime = (int)TimeManager.Instance.LimitTime;
        waterSpeed.Add(maxTime / 6);        //  50
        waterSpeed.Add(maxTime / 3 * 2);    // 200
        waterSpeed.Add(maxTime / 3);        // 100

        waterSpeed.ForEach(x => Debug.Log(x)); // Debug
        defaultSpeed = moveSpeed;
    }


    void Update()
    {
        WaterMoveUp();
        ChangeMoveSpeed();
        //Debug.Log(moveSpeed);
    }
    /// <summary>
    /// 水の移動
    /// </summary>
    private void WaterMoveUp()
    {
        this.transform.position += new Vector3(0, moveSpeed * 0.001f, 0);
    }
    /// <summary>
    /// 水の移動速度変化
    /// </summary>
    private void ChangeMoveSpeed()
    {
        // 現在の時間をInt
        nowIntTime = (int)TimeManager.Instance.LimitTime;

        switch (nowIntTime)
        {
            case 200:
                moveSpeed = defaultSpeed * 2.0f;
                break;
            case 150:
                moveSpeed = defaultSpeed * 3.0f;
                break;
            case 100:
                moveSpeed = defaultSpeed * 3.5f;
                break;
            case 50:
                moveSpeed = defaultSpeed * 4.0f;
                break;
        }

        //if (!waterSpeed.Any(x => x == nowIntTime)) return;

        //int[] displace = waterSpeed.Where(x => x == nowIntTime).ToArray();
        //moveSpeed = defaultSpeed + (300 - displace[0]) / 100;
    }
}
