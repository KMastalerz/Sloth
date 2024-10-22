namespace Sloth.Designer.Core;

public interface IBaseStateService
{
    void RegisterCallback<T>(BaseStateService.StateChangedDelegate<T> callback);
}