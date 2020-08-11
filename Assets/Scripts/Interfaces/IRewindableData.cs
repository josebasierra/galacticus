using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRewindableData<T>
{
    T GetRewindableData();
    void SetRewindableData(T data);
}
