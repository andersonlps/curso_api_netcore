using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class CepCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        public ServiceProvider _serviceProvider;

        public CepCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "CRUD de Cep")]
        [Trait("CRUD", "CepEntity")]
        public async Task E_Possivel_Realizar_CRUD_Cep()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                MunicipioImplementation _repositoryMunicipio = new MunicipioImplementation(context);
                MunicipioEntity _entityMunicipio = new MunicipioEntity
                {
                    Nome = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = new Guid("43a0f783-a042-4c46-8688-5dd4489d2ec7")
                };

                var _registroCriadoMunicipio = await _repositoryMunicipio.InsertAsync(_entityMunicipio);
                Assert.NotNull(_registroCriadoMunicipio);
                Assert.Equal(_entityMunicipio.Nome, _registroCriadoMunicipio.Nome);
                Assert.Equal(_entityMunicipio.CodIBGE, _registroCriadoMunicipio.CodIBGE);
                Assert.Equal(_entityMunicipio.UfId, _registroCriadoMunicipio.UfId);
                Assert.False(_registroCriadoMunicipio.Id == Guid.Empty);

                CepImplementation _repository = new CepImplementation(context);
                CepEntity _entity = new CepEntity
                {
                    Cep = "13.481.001",
                    Logradouro = Faker.Address.StreetName(),
                    Numero = "0 até 2000",
                    MunicipioId = _registroCriadoMunicipio.Id
                };

                var _registroCriado = await _repository.InsertAsync(_entity);
                Assert.NotNull(_registroCriado);
                Assert.Equal(_entity.Cep, _registroCriado.Cep);
                Assert.Equal(_entity.Logradouro, _registroCriado.Logradouro);
                Assert.Equal(_entity.Numero, _registroCriado.Numero);
                Assert.Equal(_entity.MunicipioId, _registroCriado.MunicipioId);
                Assert.False(_registroCriado.Id == Guid.Empty);

                _entity.Logradouro = Faker.Address.StreetName();
                _entity.Id = _registroCriado.Id;
                var _registroAtualizado = await _repository.UpdateAsync(_entity);
                Assert.NotNull(_registroAtualizado);
                Assert.Equal(_entity.Cep, _registroAtualizado.Cep);
                Assert.Equal(_entity.Logradouro, _registroAtualizado.Logradouro);
                Assert.Equal(_entity.Numero, _registroAtualizado.Numero);
                Assert.Equal(_entity.MunicipioId, _registroAtualizado.MunicipioId);
                Assert.True(_registroCriado.Id == _entity.Id);

                var _registroExiste = await _repository.ExistAsync(_registroAtualizado.Id);
                Assert.True(_registroExiste);

                var _registroSelecionado = await _repository.SelectAsync(_registroAtualizado.Id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroAtualizado.Cep, _registroSelecionado.Cep);
                Assert.Equal(_registroAtualizado.Logradouro, _registroSelecionado.Logradouro);
                Assert.Equal(_registroAtualizado.Numero, _registroSelecionado.Numero);
                Assert.Equal(_registroAtualizado.MunicipioId, _registroSelecionado.MunicipioId);

                _registroSelecionado = await _repository.SelectAsync(_registroAtualizado.Cep);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroAtualizado.Cep, _registroSelecionado.Cep);
                Assert.Equal(_registroAtualizado.Logradouro, _registroSelecionado.Logradouro);
                Assert.Equal(_registroAtualizado.Numero, _registroSelecionado.Numero);
                Assert.Equal(_registroAtualizado.MunicipioId, _registroSelecionado.MunicipioId);
                Assert.Equal(_entityMunicipio.Nome, _registroSelecionado.Municipio.Nome);
                Assert.Equal("RJ", _registroSelecionado.Municipio.Uf.Sigla);
                Assert.NotNull(_registroSelecionado.Municipio);
                Assert.NotNull(_registroSelecionado.Municipio.Uf);

                var _todosRegistros = await _repository.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() > 0);

                var _removeu = await _repository.DeleteAsync(_registroSelecionado.Id);
                Assert.True(_removeu);

                _todosRegistros = await _repository.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() == 0);

            }
        }
    }
}