using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using ToolingWPF.Classes;
using ToolingWPF.Commands;
using ToolingWPF.Model;
using ToolingWPF.View;

namespace ToolingWPF.ViewModel
{
    internal class MainViewModel : Notifier
    {
        #region ctor

        public MainViewModel()
        {
            model = new MainModel();
        }

        #endregion ctor

        #region Properties

        private readonly MainModel model;

        public ObservableCollection<ToolPress> ToolPresses
        { get => model.ToolPresses; set { model.ToolPresses = value; OnPropertyChanged(); } }

        public ObservableCollection<MagazineTool> MagazineTools
        { get => model.MagazineTools; set { model.MagazineTools = value; OnPropertyChanged(); } }

        public ObservableCollection<Recipe> Recipes
        { get => model.Recipes; set { model.Recipes = value; OnPropertyChanged(); } }

        public ObservableCollection<int> PressBars
        { get => model.PressBars; set { model.PressBars = value; OnPropertyChanged(); } }

        public ObservableCollection<int> Magazines
        { get => model.Magazines; set { model.Magazines = value; OnPropertyChanged(); } }

        public ResponseBool Response
        { get => model.Response; set { model.Response = value; OnPropertyChanged(); } }

        public Recipe SelectedRecipe
        { get => model.SelectedRecipe; set { model.SelectedRecipe = value; OnPropertyChanged(); } }

        public int SelectedPressItem
        { get => model.SelectedPressItem; set { model.SelectedPressItem = value; OnPropertyChanged(); } }

        public int SelectedMagazineItem
        { get => model.SelectedMagazineItem; set { model.SelectedMagazineItem = value; OnPropertyChanged(); } }

        public int SelectedPress
        { get => model.SelectedPress; set { model.SelectedPress = value; OnPropertyChanged(); } }

        public int SelectedMagazine
        { get => model.SelectedMagazine; set { model.SelectedMagazine = value; OnPropertyChanged(); } }

        public bool MagazineMode
        { get => model.MagazineMode; set { model.MagazineMode = value; OnPropertyChanged(); } }

        #endregion Properties

        #region Commands

        private ICommand validationCommand;
        private ICommand addToolCommand;
        private ICommand removeToolCommand;
        private ICommand removeAllToolCommand;
        private ICommand addRecipeCommand;
        private ICommand deleteRecipeCommand;
        private ICommand importRecipeCommand;
        private ICommand saveBarAsRecipeCommand;
        private ICommand reloadCommand;
        private ICommand toggleCommand;
        private ICommand mouseMoveCommand;
        private ICommand dropCommand;
        private ICommand mouseEnterTPCommand;
        private ICommand mouseLeaveTPCommand;

        public ICommand ValidationCommand
        { get { return validationCommand ?? (validationCommand = new RelayCommand<object>(model.Validation)); } }

        public ICommand AddToolCommand
        { get { return addToolCommand ?? (addToolCommand = new RelayCommand<double>(model.AddTool)); } }

        public ICommand RemoveToolCommand
        { get { return removeToolCommand ?? (removeToolCommand = new RelayCommand<int>(model.RemoveTool)); } }

        public ICommand RemoveAllToolsCommand
        { get { return removeAllToolCommand ?? (removeAllToolCommand = new RelayCommand<object>(model.RemoveAllTools)); } }

        public ICommand AddRecipeCommand
        { get { return addRecipeCommand ?? (addRecipeCommand = new RelayCommand<object>(model.AddRecipe)); } }

        public ICommand DeleteRecipeCommand
        { get { return deleteRecipeCommand ?? (deleteRecipeCommand = new RelayCommand<object>(model.DeleteRecipe)); } }

        public ICommand ImportRecipeCommand
        { get { return importRecipeCommand ?? (importRecipeCommand = new RelayCommand<object>(model.ImportRecipe)); } }

        public ICommand SaveBarAsRecipeCommand
        { get { return saveBarAsRecipeCommand ?? (saveBarAsRecipeCommand = new RelayCommand<object>(model.SaveBarAsRecipe)); } }

        public ICommand ReloadCommand
        { get { return reloadCommand ?? (reloadCommand = new RelayCommand<object>(model.Reload)); } }

        public ICommand ToggleCommand
        { get { return toggleCommand ?? (toggleCommand = new RelayCommand<object>(model.Toggle)); } }

        public ICommand MouseMoveCommand
        { get { return mouseMoveCommand ?? (mouseMoveCommand = new RelayCommand<MouseEventArgs>(model.MouseMove)); } }

        public ICommand DropCommand
        { get { return dropCommand ?? (dropCommand = new RelayCommand<DragEventArgs>(model.Drop)); } }

        public ICommand MouseEnterTPCommand
        { get { return mouseEnterTPCommand ?? (mouseEnterTPCommand = new RelayCommand<MouseEventArgs>(model.MouseEnterTP)); } }

        public ICommand MouseLeaveTPCommand
        { get { return mouseLeaveTPCommand ?? (mouseLeaveTPCommand = new RelayCommand<MouseEventArgs>(model.MouseLeaveTP)); ; } }

        #endregion Commands
    }
}