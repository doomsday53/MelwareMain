using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public AudioClip clipOne;
    public AudioClip clipTwo;

    public float transitionTime;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            AudioManager.audioManager.PlayBGM(clipOne, transitionTime, true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AudioManager.audioManager.PlayBGM(clipTwo, transitionTime, true);
        }
    }
}
