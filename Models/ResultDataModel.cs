using System.Text;
namespace lab5.Models
{
    public class ResultDataModel
    {
        public IList<int> Points {get;set;} = new List<int>();
        public IList<Result> Results {get;set;} = new List<Result>();
    }

    public class Result
    {
        public string Name {get;set;} = default!;
        public List<object> Points {get;set;} = new List<object>();
        public StringBuilder Code {get;set;} = new StringBuilder();
        public string Color {get;set;} = default!;
    }
}