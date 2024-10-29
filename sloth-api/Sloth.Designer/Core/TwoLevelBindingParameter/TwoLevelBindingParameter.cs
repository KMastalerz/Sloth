namespace Sloth.Designer.Core;
internal class TwoLevelBindingParameter(object child, object parent)
{
    public object Child { get; } = child;
    public object Parent { get; } = parent;
}
