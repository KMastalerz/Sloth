namespace Sloth.Designer.Pages;

public class PageLayoutMatadata
{
    public int Columns { get; set; } = 1;
    public int Rows { get; set; } = 1;
    public List<string> ColumnsRatio { get; set; } = [];
    public List<string> RowsRatio { get; set; } = [];
    public List<GridArea>? GridAreas { get; set; } = null;

}

public class GridArea
{
    public int ID { get; set; } = default!;
    public string Type { get; set; } = default!;
    public int SpanFrom { get; set; } = default!;
    public int SpanTo { get; set; } = default!;
}