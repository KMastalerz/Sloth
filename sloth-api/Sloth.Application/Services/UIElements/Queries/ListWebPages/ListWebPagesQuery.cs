using MediatR;
using Sloth.Shared.Models;

namespace Sloth.Application.Services.UIElements;
public class ListWebPagesQuery: IRequest<ListWebPagesItem>;