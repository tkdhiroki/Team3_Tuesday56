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
    [SerializeField]
    private float playerStateSpeed;    // プレイヤーの状態に応じた速度
    [SerializeField, Tooltip("ジャンプ時に与える速度")]
    private float jumpForce = 700.0f;

    [SerializeField, Tooltip("プレイヤーが水中にいるか")]
    private bool isDiving = false;
    public bool IsDiving { set { isDiving = value; } }
    [SerializeField, Tooltip("ジャンプを許可するか")]
    private bool permissionJump = false;
    private bool isGround;    // 地面と接地しているかを検知

    [SerializeField, Tooltip("プレイヤーの年齢変化（10段階）")]
    private PlayerState state = PlayerState.First;
    private float speedDeduction;    // 速度の減少値

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
        Sixth,
        Seventh,
        Eighth,
        Ninth,
        Tenth
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
        speedDeduction = playerStateSpeed / Enum.GetValues(typeof(PlayerState)).Length;
    }

    private void Update()
    {
        if (!isDiving)
        {
            PlayerMoveSpeed();
            PlayerMove();
        }
    }

    /// <summary>
    /// プレイヤーの状態に応じた移動速度を算出する関数
    /// </summary>
    private void PlayerMoveSpeed()
    {
        playerStateSpeed = 1.0f - (speedDeduction * (int)state);
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

        // スピード制限
        if(speedx < maxWalkSpeed)
        {
            rigid2D.AddForce(transform.right * key * walkForce * playerStateSpeed);
        }

        // 動く方向に応じて反転
        if(key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        // ジャンプ
        if(permissionJump && Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rigid2D.AddForce(transform.up * jumpForce * playerStateSpeed);
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
