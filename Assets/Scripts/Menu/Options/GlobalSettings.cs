using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class GlobalSettings : MonoBehaviour
{

    public static GlobalSettings Instance;

    [SerializeField] AudioMixer mixer;
    [SerializeField] Volume volume;

    [SerializeField] UnityEvent onLoadOptions;




    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            Debug.LogError("Already a globalSettings in scene deleting: " + this.name);

            return;
        }

        Instance = this;


       // DontDestroyOnLoad(this.gameObject);


    }

    public void Start()
    {
        volume.profile.TryGet<Exposure>(out Exposure pExposure);

        exposure = pExposure;

        LoadSettings();
    }

    public void SetSoundVolume(float amount)
    {
        amount = Mathf.Clamp(amount, 0.0f, 1.0f);

        float soundDb = (amount * 100.0f) - 80.0f;
        mixer.SetFloat("MusicVolume", soundDb);

        PlayerPrefs.SetFloat("SoundVolume", amount);


    }

    public float GetSoundVolume()
    {
        return PlayerPrefs.GetFloat("SoundVolume");
    }

    public void SetSfxVolume(float amount)
    {
        amount = Mathf.Clamp(amount, 0.0f, 1.0f);

        float soundDb = (amount * 100.0f) - 80.0f;
        mixer.SetFloat("SFXVolume", soundDb);

        PlayerPrefs.SetFloat("SfxVolume", amount);


    }

    public float GetSfxVolume()
    {
        return PlayerPrefs.GetFloat("SfxVolume");
    }


    private float brightness = 0.5f;

    private Exposure exposure;

    public void SetBrightness(float amount)
    {
        amount = Mathf.Clamp(amount, 0.0f, 1.0f);
        PlayerPrefs.SetFloat("Brightness", amount);


        amount *= 4;
        amount -= 2;

        exposure.compensation.value = amount;

    }


    public float GetBrightness()
    {
        return PlayerPrefs.GetFloat("Brightness");
    }


    public void SaveSettings()
    {
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        onLoadOptions?.Invoke();


        Debug.Log(GetSfxVolume());
        SetSoundVolume(GetSoundVolume());
        SetSfxVolume(GetSfxVolume());
        SetBrightness(GetBrightness());


    }


    public void ResetSettings()
    {
        PlayerPrefs.SetFloat("SoundVolume", 0.8f);
        PlayerPrefs.SetFloat("SfxVolume", 0.8f);
        PlayerPrefs.SetFloat("Brightness", 0.5f);
    }
}
