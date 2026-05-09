using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Repository;
using JobFinder.Domain.Common.Entities;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;
using JobFinder.Persistance.Repositories.GenericRepository;

namespace JobFinder.Persistance.Repositories
{
    //public class JobOffersRepository : IJobOffersRepository
    //{
    //    private readonly GenericReadRepository<JobOffer> _readRepository;
    //    private readonly GenericWriteRepository<JobOffer> _writeRepository;
    //    private readonly ReadDbContext _readContext; // You might need this for specific read logic
    //    private readonly WriteDbContext _writeContext; // You might need this for specific write logic
    //    public JobOffersRepository(WriteDbContext writeContext, ReadDbContext readContext)
    //    {
    //        _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
    //        _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
    //        _readRepository = new GenericReadRepository<JobOffer>(_readContext);
    //        _writeRepository = new GenericWriteRepository<JobOffer>(_writeContext);
    //    }

    //    public async Task<JobOffer> AddAsync(JobOffer entity)
    //    {
    //        await _writeRepository.AddAsync(entity);
    //        return entity;
    //    }

    //    public async Task AddRangeAsync(IEnumerable<JobOffer> entities)
    //    {
    //        await _writeRepository.AddRangeAsync(entities);
    //    }

    //    public async Task<bool> DeleteAsync(object id)
    //    {
    //        var entity = _readRepository.FindAsync(x => x.Id == (int)id);
    //        if (entity == null)
    //        {
    //            return await Task.FromResult(false);
    //        }
    //        await _writeRepository.DeleteAsync(entity);
    //        return await Task.FromResult(true);
    //    }

    //    public async Task<bool> DeleteAsync(JobOffer entity)
    //    {
    //        await _writeRepository.DeleteAsync(entity);
    //        return await Task.FromResult(true);
    //    }

    //    public async Task<bool> DeleteRangeAsync(IEnumerable<JobOffer> entities)
    //    {
    //        await _writeRepository.DeleteRangeAsync(entities);
    //        return await Task.FromResult(true);
    //    }

    //    public async Task<bool> ExistsAsync(Expression<Func<JobOffer, bool>> expression)
    //    {
    //        return await _writeRepository.ExistsAsync(expression);
    //    }

    //    public async Task<IEnumerable<JobOffer>> FindAsync(Expression<Func<JobOffer, bool>> expression)
    //    {
    //        var record = await _writeRepository.FindAsync(expression);
    //        return record;
    //    }

    //    public async Task<IEnumerable<JobOffer>> GetAllAsync(CancellationToken cancellationToken = default)
    //    {
    //        return await _writeRepository.GetAllAsync(cancellationToken);
    //    }

    //    public async Task<JobOffer?> GetByIdAsync(object id)
    //    {
    //        return await _readRepository.GetByIdAsync(id);
    //    }

    //    public IQueryable<JobOffer> GetQueryable()
    //    {
    //        return _writeRepository.GetQueryable();
    //    }

    //    public async Task<JobOffer> UpdateAsync(JobOffer entity)
    //    {
    //        var record = await _writeRepository.UpdateAsync(entity);
    //        return record;
    //    }

    //    public async Task UpdateRangeAsync(IEnumerable<JobOffer> entities)
    //    {
    //        await _writeRepository.UpdateRangeAsync(entities);
    //    }
    //}



}
