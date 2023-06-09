using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.Municipio;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Municipio
{
    public class QuandoForExecutadoDelete : MunicipioTestes
    {
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É Possível Executar o Método Delete.")]
        public async Task E_Possivel_Executar_Metodo_Delete()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.Delete(IdMunicipio))
                        .ReturnsAsync(true);
            _service = _serviceMock.Object;

            var deletado = await _service.Delete(IdMunicipio);
            Assert.True(deletado);

            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(false);
            _service = _serviceMock.Object;

            deletado = await _service.Delete(Guid.NewGuid());
            Assert.False(deletado);


        }
    }
}