using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


[Serializable]
public class FloatUnityEvent : UnityEvent<float>
{

}


public class Sanity : MonoBehaviour
{
    [SerializeField] private float sanity = 100.0f;
    [SerializeField] private float reduceSpeed = 1.0f;
    private float initialSanityAmount;
    private float initialReduceSpeed;
    private float maxSanity;
    private bool coroutinePlaying;

    public UnityEvent OnZeroSanity;

    public FloatUnityEvent OnNormalizedSanityChanged;

    private void Awake()
    {
        initialSanityAmount = sanity;
        initialReduceSpeed = reduceSpeed;
        InitializeDamagables();
    }

    void Start()
    {
        maxSanity = sanity;
    }
    // Update is called once per frame
    void Update()
    {
        ReduceSanity(reduceSpeed);
    }


    //Private functions
    private void InitializeDamagables()
    {
        Damagable[] damagables = FindObjectsOfType<Damagable>();
        for (int i = 0; i < damagables.Length; i++)
        {
            damagables[i].Initialize(this);
        }
    }
    
    private IEnumerator ResetSanityToStandard()
    {

        float sanitySpeed = reduceSpeed;
        ChangeSanitySpeed(0);

        while(sanity < maxSanity)
        {
            sanity++;
            Debug.LogWarning("teste");
            yield return 0;
        }

        ChangeSanitySpeed(sanitySpeed);
    }
     

    //Public functions
    /// <summary>
    /// Decreasy sanity by some value
    /// </summary>
    /// <param name="val">Value by which sanity will be decreased</param>
    public void DecreaseSanity(float val)
    {
        sanity -= val < 0 ? 0 : val;

        OnNormalizedSanityChanged?.Invoke(GetNormalizedSanity());
    }


    public void ChangeSanitySpeed(float reduceValue)
    {
        reduceSpeed = reduceValue;
    }


    public void ResetSanitySpeed()
    {
        reduceSpeed = initialReduceSpeed;
    }
    public void ResetSanityAmount()
    {
        Debug.LogWarning("test");
        StartCoroutine(ResetSanityToStandard());
        OnNormalizedSanityChanged?.Invoke(GetNormalizedSanity());
    }


    public void ReduceSanity(float amount)
    {
        sanity -= amount * Time.deltaTime;
        if (sanity <= 0)
        {
            Debug.Log("Sanity zero invoked");
            OnZeroSanity?.Invoke();
            ResetSanityAmount();
            return;
        }
        sanity = sanity < 0 ? 0 : sanity;

        OnNormalizedSanityChanged?.Invoke(GetNormalizedSanity());
    }
    public void AddSanity(float amount)
    {
        sanity += amount * Time.deltaTime;
        sanity = Mathf.Min(100.0f, sanity);

        OnNormalizedSanityChanged?.Invoke(GetNormalizedSanity());
    }
    public float GetNormalizedSanity()
    {
        return 1 - (sanity / maxSanity);
    }
}
