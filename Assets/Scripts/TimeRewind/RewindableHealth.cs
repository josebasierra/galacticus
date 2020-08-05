

public class RewindableHealth 
{
    Rewindable rewindable;
    Health health;

    LimitedStack<float> healthRegister;


    public RewindableHealth(Rewindable _rewindable, Health _health)
    {
        rewindable = _rewindable;
        health = _health;

        healthRegister = new LimitedStack<float>(rewindable.MaxCapacity());

        rewindable.OnRewind += OnRewind;
        rewindable.OnRecord += OnRecord;
    }


    ~RewindableHealth()
    {
        rewindable.OnRewind -= OnRewind;
        rewindable.OnRecord -= OnRecord;
    }


    void OnRewind(float time)
    {
        //TODO:  check if register empty?? Although Rewindable is checking it
        health.SetCurrentValue(healthRegister.Top());
        healthRegister.Pop();
    }


    void OnRecord()
    {
        healthRegister.Push(health.GetCurrentValue());
    }
}
