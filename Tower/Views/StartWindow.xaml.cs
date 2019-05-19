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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Windows.Interop;
using Tower.Core.Threading;
using System.Timers;
using System.Windows.Threading;

namespace Tower.Views
{
  /// <summary>
  /// Interaction logic for StartWindow.xaml
  /// </summary>
  public partial class StartWindow : MetroWindow
  {
    public StartWindow()
    {
      InitializeComponent();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
      this.Close();
    }
  }
}
