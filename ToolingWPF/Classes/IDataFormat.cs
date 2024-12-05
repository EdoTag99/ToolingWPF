using System.Windows;
using System.Windows.Controls;

namespace ToolingWPF.Model
{
    internal interface IDataFormat
    {
        DataObject GetDataObject(Canvas canvas);
    }
}