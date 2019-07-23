using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakSwitchGimmick : StageObject
{
    private Color brokeColor = new Color(142f / 255, 142f / 255, 142f / 255, 1);    // 壊れている時の色

    private void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = brokeColor;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // player探知
        if (!collision.gameObject.CompareTag(playerTag) ) return;

        MaterialChange();
        GimmickUIonoff(true);

        if (!Input.GetKeyDown(KeyCode.Space)) return;
        
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DefaultMaterial();  // 元のMaterialに戻す
        GimmickUIonoff(false);
    }

    private void ColorAdd()
    {

    }
}
