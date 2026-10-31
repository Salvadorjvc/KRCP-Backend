using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Persistence
{
    //esto es para las tranascciones
    public interface IUnitOfWork
    {
        //tarea asincrona que devuelve valor dentro de una transaccion atomica
        Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operation);
        //tarea asincrona que no devuelve valor dentro de una transaccion atomica
        Task ExecuteInTransactionAsync(Func<Task> operation);
    }
}
