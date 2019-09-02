using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    [SerializeField]
    float startX, endX;
    bool enu;//　どっち側に移動するか
    // Start is called before the first frame update
    void Start()
    {
        // スタートのX位置
        startX = transform.position.x;
       
        enu = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (startX <= transform.position.x)
        {
            enu = true;
        }else if(endX >= transform.position.x)
        {
            enu = false;
        }
        transform.position = new Vector3((enu)? transform.position.x - 0.05f : transform.position.x + 0.05f,transform.position.y,transform.position.z);
    }
}
