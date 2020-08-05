using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] float maxValue = 100;
    [SerializeField] float regenerationPerSecond;

    [SerializeField] float currentValue;


    public bool Consume(float consumeValue)
    {
        if (currentValue < consumeValue) return false;

        currentValue -= consumeValue;
        return true;
    }


    public float GetCurrentValue()
    {
        return currentValue;
    }


    public float GetMaxValue()
    {
        return maxValue;
    }


    void Start()
    {
        currentValue = maxValue;
    }


    void FixedUpdate()
    {
        currentValue = Mathf.Min(maxValue, currentValue + regenerationPerSecond * Time.fixedDeltaTime);
    }
}
