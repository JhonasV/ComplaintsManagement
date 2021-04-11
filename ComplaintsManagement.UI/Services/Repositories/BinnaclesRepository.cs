using ComplaintsManagement.Infrastructure.DTOs;
using ComplaintsManagement.Infrastructure.Entities;
using ComplaintsManagement.UI.Models;
using ComplaintsManagement.UI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ComplaintsManagement.UI.Services.Repositories
{
    public class BinnaclesRepository : IBinnaclesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICustomersRepository _customersRepository;

        public BinnaclesRepository(ApplicationDbContext dbContext, ICustomersRepository customersRepository)
        {
            _dbContext = dbContext;
            _customersRepository = customersRepository;
        }

        public async Task<TaskResult<List<BinnacleDto>>> GetAllAsync()
        {
            var result = new TaskResult<List<BinnacleDto>>();
            try
            {
                var binnacles = await _dbContext
                                      .Binnacles
                                      .Include(e => e.Status)
                                      .ToListAsync();
                var binnaclesDto = AutoMapper.Mapper.Map<List<BinnacleDto>>(binnacles);
                foreach (var binnacle in binnaclesDto)
                {
                    var user = await _customersRepository.GetAsync(binnacle.ApplicationUserId);
                    binnacle.User = AutoMapper.Mapper.Map<UsersDto>(user.Data);
                }


                result.Data = binnaclesDto;
                result.Success = true;
            }
            catch (Exception e)
            {

                result.Success = false;
                result.Message = e.InnerException.Message;
            }

            return result;
        }

        public async Task<TaskResult<List<BinnacleDto>>> GetAllByComplaintsIdAsync(int complaintsId)
        {
            var result = new TaskResult<List<BinnacleDto>>();
            try
            {
                var binnacles = await _dbContext
                                      .Binnacles
                                      .Include(e =>e.Status)
                                      .Where(e => e.Active && e.ComplaintsId == complaintsId)
                                      .ToListAsync();

                var binnaclesDto = AutoMapper.Mapper.Map<List<BinnacleDto>>(binnacles);
                foreach (var binnacle in binnaclesDto)
                {
                    var user = await _customersRepository.GetAsync(binnacle.ApplicationUserId);
                    binnacle.User = AutoMapper.Mapper.Map<UsersDto>(user.Data);
                }


                result.Data = binnaclesDto;
                result.Success = true;
            }
            catch (Exception e)
            {

                result.Success = false;
                result.Message = e.InnerException.Message;
            }

            return result;
        }

        public async Task<TaskResult<BinnacleDto>> SaveAsync(BinnacleDto binnacleDto)
        {
            var binnacle = AutoMapper.Mapper.Map<Binnacle>(binnacleDto);
            binnacle.CreatedAt = DateTime.Now;
            var result = new TaskResult<BinnacleDto>();
            try
            {
                _dbContext.Binnacles.Add(binnacle);
                await _dbContext.SaveChangesAsync();
                result.Data = AutoMapper.Mapper.Map<BinnacleDto>(binnacle);
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Message = e.InnerException.Message;
                result.Success = false;
                
            }
            return result;
        }
    }
}