using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    [SerializeField]
    private WaterIN waterIN;
    [SerializeField]
    private NewWaterIN nwIN;
    private Rigidbody2D rigid;
    

    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        nwIN.RigidSet(rigid);
    }

    private void Update()
    {
        if (nwIN.waterInCheack)
        {
            nwIN.WaterInPlayerMove();
        }
    }

    private void FixedUpdate()
    {
        if (nwIN.waterInCheack)
        {
            nwIN.WIPMove();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Water")
        {
            nwIN.waterInCheack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Water")
        {
            nwIN.waterInCheack = false;
        }
    }
}
