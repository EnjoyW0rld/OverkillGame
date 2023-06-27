using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwitcher : MonoBehaviour
{

    [SerializeField] AudioSource firstSource;
    [SerializeField] AudioSource secondSource;

    bool firstSourcePlaying = true;

    bool isSwitchingTracks = false;

    IEnumerator switchTracks;

    public void StartSwitchingTracks()
    {
        if (isSwitchingTracks) return;

        if (firstSourcePlaying) switchTracks = SwitchTracks(firstSource, secondSource);
        else switchTracks = SwitchTracks(secondSource, firstSource);

        StartCoroutine(switchTracks);

        firstSourcePlaying = !firstSourcePlaying;
    }


    public void SwithToDifferent()
    {
        if (isSwitchingTracks) return;

        switchTracks = SwitchTracks(firstSource, secondSource);
        StartCoroutine(switchTracks);

        firstSourcePlaying = false;
    }

    public void SwithToDifferent(float time)
    {
        if (isSwitchingTracks) return;

        switchTracks = SwitchTracks(firstSource, secondSource, time);
        StartCoroutine(switchTracks);

        firstSourcePlaying = false;
    }

    public void SwitchToNormal()
    {
        if (isSwitchingTracks) return;

        switchTracks = SwitchTracks(secondSource, firstSource);
        StartCoroutine(switchTracks);

        firstSourcePlaying = true;
    }


    public IEnumerator SwitchTracks(AudioSource beginning, AudioSource ending)
    {
        isSwitchingTracks = true;

        float time = 0;

        while (time < 1)
        {

            beginning.volume = 1 - time;
            ending.volume = time;

            time += Time.deltaTime;

            yield return 0;
        }

        isSwitchingTracks = false;
    }

    public IEnumerator SwitchTracks(AudioSource beginning, AudioSource ending, float totalTime)
    {
        isSwitchingTracks = true;

        float time = 0;

        while (time < totalTime)
        {

            beginning.volume = 1 - (time/ totalTime);
            ending.volume = time/ totalTime;

            time += Time.deltaTime;

            yield return 0;
        }

        isSwitchingTracks = false;
    }
}
