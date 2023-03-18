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
    public class UfGets : BaseTest, IClassFixture<DbTeste>
    {
        public ServiceProvider _serviceProvider;

        public UfGets(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "Gets e UF")]
        [Trait("Gets", "UfEntity")]
        public async Task E_Possivel_Realizar_Gets_UF()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UfImplementation _repository = new UfImplementation(context);
                UfEntity _entity = new UfEntity
                {
                    Id = new Guid("43a0f783-a042-4c46-8688-5dd4489d2ec7"),
                    Sigla = "RJ",
                    Nome = "Rio de Janeiro"
                };

                var _registroExiste = await _repository.ExistAsync(_entity.Id);
                Assert.True(_registroExiste);

                var _registroSelecionado = await _repository.SelectAsync(_entity.Id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_entity.Sigla, _registroSelecionado.Sigla);
                Assert.Equal(_entity.Nome, _registroSelecionado.Nome);
                Assert.Equal(_entity.Id, _registroSelecionado.Id);

                var _todosRegistros = await _repository.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() == 27);
            }
        }
    }
}