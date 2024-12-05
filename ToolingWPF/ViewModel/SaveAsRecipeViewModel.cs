using System.Windows.Input;

using ToolingWPF.Commands;
using ToolingWPF.Model;

namespace ToolingWPF.ViewModel
{
    public class SaveAsRecipeViewModel
    {
        private SaveAsRecipeModel model;

        public SaveAsRecipeViewModel(ResponseBool Response, bool SelectedBtnVisibility, int SelectedPress)
        {
            this.model = new SaveAsRecipeModel(Response, SelectedBtnVisibility, SelectedPress);
        }

        public bool DBIsChecked
        { get { return model.DBIsChecked; } set { model.DBIsChecked = value; } }

        public bool JSONIsChecked
        { get { return model.JSONIsChecked; } set { model.JSONIsChecked = value; } }

        public bool SelectBtnVisibility
        { get => model.SelectedBtnVisibility; internal set { model.SelectedBtnVisibility = value; } }

        public string RecipeName
        { get => model.RecipeName; set { model.RecipeName = value; } }

        public ICommand SaveCommand => new RelayCommand<object>(model.Save);
        public ICommand SelectFileCommand => new RelayCommand<object>(model.SelectFile);
    }
}