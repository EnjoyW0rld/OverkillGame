using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanity : MonoBehaviour
{
    [SerializeField] float sanity = 100.0f;
    [SerializeField] float reduceSpeed = 1.0f;
    private float _initialReduceSpeed;
    private float _maxSanity;
    private bool _coroutinePlaying;

    private void Awake()
    {
        _initialReduceSpeed = reduceSpeed;
        InitializeDamagables();
    }

    void Start()
    {
        //_fader = FindObjectOfType<VignetteFader>();
        //if (_fader == null) Debug.LogWarning("No VignetteFader found, ignore this message if don`t want to use one");
        _maxSanity = sanity;
    }
    // Update is called once per frame
    void Update()
    {
        ReduceSanity(reduceSpeed);
        //if (_fader != null) _fader.SetIntensity(1 - (sanity / _maxSanity));
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
    private IEnumerator ChangeReductionSpeed(float reduceValue, float time)
    {
        _coroutinePlaying = true;
        reduceSpeed = reduceValue;
        yield return new WaitForSeconds(time);
        reduceSpeed = _initialReduceSpeed;
        _coroutinePlaying = false;
    }

    //Public functions
    /// <summary>
    /// Change sanity decrease speed from outside the class
    /// </summary>
    /// <param name="reduceValue">New value by what sanity will be reduced</param>
    /// <param name="time">Time for how long effect will last in seconds</param>
    public void ChangeSanitySpeed(float reduceValue,float time)
    {
        if (!_coroutinePlaying) StartCoroutine(ChangeReductionSpeed(reduceValue,time));
    }

    public void ReduceSanity(float amount)
    {
        sanity -= amount * Time.deltaTime;
        sanity = sanity < 0 ? 0 : sanity;
    }
    public void AddSanity(float amount)
    {

        sanity += amount * Time.deltaTime;

        sanity = Mathf.Min(100.0f, sanity);
    }
    public float GetNormalizedSanity()
    {
        return 1 - (sanity / _maxSanity);
    }
}
