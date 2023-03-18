using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.Cep;

namespace Api.Domain.Interfaces.Services.Cep
{
    public interface ICepService
    {
        Task<CepDto> Get(Guid id);
        Task<CepDto> Get(string cep);
        Task<CepDtoCreateResult> Post(CepDtoCreate cep);
        Task<CepDtoUpdateResult> Post(CepDtoUpdate cep);
        Task<bool> Delete(Guid id);
    }
}