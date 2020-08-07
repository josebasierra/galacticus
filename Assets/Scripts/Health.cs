using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] float maxValue;
    [SerializeField] float currentValue;
    [SerializeField] GameObject deathEffect;
    [SerializeField] AudioClip deathSound;

    public event Action OnDeath;
    bool isDead = false;
    AudioComponent audioComponent;


    public void TakeDamage(float value)
    {
        var rewindable = GetComponent<Rewindable>();
        if (rewindable != null && rewindable.IsRewinding()) return;

        //fuck time rewinding shit fucking all components
        if(!isDead) currentValue -= value;
        if (!isDead && currentValue <= 0 || (rewindable != null && !rewindable.IsDestroyed() && isDead))
        {
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
    }
}
