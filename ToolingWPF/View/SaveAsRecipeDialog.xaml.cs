using System.Windows;
using ToolingWPF.Model;
using ToolingWPF.ViewModel;

namespace ToolingWPF.View
{
    /// <summary>
    /// Interaction logic for SaveAsRecipeDialog.xaml
    /// </summary>
    public partial class SaveAsRecipeDialog : Window
    {
        public SaveAsRecipeDialog(ResponseBool Response, bool Visibility, int SelectedPress)
        {
            InitializeComponent();
            SaveAsRecipeViewModel vm = new SaveAsRecipeViewModel(Response, Visibility, SelectedPress);
            this.DataContext = vm;
        }
    }
}