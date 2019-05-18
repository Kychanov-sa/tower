using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Diagnostics;


using MahApps.Metro.Controls;
using System.Windows.Interop;

namespace Tower.Views
{
  public abstract class FullScreenWindow : MetroWindow
  {
    const int WM_SYSCOMMAND = 0x0112;
    const int SC_MOVE = 0xF010;

    private HwndSource _sourceWindow;

    public FullScreenWindow()
    {
      this.ShowMaxRestoreButton = false;
      this.ShowMinButton = false;
      this.IgnoreTaskbarOnMaximize = true;
      this.WindowState = System.Windows.WindowState.Maximized;
      this.WindowStyle = System.Windows.WindowStyle.None;
      this.ResizeMode = System.Windows.ResizeMode.NoResize;
      this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

      this.SourceInitialized += SourceInitializedEventHandler;
      this.Closed += ClosedEventHandler;
    }

    private void SourceInitializedEventHandler(object sender, EventArgs e)
    {
      WindowInteropHelper helper = new WindowInteropHelper(this);
      _sourceWindow = HwndSource.FromHwnd(helper.Handle);
      _sourceWindow.AddHook(WndProc);
    }

    private void ClosedEventHandler(object sender, EventArgs e)
    {
      if (_sourceWindow != null)
        _sourceWindow.RemoveHook(WndProc);
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
  }
}
