using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower.ViewModels
{
  public class MainWindow : BaseViewModel
  {
    public ObservableCollection<Report> Reports { get; set; } = new ObservableCollection<ViewModels.Report>();
    public ObservableCollection<Annoncement> Annoncements { get; set; } = new ObservableCollection<ViewModels.Annoncement>();

    public IEnumerable<Annoncement> UnreadAnnoncements { get { return from a in Annoncements where !a.IsRead select a; } }
    public IEnumerable<Annoncement> ImportantAnnoncements { get { return from a in Annoncements where a.IsImportant select a; } }

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
  }
}
