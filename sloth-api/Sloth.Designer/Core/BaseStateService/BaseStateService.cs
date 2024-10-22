

namespace Sloth.Designer.Core;

public class BaseStateService : IBaseStateService
{
    // Generic delegate for state changes
    public delegate void StateChangedDelegate<T>(T updatedValue);

    // Dictionary to store callbacks based on the type of state
    private readonly Dictionary<Type, Delegate> _callbacks = new();

    // Method to register a callback for any state change
    public void RegisterCallback<T>(StateChangedDelegate<T> callback)
    {
        var key = typeof(T);
        if (_callbacks.ContainsKey(key))
        {
            _callbacks[key] = Delegate.Combine(_callbacks[key], callback);
        }
        else
        {
            _callbacks[key] = callback;
        }
    }

    // Method to notify all subscribers of a state change
    protected void NotifyStateChanged<T>(T updatedValue)
    {
        var key = typeof(T);
        if (_callbacks.TryGetValue(key, out var callback))
        {
            var typedCallback = (StateChangedDelegate<T>)callback;
            typedCallback?.Invoke(updatedValue); // Invoke the registered callbacks
        }
    }
}
