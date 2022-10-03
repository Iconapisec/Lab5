using System.Text;
using lab5.Models;

namespace lab5.Services
{
    public class CalculateData : ICalculateData
    {
        public async Task<Result> NRZ(int[] data)
        {
            Result result = new ();
            for(int i = 0; i < data.Length; i++)
            {
                result.Points.Add(data[i] == 0 ? 0 : 1);
                result.Code.Append(data[i] == 0 ? 0 : "+");
            }
            result.Color = "rgb(255, 99, 132)";
            result.Name = "NRZ";
            return await Task.FromResult(result);
        }
        public async Task<Result> AMI(int[] data)
        {
            Result result = new ();
            bool state = false;
            for(int i = 0; i < data.Length; i++)
            {
                if (data[i] == 1)
                {
                    result.Points.Add(!state? 1 : -1);
                    result.Code.Append(!state? "+" : "-");
                    state = !state;
                }
                else
                {
                    result.Points.Add(0);
                    result.Code.Append(0);
                }
            }
            result.Color = "rgb(0,0,0)";
            result.Name = "AMI";
            return await Task.FromResult(result);
        }
        public async Task<Result> NRZI(int[] data)
        {
            Result result = new ();
            bool state = false;

            for(int i = 0; i < data.Length; i++)
            {
                if(!state && data[i] == 0)
                {
                    result.Points.Add(0);
                    result.Code.Append(0);
                }
                else if(state && data[i] == 0)
                {
                    result.Points.Add(1);
                    result.Code.Append('+');
                }
                else if(!state && data[i] == 1)
                {
                    result.Points.Add(1);
                    result.Code.Append('+');
                    state = !state;
                }
                else if(state && data[i] == 1)
                {
                    result.Points.Add(0);
                    result.Code.Append(0);
                    state = !state;
                }
            }

            result.Color = "rgb(204,255,204)";
            result.Name = "NRZI";
            return await Task.FromResult(result);
        }
        public async Task<Result> B2B1Q(int[] data)
        {
            Result result = new ();
            for(int i=0; i < data.Length-1; i+=2)
            {
                if(data[i] == 0 && data[i+1] == 0)
                {
                    result.Points.AddRange(new object[2]{-2.5,-2.5});
                }
                else if(data[i] == 0 && data[i+1] == 1)
                {
                    result.Points.AddRange(new object[2]{-0.833,-0.833});
                }
                else if(data[i] == 1 && data[i+1] == 1)
                {
                    result.Points.AddRange(new object[2]{0.833,0.833});
                }
                else
                {
                    result.Points.AddRange(new object[2]{2.5,2.5});
                }
            }
            result.Color = "rgb(51,0,0)";
            result.Name = "2B1Q";
            return await Task.FromResult(result);
        }
        public async Task<Tuple<int[],string>> MLT3(int[] data)
        {
            List<int> result  = new();
            StringBuilder sb  = new();
            bool state = false;
            for(int i = 0; i < data.Length; i++)
            {
                if (data[i] == 1)
                {
                    result.Add(!state? 1 : -1);
                    sb.Append(!state? "+" : "-");
                    state = !state;
                }
                else
                {
                    result.Add(0);
                    sb.Append(0);
                }
            }
            return await Task.FromResult(new Tuple<int[],string>(result.ToArray(),sb.ToString()));
        }
        public async Task<string> GetBinary(string word)
        {
            string data = string.Empty;
            for(int i =0; i < word.Length; i++)
            {
                data +=Convert.ToString(word[i],2);
            }
            return await Task.FromResult(data);
        }
    }
}