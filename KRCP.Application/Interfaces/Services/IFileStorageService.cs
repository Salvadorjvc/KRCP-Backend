using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Services
{
    public interface IFileStorageService
    {
        // El Stream lee el archivo de forma secuencial en bloques, evitando cargar todo el archivo completo en la memoria RAM.
        Task<string> UploadAsync(Stream fileStream, string fileName);
    }
}
