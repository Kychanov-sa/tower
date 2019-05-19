using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Tower.Views
{
  /// <summary>
  /// Логика взаимодействия для Counter.xaml
  /// </summary>
  public partial class Counter : UserControl
  {
    protected ObservableCollection<ViewModels.Counter> Counters { get; set; }
    public Counter()
    {
      InitializeComponent();
    }

    private void Root_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      Counters = DataContext as ObservableCollection<ViewModels.Counter>;
    }

    private void SendData_Click(object sender, RoutedEventArgs e)
    {
      var newCounter = new ViewModels.Counter(Counters.Max(curCounter => curCounter.Date).AddMonths(1), Convert.ToDecimal(value_textBox.Text));
      value_textBox.Text = "";
      Counters.Add(newCounter);
    }
  }
}
