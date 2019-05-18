using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower.ViewModels
{
  public class MainWindow
  {
    public ObservableCollection<Annoncement> Annoncement { get; set; } = new ObservableCollection<ViewModels.Annoncement>();

    public MainWindow()
    {
      Annoncement.Add(new ViewModels.Annoncement()
      {
        Title = "Снова собрание жильцов",
        Text = "Уважаемые жильцы! 32 ноября 2024г. в помещении подвала по адресу ул.Ленина, д.16 состоится собрание жильцов нашего дома. На собрании будут обсуждаться организационные вопросы создания в нашем доме Товарищества собственников жилья. В проведении собрания примет участие представитель ООО 'Безругие Бобры'. На собрание приглашаются все желающие. Инициатор проведения собрания Захапугин И.В."
      });

      Annoncement.Add(new ViewModels.Annoncement()
      {
        Title = "Не муссорить",
        Text = "Уважаемые жильцы! Убедительная просьба не выкидывать мусор из окон квартир. С уважением, администрация этого зоопарка."
      });

      Annoncement.Add(new ViewModels.Annoncement()
      {
        Title = "Продам таксу",
        Text = "Продам таксу, можно по частям. Самомвывоз. т.32-54-23"
      });
    }
  }
}
