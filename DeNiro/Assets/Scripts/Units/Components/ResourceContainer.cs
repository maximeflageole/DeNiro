using UnityEngine;

public class ResourceContainer
{
    public float Max { get; protected set; }
    public float Current { get; protected set; }

    public void Init(float maxValue)
    {
        Init(maxValue, maxValue);
    }

    public void Init(float maxValue, float currentValue)
    {
        Max = maxValue;
        Current = currentValue;
    }

    public bool RemoveResource(float amount)
    {
        bool returnValue = amount >= Current;
        Current -= amount;
        Current = Mathf.Clamp(Current, 0, Max);
        return (returnValue);
    }

    public void AddResource(float amount)
    {
        Current += amount;
        Current = Mathf.Clamp(Current, 0, Max);
    }

}
