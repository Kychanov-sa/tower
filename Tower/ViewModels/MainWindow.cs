using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Tower.ViewModels
{
  public class MainWindow : BaseViewModel
  {
    public ObservableCollection<Report> Reports { get; set; } = new ObservableCollection<ViewModels.Report>();
    public ObservableCollection<Annoncement> Annoncements { get; set; } = new ObservableCollection<ViewModels.Annoncement>();
    public ObservableCollection<Counter> Counters { get; protected set; } = new ObservableCollection<Counter>();
    public ObservableCollection<Question> Questions { get; protected set; } = new ObservableCollection<Question>();

    public IEnumerable<Annoncement> UnreadAnnoncements { get { return from a in Annoncements where !a.IsRead select a; } }
    public IEnumerable<Annoncement> ImportantAnnoncements { get { return from a in Annoncements where a.IsImportant select a; } }

    protected IList<Counter> CountersForColdWater { get; set; } = new List<Counter>();
    protected IList<Counter> CountersForHotWater { get; set; } = new List<Counter>();

    protected IList<Counter> CountersForElectricity { get; set; } = new List<Counter>();

    protected IList<Counter> CountersForGas { get; set; } = new List<Counter>();

    public MainWindow()
    {
      Annoncements.Add(new ViewModels.Annoncement()
      {
        Title = "Снова собрание жильцов",
        Text = "Уважаемые жильцы! 32 ноября 2024г. в помещении подвала по адресу ул.Ленина, д.16 состоится собрание жильцов нашего дома. На собрании будут обсуждаться организационные вопросы создания в нашем доме Товарищества собственников жилья. В проведении собрания примет участие представитель ООО 'Безругие Бобры'. На собрание приглашаются все желающие. Инициатор проведения собрания Захапугин И.В.",
        IsRead = false,
        PublishDate = DateTime.Now.AddDays(-3),
        IsImportant = true
      });

      Annoncements.Add(new ViewModels.Annoncement()
      {
        Title = "Не муссорить",
        Text = "Уважаемые жильцы! Убедительная просьба не выкидывать мусор из окон квартир. С уважением, администрация этого зоопарка.",
        IsRead = false,
        PublishDate = DateTime.Now.AddDays(-2).AddHours(3),
        IsImportant = false
      });

      Annoncements.Add(new ViewModels.Annoncement()
      {
        Title = "Продам таксу",
        Text = "Продам таксу, можно по частям. Самомвывоз. т.32-54-23",
        IsRead = true,
        PublishDate = DateTime.Now.AddDays(-4).AddHours(-1).AddMinutes(-32),
        IsImportant = false
      });

      Questions.Add(new Question()
      {
        Title = "Что делать с Зинаидой из 14-й квартиры?",
        Text = "Зинаида Фёдоровна из квартиры 14 снова попыталась сломать камеры видеонаблюдения на 3-м этаже. Это уже пятый случай подобного вандализма с её стороны. Оправдывает она это тем, что через камеры её облучают зомби лучами с Лубянки. Нужно раз и навсегда закрыть этот вопрос.",
        PublishDate = DateTime.Now.AddDays(-2).AddHours(3)
      });
      Questions[0].Answers.Add("Понять и простить");
      Questions[0].Answers.Add("Отправить на лечение");
      Questions[0].Answers.Add("Отключить облучение из камер");


      var startDate = new DateTime(2018, 1, 1, 0, 0, 0);
      var randomizer = new Random();
      int prevRandomValueForColdWater = 0;
      int prevRandomValueForHotWater = 0;
      int prevRandomValueForElectricity = 0;
      int prevRandomValueForGas = 0;
      for (int i = 0; i != 12; i++)
      {
        int currentRandomValueForColdWater = randomizer.Next(100, 500);
        int currentRandomValueForHotWater = randomizer.Next(300, 700);
        int currentRandomValueForElectricity = randomizer.Next(200, 300);
        int currentRandomValueForGas = randomizer.Next(200, 300);
        CountersForColdWater.Add(new Counter(startDate.AddMonths(i), prevRandomValueForColdWater + currentRandomValueForColdWater));
        CountersForHotWater.Add(new Counter(startDate.AddMonths(i), prevRandomValueForHotWater + currentRandomValueForHotWater));
        CountersForElectricity.Add(new Counter(startDate.AddMonths(i), prevRandomValueForElectricity + currentRandomValueForElectricity));
        CountersForGas.Add(new Counter(startDate.AddMonths(i), prevRandomValueForGas + currentRandomValueForGas));
        prevRandomValueForColdWater += currentRandomValueForColdWater;
        prevRandomValueForHotWater += currentRandomValueForHotWater;
        prevRandomValueForElectricity += currentRandomValueForElectricity;
        prevRandomValueForGas += currentRandomValueForGas;
      }

      Reports.Add(new ViewModels.Report()
      {
        Title = "Годовой отчет",
        FileName = "Resources\\Reports\\obrazets-godovogo-otcheta-UK.rtf",
        PublishDate = DateTime.Now.AddDays(-4).AddHours(-1).AddMinutes(-32)
      });

      Reports.Add(new ViewModels.Report()
      {
        Title = "Годовой план работ",
        FileName = "Resources\\Reports\\obrazets-godovogo-plana-rabot.rtf",
        PublishDate = DateTime.Now.AddDays(-2).AddHours(3),
      });

      Reports.Add(new ViewModels.Report()
      {
        Title = "Отчет об эффективности персонала",
        FileName = "Resources\\Reports\\obrazets-godovogo-otcheta-UK.rtf",
        PublishDate = DateTime.Now.AddDays(-34).AddHours(3),
      });
    }

    public void ReadAnnoncement(Annoncement annoncement)
    {
      annoncement.IsRead = true;
      OnPropertyChanged("UnreadAnnoncements");
    }

    public void ApplyColdWater()
    {
      Counters.Clear();
      Counters = new ObservableCollection<Counter>(CountersForColdWater);
      OnPropertyChanged(nameof(Counters));
    }

    public void ApplyHotWater()
    {
      Counters.Clear();
      Counters = new ObservableCollection<Counter>(CountersForHotWater);
      OnPropertyChanged(nameof(Counters));
    }

    public void ApplyElectricity()
    {
      Counters.Clear();
      Counters = new ObservableCollection<Counter>(CountersForElectricity);
      OnPropertyChanged(nameof(Counters));
    }

    public void ApplyGas()
    {
      Counters.Clear();
      Counters = new ObservableCollection<Counter>(CountersForGas);
      OnPropertyChanged(nameof(Counters));
    }
  }
}
