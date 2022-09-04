using pp = LockpickersGuide.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;

namespace LockpickersGuide.Views
{
    /// <summary>
    /// Interaction logic for Preload.xaml
    /// </summary>
    public partial class Preload : Page
    {
        private static readonly Timer dotTimer = new();
        public Preload()
        {
            InitializeComponent();

            dotTimer.Elapsed += DotTimer_Elapsed;
            dotTimer.Interval = 500;
            dotTimer.Enabled = true;
            dotTimer.Start();

            Task.Factory.StartNew(async ()=>
            {
                await Task.Delay(1500);

                pp.Preload.PreloadStep += (o, e) => { this.Dispatcher.Invoke(() => { PRG_Progress.Value += 10; }); };
                pp.Preload.PreloadComplete += (o, e) => { this.Dispatcher.Invoke(() => { LBL_Loading.Content = "Complete"; PRG_Progress.Value = 100; }); };

                await Task.Factory.StartNew(() =>
                {
                    if (pp.Preload.Load())
                    {
                        //TODO: handle error
                    }
                });

                dotTimer.Stop();
            });
        }

        private int dotStatus = 3;
        private void DotTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                string tx = LBL_Loading.Content.ToString();
                LBL_Loading.Content = tx.Substring(0, tx.LastIndexOf(' ')+1) + new string('.', dotStatus);
                dotStatus++;

                if (dotStatus >= 4)
                {
                    dotStatus = 1;
                }
            });
        }
    }
}
