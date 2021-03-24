
using System;

public abstract class AbstractSequence
{
    internal abstract void Start();
    internal abstract void Update(float t);
}

public class Sequence<T> : AbstractSequence
{
    internal T FromValue { get; private set; } = default;
    internal Func<T> GetFromValue { get; private set; }
    internal Action<T, float> OnUpdateCallback { get; private set; }

    internal override void Start()
    {
        if (GetFromValue != null)
        {
            FromValue = GetFromValue.Invoke();
        }
    }

    internal override void Update(float t)
    {
        OnUpdateCallback?.Invoke(FromValue, t);
    }

    public Sequence<T> From(Action<T, float> callback)
    {
        OnUpdateCallback = callback;
        return this;
    }
}