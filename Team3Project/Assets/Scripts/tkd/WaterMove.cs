using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * 水位があがる
 */
public class WaterMove : MonoBehaviour
{
    private int nowIntTime = 0;
    private int maxTime = 0;    // 制限時間の最初

    private float defaultSpeed = 0f;
    private GameObject Camera;
    [SerializeField] private float moveSpeed = 2.0f;    // 移動速度
    public float MoveSpeed { set { moveSpeed = value * defaultSpeed; } }

    private void Awake()
    {
        defaultSpeed = moveSpeed;
    }
    void Start()
    {
        //maxTime = (int)TimeManager.Instance.LimitTime;
        Camera = GameObject.Find("Main Camera");
    }

    void Update()
    {
        WaterMoveUp();
        Debug.Log("Camera"+ Camera.transform.position.y);
        Debug.Log("水" +transform.position.y);
        if (Camera.transform.position.y <= transform.position.y)
        {
            Debug.Log("あ");
            SceneManager.LoadScene("GameOver");
        }
        //Debug.Log(moveSpeed);
    }
    /// <summary>
    /// 水の移動
    /// </summary>
    private void WaterMoveUp()
    {
        this.transform.position += new Vector3(0, moveSpeed * 0.001f, 0);
    }
}
