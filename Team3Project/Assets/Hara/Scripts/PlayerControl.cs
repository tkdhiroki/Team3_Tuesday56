using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    private SceneManager sceneManager;

    public static PlayerControl Instance;

    private Rigidbody2D rigid2D;
    private SpriteRenderer sprite;
    private Animator animator;
    private int key = 0;    // 左右の入力値
    private float speedx = 0;    // プレイヤーのX方向の移動速度
    private float maxWalk = 0;    // プレイヤーのスピード上限
    private float maxJump = 0;    // プレイヤーのジャンプ高さの上限値

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
    
    [SerializeField, Tooltip("ステージに接地しているか")]
    private bool isGround = false; //追記項目

    // プレイヤーの年齢変化
    [SerializeField, Tooltip("プレイヤーの年齢変化（5段階）")]
    private PlayerState state = PlayerState.First;
    private float speedDeduction;    // 速度の減少値
    private float jumpDeduction;     // ジャンプ力の減少値
    private int indexLength;         // 要素数
    public int PlayerStatePhase
    {
        set
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

            switch (state)
            {
                case PlayerState.First:
                    plusSAN = 5;
                    break;
                case PlayerState.Second:
                    plusSAN = 10;
                    break;
                case PlayerState.Third:
                    plusSAN = 15;
                    break;
                case PlayerState.Fourth:
                    plusSAN = 20;
                    break;
                default:
                    plusSAN = 25;
                    break;
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

    // プレイヤーのSAN値
    [SerializeField, Tooltip("プレイヤーのSAN値の上限値")]
    private int maxSAN = 50;
    public int MaxSAN { get { return maxSAN; } }
    private int nowSAN = 0;    // 現在のSAN値
    public int NowSAN { get { return nowSAN; } }
    private int plusSAN = 5;    // SAN値の加算値
    private int riseSAN = 0;    // SAN値の上昇値
    public int RiseSAN { get { return riseSAN; } }
    private int clickCounter = 0;    // 連打用のカウント
    private bool flagSAN = false;    // SAN値がMAXならture
    public bool FlagSAN { set { flagSAN = value; } }
    private bool resetSAN = false;    // SAN値が0になったことを検知するフラグ
    public bool ResetSAN { set { resetSAN = value; } }

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
        animator = GetComponent<Animator>();
        indexLength = Enum.GetValues(typeof(PlayerState)).Length;
        speedDeduction = maxWalkSpeed / indexLength;
        jumpDeduction = jumpForce / indexLength / 2;
    }

    private void Update()
    {
        Common();
        CameraMove();
        if(!flagSAN)
        {
            if (!isDiving)
            {
                PlayerMove();
            }
            else
            {
                speedx = 0;
            }
        }
        else
        {
            ActionSAN(true);
            if (resetSAN)
            {
                ActionSAN(false);
                flagSAN = false;
            }
            /*
            if(clickCounter < 10)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    clickCounter++;
                }
            }
            else
            {
                ActionSAN(false);
                clickCounter = 0;
            }
            */
        }
        
    }

    /// <summary>
    /// プレイヤーの移動を管理する関数
    /// </summary>
    private void PlayerMove()
    {
        speedx = Mathf.Abs(rigid2D.velocity.x);
        maxWalk = maxWalkSpeed - (speedDeduction * (int)state);
        maxJump = jumpForce - (jumpDeduction * (int)state);

        // スピード制限
        if(speedx < maxWalk)
        {
            rigid2D.AddForce(transform.right * key * walkForce);
        }

        // ジャンプ
        if(permissionJump && Input.GetKeyDown(KeyCode.Space) && isGround && rigid2D.velocity.y <= 1)
        {
            rigid2D.AddForce(transform.up * maxJump);
        }
        
    }

    /// <summary>
    /// 水中時でも実行する処理
    /// </summary>
    private void Common()
    {
        // 左右の入力検知
        key = 0;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) key = -1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) key = 1;

        // 動く方向に応じて反転
        if (key != 0)
        {
            if (key > 0)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
        }

        animator.speed = speedx / 2.0f;
    }

    /// <summary>
    /// SAN値を上昇させる処理
    /// </summary>
    public void UpSAN()
    {
        if(nowSAN < maxSAN)
        {
            riseSAN = plusSAN * UnityEngine.Random.Range(1, 3);
            nowSAN += riseSAN;
        }
    }

    /// <summary>
    /// SAN値の変動による処理
    /// </summary>
    /// <param name="flag">true=SAN値が上限値を上回った場合の処理を実行、false=SAN値をリセットする処理を実行</param>
    private void ActionSAN(bool flag)
    {
        if (!flag)
        {
            nowSAN = 0;
            sprite.color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
            resetSAN = false;
            return;
        }

        speedx = 0;
        sprite.color = new Color(255f / 255f, 120f / 255f, 0f / 255f);
    }

    //追加項目
    /// <summary>
    /// カメラの縦移動をプレイヤーに同期
    /// </summary>
    private void CameraMove()
    {
        camera.transform.position = new Vector3(camera.transform.position.x,
                                                1.15f + transform.position.y, 
                                                camera.transform.position.z);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "clear")
        {
            SceneManager.LoadScene("Clear");
        }
    }

    //追加項目
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Stage" || collision.gameObject.tag == "MoveStage")
        {
            isGround = true;
        }
    }

    //追加項目
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Stage" || collision.gameObject.tag == "MoveStage")
        {
            isGround = false;
        }
    }
}
