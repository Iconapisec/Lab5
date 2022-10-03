using System.Text;
using lab5.Models;

namespace lab5.Services
{
    public class CalculateData : ICalculateData
    {
        public async Task<Result> NRZ(int[] data)
        {

            Result result = new ();
            result.Code.Append("NRZ code value: ");
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
            result.Code.Append("AMI code value: ");
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
            result.Code.Append("NRZI code value: ");
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

            result.Color = "rgb(0,0,204)";
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
        public async Task<Result> MLT3(int[] data)
        {
            Result result = new();
            result.Code.Append("MLT3 code value: ");
            int currentState = data[0];
            bool state = false;
            for(int i = 0; i < data.Length; i++)
            {
                if(data[i] == 0)
                {
                    result.Points.Add(currentState);
                    result.Code.Append(currentState == 0 ? 0 : (currentState == -1? '-':"+"));
                }
                else if(data[i] == 1 && currentState == 0)
                {
                    result.Points.Add(!state? 1 : -1);
                    currentState = !state? 1 : -1;
                    result.Code.Append(!state ? '+':'-');
                    state = !state;
                }
                else if(data[i] == 1 && (currentState == -1 || currentState == 1))
                {
                    result.Points.Add(0);
                    result.Code.Append(0);
                    currentState = 0;
                }
            }
            result.Color = "rgb(25,0,51)";
            result.Name = "MLT-3";
            return await Task.FromResult(result);
        }
        public async Task<Result> Skremb(int[] data)
        {
            Result result = new();
            result.Code.AppendLine("Скремблирование:<br />");
            for(int i = 0; i < data.Length; i++)
            {
                if(i > 4)
                {
                    result.Points.Add(data[i] ^ (int)result.Points[i-3] ^ (int)result.Points[i-5]);
                    result.Code.AppendLine($"B{i+1} = A{i+1} + B{i-2} + B{i-4} = {data[i]} + {(int)result.Points[i-3]} + {(int)result.Points[i-5]} = {data[i] ^ (int)result.Points[i-3] ^ (int)result.Points[i-5]}<br />");
                }
                else if(i > 2)
                {
                    result.Points.Add(data[i] ^ (int)result.Points[i-3]);
                    result.Code.AppendLine($"B{i+1} = A{i+1} + B{i-2} = {data[i]} + {(int)result.Points[i-3]} = {data[i] ^ (int)result.Points[i-3]}<br />");
                }
                else
                {
                    result.Points.Add(data[i]);
                    result.Code.AppendLine($"B{i+1} = A{i+1} = {data[i]}<br />");
                }
            }
            result.Code.AppendLine($"Result: {string.Join("",result.Points)}<br />");
            result.Color = "rgb(0,102,102)";
            return await Task.FromResult(result);
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