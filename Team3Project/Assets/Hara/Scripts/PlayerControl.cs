using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance;

    private Rigidbody2D rigid2D;
    private SpriteRenderer sprite;
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
    [SerializeField, Tooltip("接地を確認するためのRayの長さ")]
    private float rayRange = 0.5f;

    [SerializeField, Tooltip("プレイヤーの年齢変化（5段階）")]
    private PlayerState state = PlayerState.First;
    private float speedDeduction;    // 速度の減少値
    private float jumpDeduction;     // ジャンプ力の減少値
    private int indexLength;         // 要素数
    public int PlayerStatePhase
    {
        set
        {
            if (!isDiving)
            {
                if (value < 0)
                {
                    state = PlayerState.First;
                }
                else if (value >= indexLength)
                {
                    state = (PlayerState)Enum.ToObject(typeof(PlayerState), indexLength - 1);
                }
                else
                {
                    state = (PlayerState)Enum.ToObject(typeof(PlayerState), value);
                }
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
        sprite = GetComponent<SpriteRenderer>();
        indexLength = Enum.GetValues(typeof(PlayerState)).Length;
        speedDeduction = maxWalkSpeed / indexLength;
        jumpDeduction = jumpForce / indexLength / 2;
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
            if(key > 0)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
        }

        // ジャンプ
        if(permissionJump && Input.GetKeyDown(KeyCode.Space) && rigid2D.velocity.y == 0)
        {
            rigid2D.AddForce(transform.up * maxJump);
        }
    }
}
