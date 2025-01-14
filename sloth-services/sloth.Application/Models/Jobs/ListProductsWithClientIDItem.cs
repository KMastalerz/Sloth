using sloth.Application.Models.Miscellaneous;

namespace sloth.Application.Models.Jobs;
public class ListProductsWithClientIDItem(IEnumerable<ListItem> products)
{
    public IEnumerable<ListItem> Products { get; set; } = products;
}
