using KRCP.Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operation)
        {
            // Evitamos abrir una nueva transacción si ya existe una activa.
            if (_context.Database.CurrentTransaction is not null)
                return await operation();

            //Prepara el mecanismo de reintentos ante errores temporales de conexión
            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();

                var result = await operation();

                await transaction.CommitAsync();
                return result;
                // Si ocurre una excepción antes del Commit, la transacción hace un rollback.
            });
        }

        public Task ExecuteInTransactionAsync(Func<Task> operation)
          => ExecuteInTransactionAsync<bool>(async () =>
          {
              await operation();
              return true;
          });


    }
}
