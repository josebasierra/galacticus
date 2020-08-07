using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] float maxValue = 100;
    [SerializeField] float regenerationPerSecond;

    [SerializeField] float currentValue = 0f;

    bool isOverheated = true;


    public bool Consume(float consumeValue)
    {
        if (isOverheated) return false;

        currentValue -= consumeValue;
        if (currentValue < 0) isOverheated = true;

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

    public bool IsOverheated()
    {
        return isOverheated;
    }


    void FixedUpdate()
    {
        currentValue = Mathf.Min(maxValue, currentValue + regenerationPerSecond * Time.fixedDeltaTime);
        if (currentValue > maxValue * 0.20) isOverheated = false;
    }
}
