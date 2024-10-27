using System.Windows.Controls;

namespace Sloth.Designer.Services;
public interface IWindowService
{
    void CloseDialog();
    void ShowDialog(UserControl control);
    void LoadPage(UserControl control);
    void ClosePage();
}