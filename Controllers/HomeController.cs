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
        var ResultData = new ResultDataModel(){Points = data};
        ResultData.Results.Add(await _calculateData.NRZ(data));
        ResultData.Results.Add(await _calculateData.AMI(data));
        ResultData.Results.Add(await _calculateData.NRZI(data));
        ResultData.Results.Add(await _calculateData.B2B1Q(data));
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
