using lab5.Models;

namespace lab5.Services
{
    public interface ICalculateData
    {
        Task<Result> NRZ(int[] data);
        Task<Result> AMI(int[] data);
        //Task<Result<int[]>> MLT3(int[] data);
        Task<Result> NRZI(int[] data);
        Task<Result> B2B1Q (int[] data);
        Task<string> GetBinary(string word);
    }
}