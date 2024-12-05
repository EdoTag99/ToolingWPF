using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ToolingWPF.Commands
{
    /// <summary>
    /// Classe per la creazione delle DependencyProperty per i comandi necessari
    /// </summary>
    public class EventBehaviourCommands
    {
        public static readonly DependencyProperty MouseMoveCommand = EventBehaviourFactory.CreateCommandExecutionEventBehaviour(UIElement.MouseMoveEvent, "MouseMoveCommand", typeof(EventBehaviourCommands));

        public static void SetMouseMoveCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MouseMoveCommand, value);
        }

        public static ICommand GetMouseMoveCommand(DependencyObject obj)
        {
            return obj.GetValue(MouseMoveCommand) as ICommand;
        }

        public static readonly DependencyProperty DragEnterCommand = EventBehaviourFactory.CreateCommandExecutionEventBehaviour(UIElement.DragEnterEvent, "DragEnterCommand", typeof(EventBehaviourCommands));

        public static void SetDragEnterCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DragEnterCommand, value);
        }

        public static ICommand GetDragEnterCommand(DependencyObject obj)
        {
            return obj.GetValue(DragEnterCommand) as ICommand;
        }

        public static readonly DependencyProperty DragOverCommand = EventBehaviourFactory.CreateCommandExecutionEventBehaviour(UIElement.DragOverEvent, "DragOverCommand", typeof(EventBehaviourCommands));

        public static void SetDragOverCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DragOverCommand, value);
        }

        public static ICommand GetDragOverCommand(DependencyObject obj)
        {
            return obj.GetValue(DragOverCommand) as ICommand;
        }

        public static readonly DependencyProperty DragLeaveCommand = EventBehaviourFactory.CreateCommandExecutionEventBehaviour(UIElement.DragLeaveEvent, "DragLeaveCommand", typeof(EventBehaviourCommands));

        public static void SetDragLeaveCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DragLeaveCommand, value);
        }

        public static ICommand GetDragLeaveCommand(DependencyObject obj)
        {
            return obj.GetValue(DragLeaveCommand) as ICommand;
        }

        public static readonly DependencyProperty DropCommand = EventBehaviourFactory.CreateCommandExecutionEventBehaviour(UIElement.DropEvent, "DropCommand", typeof(EventBehaviourCommands));

        public static void SetDropCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DropCommand, value);
        }

        public static ICommand GetDropCommand(DependencyObject obj)
        {
            return obj.GetValue(DropCommand) as ICommand;
        }

        public static readonly DependencyProperty SelectionChangedCommand = EventBehaviourFactory.CreateCommandExecutionEventBehaviour(ComboBox.SelectionChangedEvent, "SelectionChangedCommand", typeof(EventBehaviourCommands));

        public static void SetSelectionChangedCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(SelectionChangedCommand, value);
        }

        public static ICommand GetSelectionChangedCommand(DependencyObject obj)
        {
            return obj.GetValue(SelectionChangedCommand) as ICommand;
        }

        public static readonly DependencyProperty ToggleCommand = EventBehaviourFactory.CreateCommandExecutionEventBehaviour(ToggleButton.ClickEvent, "ToggleCommand", typeof(EventBehaviourCommands));

        public static void SetToggleCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(ToggleCommand, value);
        }

        public static ICommand GetToggleCommand(DependencyObject obj)
        {
            return obj.GetValue(ToggleCommand) as ICommand;
        }

        public static readonly DependencyProperty MouseEnterTPCommand = EventBehaviourFactory.CreateCommandExecutionEventBehaviour(Canvas.MouseEnterEvent, "MouseEnterTPCommand", typeof(EventBehaviourCommands));

        public static void SetMouseEnterTPCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MouseEnterTPCommand, value);
        }

        public static ICommand GetMouseEnterTPCommand(DependencyObject obj)
        {
            return obj.GetValue(MouseEnterTPCommand) as ICommand;
        }

        public static readonly DependencyProperty MouseLeaveTPCommand = EventBehaviourFactory.CreateCommandExecutionEventBehaviour(Canvas.MouseLeaveEvent, "MouseLeaveTPCommand", typeof(EventBehaviourCommands));

        public static void SetMouseLeaveTPCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MouseLeaveTPCommand, value);
        }

        public static ICommand GetMouseLeaveTPCommand(DependencyObject obj)
        {
            return obj.GetValue(MouseLeaveTPCommand) as ICommand;
        }
    }
}