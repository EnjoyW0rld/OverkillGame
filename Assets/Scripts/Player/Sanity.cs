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

    private bool fading = false;

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
    /*
    private IEnumerator ChangeReductionSpeed(float reduceValue, float time)
    {
        coroutinePlaying = true;
        reduceSpeed = reduceValue;
        yield return new WaitForSeconds(time);
        reduceSpeed = initialReduceSpeed;
        coroutinePlaying = false;
    }
     */

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

    /*
    /// <summary>
    /// Change sanity decrease speed from outside the class
    /// </summary>
    /// <param name="reduceValue">New value by what sanity will be reduced</param>
    /// <param name="time">Time for how long effect will last in seconds</param>
    public void ChangeSanitySpeed(float reduceValue, float time)
    {
        if (!coroutinePlaying) StartCoroutine(ChangeReductionSpeed(reduceValue, time));
    }
     */

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
        sanity = initialSanityAmount;
        OnNormalizedSanityChanged?.Invoke(GetNormalizedSanity());
        
        fading = false;
    }

    public void ResetSanityAmountFader()
    {
        fading = true;
        FadeInOut.Instance.Fade(ResetSanityAmount);
        FadeInOut.Instance.Fade(OnZeroSanity.Invoke);
    }


    public void ReduceSanity(float amount)
    {
        if (fading) return;

        sanity -= amount * Time.deltaTime;

        Debug.Log("_______________");
        Debug.Log("Sanity amount: " + sanity);
        if (sanity <= 0)
        {

           // Debug.Log("Sanity zero invoked");
            
            ResetSanityAmountFader();
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
