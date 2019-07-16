using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    [SerializeField]
    private WaterIN waterIN;
    private Rigidbody2D rigid;
    

    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        waterIN.RigidSet(rigid);
    }

    private void Update()
    {
        if (waterIN.waterInCheack)
        {
            waterIN.WaterInPlayerSetInput();
        }
    }

    private void FixedUpdate()
    {
        if (waterIN.waterInCheack)
        {
            waterIN.WaterInPlayerMove();
            waterIN.Fall();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Water")
        {
            waterIN.waterInCheack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Water")
        {
            waterIN.waterInCheack = false;
        }
    }
}
