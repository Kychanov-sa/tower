

using System;

namespace Tower.ViewModels
{
  public class Counter : BaseViewModel
  {
    public DateTime Date
    {
      get { return _date; }
      set { SetField(ref _date, value); }
    }
    private DateTime _date;

    public decimal Value
    {
      get { return _value; }
      set { SetField(ref _value, value); }
    }
    private decimal _value;

    public Counter(DateTime date, decimal value)
    {
      Date = date;
      Value = value;
    }
  }
}
