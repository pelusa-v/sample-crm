using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sample_crm.Application.DTOs;
using sample_crm.Application.Services.Interfaces;

namespace sample_crm.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FlowController : ControllerBase
    {
        private readonly IFlowService _flowService;
        private readonly UserManager<IdentityUser> _userManager;

        public FlowController(IFlowService flowService, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _flowService = flowService;
        }

        [HttpGet("{flowId:int}")]
        public async Task<ActionResult<FlowDTO>> GetFlowById(int flowId)
        {
            if(!await _flowService.ValidateFlow(flowId))
            {
                return NotFound("Flow doesn't exist");
            }

            return await _flowService.Get(flowId);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<FlowDTO>>> ListFlows()
        {
            return (await _flowService.List()).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<FlowDTO>> CreateFlow([FromBody]CreateFlowDTO flow)
        {
            var userEmailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var userEmail = userEmailClaim.Value;
            var user = await _userManager.FindByEmailAsync(userEmail);

            return await _flowService.Create(flow, user.Id);
        }
    }
}