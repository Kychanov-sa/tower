using System;
using System.Collections.ObjectModel;

namespace Tower.ViewModels
{
  public class Question : BaseViewModel
  {
    public string Title
    {
      get { return _title; }
      set { SetField(ref _title, value); }
    }
    private string _title;

    public string Text
    {
      get { return _text; }
      set { SetField(ref _text, value); }
    }
    private string _text;

    public DateTime PublishDate
    {
      get { return _publishDate; }
      set { SetField(ref _publishDate, value); }
    }
    private DateTime _publishDate;

    public ObservableCollection<string> Answers { get; protected set; } = new ObservableCollection<string>();
  }
}
