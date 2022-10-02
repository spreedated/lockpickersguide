using LockpickersGuide.Logic;
using System.ComponentModel;
using System.Windows;

namespace LockpickersGuide.ViewLogic
{
    public class AdvancedWindow : Window
    {
        public bool IsModal { get; private set; }
        public new bool? ShowDialog()
        {
            this.IsModal = true;
            this.ConfigureModal();
            return base.ShowDialog();
        }

        private void ConfigureModal()
        {
            this.ShowInTaskbar = false;
            this.LocationChanged += this.Window_LocationChanged;
            this.Loaded += this.Window_Loaded;
            this.Closing += this.Window_Closing;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.IsModal && this.Owner.IsSet() && this.Owner.DataContext is IShadow)
            {
                ((IShadow)((AdvancedWindow)this.Owner).DataContext).GreyOut = true;
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (this.IsModal && this.Owner.IsSet() && this.Owner.DataContext is IShadow)
            {
                ((IShadow)((AdvancedWindow)this.Owner).DataContext).GreyOut = false;
            }
        }

        private void Window_LocationChanged(object sender, System.EventArgs e)
        {
            if (this.IsModal && this.Owner.IsSet())
            {
                this.Owner.Left = (this.Left + (this.Width / 2)) - (this.Owner.Width / 2);
                this.Owner.Top = (this.Top + (this.Height / 2)) - (this.Owner.Height / 2);
            }
        }
    }
}
