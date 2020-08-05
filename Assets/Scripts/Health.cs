using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] float maxValue;
    [SerializeField] float currentValue;
    [SerializeField] GameObject deathEffect;


    public event Action OnDeath;
    bool isDead = false;
    bool isImmune = false;


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


    public void SetIsImmune(bool value)
    {
        isImmune = value;
    }


    void Start()
    {
        currentValue = maxValue;
    }
}
