using Sloth.Designer.Constants;
using Sloth.Designer.Core;
using Sloth.Designer.Services;
using Sloth.Shared.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;
using static Sloth.Designer.Pages.PageLayoutCommands;

namespace Sloth.Designer.Pages;
public class PageLayoutViewModel : BaseViewModel
{
    public PageLayoutViewModel(IWindowService windowService, IWebPageStateService webPageStateService)
    {
        AddArea = new AddArea();
        Clear = new Clear(webPageStateService, windowService);
        DeleteArea = new DeleteArea(this);
        Save = new Save(webPageStateService, windowService);

        Initialize(webPageStateService);
    }

    private void Initialize(IWebPageStateService webPageStateService)
    {
        var layout = JsonHelper.TryConvert(webPageStateService.WebPage?.Layout ?? "", new PageLayoutMatadata())!;

        columns = layout.Columns;
        rows = layout.Rows;

        for (int i = 0; i < layout.ColumnsRatio.Count(); i++)
        {

            ColumnsRatio.Add(new(i + 1, layout.ColumnsRatio[i]));
        }

        for (int i = 0; i < layout.RowsRatio.Count(); i++)
        {
            RowsRatio.Add(new(i + 1, layout.RowsRatio[i]));
        }

        if (layout.GridAreas is not null)
        {
            GridAreas = new(layout.GridAreas);
        }

        SetColumns();
        SetRows();
    }

    private int columns;
    public int Columns
    {
        get => columns;
        set => ValidateCount(ref columns, value, columns);
    }

    private int rows;
    public int Rows
    {
        get => rows;
        set => ValidateCount(ref rows, value, rows);
    }

    private ObservableCollection<LayoutColumn> columnsRatio = [];
    public ObservableCollection<LayoutColumn> ColumnsRatio
    {
        get => columnsRatio;
        set => SetProperty(ref columnsRatio, value);
    }

    private ObservableCollection<LayoutRow> rowsRatio = [];
    public ObservableCollection<LayoutRow> RowsRatio
    {
        get => rowsRatio;
        set => SetProperty(ref rowsRatio, value);
    }

    private ObservableCollection<GridArea> gridAreas = [];
    public ObservableCollection<GridArea> GridAreas
    {
        get => gridAreas;
        set => SetProperty(ref gridAreas, value);
    }

    private Dictionary<string, string> areaTypes = PageConstants.AreaTypes;
    public Dictionary<string, string> AreaTypes
    {
        get => areaTypes;
        set => SetProperty(ref areaTypes, value);
    }

    public ICommand AddArea { get; }
    public ICommand Clear { get; }
    public ICommand DeleteArea { get; }
    public ICommand Save { get; }

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
        if (Columns > ColumnsRatio.Count)
            for (int i = ColumnsRatio.Count; i < Columns; i++)
            {
                ColumnsRatio.Add(new(i+1, "1"));
            }
        else if (Columns < ColumnsRatio.Count)
            for (int i = ColumnsRatio.Count; i > Columns; i--) 
            {
                ColumnsRatio.Remove(ColumnsRatio[i-1]);
            }
    }
    private void SetRows()
    {
        if (Rows > RowsRatio.Count)
            for (int i = RowsRatio.Count; i < Rows; i++)
            {
                RowsRatio.Add(new(i+1, "1"));
            }
        else if (Rows < RowsRatio.Count)
            for (int i = RowsRatio.Count; i > Rows; i--)
            {
                RowsRatio.Remove(RowsRatio[i-1]);
            }
    }
}       
