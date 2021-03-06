﻿using System;
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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Windows.Interop;
using Tower.Core.Threading;
using System.Timers;
using System.Windows.Threading;

namespace Tower.Views
{
  /// <summary>
  /// Interaction logic for LockWindow.xaml
  /// </summary>
  public partial class LockWindow : MetroWindow
  {
    const int WM_SYSCOMMAND = 0x0112;
    const int SC_MOVE = 0xF010;

    private bool _isClosing;
    private HwndSource _sourceWindow;
    private WatchdogTimer _emergencyTimer;
    private DispatcherTimer _timeUpdateTimer;
    private ViewModels.LockWindow _viewModel;

    public LockWindow()
    {
      this.ShowMaxRestoreButton = false;
      this.ShowMinButton = false;
      this.IgnoreTaskbarOnMaximize = true;
      this.ShowCloseButton = false;
      this.ShowTitleBar = false;
      this.WindowState = System.Windows.WindowState.Maximized;
      this.WindowStyle = System.Windows.WindowStyle.None;
      this.ResizeMode = System.Windows.ResizeMode.NoResize;
      this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
      this._isClosing = false;

      this.SourceInitialized += SourceInitializedEventHandler;
      this.Closing += ClosingEventHandler;
      this.Closed += ClosedEventHandler;

      _viewModel = new ViewModels.LockWindow();
      this.DataContext = _viewModel;
      InputManager.Current.PreProcessInput += GlobalClickEventHandler;

      InitializeComponent();

      _emergencyTimer = new WatchdogTimer(30 * 1000, Emergency_Start);
      _emergencyTimer.Start();

      _timeUpdateTimer = new DispatcherTimer();
      _timeUpdateTimer.Interval = new TimeSpan(0, 0, 30);
      _timeUpdateTimer.IsEnabled = true;
      _timeUpdateTimer.Tick += TimeUpdateTimer_Tick; ;
      _timeUpdateTimer.Start();
    }

    private void TimeUpdateTimer_Tick(object sender, EventArgs e)
    {
      this.Dispatcher.Invoke(new Action(() =>
      {
        _viewModel.UpdateTime(DateTime.Now);
      }));
    }

    private void Emergency_Start(object sender, ElapsedEventArgs e)
    {
      this.Dispatcher.Invoke(new Action(() =>
      {
        Emergency.IsOpen = true;
      }));
    }

    private void SourceInitializedEventHandler(object sender, EventArgs e)
    {
      WindowInteropHelper helper = new WindowInteropHelper(this);
      _sourceWindow = HwndSource.FromHwnd(helper.Handle);
      _sourceWindow.AddHook(WndProc);
    }

    private void ClosedEventHandler(object sender, EventArgs e)
    {
      InputManager.Current.PreProcessInput -= GlobalClickEventHandler;
      if (_sourceWindow != null)
        _sourceWindow.RemoveHook(WndProc);

      if (_emergencyTimer != null)
      {
        _emergencyTimer.Stop();
        _emergencyTimer.Dispose();
      }

      if (_timeUpdateTimer != null)
      {
        _timeUpdateTimer.Stop();
      }
    }

    private void GlobalClickEventHandler(object sender, PreProcessInputEventArgs e)
    {
      if (e.StagingItem.Input is MouseButtonEventArgs)
      {
        if (!_isClosing)
          Close();
      }
    }

    private void ClosingEventHandler(object sender, System.ComponentModel.CancelEventArgs e)
    {
      //переходим в режим закрытия окна
      _isClosing = true;
    }

    private IntPtr WndProc(IntPtr windowHandler, int message, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
      switch (message)
      {
        case WM_SYSCOMMAND:
          int command = wParam.ToInt32() & 0xfff0;
          if (command == SC_MOVE)
          {
            handled = true;
          }
          break;
        default:
          break;
      }
      return IntPtr.Zero;
    }

    private void CloseEmergencyButton_Click(object sender, RoutedEventArgs e)
    {
      Emergency.IsOpen = false;
    }
  }
}
