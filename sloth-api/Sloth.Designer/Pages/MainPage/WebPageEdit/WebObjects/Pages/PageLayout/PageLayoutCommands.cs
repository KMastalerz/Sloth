using Sloth.Designer.Core;
using Sloth.Designer.Services;
using Sloth.Shared.Helpers;

namespace Sloth.Designer.Pages;
public static class PageLayoutCommands
{
    public class AddArea : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) 
        {
            if (parameter is not PageLayoutViewModel pageLayoutViewModel) return false;

            // check available area and taken area
            var fullArea = pageLayoutViewModel.Columns * pageLayoutViewModel.Rows;
            var takenArea = pageLayoutViewModel.GridAreas.Sum(a => a.SpanTo - a.SpanFrom + 1);

            return fullArea > takenArea;
        }

        protected override void ExecuteSync(object? parameter = null)
        {
            if (parameter is not PageLayoutViewModel pageLayoutViewModel) return;

            pageLayoutViewModel.GridAreas.Add(new()
            {
                ID = pageLayoutViewModel.GridAreas.Count() + 1
            });
        }
    }

    public class Clear(IWebPageStateService webPageStateService, IWindowService windowService) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            webPageStateService.WebPage!.Layout = null;
            windowService.CloseDialog();
        }
    }

    public class DeleteArea(PageLayoutViewModel pageLayoutViewModel) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            if (parameter is not GridArea gridArea) return;
            pageLayoutViewModel.GridAreas.Remove(gridArea);
        }
    }

    public class Save(IWebPageStateService webPageStateService, IWindowService windowService) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            if (parameter is not PageLayoutViewModel pageLayoutViewModel) return;

            var layout = new PageLayoutMatadata
            {
                Rows = pageLayoutViewModel.Rows,
                Columns = pageLayoutViewModel.Columns,
                RowsRatio = pageLayoutViewModel.RowsRatio.Select(r => r.RowRatio).ToList(),
                ColumnsRatio = pageLayoutViewModel.ColumnsRatio.Select(c => c.ColumnRatio).ToList(),
                GridAreas = pageLayoutViewModel.GridAreas.ToList()
            };

            webPageStateService.WebPage!.Layout = layout.SerializeToCamelCase();
            windowService.CloseDialog();
        }
    }
}
