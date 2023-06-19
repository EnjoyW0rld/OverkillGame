using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{

    public static FadeInOut Instance;

    [SerializeField] Animator animator;

    public delegate void FadedOut();

    FadedOut onFadedOut;

    bool currentlyFading = false;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            Debug.LogWarning("Duplicate, deleting:" + gameObject.name);
            return;
        }

        Instance = this;
    }


    public void Fade(FadedOut fadeOutDelegate)
    {
        onFadedOut += fadeOutDelegate;

        if (currentlyFading) return;

        animator.Play("Fader");
        currentlyFading = true;

    }

    public void DoDelegates()
    {
        if (onFadedOut == null) return;
        onFadedOut();
        RemoveDelegates();
        
        //Could be error, as it can be faded out whilst fading in
        currentlyFading = false;
    }  
    

    public void RemoveDelegates()
    {
        onFadedOut = null;
    }

  
}
