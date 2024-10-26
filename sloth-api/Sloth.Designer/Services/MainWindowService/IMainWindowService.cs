using System.Windows.Controls;

namespace Sloth.Designer.Services;
public interface IMainWindowService
{
    void CloseDialog();
    void ShowDialog(UserControl control);
    void LoadPage(UserControl control);
    void ClosePage();
}