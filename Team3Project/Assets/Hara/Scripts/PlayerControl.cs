using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance;

    private Rigidbody2D rigid2D;
    [SerializeField, Tooltip("移動時に与える速度")]
    private float walkForce = 30.0f;
    [SerializeField, Tooltip("プレイヤーの歩く最大速度")]
    private float maxWalkSpeed = 5.0f;
    [SerializeField, Tooltip("ジャンプ時に与える速度")]
    private float jumpForce = 700.0f;

    [SerializeField, Tooltip("プレイヤーが水中にいるか")]
    private bool isDiving = false;
    public bool IsDiving { set { isDiving = value; } }
    [SerializeField, Tooltip("ジャンプを許可するか")]
    private bool permissionJump = false;
    private bool isGround;    // 地面と接地しているかを検知

    [SerializeField, Tooltip("プレイヤーの年齢変化（5段階）")]
    private PlayerState state = PlayerState.First;
    private float speedDeduction;    // 速度の減少値
    private float jumpDeduction;     // ジャンプ力の減少値
    public int PlayerStatePhase
    {
        set
        {
            if(value < 0)
            {
                state = PlayerState.First;
            }
            else if(value >= Enum.GetValues(typeof(PlayerState)).Length)
            {
                state = (PlayerState)Enum.ToObject(typeof(PlayerState), Enum.GetValues(typeof(PlayerState)).Length - 1);
            }
            else
            {
                state = (PlayerState)Enum.ToObject(typeof(PlayerState), value);
            }
        }
        get
        {
            return (int)state;
        }
    }

    /// <summary>
    /// プレイヤーの状態（年齢変化）
    /// </summary>
    private enum PlayerState
    {
        First,
        Second,
        Third,
        Fourth,
        Fifth,
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        speedDeduction = maxWalkSpeed / Enum.GetValues(typeof(PlayerState)).Length;
        jumpDeduction = jumpForce / Enum.GetValues(typeof(PlayerState)).Length / 2;
    }

    private void Update()
    {
        if (!isDiving)
        {
            PlayerMove();
        }
    }

    /// <summary>
    /// プレイヤーの移動を管理する関数
    /// </summary>
    private void PlayerMove()
    {
        // 左右の入力検知
        int key = 0;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) key = -1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) key = 1;

        // プレイヤーの速度
        float speedx = Mathf.Abs(rigid2D.velocity.x);
        float maxWalk = maxWalkSpeed - (speedDeduction * (int)state);
        float maxJump = jumpForce - (jumpDeduction * (int)state);

        // スピード制限
        if(speedx < maxWalk)
        {
            rigid2D.AddForce(transform.right * key * walkForce);
        }

        // 動く方向に応じて反転
        if(key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        // ジャンプ
        if(permissionJump && Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rigid2D.AddForce(transform.up * maxJump);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 地面との接地を検知
        isGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 地面との非接地を検知
        isGround = false;
    }
}
