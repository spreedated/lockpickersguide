using Serilog;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using pp = LockpickersGuide.Logic;

namespace LockpickersGuide.Views
{
    /// <summary>
    /// Interaction logic for Preload.xaml
    /// </summary>
    public partial class Preload : Page, INotifyPropertyChanged
    {
        private string _LabelContent = "Loading please wait ...";
        public string LabelContent
        {
            get
            {
                return this._LabelContent;
            }
            set
            {
                this._LabelContent = value;
                this.OnPropertyChanged(nameof(this.LabelContent));
            }
        }

        private double _ProgressValue = 0;
        public double ProgressValue
        {
            get
            {
                return this._ProgressValue;
            }
            set
            {
                this._ProgressValue = value;
                this.OnPropertyChanged(nameof(this.ProgressValue));
            }
        }

        private Visibility _ProgressBarVisibility = Visibility.Visible;
        public Visibility ProgressBarVisibility
        {
            get
            {
                return this._ProgressBarVisibility;
            }
            set
            {
                this._ProgressBarVisibility = value;
                this.OnPropertyChanged(nameof(this.ProgressBarVisibility));
            }
        }

        private const string errorMessage = "Incomplete, no database available - shutting down in # seconds";
        private int countError = 10;
        private static readonly Timer dotTimer = new();

        public Preload()
        {
            InitializeComponent();
            this.DataContext = this;

            dotTimer.Elapsed += DotTimer_Elapsed;
            dotTimer.Interval = 500;
            dotTimer.Enabled = true;
            dotTimer.Start();

            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(1500);

                pp.Preload.PreloadStep += (o, e) => { this.Dispatcher.Invoke(() => { ProgressValue += (double)100 / pp.Preload.Steps; }); };
                pp.Preload.PreloadComplete += (o, e) => { this.Dispatcher.Invoke(() => { this.LabelContent = "Complete"; ProgressValue = 100; }); };

                await Task.Factory.StartNew(async () =>
                {
                    if (!pp.Preload.Load())
                    {
                        Log.Debug($"[View][Preload] Preload error");
                        this.Dispatcher.Invoke(() =>
                        {
                            dotTimer.Stop();
                            this.ProgressValue = 0;
                            this.ProgressBarVisibility = Visibility.Collapsed;
                        });

                        while (countError > 0)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                this.LabelContent = errorMessage.Replace("#", countError.ToString());
                            });
                            await Task.Delay(TimeSpan.FromSeconds(1));
                            countError--;
                        }
                        Environment.Exit(-1);
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
                LabelContent = this.LabelContent.Substring(0, this.LabelContent.LastIndexOf(' ') + 1) + new string('.', dotStatus);
                dotStatus++;

                if (dotStatus >= 4)
                {
                    dotStatus = 1;
                }
            });
        }

        #region WPF Stuff
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event NotifyCollectionChangedEventHandler CollectionChanged = delegate { };
        public void OnCollectionChanged(NotifyCollectionChangedAction action)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action));
        }
        #endregion
    }
}
