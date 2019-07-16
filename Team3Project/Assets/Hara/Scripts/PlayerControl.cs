using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance;

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private float stateEffect = 1.0f;     // キャラクターの状態に応じてスピードを変化させる
    private float runForce = 1.5f;        // 走り始めに加える速度
    private float runSpeed = 0.5f;        // キャラクターの移動速度
    private float runThreshold = 2.2f;    // 速度切り替え判定のための閾値
    private int key = 0;                  // 左右の入力検知　1ならば右、-1ならば左
    [SerializeField]
    private bool isNormal = true;         // ダイビングスーツを着ていない状態ならばtrue


    public bool IsNormal { set { isNormal = value; } }


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isNormal)
        {
            GetInputKey();
            Move();
        }
    }

    /// <summary>
    /// 左右の入力を検知する
    /// </summary>
    private void GetInputKey()
    {
        key = 0;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) key = 1;    // 右の入力を検知
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) key = -1;    // 左の入力を検知
    }

    /// <summary>
    /// 検知した入力に応じて移動処理を開始する
    /// </summary>
    private void Move()
    {
        float speedX = Mathf.Abs(rb.velocity.x);
        if (speedX < this.runThreshold)
        {
            rb.AddForce(transform.right * key * this.runForce * stateEffect); //未入力の場合は key の値が0になるため移動しない
        }
        else
        {
            transform.position += new Vector3(runSpeed * Time.deltaTime * key * stateEffect, 0, 0);
        }
    }
}
