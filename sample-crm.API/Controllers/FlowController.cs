using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sample_crm.Application.DTOs;
using sample_crm.Application.Services;

namespace sample_crm.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlowController : ControllerBase
    {
        public FlowService _flowService;

        public FlowController(FlowService flowService)
        {
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
        public async Task<ActionResult<List<FlowDTO>>> ListFlows()
        {
            return (await _flowService.List()).ToList();
        }
    }
}