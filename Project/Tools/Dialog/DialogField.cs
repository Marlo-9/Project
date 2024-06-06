using System.Windows.Controls;
using System.Windows.Data;
using TextBox = Wpf.Ui.Controls.TextBox;

namespace Project.Tools.Dialog;

public class DialogField
{
    public StackPanel StackPanel { get; set; }
    public TextBox TextBox { get; set; }
    public Label Label { get; set; }
    
    public DialogField(string name, string xName, string defaultValue = "")
    {
        StackPanel = new()
        {
            MaxWidth = 500
        };
        TextBox = new()
        {
            Name = xName + "Box",
            Text = defaultValue
        };
        Label = new()
        {
            Content = name
        };

        Label.SetBinding(Label.TargetProperty, new Binding()
        {
            ElementName = TextBox.Name
        });

        StackPanel.Children.Add(Label);
        StackPanel.Children.Add(TextBox);
    }

    public object GetContent()
    {
        return StackPanel;
    }

    public string GetText()
    {
        return TextBox.Text;
    }
}