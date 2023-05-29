using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Octokit;
using System.Net;
using System.Net.Http.Headers;
using TesteTerraInvest.Interfaces;

namespace TesteTerraInvest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitHubController : ControllerBase
    {
        private readonly ILogger<GitHubController> _logger;
        private readonly IGitHubClientService _gitHubClientService;

        public GitHubController(ILogger<GitHubController> logger, IGitHubClientService gitHubClientService)
        {
            _logger = logger;
            _gitHubClientService = gitHubClientService;
        }

        [HttpPost]
        [Route("repository/create")]
        public async Task<object> CreateRepository([FromBody] NewRepository newRepository)
        {
            try
            {
                _gitHubClientService.SetCredentials(Request);

                var repository = await _gitHubClientService.Repository.Create(newRepository);

                return Ok(repository);
            }
            catch (ApiException ex)
            {
                if (ex.HttpResponse != null && ex.HttpResponse.Body != null)
                {
                    return BadRequest(ex.HttpResponse.Body);
                }
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("repository/{repositoryID}/branch")]
        public async Task<object> ListBranchs([FromRoute] long repositoryID)
        {
            try
            {
                _gitHubClientService.SetCredentials(Request);

                var branchs = await _gitHubClientService.Repository.Branch.GetAll(repositoryID);

                return Ok(branchs);
            }
            catch (ApiException ex)
            {
                if (ex.HttpResponse != null && ex.HttpResponse.Body != null)
                {
                    return BadRequest(ex.HttpResponse.Body);
                }
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("repository/{repositoryID}/hook")]
        public async Task<object> ListWebhooks([FromRoute] long repositoryID)
        {
            try
            {
                _gitHubClientService.SetCredentials(Request);

                var hooks = await _gitHubClientService.Repository.Hooks.GetAll(repositoryID);

                return Ok(hooks);
            }
            catch (ApiException ex)
            {
                if (ex.HttpResponse != null && ex.HttpResponse.Body != null)
                {
                    return BadRequest(ex.HttpResponse.Body);
                }
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("repository/{repositoryID}/hooks")]
        public async Task<object> AdicionarWebhook([FromRoute] long repositoryID, [FromBody] NewRepositoryHook newRepositoryHook)
        {
            try
            {
                _gitHubClientService.SetCredentials(Request);

                var hook = await _gitHubClientService.Repository.Hooks.Create(repositoryID, newRepositoryHook);

                return Ok(hook);
            }
            catch (ApiException ex)
            {
                if (ex.HttpResponse != null && ex.HttpResponse.Body != null)
                {
                    return BadRequest(ex.HttpResponse.Body);
                }
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("repository/{repositoryID}/hooks/{hooksID}")]
        public async Task<object> AtualizarWebhook([FromRoute] long repositoryID, [FromRoute] int hooksID, [FromBody] EditRepositoryHook editRepositoryHook)
        {
            try
            {
                _gitHubClientService.SetCredentials(Request);

                var hook = await _gitHubClientService.Repository.Hooks.Edit(repositoryID, hooksID, editRepositoryHook);

                return Ok(hook);
            }
            catch (ApiException ex)
            {
                if (ex.HttpResponse != null && ex.HttpResponse.Body != null)
                {
                    return BadRequest(ex.HttpResponse.Body);
                }
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}