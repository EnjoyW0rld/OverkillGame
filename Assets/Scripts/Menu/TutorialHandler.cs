using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialHandler : MonoBehaviour
{
    [SerializeField] private CanvasGroup legControlls, jumpControlls, sanityTutorial, enemyTutorial;
    [SerializeField] private float fadeTime;
    private Gamepad[] gamepads;

    private enum TutorialState { Leg, Jump, Sanity, Enemy, Finished }
    private TutorialState state;

    [Header("Leg controlls variables")]
    private bool legsMoved;
    [SerializeField] private float legControllsTimer;
    private float legControllsCurrentTime;

    [Header("Jump controlls variables")]
    [SerializeField] private float jumpControllsTimer;
    private float jumpControllsCurrentTime;
    private bool frogJumped;

    [Header("Sanity tutorial variables")]
    [SerializeField] private float sanityTutorialTimer;
    private float sanityTutorialCurrentTime;

    [Header("Enemy tutorial variables")]
    [SerializeField] private float enemyTutorialTimer;
    private float enemyTutorialCurrentTime;


    private JumpFrog jumpFrog;

    private void Start()
    {
        gamepads = Gamepad.all.ToArray();
        StartCoroutine(Fade(legControlls, 1));
        jumpFrog = FindObjectOfType<JumpFrog>();
        jumpFrog.OnJumped.AddListener(SetJumped);
    }
    private void Update()
    {
        switch (state)
        {
            case TutorialState.Leg:
                LegTutorialControll();
                break;
            case TutorialState.Jump:
                JumpTutorialControll();
                break;
            case TutorialState.Sanity:
                SanityTutorialControll();
                break;
            case TutorialState.Finished:
                jumpFrog.OnJumped.RemoveListener(SetJumped);
                Destroy(gameObject);
                break;
            case TutorialState.Enemy:
                EnemyTutorialControll();
                break;
        }
    }

    private void LegTutorialControll()
    {
        if (gamepads[0].leftStick.ReadValue().magnitude > 0)
        {
            legsMoved = true;
        }

        legControllsCurrentTime += Time.deltaTime;

        if (legsMoved && legControllsCurrentTime > legControllsTimer)
        {
            StartCoroutine(Fade(legControlls, 0, () =>
            {
                this.state = TutorialState.Jump;
                StartCoroutine(Fade(jumpControlls, 1));
            }));
        }

    }
    private void JumpTutorialControll()
    {
        jumpControllsCurrentTime += Time.deltaTime;
        if (frogJumped && jumpControllsCurrentTime > jumpControllsTimer)
        {
            StartCoroutine(Fade(jumpControlls, 0, () =>
            {
                StartCoroutine(Fade(sanityTutorial, 1));
                state = TutorialState.Sanity;
            }));
        }
    }
    private void SanityTutorialControll()
    {
        sanityTutorialCurrentTime += Time.deltaTime;
        if (sanityTutorialCurrentTime > sanityTutorialTimer)
        {
            StartCoroutine(Fade(sanityTutorial, 0, () =>
            {
                StartCoroutine(Fade(enemyTutorial, 1));
                state = TutorialState.Enemy;
            }));
        }
    }
    private void EnemyTutorialControll()
    {
        enemyTutorialCurrentTime += Time.deltaTime;
        if (enemyTutorialCurrentTime > sanityTutorialTimer)
        {
            StartCoroutine(Fade(enemyTutorial, 0, () =>
            {
                state = TutorialState.Finished;
            }));
        }
    }

    private void SetJumped()
    {
        frogJumped = true;
    }

    private IEnumerator Fade(CanvasGroup targ, float targetValue, Action action = null)
    {
        float initialValue = targ.alpha;
        float timePassed = 0;
        while (initialValue != targetValue)
        {
            timePassed += Time.deltaTime;
            targ.alpha = Mathf.Lerp(initialValue, targetValue, timePassed / fadeTime);
            if (timePassed > fadeTime) break;
            yield return null;
        }
        targ.alpha = targetValue;
        if (action != null)
        {
            action();
        }
    }
}