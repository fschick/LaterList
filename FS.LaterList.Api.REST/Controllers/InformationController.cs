using FS.LaterList.Common.Routing;
using FS.LaterList.IoC.Interfaces.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FS.LaterList.Api.REST.Controllers
{
    [ApiController]
    public class InformationController : ControllerBase
    {
        private readonly IInformationService _informationService;

        public InformationController(IInformationService informationService)
            => _informationService = informationService;

        [HttpGet(Routes.Information.GetProductName)]
        public string GetProductName()
            => _informationService.GetProductName();

        [HttpGet(Routes.Information.GetProductVersion)]
        public string GetProductVersion()
            => _informationService.GetProductVersion();
    }
}