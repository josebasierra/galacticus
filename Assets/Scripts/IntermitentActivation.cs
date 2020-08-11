using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermitentActivation : MonoBehaviour, IRewindableData<float>
{
    [SerializeField] float timeBetweenSwitch = 1f;
    [SerializeField] float timeWithoutSwitch = 0;
    IItem item;

    BasicDataRewinder<float> rewinder;


    void Start()
    {
        item = GetComponent<IItem>();

        rewinder = new BasicDataRewinder<float>(GetComponentInParent<Rewindable>(), this);
    }


    void FixedUpdate()
    {
        timeWithoutSwitch += Time.fixedDeltaTime;
        if (timeWithoutSwitch > timeBetweenSwitch)
        {
            timeWithoutSwitch = 0;
            item.Activate(!item.IsActivated());
        }
    }


    public float GetRewindableData()
    {
        return timeWithoutSwitch;
    }


    public void SetRewindableData(float data)
    {
        timeWithoutSwitch = data;
    }
}
