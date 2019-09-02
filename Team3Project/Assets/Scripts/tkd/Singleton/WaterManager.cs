using UnityEngine;

// 水の状態遷移
public enum WaterState
{
    None,
    Easy,
    Normal,
    Hard,
    Warning
}


/// <summary>
/// 水の状態によってSpeedを切り替えたり
/// </summary>
public class WaterManager : SingletonMonoBehavior<WaterManager>
{
    // 現在の水の状態
    public WaterState currentWater { get; private set; } = WaterState.None;
    [SerializeField] private WaterMove waterMove = null;

    private void Start()
    {
        // Game開始時点のStateに切り替える
        ChangeCurrentWaterState(WaterState.Easy);
    }

    // 水の状態を変える
    public void ChangeCurrentWaterState(WaterState waterState)
    {
        currentWater = waterState;
        WaterStateManager();
    }

    /// <summary>
    ///  現在の水の状態によって処理を変える
    /// </summary>
    private void WaterStateManager()
    {
        switch(currentWater)
        {            
            case WaterState.Easy:
                waterMove.MoveSpeed = 1f;
                break;
            case WaterState.Normal:
                waterMove.MoveSpeed = 2f;
                break;
            case WaterState.Hard:
                waterMove.MoveSpeed = 3f;
                break;
            case WaterState.Warning:
                waterMove.MoveSpeed = 4f;
                break;
            default:
                break;
        }
    }
}
