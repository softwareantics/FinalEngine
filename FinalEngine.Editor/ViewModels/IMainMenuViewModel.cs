using System.Windows.Input;

namespace FinalEngine.Editor.ViewModels
{
    public interface IMainMenuViewModel
    {
        ICommand ExitCommand { get; }
    }
}