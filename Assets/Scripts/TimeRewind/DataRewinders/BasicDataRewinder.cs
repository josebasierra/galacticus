using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDataRewinder<T>
{
    Rewindable rewindable;
    IRewindableData<T> rewindableData;

    LimitedStack<T> rewindableDataRegister;


    public BasicDataRewinder(Rewindable _rewindable, IRewindableData<T> _rewindableData)
    {
        rewindable = _rewindable;
        rewindableData = _rewindableData;

        rewindableDataRegister = new LimitedStack<T>(rewindable.MaxCapacity());

        rewindable.OnRewind += OnRewind;
        rewindable.OnRecord += OnRecord;
    }


    ~BasicDataRewinder()
    {
        rewindable.OnRewind -= OnRewind;
        rewindable.OnRecord -= OnRecord;
    }


    void OnRewind(float time)
    {
        rewindableData.SetRewindableData(rewindableDataRegister.Top());
        rewindableDataRegister.Pop();
    }


    void OnRecord()
    {
        rewindableDataRegister.Push(rewindableData.GetRewindableData());
    }

}
