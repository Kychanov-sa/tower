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
using MahApps;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Tower.Views;
using Tower.Core.Threading;

namespace Tower
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : MetroWindow
  {
    private WatchdogTimer _screenSaverTimer;
    private ViewModels.MainWindow _viewModel;

    public MainWindow()
    {
      _viewModel = new ViewModels.MainWindow();
      this.DataContext = _viewModel;

      InitializeComponent();

      CreateTimers();

      InputManager.Current.PostProcessInput += (sender, e) =>
      {
        if (e.StagingItem.Input is MouseButtonEventArgs)
          GlobalClickEventHandler(sender, (MouseButtonEventArgs)e.StagingItem.Input);
      };
    }

    private void CreateTimers()
    {
        _screenSaverTimer = new WatchdogTimer(1 * 60 * 1000, ScreenSaver_Start);
        _screenSaverTimer.Start();
    }

    private void DestroyTimers()
    {
      if (_screenSaverTimer != null)
        _screenSaverTimer.Stop();
    }

    private void GlobalClickEventHandler(object sender, EventArgs e)
    {
      if (_screenSaverTimer != null)
        _screenSaverTimer.Reset();
    }

    void ScreenSaver_Start(object sender, EventArgs e)
    {
      this.Dispatcher.Invoke(new Action(() =>
      {
        try
        {
          //Запускаем хранитель экрана
          var screenSaverWindow = new LockWindow();
          screenSaverWindow.ShowDialog();
        } catch (Exception ex) {
          //ignore
          //Log.Exception(ex);
        }
      }));
    }

    private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
    {
      //Устанавливаем размер окна по размера экрана
      var primaryScreen = System.Windows.Forms.Screen.AllScreens[0];
      this.Left = (double)primaryScreen.WorkingArea.Left;
      this.Top = (double)primaryScreen.WorkingArea.Top;
      this.Width = (double)primaryScreen.WorkingArea.Width;
      this.Height = (double)primaryScreen.WorkingArea.Height;
    }

    private void Annoncements_Click(object sender, RoutedEventArgs e)
    {
      MainWindowPages.SelectedItem = AnnoncementsPage;
      PageTitle.Text = "Объявления";

    }
  }
}
