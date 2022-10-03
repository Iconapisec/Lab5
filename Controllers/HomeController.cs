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

    public async Task<IActionResult> Index()
    {
        //var data = await _calculateData.GetBinary(word);
        var data = "0100101000001111".Select(d => int.Parse(d.ToString())).ToArray();
        data = "11000011".Select(d => int.Parse(d.ToString())).ToArray();
        var ResultData = new ResultDataModel(){Points = data};
        ResultData.Results.Add(await _calculateData.NRZ(data));
        ResultData.Results.Add(await _calculateData.AMI(data));
        ResultData.Results.Add(await _calculateData.NRZI(data));
        ResultData.Results.Add(await _calculateData.B2B1Q(data));
        ResultData.Results.Add(await _calculateData.MLT3(data));
        #region 8
        var s  = await _calculateData.Skremb("1010000000001101".Select(d => int.Parse(d.ToString())).ToArray());
        var sAMI = await _calculateData.AMI(s.Points.Cast<int>().ToArray());
        s.Code.AppendLine($"Скремблирование:{sAMI.Code}<br />");
        s.Points = sAMI.Points;
        s.Name = "S AMI";
        #endregion
        ResultData.Results.Add(s);
        //var q = await _calculateData.AMI(s.Points.Cast<int>().ToArray());
        //q.Name = "SKREMB AMI";
        //ResultData.Results.Add(q);
        return View(ResultData);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
