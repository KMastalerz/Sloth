using MediatR;
using Sloth.Shared.Models.Items;

namespace Sloth.Application.Services.UIElements;
public class ListWebPagesQuery: IRequest<ListWebPageFull>;