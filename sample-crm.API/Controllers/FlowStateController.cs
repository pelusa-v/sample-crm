using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sample_crm.Application.DTOs;
using sample_crm.Application.Services.Interfaces;

namespace sample_crm.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlowStateController : ControllerBase
    {
        public IFlowStateService _flowStateService;

        public FlowStateController(IFlowStateService flowStateService)
        {
            _flowStateService = flowStateService;
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<FlowStateDTO>> GetFlowStateById(int Id)
        {
            if(!await _flowStateService.ValidateFlowState(Id))
            {
                return NotFound("Flow doesn't exist");
            }

            return await _flowStateService.Get(Id);
        }

        [HttpGet]
        public async Task<ActionResult<List<FlowStateDTO>>> ListFlows()
        {
            return (await _flowStateService.List()).ToList();
        }
    }
}