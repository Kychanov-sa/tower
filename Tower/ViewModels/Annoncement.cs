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

    public string Text
    {
      get { return _text; }
      set { SetField(ref _text, value); }
    }
    private string _text;
  }
}
