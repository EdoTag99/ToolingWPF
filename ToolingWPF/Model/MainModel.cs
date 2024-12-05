using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using ToolingWPF.Commands;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using ToolingWPF.View;
using System.Windows.Shapes;

namespace ToolingWPF.Model
{
    internal class MainModel : Notifier
    {
        #region ctor

        public MainModel()
        {
            GetPressBars();
            GetMagazines();
            GetRecipes();
            Response.Data = true;
            Response.Message = "Client Ready!";
        }

        #endregion ctor

        #region Properties

        private ObservableCollection<ToolPress> toolPresses;
        private ObservableCollection<MagazineTool> magazineTools;
        private ObservableCollection<Recipe> recipes;
        private ObservableCollection<int> pressBars;
        private ObservableCollection<int> magazines;
        private ResponseBool response;
        private Recipe selectedRecipe;
        private int selectedPressItem;
        private int selectedMagazineItem;
        private int selectedPress;
        private int selectedMagazine = -1;
        private bool magazineMode;
        private bool loadMagazine;

        public ObservableCollection<ToolPress> ToolPresses
        {
            get
            {
                if (toolPresses == null)
                {
                    toolPresses = new ObservableCollection<ToolPress>();
                }
                return toolPresses;
            }
            internal set
            {
                toolPresses = value; OnPropertyChanged();
            }
        }

        public ObservableCollection<MagazineTool> MagazineTools
        {
            get
            {
                if (magazineTools == null)
                {
                    magazineTools = new ObservableCollection<MagazineTool>();
                    GetStatusMagazine();
                }
                return magazineTools;
            }
            internal set
            {
                magazineTools = value; OnPropertyChanged();
            }
        }

        public ObservableCollection<Recipe> Recipes
        {
            get
            {
                if (recipes == null)
                {
                    recipes = new ObservableCollection<Recipe>();
                    GetRecipes();
                }
                return recipes;
            }
            internal set
            {
                recipes = value; OnPropertyChanged();
            }
        }

        public ObservableCollection<int> PressBars

        {
            get
            {
                if (pressBars == null)
                {
                    pressBars = new ObservableCollection<int>();
                    GetPressBars();
                }
                return pressBars;
            }
            internal set
            {
                pressBars = value; OnPropertyChanged();
            }
        }

        public ObservableCollection<int> Magazines
        {
            get
            {
                if (magazines == null)
                {
                    magazines = new ObservableCollection<int>();
                    GetMagazines();
                }
                return magazines;
            }
            internal set
            {
                magazines = value; OnPropertyChanged();
            }
        }

        public ResponseBool Response
        {
            get
            {
                if (response == null)
                {
                    response = new ResponseBool();
                }
                return response;
            }
            internal set
            {
                response = value; OnPropertyChanged();
            }
        }

        public Recipe SelectedRecipe
        {
            get
            {
                if (selectedRecipe == null)
                {
                    selectedRecipe = new Recipe();
                }
                return selectedRecipe;
            }
            internal set
            {
                selectedRecipe = value; OnPropertyChanged();
            }
        }

        public int SelectedPressItem
        {
            get
            {
                return selectedPressItem;
            }
            internal set
            {
                if (pressBars.Count != 0 && value > 0)
                {
                    selectedPressItem = value;
                }
                OnPropertyChanged();
            }
        }

        public int SelectedMagazineItem
        {
            get
            {
                return selectedMagazineItem;
            }
            internal set
            {
                if (magazines.Count != 0 && value > 0)
                {
                    selectedMagazineItem = value;
                }
                OnPropertyChanged();
            }
        }

        public int SelectedPress
        { get => selectedPress; internal set { selectedPress = value; OnPropertyChanged(); GetStatusPress(); } }

        public int SelectedMagazine
        { get => selectedMagazine; internal set { selectedMagazine = value; OnPropertyChanged(); GetStatusMagazine(); } }

        public bool MagazineMode
        { get => magazineMode; internal set { magazineMode = value; OnPropertyChanged(); } }

        #endregion Properties

        #region Methods

        internal async void Validation(object obj)
        {
            ResponseBool responseRequest;
            responseRequest = await AsyncRequest.Validation();

            SetResponse(responseRequest);
        }

        internal async void AddTool(double obj)
        {
            var mt = MagazineTools.First(x => x.Width == obj);
            ResponseBool responseRequest = new ResponseBool();
            if (mt.Check)
            {
                responseRequest = await AsyncRequest.AddToolFirstFreePosition(SelectedPress, (int)mt.Width, SelectedMagazine);
            }
            else if (!mt.Check)
            {
                if (int.TryParse(mt.Position, out int position))
                {
                    responseRequest = await AsyncRequest.AddTool(SelectedPress, (int)mt.Width, position, SelectedMagazine);
                }
                else
                {
                    responseRequest.Status = 200;
                    responseRequest.Message = "Invalid Position";
                    responseRequest.Data = false;
                }
            }
            SetResponse(responseRequest);

            GetStatusPress();
            GetStatusMagazine();
        }

        internal async void RemoveTool(int obj)
        {
            ResponseBool responseRequest = new ResponseBool();
            var tp = ToolPresses.First(x => x.Position == obj);

            try
            {
                responseRequest = await AsyncRequest.RemoveTool(SelectedPress, tp.Width, tp.Position);
            }
            catch (Exception ex)
            {
                responseRequest.Data = false;
                responseRequest.Status = 500;
                responseRequest.Message = ex.Message;
            }
            SetResponse(responseRequest);

            GetStatusPress();
            GetStatusMagazine();
        }

        internal async void RemoveAllTools(object obj)
        {
            ResponseBool responseRequest;
            responseRequest = await AsyncRequest.RemoveAllTools(SelectedPress);

            SetResponse(responseRequest);

            if (Response != null && Response.Data)
            {
                GetStatusPress();
                GetStatusMagazine();
            }
        }

        internal void AddRecipe(object obj)
        {
            SaveAsRecipeDialog dialog = new SaveAsRecipeDialog(Response, true, SelectedPress);
            dialog.ShowDialog();
            GetRecipes();
        }

        internal async void DeleteRecipe(object obj)
        {
            ResponseBool responseRequest = new ResponseBool();

            if (SelectedRecipe != null)
            {
                responseRequest = await AsyncRequest.DeleteRecipe(SelectedRecipe);
            }
            else
            {
                responseRequest.Data = false;
                responseRequest.Status = 200;
                responseRequest.Message = "Invalid Recipe!";
            }
            if (Response.Message != null)
            {
                SetResponse(responseRequest);
            }

            if (Response.Data)
            {
                GetRecipes();
            }
        }

        internal async void ImportRecipe(object obj)
        {
            ResponseBool responseRequest = new ResponseBool();

            if (SelectedRecipe != null)
            {
                responseRequest = await AsyncRequest.ImportRecipe(SelectedPress, SelectedRecipe);
            }
            else
            {
                responseRequest.Data = false;
                responseRequest.Status = 200;
                responseRequest.Message = "Invalid Recipe!";
            }
            if (Response.Message != null)
            {
                SetResponse(responseRequest);
            }

            if (Response.Data)
            {
                GetStatusPress();
                GetStatusMagazine();
            }
        }

        internal void SaveBarAsRecipe(object obj)
        {
            SaveAsRecipeDialog dialog = new SaveAsRecipeDialog(Response, false, SelectedPress);
            dialog.ShowDialog();
            GetRecipes();
        }

        internal void Reload(object obj)
        {
            PressBars.Clear();
            Magazines.Clear();
            Recipes.Clear();

            GetMagazines();

            GetPressBars();

            GetRecipes();
        }

        internal void Toggle(object obj)
        {
            if (MagazineMode)
            {
                SelectedMagazine = Magazines.FirstOrDefault();
            }
            else
            {
                SelectedMagazine = -1;
            }
            GetStatusPress();
            GetStatusMagazine();
        }

        internal void MouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                UIElement element = e.OriginalSource as UIElement;

                if (element != null)
                {
                    var canvas = FindParent<Canvas>(element);
                    if (canvas != null)
                    {
                        DataObject data;

                        #region codiceStefano

                        //if (canvas.DataContext is IDataFormat)
                        //{
                        //    data = (canvas.DataContext as IDataFormat).GetDataObject(canvas);
                        //    DragDrop.DoDragDrop(canvas, data, DragDropEffects.Move);
                        //}
                        //else
                        //{
                        //}

                        #endregion codiceStefano

                        if (canvas.DataContext.GetType().Name == "MagazineTool")
                        {
                            data = new DataObject("MagazineCanvasFormat", canvas);
                        }
                        else if (canvas.DataContext.GetType().Name == "ToolPress")
                        {
                            data = new DataObject("ToolPressCanvasFormat", canvas);
                        }
                        else
                        {
                            data = new DataObject("Null", null);
                        }
                        DragDrop.DoDragDrop(canvas, data, DragDropEffects.Move);
                    }
                }
            }
        }

        internal async void Drop(DragEventArgs e)
        {
            ResponseBool responseRequest = new ResponseBool() { Data = false, Status = 500, Message = "Error!" };
            if (e.Source != null && e.Source.GetType() != typeof(Canvas) || !(e.Source as Canvas).Name.Equals("PressBar"))
            {
                if (e.Data.GetDataPresent("ToolPressCanvasFormat"))
                {
                    Canvas canvas = e.Data.GetData("ToolPressCanvasFormat") as Canvas;
                    if (canvas != null)
                    {
                        ToolPress tp = canvas.DataContext as ToolPress;
                        responseRequest = await AsyncRequest.RemoveTool(SelectedPress, tp.Width, tp.Position);

                        SetResponse(responseRequest);
                    }
                }
            }
            else
            {
                if (e.Data.GetDataPresent("MagazineCanvasFormat") && e.Source != null && e.Source.GetType() == typeof(Canvas) && (e.Source as Canvas).Name.Equals("PressBar"))
                {
                    Canvas canvas = e.Data.GetData("MagazineCanvasFormat") as Canvas;
                    if (canvas != null)
                    {
                        var dropPosition = e.GetPosition((UIElement)e.Source);

                        responseRequest = await AsyncRequest.AddTool(SelectedPress, (int)canvas.Width, (int)dropPosition.X, SelectedMagazine);
                    }
                }
                else if (e.Data.GetDataPresent("ToolPressCanvasFormat"))
                {
                    Canvas canvas = e.Data.GetData("ToolPressCanvasFormat") as Canvas;
                    if (canvas != null)
                    {
                        ToolPress tp = canvas.DataContext as ToolPress;
                        await AsyncRequest.RemoveTool(SelectedPress, tp.Width, tp.Position);
                        var dropPosition = e.GetPosition((UIElement)e.Source);
                        responseRequest = await AsyncRequest.AddTool(SelectedPress, tp.Width, (int)dropPosition.X, SelectedMagazine);
                    }
                }
            }
            SetResponse(responseRequest);

            GetStatusPress();
            GetStatusMagazine();
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent as T;
        }

        internal void MouseEnterTP(MouseEventArgs e)
        {
            var canvas = e.OriginalSource as Canvas;
            Button button = canvas.FindName("RemoveToolBtn") as Button;
            if (button != null)
            {
                if (!button.IsVisible)
                {
                    button.Visibility = Visibility.Visible;
                }

                Rectangle rectangle = canvas.FindName("RecatngleToolPress") as Rectangle;
                if (rectangle != null)
                {
                    rectangle.Fill = Brushes.LightYellow;
                    rectangle.Stroke = Brushes.OrangeRed;
                }
            }
        }

        internal void MouseLeaveTP(MouseEventArgs e)
        {
            var canvas = e.OriginalSource as Canvas;
            Button button = canvas.FindName("RemoveToolBtn") as Button;
            if (button != null)
            {
                if (button.IsVisible)
                {
                    button.Visibility = Visibility.Hidden;
                }
            }

            Rectangle rectangle = canvas.FindName("RecatngleToolPress") as Rectangle;
            if (rectangle != null)
            {
                rectangle.Fill = Brushes.DarkGray;
                rectangle.Stroke = Brushes.Black;
            }
        }

        private async void GetStatusPress()
        {
            ResponseTP responseRequest = await AsyncRequest.GetStatusPress(SelectedPress);
            if (responseRequest != null && responseRequest.Data != null)
            {
                ToolPresses.Clear();
                foreach (ToolPress tp in responseRequest.Data)
                {
                    ToolPresses.Add(tp);
                }
            }
        }

        private async void GetStatusMagazine()
        {
            ResponseMT responseRequest = await AsyncRequest.GetStatusMagazine(MagazineMode ? SelectedMagazine : -1);

            if (responseRequest != null && responseRequest.Data != null)
            {
                foreach (MagazineTool mt in responseRequest.Data)
                {
                    foreach (MagazineTool tool in MagazineTools)
                    {
                        if (mt.Width == tool.Width)
                        {
                            mt.Check = tool.Check;

                            break;
                        }
                    }
                }

                MagazineTools.Clear();

                foreach (MagazineTool mt in responseRequest.Data)
                {
                    MagazineTools.Add(mt);
                }
            }
            else if (!loadMagazine && (responseRequest == null || responseRequest.Data == null))
            {
                loadMagazine = true;
                LoadMagazine();
            }
        }

        private async void GetRecipes()
        {
            try
            {
                List<Recipe> recipesRequest = await AsyncRequest.GetRecipes();

                if (recipesRequest != null)
                {
                    Recipes.Clear();
                    foreach (Recipe recipe in recipesRequest)
                    {
                        Recipes.Add(recipe);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void LoadMagazine()
        {
            ResponseBool response = await AsyncRequest.LoadMagazine();
            if (response != null && response.Data)
            {
                GetStatusMagazine();
            }
        }

        private async void GetPressBars()
        {
            var result = await AsyncRequest.GetPressBars();
            if (result != null && PressBars.Count != result.Count())
            {
                SelectedPress = result[0].PressID;
                GetStatusPress();

                try
                {
                    foreach (var press in result)
                    {
                        PressBars.Add(press.PressID);
                    }
                    SelectedPressItem = result.First(x => x.PressID != 0).PressID;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async void GetMagazines()
        {
            var result = await AsyncRequest.GetMagazines();
            if (result != null && magazines.Count != result.Count)
            {
                try
                {
                    foreach (var id in result)
                    {
                        Magazines.Add(id);
                    }
                    SelectedMagazineItem = result.First(x => x != 0);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            SelectedMagazine = -1;
            GetStatusMagazine();
        }

        private void SetResponse(ResponseBool responseRequest)
        {
            if (responseRequest != null && responseRequest.Message != null)
            {
                Response.Status = responseRequest.Status;

                Response.Message = responseRequest.Message;

                Response.Data = responseRequest.Data;
            }
        }

        #endregion Methods
    }
}