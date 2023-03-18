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
    public class MunicipioCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        public ServiceProvider _serviceProvider;

        public MunicipioCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "CRUD de Municipio")]
        [Trait("CRUD", "MunicipioEntity")]
        public async Task E_Possivel_Realizar_CRUD_Municipio()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                MunicipioImplementation _repository = new MunicipioImplementation(context);
                MunicipioEntity _entity = new MunicipioEntity
                {
                    Nome = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = new Guid("43a0f783-a042-4c46-8688-5dd4489d2ec7")
                };

                var _registroCriado = await _repository.InsertAsync(_entity);
                Assert.NotNull(_registroCriado);
                Assert.Equal(_entity.Nome, _registroCriado.Nome);
                Assert.Equal(_entity.CodIBGE, _registroCriado.CodIBGE);
                Assert.Equal(_entity.UfId, _registroCriado.UfId);
                Assert.False(_registroCriado.Id == Guid.Empty);

                _entity.Nome = Faker.Address.City();
                _entity.Id = _registroCriado.Id;
                var _registroAtualizado = await _repository.UpdateAsync(_entity);
                Assert.NotNull(_registroAtualizado);
                Assert.Equal(_entity.Nome, _registroAtualizado.Nome);
                Assert.Equal(_entity.CodIBGE, _registroAtualizado.CodIBGE);
                Assert.Equal(_entity.UfId, _registroAtualizado.UfId);
                Assert.Equal(_entity.Id, _registroAtualizado.Id);

                var _registroExiste = await _repository.ExistAsync(_registroAtualizado.Id);
                Assert.True(_registroExiste);

                var _registroSelecionado = await _repository.SelectAsync(_registroAtualizado.Id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroAtualizado.Nome, _registroSelecionado.Nome);
                Assert.Equal(_registroAtualizado.CodIBGE, _registroSelecionado.CodIBGE);
                Assert.Equal(_registroAtualizado.UfId, _registroSelecionado.UfId);
                Assert.Null(_registroSelecionado.Uf);

                _registroSelecionado = await _repository.GetCompletoByIBGE(_registroAtualizado.CodIBGE);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroAtualizado.Nome, _registroSelecionado.Nome);
                Assert.Equal(_registroAtualizado.CodIBGE, _registroSelecionado.CodIBGE);
                Assert.Equal(_registroAtualizado.UfId, _registroSelecionado.UfId);
                Assert.NotNull(_registroSelecionado.Uf);

                _registroSelecionado = await _repository.GetCompletoById(_registroAtualizado.Id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroAtualizado.Nome, _registroSelecionado.Nome);
                Assert.Equal(_registroAtualizado.CodIBGE, _registroSelecionado.CodIBGE);
                Assert.Equal(_registroAtualizado.UfId, _registroSelecionado.UfId);
                Assert.NotNull(_registroSelecionado.Uf);

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