using System.Timers;

namespace Tower.Core.Threading
{
  public class WatchdogTimer : Timer
  {
    private double _timeout;

    public WatchdogTimer()
      : base()
    {
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="Timeout">Таймаут.</param>
    public WatchdogTimer(double Timeout)
      : base(Timeout)
    {
      this.AutoReset = false;
      this._timeout = Timeout;
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="Timeout">Таймаут.</param>
    public WatchdogTimer(double Timeout, ElapsedEventHandler Handler)
      : this(Timeout)
    {
      this.Elapsed += Handler;
    }

    /// <summary>
    /// Сбрасывает таймер.
    /// </summary>
    public void Reset()
    {
      // Resetting the interval property
      // forces the timer to restart.
      this.Interval = this._timeout;
    }
  }
}
