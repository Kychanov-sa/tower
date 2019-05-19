using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using MahApps.Metro.Controls;
using Tower.Views;
using Tower.Core.Threading;
using ToastNotifications;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using System.IO;

namespace Tower
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : MetroWindow
  {
    private Notifier _notifier;
    private WatchdogTimer _screenSaverTimer;
    private ViewModels.MainWindow _viewModel;

    public MainWindow()
    {
      _viewModel = new ViewModels.MainWindow();
      this.DataContext = _viewModel;

      _notifier = new Notifier(cfg =>
      {
        cfg.PositionProvider = new WindowPositionProvider(
          parentWindow: this,
          corner: Corner.BottomRight,
          offsetX: 10, offsetY: 10);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
          notificationLifetime: TimeSpan.FromSeconds(3),
          maximumNotificationCount: MaximumNotificationCount.FromCount(5));
        cfg.Dispatcher = this.Dispatcher;
      });

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
        }
        catch (Exception ex)
        {
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
      BackButton.Visibility = Visibility.Visible;
    }

    private void Specialist_Click(object sender, RoutedEventArgs e)
    {
      MainWindowPages.SelectedItem = SpecialistPage;
      PageTitle.Text = "Вызов специалиста";
      BackButton.Visibility = Visibility.Visible;
      OrderWizard.Visibility = Visibility.Visible;
      //SpecialistWasOrdered.Visibility = Visibility.Collapsed;
      FirstStepInput.SelectedItem = null;
      SecondStepInput.SelectedItem = null;
      SecondStepInput.IsEnabled = false;
      SecondStep.Foreground = Brushes.DimGray;
      SecondStepInput.Background = Brushes.DimGray;
      ThirdStepInput.Text = String.Empty;
      ThirdStep.Foreground = Brushes.DimGray;
      ThirdStepInput.Background = Brushes.DimGray;
      SendOrder.IsEnabled = false;
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
      MainWindowPages.SelectedItem = StartPage;
      PageTitle.Text = "Система управления многоквартирным домом";
      BackButton.Visibility = Visibility.Collapsed;
    }

    private void ColdWater_Click(object sender, RoutedEventArgs e)
    {
      MainWindowPages.SelectedItem = CounterPage;
      PageTitle.Text = "Холодная вода";
      _viewModel.ApplyColdWater();
      BackButton.Visibility = Visibility.Visible;
    }

    private void HotWater_Click(object sender, RoutedEventArgs e)
    {
      MainWindowPages.SelectedItem = CounterPage;
      PageTitle.Text = "Горячая вода";
      _viewModel.ApplyHotWater();
      BackButton.Visibility = Visibility.Visible;
    }

    private void Electricity_Click(object sender, RoutedEventArgs e)
    {
      MainWindowPages.SelectedItem = CounterPage;
      PageTitle.Text = "Электроэнергия";
      _viewModel.ApplyElectricity();
      BackButton.Visibility = Visibility.Visible;
    }

    private void Gas_Click(object sender, RoutedEventArgs e)
    {
      MainWindowPages.SelectedItem = CounterPage;
      PageTitle.Text = "Газ";
      _viewModel.ApplyGas();
      BackButton.Visibility = Visibility.Visible;
    }

    private void SendOrder_OnClick(object sender, RoutedEventArgs e)
    {
      //SpecialistWasOrdered.Visibility = Visibility.Visible;
      //BackButton_Click(sender, e);
      MainWindowPages.SelectedItem = StartPage;
      PageTitle.Text = "Система управления многоквартирным домом";
      BackButton.Visibility = Visibility.Collapsed;

      _notifier.ShowSuccess("Обращение успешно отправлено.");
      //OrderWizard.Visibility = Visibility.Collapsed;
    }

    private void FirstStepInput_OnSelected(object sender, RoutedEventArgs e)
    {
      SecondStepInput.IsEnabled = true;
      SecondStepInput.IsEnabled = true;
      SecondStep.Foreground = Brushes.White;
      SecondStepInput.Background = Brushes.White;
    }

    private void SecondStepInput_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      ThirdStep.Foreground = Brushes.White;
      ThirdStepInput.Background = Brushes.White;
      SendOrder.IsEnabled = true;
    }

    private void MetroWindow_Closed(object sender, EventArgs e)
    {
      _notifier.Dispose();
      DestroyTimers();
    }

    private void Reports_Click(object sender, RoutedEventArgs e)
    {
      MainWindowPages.SelectedItem = ReportsPage;
      PageTitle.Text = "Отчёты";
      BackButton.Visibility = Visibility.Visible;
    }

    private void ReportsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (sender is ListBox reportList)
      {
        if (reportList.SelectedItem is ViewModels.Report report)
        {          
          TextRange reportDocument = new TextRange(ReportPreview.Document.ContentStart, ReportPreview.Document.ContentEnd);
          using (var fs = new FileStream(report.FileName, FileMode.Open))
          {
            if (System.IO.Path.GetExtension(report.FileName).ToLower() == ".rtf")
              reportDocument.Load(fs, DataFormats.Rtf);
            else if (System.IO.Path.GetExtension(report.FileName).ToLower() == ".txt")
              reportDocument.Load(fs, DataFormats.Text);
            else
              reportDocument.Load(fs, DataFormats.Xaml);
          }
          ReportPreview.Visibility = Visibility.Visible;
          PrintReport.Visibility = Visibility.Visible;
        } else {
          ReportPreview.Visibility = Visibility.Hidden;
          PrintReport.Visibility = Visibility.Hidden;
        }
      }
    }
  }
}
