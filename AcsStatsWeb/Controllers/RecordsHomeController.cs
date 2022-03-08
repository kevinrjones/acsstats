using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AcsStatsWeb.Models;
using Services;

namespace AcsStatsWeb.Controllers
{
  public class RecordsHomeController : Controller
  {
    private readonly ILogger<RecordsHomeController> _logger;
    private readonly IMatchesService _matchesService;

    public RecordsHomeController(ILogger<RecordsHomeController> logger, IMatchesService matchesService)
    {
      _logger = logger;
      _matchesService = matchesService;
    }

    public IActionResult Index()
    {
      return View();
    }
  }
}