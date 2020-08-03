using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] int maxValue;
    [SerializeField] int currentValue;

    public event Action OnDeath;
    bool isDead = false;

    //Rewind...
    Rewindable rewindable;
    List<int> timeCuts; //TODO: Save only changes, not every iteration/fixedUpdate


    public void TakeDamage(int value)
    {
        currentValue -= value;
        if (currentValue <= 0 && !isDead)
        {
            //death...
            Debug.Log("DEATH TRIGGERED");
            isDead = false;
            OnDeath?.Invoke();
            GameManager.Instance().CustomDestroy(this.gameObject);
        }
    }

    void Start()
    {
        timeCuts = new List<int>();
        currentValue = maxValue;
    }

    void OnEnable()
    {
        rewindable = GetComponent<Rewindable>();

        rewindable.OnRewind += OnRewind;
        rewindable.OnRecord += OnRecord;
    }

    void OnDisable()
    {
        rewindable.OnRewind -= OnRewind;
        rewindable.OnRecord -= OnRecord;
    }

    void OnRewind(float time)
    {
        var timeCut = timeCuts[timeCuts.Count - 1];
        currentValue = timeCut;
        isDead = currentValue < 0;
        timeCuts.RemoveAt(timeCuts.Count - 1);
    }

    void OnRecord()
    {
        if (rewindable.IsAtMaxCapacity())
        {
            timeCuts.RemoveAt(0);
        }
        timeCuts.Add(currentValue);
    }
}
