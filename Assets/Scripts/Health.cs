﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour, IRewindableData<float>
{
    [SerializeField] protected float maxValue;
    [SerializeField] protected float currentValue;
    [SerializeField] GameObject deathEffect;
    [SerializeField] AudioClip deathSound;

    public event Action OnDeath;
    bool isDead = false;
    AudioComponent audioComponent;

    BasicDataRewinder<float> rewinder;

    public void TakeDamage(float value)
    {
        var rewindable = GetComponent<Rewindable>();
        if (rewindable != null && rewindable.IsRewinding()) return;

        //fuck time rewinding shit fucking all components
        if(!isDead) currentValue -= value;
        if (!isDead && currentValue <= 0 || (rewindable != null && !rewindable.IsDestroyed() && isDead))
        {
            Debug.Log("TakeDamage:" + value.ToString());
            isDead = true;
            OnDeath?.Invoke();

            if (deathEffect != null)
            {
                Instantiate(deathEffect).transform.position = transform.position;
            }
            if (audioComponent != null)
            {
                audioComponent.Play(deathSound);
            }

            GameManager.Instance().CustomDestroy(this.gameObject);
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    public float GetCurrentValue()
    {
        return currentValue;
    }

    public float GetMaxValue()
    {
        return maxValue;
    }


    public void SetCurrentValue(float value)
    {
        isDead = currentValue <= 0;
        currentValue = value;
    }


    void Start()
    {
        currentValue = maxValue;
        audioComponent = GetComponent<AudioComponent>();

        rewinder = new BasicDataRewinder<float>(GetComponentInParent<Rewindable>(), this); 
    }


    public float GetRewindableData()
    {
        return currentValue;
    }


    public void SetRewindableData(float data)
    {
        SetCurrentValue(data);
    }
}
