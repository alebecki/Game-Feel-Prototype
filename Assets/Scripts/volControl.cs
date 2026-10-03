using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class volControl : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetLevel(float sliderVal){
        mixer.SetFloat("Vol", Mathf.Log10(sliderVal) * 20);
}
}
