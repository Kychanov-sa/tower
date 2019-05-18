using System;
using System.Windows;

namespace Tower.ViewModels
{
  public class Annoncement : BaseViewModel
  {
    public string Title
    {
      get { return _title; }
      set { SetField(ref _title, value); }
    }
    private string _title;

    public DateTime PublishDate
    {
      get { return _publishDate; }
      set { SetField(ref _publishDate, value); }
    }
    private DateTime _publishDate;

    public string Text
    {
      get { return _text; }
      set { SetField(ref _text, value); }
    }
    private string _text;

    public bool IsRead
    {
      get { return _isRead; }
      set { SetField(ref _isRead, value); OnPropertyChanged("TitleWeight"); }
    }
    private bool _isRead;

    public bool IsImportant
    {
      get { return _isImportant; }
      set { SetField(ref _isImportant, value); OnPropertyChanged("FlagVisibility"); }
    }
    private bool _isImportant;

    public FontWeight TitleWeight
    {
      get { return (IsRead ? FontWeights.Regular : FontWeights.Bold); }
    }

    public Visibility FlagVisibility
    {
      get { return IsImportant ? Visibility.Visible : Visibility.Hidden;  }
    }
  }
}
