using FluentResults;
using JobFinder.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.Advertisement;
using Domain.WriteRepository;

namespace JobFinder.Application.Repositories.Invoice
{

    public interface IInventoryRepository : IWriteRepository<ProductInventory> //, IReadRepository<ProductInventory>//IRepository<ProductInventory>
    {
        public List<ProductInventory> GetCurrentInventory();
        public Result<ProductInventory> UpdateUnitsAvailable(Guid id, int adjustment);
        public ProductInventory GetByProductId(Guid productId);
        public List<ProductInventorySnapshot> GetSnapshotHistory();
    }
}
