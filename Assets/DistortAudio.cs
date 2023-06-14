using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;

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

    Sanity sanity;


    private void Start()
    {
        sanity = FindObjectOfType<Sanity>();
        if (sanity == null)
        {
            Debug.LogWarning("No sanity object was found, destroying self - " + name);
            Destroy(this);
        }
    }

    private void Update()
    {

        //Makes the distortion happen roughly 2/3 way in, and quickly distorting
        float percentage = Mathf.Pow(sanity.GetNormalizedSanity(), 10);

        SetEffectMixer(percentage);
    }

    public void SetEffectMixer(float percentage)
    {
        foreach(AudioEffect effect in effects)
        {
            mixer.SetFloat(effect.name, Mathf.Lerp(effect.startValue, effect.endValue, percentage));   
        }
    }
}
