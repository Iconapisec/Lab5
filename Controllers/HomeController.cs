using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using lab5.Models;
using lab5.Services;

namespace lab5.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICalculateData _calculateData;

    public HomeController(ILogger<HomeController> logger,ICalculateData calculateData)
    {
        _logger = logger;
        _calculateData = calculateData;
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Calculate(string word)
    {
        string b = Convert.ToString(Request.Form["word"]);

        var t = Request.Form;
        Console.WriteLine(b);
        var w = await _calculateData.GetBinary("sdfsdf");
        //var data = w.Select(d => int.Parse(d.ToString())).ToArray();
        var data = "0100101000001111".Select(d => int.Parse(d.ToString())).ToArray();
        //data = "0100101000001111".Select(d => int.Parse(d.ToString())).ToArray();
        var ResultData = new ResultDataModel(){Points = data};
        ResultData.Results.Add(await _calculateData.NRZ(data));
        ResultData.Results.Add(await _calculateData.AMI(data));
        ResultData.Results.Add(await _calculateData.NRZI(data));
        ResultData.Results.Add(await _calculateData.B2B1Q(data));
        ResultData.Results.Add(await _calculateData.MLT3(data));
        ResultData.Results.Add(await _calculateData.Bipolyar(data));
        ResultData.Results.Add(await _calculateData.Manchester(data));
        #region 8
        var s  = await _calculateData.Skremb(data);
        ResultData.Results.Add(s);
        var sAMI = await _calculateData.AMI(s.Points.Cast<int>().ToArray());
        sAMI.Name = "S AMI";
        sAMI.Code.Replace("AMI", "sAMI");
        ResultData.Results.Add(sAMI);
        #endregion
        return View(ResultData);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
