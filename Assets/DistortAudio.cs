using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DistortAudio : MonoBehaviour
{

    [Serializable]
    public struct AudioEffect
    {
        public string name;
        public float startValue;
        public float endValue;
    }

    [SerializeField] List<AudioEffect> effects;

    [SerializeField] AudioMixer mixer;


    public void Start()
    {
        SetEffectMixer(.5f);
    }

    public void SetEffectMixer(float percentage)
    {
        foreach(AudioEffect effect in effects)
        {
            mixer.SetFloat(effect.name, Mathf.Lerp(effect.startValue, effect.endValue, percentage));   
        }
    }
}
