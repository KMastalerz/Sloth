using Sloth.Designer.Core;

namespace Sloth.Designer.Pages;
public class LayoutColumn(int columnID, string columnRatio) : BaseViewModel
{
    private int columnID = columnID;
    public int ColumnID
    {
        get => columnID;
        set => SetProperty(ref columnID, value);
    }

    private string columnRatio = columnRatio;
    public string ColumnRatio
    {
        get => columnRatio;
        set => ValidateGridTemplate(ref columnRatio, value, columnRatio);
    }
}
