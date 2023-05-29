using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Octokit;
using System.Security.Claims;
using TesteTerraInvest.Controllers;
using TesteTerraInvest.Services;

namespace TesteTerraInvest_TesteUnit
{
    public class GitHubControllerTest
    {
        //Confesso que fiquei sem ideias de como testar essa api já que as operações para terem sucesso, teriam que de fato, fazer operações reais no github de alguém.

        private const string TokenGitHub = "";

        private GitHubController GetController(bool useAuthorization)
        {
            var logger = A.Fake<ILogger<GitHubController>>();

            var productHeaderValue = new ProductHeaderValue("TesteTerraInvest");

            var gitHubClientService = new GitHubClientService(productHeaderValue);

            if (useAuthorization)
            {
                gitHubClientService.SetCredentials(TokenGitHub);
            }

            var controller = new GitHubController(logger, gitHubClientService);

            return controller;
        }

        [Fact]
        public void CreateRepository_VerifyToken()
        {
            Assert.False(string.IsNullOrWhiteSpace(TokenGitHub));
        }

        [Theory]
        [InlineData(false, 400)]
        [InlineData(true, 200)]
        public async void CreateRepository_CreateRepository(bool useAuthorization, int expectedStatusCode)
        {
            var controller = GetController(useAuthorization);

            var newRepository = new NewRepository("Teste");

            var response = await controller.CreateRepository(newRepository) as ObjectResult;

            Assert.NotNull(response);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        //Essa teste terá sucesso já que, existe um repository publico de id 1 que conseguimos listar teus branchs, sem token.
        [Fact]
        public async void CreateRepository_ListBranchs()
        {
            var controller = GetController(false);

            var response = await controller.ListBranchs(1) as ObjectResult;

            Assert.NotNull(response);
            Assert.Equal(200, response.StatusCode);
        }
    }
}