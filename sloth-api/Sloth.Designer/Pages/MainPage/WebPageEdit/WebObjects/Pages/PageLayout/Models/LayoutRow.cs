using Sloth.Designer.Core;

namespace Sloth.Designer.Pages;
public class LayoutRow(int rowID, string rowRatio) : BaseViewModel
{

    private int rowID = rowID;
    public int RowID
    {
        get => rowID;
        set => SetProperty(ref rowID, value);
    }

    private string rowRatio = rowRatio;
    public string RowRatio
    {
        get => rowRatio;
        set => ValidateGridTemplate(ref rowRatio, value, rowRatio);
    }
}
