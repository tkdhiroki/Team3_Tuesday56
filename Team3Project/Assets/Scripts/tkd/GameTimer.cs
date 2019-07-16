using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    private Text timeText = null;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<Text>();
        timeText.text = TimeManager.Instance.LimitTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        TimerText();
    }

    private void TimerText()
    {
        timer = TimeManager.Instance.LimitTime;
        timeText.text = timer.ToString();
    }
}
