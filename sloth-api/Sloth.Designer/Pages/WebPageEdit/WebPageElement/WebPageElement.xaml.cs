using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sloth.Designer.Pages;

/// <summary>
/// Interaction logic for WebPageElement.xaml
/// </summary>
public partial class WebPageElement : UserControl
{
    public WebPageElement()
    {
        InitializeComponent();
    }



    public object CommandParameter
    {
        get { return (object)GetValue(CommandParameterProperty); }
        set { SetValue(CommandParameterProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CommandParameter.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CommandParameterProperty =
        DependencyProperty.Register("CommandParameter", typeof(object), typeof(WebPageElement), new PropertyMetadata(null));


    public string Label
    {
        get { return (string)GetValue(LabelProperty); }
        set { SetValue(LabelProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Label.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register("Label", typeof(string), typeof(WebPageElement), new PropertyMetadata(string.Empty));

    public PackIconKind Icon
    {
        get { return (PackIconKind)GetValue(IconProperty); }
        set { SetValue(IconProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(PackIconKind), typeof(WebPageElement), new PropertyMetadata(null));


    public bool CanExpand
    {
        get { return (bool)GetValue(CanExpandProperty); }
        set { SetValue(CanExpandProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CanExpand.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CanExpandProperty =
        DependencyProperty.Register("CanExpand", typeof(bool), typeof(WebPageElement), new PropertyMetadata(false));

    public bool IsExpanded
    {
        get { return (bool)GetValue(IsExpandedProperty); }
        set { SetValue(IsExpandedProperty, value); }
    }

    // Using a DependencyProperty as the backing store for IsExpanded.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IsExpandedProperty =
        DependencyProperty.Register("IsExpanded", typeof(bool), typeof(WebPageElement), new PropertyMetadata(false));

    public bool CanEdit
    {
        get { return (bool)GetValue(CanEditProperty); }
        set { SetValue(CanEditProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CanEdit.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CanEditProperty =
        DependencyProperty.Register("CanEdit", typeof(bool), typeof(WebPageElement), new PropertyMetadata(false));

    public bool CanDelete
    {
        get { return (bool)GetValue(CanDeleteProperty); }
        set { SetValue(CanDeleteProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CanDelete.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CanDeleteProperty =
        DependencyProperty.Register("CanDelete", typeof(bool), typeof(WebPageElement), new PropertyMetadata(false));

    public bool CanAdd
    {
        get { return (bool)GetValue(CanAddProperty); }
        set { SetValue(CanAddProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CanAdd.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CanAddProperty =
        DependencyProperty.Register("CanAdd", typeof(bool), typeof(WebPageElement), new PropertyMetadata(false));

    public ICommand Edit
    {
        get { return (ICommand)GetValue(EditProperty); }
        set { SetValue(EditProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Edit.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty EditProperty =
        DependencyProperty.Register("Edit", typeof(ICommand), typeof(WebPageElement), new PropertyMetadata(null));

    public ICommand Delete
    {
        get { return (ICommand)GetValue(DeleteProperty); }
        set { SetValue(DeleteProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Delete.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DeleteProperty =
        DependencyProperty.Register("Delete", typeof(ICommand), typeof(WebPageElement), new PropertyMetadata(null));

    public ICommand Add
    {
        get { return (ICommand)GetValue(AddProperty); }
        set { SetValue(AddProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Add.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty AddProperty =
        DependencyProperty.Register("Add", typeof(ICommand), typeof(WebPageElement), new PropertyMetadata(null));

    public string AddTooltip
    {
        get { return (string)GetValue(AddTooltipProperty); }
        set { SetValue(AddTooltipProperty, value); }
    }

    // Using a DependencyProperty as the backing store for AddTooltip.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty AddTooltipProperty =
        DependencyProperty.Register("AddTooltip", typeof(string), typeof(WebPageElement), new PropertyMetadata(string.Empty));

    public string EditTooltip
    {
        get { return (string)GetValue(EditTooltipProperty); }
        set { SetValue(EditTooltipProperty, value); }
    }

    // Using a DependencyProperty as the backing store for EditTooltip.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty EditTooltipProperty =
        DependencyProperty.Register("EditTooltip", typeof(string), typeof(WebPageElement), new PropertyMetadata(string.Empty));

    public string DeleteTooltip
    {
        get { return (string)GetValue(DeleteTooltipProperty); }
        set { SetValue(DeleteTooltipProperty, value); }
    }

    // Using a DependencyProperty as the backing store for DeleteTooltip.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DeleteTooltipProperty =
        DependencyProperty.Register("DeleteTooltip", typeof(string), typeof(WebPageElement), new PropertyMetadata(string.Empty));
}
