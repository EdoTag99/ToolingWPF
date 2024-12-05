using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using ToolingWPF.Commands;

namespace ToolingWPF.Model
{
    internal class SaveAsRecipeModel : Notifier
    {
        public SaveAsRecipeModel(ResponseBool Response, bool SelectedBtnVisibility, int SelectedPress)
        {
            this.response = Response;
            this.selectedBtnVisibility = SelectedBtnVisibility;
            this.selectedPress = SelectedPress;
        }

        private bool dbIsChecked = true;
        private bool jsonIsChecked;
        private bool selectedBtnVisibility;
        private string recipeName;
        private int selectedPress;
        private string filePath = "";
        private ResponseBool response;

        public bool DBIsChecked
        { get => dbIsChecked; internal set { dbIsChecked = value; OnPropertyChanged(); } }

        public bool JSONIsChecked
        { get => jsonIsChecked; internal set { jsonIsChecked = value; OnPropertyChanged(); } }

        public bool SelectedBtnVisibility
        { get => selectedBtnVisibility; internal set { selectedBtnVisibility = value; } }

        public string RecipeName
        { get => recipeName; set { recipeName = value; OnPropertyChanged(); } }

        internal async void Save(object obj)
        {
            Window win = (obj as Window);
            ResponseBool responseRequest = null;
            string format = "";
            string message = "";
            RadioButton dbRadio = win.FindName("DBRadioButton") as RadioButton;
            RadioButton jsonRadio = win.FindName("JSONRadioButton") as RadioButton;
            string recipeName = (win.FindName("RecipeNameTextBox") as TextBox).Text;

            if (dbRadio.IsChecked == true && jsonRadio.IsChecked == false)
            {
                format = "sql";
            }
            else if (dbRadio.IsChecked == false && jsonRadio.IsChecked == true)
            {
                format = "json";
            }

            if (string.IsNullOrEmpty(filePath))
            {
                try
                {
                    responseRequest = await AsyncRequest.SaveBarAsRecipe(selectedPress, format, recipeName);
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
            else
            {
                try
                {
                    responseRequest = await AsyncRequest.AddRecipe(format, recipeName, filePath);
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }

            if (responseRequest != null)
            {
                response.Data = responseRequest.Data;
                response.Status = responseRequest.Status;
                response.Message = responseRequest.Message;
            }
            else
            {
                response.Data = false;
                response.Message = message;
            }

            win.Close();
        }

        internal void SelectFile(object obj)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "Recipe",
                DefaultExt = ".json",
                Filter = "JSON |*.json"
            };

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                filePath = dialog.FileName;
            }
        }
    }
}