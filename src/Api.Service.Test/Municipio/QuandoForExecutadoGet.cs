using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.Municipio;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Municipio
{
    public class QuandoForExecutadoGet : MunicipioTestes
    {
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É Possível Executar o Método GET.")]
        public async Task E_Possivel_Executar_Metodo_Get()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.Get(IdMunicipio)).ReturnsAsync(municipioDto);
            _service = _serviceMock.Object;

            var result = await _service.Get(IdMunicipio);
            Assert.NotNull(result);
            Assert.True(result.Id == IdMunicipio);
            Assert.Equal(NomeMunicipio, result.Nome);

            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(Task.FromResult((MunicipioDto)null));
            _service = _serviceMock.Object;

            var _record = await _service.Get(Guid.NewGuid());
            Assert.Null(_record);
        }
    }
}