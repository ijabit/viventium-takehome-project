using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using HrDataManager.Domain.Models;
using Microsoft.EntityFrameworkCore.Storage;

public interface IHrDataDbContext
{
    DbSet<Employee> Employees { get; }

    DbSet<Company> Companies { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
}
