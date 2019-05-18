using System;
using System.Windows;

namespace Tower.ViewModels
{
  public class Report : BaseViewModel
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

    public string FileName
    {
      get { return _fileName; }
      set { SetField(ref _fileName, value); }
    }
    private string _fileName;
  }
}
