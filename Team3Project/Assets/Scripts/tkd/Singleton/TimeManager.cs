using UnityEngine;

public class TimeManager : SingletonMonoBehavior<TimeManager>
{
    public float LimitTime { get; private set; } = 300f;     // ゲームが終わるまでの時間
    
    private void Update()
    {
        ReduceTime();        
    }

    /// <summary>
    /// 時間減少
    /// </summary>
    private void ReduceTime()
    {
        LimitTime -= Time.deltaTime * 10;
    }
}
