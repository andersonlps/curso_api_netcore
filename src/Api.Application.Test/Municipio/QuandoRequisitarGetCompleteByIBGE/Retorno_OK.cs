using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.Municipio;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Municipio.QuandoRequisitarGetCompleteByIBGE
{
    public class Retorno_OK
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "É Possível Realizar o Get.")]
        public async Task E_Possivel_Invocar_a_Controller_Get()
        {
            var serviceMock = new Mock<IMunicipioService>();

            serviceMock.Setup(m => m.GetCompletoByIBGE(It.IsAny<int>())).ReturnsAsync(
                new MunicipioDtoCompleto
                {
                    Id = Guid.NewGuid(),
                    Nome = "São Paulo"
                }
            );

            _controller = new MunicipiosController(serviceMock.Object);

            var result = await _controller.GetCompletoByIBGE(1);
            Assert.True(result is OkObjectResult);

        }
    }
}