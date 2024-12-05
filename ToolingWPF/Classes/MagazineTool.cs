using System;
using System.Windows;
using System.Windows.Controls;

namespace ToolingWPF.Model
{
    public class MagazineTool : IComparable<MagazineTool>, IDataFormat
    {
        public double Width { get; set; }
        public int Count { get; set; }
        public bool Check { get; set; }

        public string Position { get; set; } = "Position";

        public MagazineTool()

        { }

        public int CompareTo(MagazineTool other)
        {
            return this.Width.CompareTo(other.Width);
        }

        public DataObject GetDataObject(Canvas canvas)
        {
            throw new NotImplementedException();
        }
    }
}