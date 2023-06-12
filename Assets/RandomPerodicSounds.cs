using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomPerodicSounds : MonoBehaviour
{


    [SerializeField] float soundMinBetween;
    [SerializeField] float soundMaxBetween;

    [SerializeField] List<AudioClip> clips;

    private AudioSource source;

    public void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void Start()
    {
        StartCoroutine(PlaySoundPeriodicly());
    }



    public void PlayRandomSound()
    {
        int randomIndex = Random.Range(0, clips.Count);

        source.clip = clips[randomIndex];
        source.Play();
    }

    public IEnumerator PlaySoundPeriodicly()
    {
        float waitTime = Random.Range(soundMinBetween, soundMaxBetween);


        while(true)
        {
            yield return new WaitForSeconds(waitTime);
            PlayRandomSound();
            waitTime = Random.Range(soundMinBetween, soundMaxBetween);
        }
    }
        

}
