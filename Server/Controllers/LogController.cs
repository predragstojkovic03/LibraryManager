using System;
using Application.Services;
using Domain.Entities;

namespace Server.Controllers
{
  public class LogController
  {
    private readonly CrudService<Log> _logService;

    public LogController(CrudService<Log> logService)
    {
      _logService = logService;
    }

    public Log CreateLog(Log log)
    {
      return _logService.Create(log);
    }

    public Log CreateLog(Log log, Microsoft.Data.SqlClient.SqlTransaction transaction)
    {
      return _logService.Create(log, transaction);
    }
  }
}

