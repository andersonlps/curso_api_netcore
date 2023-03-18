using System;
using System.Collections;
using System.Threading.Tasks;
using Api.Domain.Dtos.Uf;

namespace Api.Domain.Interfaces.Services.Uf
{
    public interface IUfService
    {
        Task<UfDto> Get(Guid id);
        Task<IEnumerable> GetAll(Guid id);
    }
}