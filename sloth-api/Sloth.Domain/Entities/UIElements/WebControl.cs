namespace Sloth.Domain.Entities;
public class WebControl
{
    /// <summary>
    /// references WebPage
    /// </summary>
    public string PageID { get; set; } = default!;
    public string ControlID { get; set; } = default!;
    /// <summary>
    /// references SecurityTable, If added and not overriten it will take flags from table security. 
    /// </summary>
    public string? SecurityTableID { get; set; }
    /// <summary>
    /// type protected by constant List<string> ControlTypes;
    /// </summary>
    public string ControlType { get; set; } = default!;
    public string ControlLabel { get; set; } = default!;
    public string ControlPlaceholder { get; set; } = default!;
    public string? Route { get; set; } = null;
    public string? RoutePageID { get; set; } = null;

    /* External Properties */
    /// <summary>
    /// get WebControlSecurities where SecurityTableID = SecurityTableID
    /// </summary>
    public ICollection<WebControlSecurity> WebControlSecurities { get; set; } = [];
    /// <summary>
    /// get Validations with help of WebControlValidation, WebControlValidation can be mapped by PageID and ControlID
    /// </summary>
    public ICollection<Validation> Validations { get; set; } = [];
    /// <summary>
    /// get SecurityTables where SecurityTableID = SecurityTableID and ControlName = ControlID, the problem here is that when SecurityTableID in Control is empty we have to check SecurityTableID in WebPage. 
    /// </summary>
    public ICollection<SecurityTable> SecurityTables { get; set; } = [];
    /// <summary>
    /// get Translations where ENUText = ControlLabel or ENUText = ControlPlaceholder
    /// </summary>
    public ICollection<Translation> Translations { get; set; } = [];
}
