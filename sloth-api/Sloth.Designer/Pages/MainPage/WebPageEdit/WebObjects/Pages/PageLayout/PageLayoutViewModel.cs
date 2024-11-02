using Sloth.Designer.Core;
using Sloth.Designer.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sloth.Designer.Pages;
public class PageLayoutViewModel : BaseViewModel
{
    public PageLayoutViewModel(IWindowService windowService, IWebPageStateService webPageStateService)
    {
        
    }

    private int columnCount = 1;
    public int ColumnCount
    {
        get => columnCount;
        set => ValidateCount(ref columnCount, value, rowCount);
    }

    private int rowCount = 1;
    public int RowCount
    {
        get => rowCount;
        set => ValidateCount(ref rowCount, value, rowCount);
    }

    private ObservableCollection<LayoutColumn> columns = [];
    public ObservableCollection<LayoutColumn> Columns
    {
        get => columns;
        set => SetProperty(ref columns, value);
    }

    private ObservableCollection<LayoutRow> rows = [];
    public ObservableCollection<LayoutRow> Rows
    {
        get => rows;
        set => SetProperty(ref rows, value);
    }

    private bool ValidateCount(ref int backingField, int newValue, int oldValue)
    {
        if (newValue < 1 || newValue > 20)
            return SetProperty(ref backingField, oldValue);

        var prop = SetProperty(ref backingField, newValue);

        SetColumns();
        SetRows();
        return prop;
    }

    private void SetColumns()
    {
        if (columnCount > Columns.Count)
            for (int i = Columns.Count; i < columnCount; i++)
            {
                Columns.Add(new(i+1, "1"));
            }
        else if (columnCount < Columns.Count)
            for (int i = Columns.Count; i > columnCount; i--) 
            { 
                Columns.Remove(Columns[i-1]);
            }
    }

    private void SetRows()
    {
        if (rowCount > rows.Count)
            for (int i = rows.Count; i < rowCount; i++)
            {
                Rows.Add(new(i+1, "1"));
            }
        else if (rowCount < rows.Count)
            for (int i = rows.Count; i > rowCount; i--)
            {
                Rows.Remove(rows[i-1]);
            }
    }

}       
