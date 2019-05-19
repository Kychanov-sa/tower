using System;
using System.Windows;

namespace Tower.ViewModels
{
  public class LockWindow : BaseViewModel
  {
    public string Wheather
    {
      get { return _wheather; }
      set { SetField(ref _wheather, value); }
    }
    private string _wheather;

    public string CurrentTime
    {
      get { return _currentTime; }
      set { SetField(ref _currentTime, value); }
    }
    private string _currentTime;

    public string CurrentDate
    {
      get { return _currentDate; }
      set { SetField(ref _currentDate, value); }
    }
    private string _currentDate;

    internal void UpdateTime(DateTime now)
    {
      CurrentTime = now.ToString("HH:mm");
      CurrentDate = now.ToString("dddd, dd MMMM");
    }

    public LockWindow()
    {
      UpdateTime(DateTime.Now);
      Wheather = "7°, осадки";
    }
  }
}
