using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class VignetteFader : MonoBehaviour
{
    [SerializeField] private Volume _volumeProfile;
    private Sanity _sanity;
    private Vignette _vignette;
    private float _targetIntensity;

    private void Start()
    {
        _volumeProfile.profile.TryGet<Vignette>(out _vignette);
        if (_vignette == null) Debug.LogError("No vignete found!");
        _sanity = FindObjectOfType<Sanity>();
        if(_sanity == null)
        {
            Debug.LogWarning("No sanity object was found, destroying self - " + name);
            Destroy(this);
        }
    }

    private void Update()
    {
        _targetIntensity = _sanity.GetNormalizedSanity();
    }
    private void FixedUpdate()
    {
        _vignette.intensity.value = _targetIntensity * .1f + _vignette.intensity.value * .9f;
    }

    /// <summary>
    /// Sets the intensity of the vignette 
    /// </summary>
    /// <param name="value">Value has to be between 0 and 1</param>
    public void SetIntensity(float value)
    {
        //_vignette.intensity.value = Mathf.Clamp(value,0,1);
        _targetIntensity = Mathf.Clamp(value, 0, 1);
    }
}
