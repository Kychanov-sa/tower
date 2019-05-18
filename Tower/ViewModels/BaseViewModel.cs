using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tower.ViewModels
{
  public abstract class BaseViewModel : INotifyPropertyChanged
  {
    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name)
    {
      if (string.IsNullOrWhiteSpace(name)) return;

      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    protected void OnPropertyChanged(params string[] propertyNames)
    {
      if (propertyNames == null || propertyNames.Length == 0) return;

      foreach (string name in propertyNames)
        OnPropertyChanged(name);
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
      if (EqualityComparer<T>.Default.Equals(field, value)) return false;
      field = value;
      OnPropertyChanged(propertyName);
      return true;
    }

    protected bool SetField<T>(T currentValue, T newValue, Action doSet, [CallerMemberName] string property = null)
    {
      if (EqualityComparer<T>.Default.Equals(currentValue, newValue)) return false;
      doSet.Invoke();
      OnPropertyChanged(property);
      return true;
    }

    #endregion
  }
}
