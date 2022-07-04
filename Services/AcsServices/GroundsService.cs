using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcsCommands.Query;
using AcsRepository;
using AcsStatsWeb.Dtos;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Services.AcsServices
{
    public class GroundsService : IGroundsService
    {
        private readonly IEfUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly ILogger<GroundsService> _logger;

        public GroundsService(IEfUnitOfWork unitOfWork, IMediator mediator, ILogger<GroundsService> logger)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<Result<GroundDto, Error>> GetGround(int id)
        {
            if (id == 0)
            {
                return new GroundDto(0, 0, "All Grounds", 0, "", "");
            }
            return await _mediator.Send(new GroundQuery(id));
        }
    }
}