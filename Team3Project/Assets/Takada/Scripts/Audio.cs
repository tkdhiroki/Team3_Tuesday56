using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip sound1;
    AudioSource audioSource;

    //一定時間ごとに爆発音を鳴らす
    [SerializeField]
    private float waitTime;

    //屋上の1つ前のフロアに着いた時の連続爆発音用
    private bool LastFloor = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(LastFloor == false)
        {
            Explosion();

        } else if(LastFloor == true){

        }
    }

    void Explosion()
    {
        StartCoroutine(ExplosionAudio());
    }

    IEnumerator ExplosionAudio()
    {
        yield return new WaitForSeconds(waitTime);



        yield break;
    }
}
