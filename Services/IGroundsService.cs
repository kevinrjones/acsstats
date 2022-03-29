using System.Collections.Generic;
using System.Threading.Tasks;
using AcsStatsWeb.Dtos;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Domain;

namespace Services
{
  public interface IGroundsService
  {
    public Task<Result<GroundDto, Error>> GetGround(int id);
  }
}