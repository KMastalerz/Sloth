

namespace Sloth.Domain.Entities;
public class WebPage
{
    public string PageID { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string? SecurityTableID { get; set; }
    public string? Description { get; set; }

    /* External Properties */
    /// <summary>
    /// get SecurityTable where SecurityTableID = SecurityTableID
    /// </summary>
    public ICollection<SecurityTable> SecurityTables { get; set; } = [];
    /// <summary>
    /// get WebControl where PageID = PageID
    /// </summary>
    public ICollection<WebControl> WebControls { get; set; } = [];
    /// <summary>
    /// get WebPageSecurity where PageID = PageID
    /// </summary>
    public ICollection<WebPageSecurity> WebPageSecurities { get; set; } = [];
}
