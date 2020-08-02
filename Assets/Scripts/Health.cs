using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxValue;
    int currentValue;

    //Rewind...
    Rewindable rewindable;
    List<int> timeCuts; //TODO: Save only changes, not every iteration/fixedUpdate


    public void TakeDamage(int value)
    {
        currentValue -= value;
    }

    private void Start()
    {
        timeCuts = new List<int>();
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
        timeCuts.RemoveAt(timeCuts.Count - 1);
    }

    void OnRecord()
    {
        if (rewindable.IsTimeRecordingFull())
        {
            timeCuts.RemoveAt(0);
        }
        timeCuts.Add(currentValue);
    }
}
