using FuncionariosWAClean.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncionariosWAClean.Domain.Interfaces
{
    public interface ICargoRepository
    {
        Task<IEnumerable<Cargo>> GetAllAsync();
        Task<Cargo> GetByIdAsync(int? id);
        Task<Cargo> CreateAsync(Cargo cargo);
        Task<Cargo> UpdateAsync(Cargo cargo);
        Task<Cargo> RemoveAsync(Cargo cargo);
    }
}
