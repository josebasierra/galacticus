using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermitentActivation : MonoBehaviour
{
    [SerializeField] float timeBetweenSwitch = 1f;
    [SerializeField] float timeWithoutSwitch = 0;
    IItem item;

    Rewindable rewindable;
    LimitedStack<float> timeWithoutSwitchRegister;


    void Start()
    {
        item = GetComponent<IItem>();
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


    void OnEnable()
    {
        if (rewindable == null)
        {
            rewindable = GetComponentInParent<Rewindable>();
        }
        timeWithoutSwitchRegister = new LimitedStack<float>(rewindable.MaxCapacity());
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
        timeWithoutSwitch = timeWithoutSwitchRegister.Top();
        timeWithoutSwitchRegister.Pop();
    }


    void OnRecord()
    {
        timeWithoutSwitchRegister.Push(timeWithoutSwitch);
    }
}
