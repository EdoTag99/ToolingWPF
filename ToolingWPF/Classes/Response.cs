using System.ComponentModel;
using ToolingWPF.Model;

namespace ToolingWPF
{
    public class Response : INotifyPropertyChanged
    {
        private int _status;
        private string _message;

        public int Status
        { get { return _status; } set { _status = value; OnPropertyChanged(nameof(Status)); } }

        public string Message
        { get { return _message; } set { _message = value; OnPropertyChanged(nameof(Message)); } }

        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ResponseBool : Response
    {
        private bool _data;

        public bool Data
        { get { return _data; } set { _data = value; OnPropertyChanged(nameof(Data)); } }
    }

    public class ResponseMT : Response
    {
        public MagazineTool[] Data { get; set; }
    }

    public class ResponseTP : Response
    {
        public ToolPress[] Data { get; set; }
    }

    public class ResponseManager
    {
        public static ResponseBool Response = new ResponseBool();

        public static ResponseBool GetResponse()
        { return Response; }

        public static void SetResponse(ResponseBool response)
        { Response = response; }
    }
}