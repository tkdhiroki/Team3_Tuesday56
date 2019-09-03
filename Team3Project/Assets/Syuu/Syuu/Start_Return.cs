using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Start_Return : MonoBehaviour
{
    public int SceneBuildIndex = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            //Application.Quit();
            SceneManager.LoadScene(SceneBuildIndex);//行きたい　sceneBuildIndex　Sceneの番号
        }
    }
}
