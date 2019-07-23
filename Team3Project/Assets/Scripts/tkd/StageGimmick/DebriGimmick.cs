using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  ステージ上に存在する瓦礫
/// </summary>
public class DebriGimmick : StageObject
{    
    private void OnTriggerStay2D(Collider2D collision)
    {
        // player探知
        if ( !collision.gameObject.CompareTag(playerTag) ) return;

        MaterialChange();
        GimmickUIonoff(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DefaultMaterial();  // 元のMaterialに戻す
        GimmickUIonoff(false);
    }
}
