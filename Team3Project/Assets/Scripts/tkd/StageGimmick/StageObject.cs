using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 時間巻き戻しの効果を受けるオブジェクトに継承させる
/// </summary>
public class StageObject : MonoBehaviour
{
    // 時間巻き戻し時のPlayerが押すボタンを表示 （ 仮 ）
    [SerializeField] private GameObject gimmickUI = null;
    protected readonly string playerTag = "Player";   // player判定用のTag
    [SerializeField] private Material outlineMaterial = null;   // 判定内にplayerがいるときに強調表示

    protected void GimmickUIonoff(bool swit)
    {
        gimmickUI.SetActive(swit);
    }

    /// <summary>
    ///  時間を巻き戻せる範囲にいるときにMaterialを変える
    /// </summary>
    protected void MaterialChange()
    {
        gameObject.GetComponent<SpriteRenderer>().material = outlineMaterial;
    }

    /// <summary>
    /// defaultに戻す
    /// </summary>
    protected void DefaultMaterial()
    {
        gameObject.GetComponent<SpriteRenderer>().material = default;
    }
}
