using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] int maxValue;
    [SerializeField] int currentValue;
    [SerializeField] GameObject deathEffect;


    public event Action OnDeath;
    bool isDead = false;
    bool isImmune = false;


    public void TakeDamage(int value)
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


    public int GetCurrentValue()
    {
        return currentValue;
    }


    public void SetCurrentValue(int value)
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
