using System.Collections.ObjectModel;
using WPFUI_SAMPLE.Entity;

namespace WPFUI_SAMPLE.Helpers;
public class MaterialIdToCodeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Guid materialId && parameter is ObservableCollection<MaterialEntity> materials)
        {
            MaterialEntity? material = materials.FirstOrDefault(m => m.Id == materialId);
            return material?.Code ?? string.Empty;
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
