using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public enum OwnSwitch
    {
        Floor1 = 0,
        Floor2  ,
        Floor3 ,
        Floor4 ,
        Floor5 ,
        Door2, 
        Door4,
        Door5_1,
        Door5_2,
    }
    public OwnSwitch own;
    [SerializeField]
    private GameObject Floor;
    bool enable;
    GameObject Camera;
    [SerializeField]
    float doorWaitTime;
    // Start is called before the first frame update
    void Start()
    {
        enable = false;
        Camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Space)&&collision.gameObject.tag == "Player"&&!enable)
        {
            switch (own)
            {
                case OwnSwitch.Floor1:
                    StartCoroutine(Floor1Up());
                    enable = true;
                    break;
                case OwnSwitch.Floor2:
                    StartCoroutine(FloorUp());
                    enable = true;
                    break;
                case OwnSwitch.Floor3:
                    StartCoroutine(FloorUp());
                    enable = true;
                    break;
                case OwnSwitch.Floor4:
                    StartCoroutine(FloorUp());
                    enable = true;
                    break;
                case OwnSwitch.Floor5:
                    StartCoroutine(FloorUp());
                    enable = true;
                    break;
                case OwnSwitch.Door2:
                    StartCoroutine(DoorDown());
                    enable = true;
                    break;
                case OwnSwitch.Door4:
                    StartCoroutine(DoorDoubleDown());
                    break;
                case OwnSwitch.Door5_1:
                    StartCoroutine(DoorDoubleDown());
                    break;
                case OwnSwitch.Door5_2:
                    StartCoroutine(DoorDoubleDown());
                    break;
            }
        }
    }
    
    IEnumerator Floor1Up()
    {
        float up = 0;
        float now = Floor.transform.position.y;
        float nowC = Camera.transform.position.y;
        while (up < 7)
        {
            yield return new WaitForSeconds(Time.deltaTime); 
            Floor.transform.position = new Vector3(Floor.transform.position.x, now + up, Floor.transform.position.z);
            Camera.transform.position = new Vector3(Camera.transform.position.x, nowC + up*1.5f, Camera.transform.position.z);
            up += 0.1f;
        }
        up = 7;
        Floor.transform.position = new Vector3(Floor.transform.position.x, now + up, Floor.transform.position.z);
        Camera.transform.position = new Vector3(Camera.transform.position.x, nowC + up*1.35f, Camera.transform.position.z);
    }
    IEnumerator FloorUp()
    {
        float up = 0;
        float now = Floor.transform.position.y;
        float nowC = Camera.transform.position.y;
        while (up < 9)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            Floor.transform.position = new Vector3(Floor.transform.position.x, now + up, Floor.transform.position.z);
            Camera.transform.position = new Vector3(Camera.transform.position.x, nowC + up, Camera.transform.position.z);
            up += 0.1f;
        }
        up = 9;
        Floor.transform.position = new Vector3(Floor.transform.position.x, now + up, Floor.transform.position.z);
        Camera.transform.position = new Vector3(Camera.transform.position.x, nowC + up, Camera.transform.position.z);


    }
    IEnumerator DoorDown()
    {
        float up = 0;
        float now = Floor.transform.position.y;

        while (up < 3.5f)
        {
            yield return new WaitForSeconds(0.05f);
            Floor.transform.position = new Vector3(Floor.transform.position.x, now - up, Floor.transform.position.z);
            up += 0.1f;
        }
        up = 3.5f;
        Floor.transform.position = new Vector3(Floor.transform.position.x, now - up, Floor.transform.position.z);

    }
    IEnumerator DoorDoubleDown()
    {
        float up = 0;
        float now = Floor.transform.position.y;
        enable = true;

        while (up < 3.5f)
        {
            yield return new WaitForSeconds(0.05f);
            Floor.transform.position = new Vector3(Floor.transform.position.x, now - up, Floor.transform.position.z);
            up += 0.1f;
        }
        up = 3.5f;
        Floor.transform.position = new Vector3(Floor.transform.position.x, now - up, Floor.transform.position.z);
        yield return new WaitForSeconds(doorWaitTime);
        while (up > 0f)
        {
            yield return new WaitForSeconds(0.03f);
            Floor.transform.position = new Vector3(Floor.transform.position.x, now - up, Floor.transform.position.z);
            up -= 0.1f;
        }
        Floor.transform.position = new Vector3(Floor.transform.position.x, now, Floor.transform.position.z);
        enable = false;
    }
}
