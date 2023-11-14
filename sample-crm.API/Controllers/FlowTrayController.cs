using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sample_crm.Application;
using sample_crm.Application.DTOs;
using sample_crm.Application.Services;

namespace sample_crm.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlowTrayController : ControllerBase
    {
        private readonly FlowTrayService _flowTrayService;
        private readonly FlowService _flowService;
        private readonly FlowStateService _flowStateService;

        public FlowTrayController(FlowTrayService flowTrayService, FlowService flowService, FlowStateService flowStateService)
        {
            _flowTrayService = flowTrayService;
            _flowStateService = flowStateService;
            _flowService = flowService;
        }

        [HttpPut("flow/promote/{flowId:int}")]
        public async Task<ActionResult<FlowDTO>> PromoteFlowTray(int flowId, [FromBody]PromoteFlowTrayDTO promotion)
        {
            if(!await _flowService.ValidateFlow(flowId))
            {
                return NotFound("Flow doesn't exist");
            }
            
            if(!await _flowStateService.ValidateFlowState(promotion.StateId))
            {
                return NotFound("State doesn't exist");
            }

            var promotedFlow = await _flowTrayService.PromoteFlowState(flowId, promotion.StateId);
            return Ok(promotedFlow);
        }

        [HttpPut("flow/freeze/{flowId:int}")]
        public async Task<ActionResult<FlowDTO>> FreezeFlowTray(int flowId)
        {
            if(!await _flowService.ValidateFlow(flowId))
            {
                return NotFound("Flow doesn't exist");
            }

            var frozenFlow = await _flowTrayService.FreezeFlowTray(flowId);
            return Ok(frozenFlow);
        }

        [HttpPut("flow/unfreeze/{flowId:int}")]
        public async Task<ActionResult<FlowDTO>> UnfreezeFlowTray(int flowId)
        {
            if(!await _flowService.ValidateFlow(flowId))
            {
                return NotFound("Flow doesn't exist");
            }

            var frozenFlow = await _flowTrayService.UnfreezeFlowTray(flowId);
            return Ok(frozenFlow);
        }
    }
}