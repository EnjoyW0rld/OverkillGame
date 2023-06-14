using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomSound : MonoBehaviour
{


    AudioSource source;

    [SerializeField] List<AudioClip> clips = new List<AudioClip>();

    public void Awake()
    {
        source = GetComponent<AudioSource>();
    }


    public void PlayRandomSounds()
    {

        int randomIndex = Random.Range(0, clips.Count);

        source.clip = clips[randomIndex];
        source.Play();
    }
}
