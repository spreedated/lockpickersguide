using System.Windows;

namespace LockpickersGuide.ViewLogic
{
    public class AdvancedWindow : Window
    {
        public bool IsModal { get; private set; }
        public new bool? ShowDialog()
        {
            this.IsModal = true;
            return base.ShowDialog();
        }
    }
}
