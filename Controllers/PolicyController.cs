
using Microsoft.AspNetCore.Mvc;
using API1.Models;
using API1.Services;
using Policy = API1.Models.Policy;

namespace API1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliciesController : ControllerBase
    {
        private readonly IPolicyService _policyService;

        public PoliciesController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        [HttpGet]
        public ActionResult<List<Policy>> Get() =>
            _policyService.Get();

        [HttpGet("{id:length(24)}", Name = "GetPolicy")]
        public ActionResult<Policy> Get(string id)
        {
            var policy = _policyService.Get(id);

            if (policy == null)
            {
                return NotFound();
            }

            return policy;
        }

        [HttpPost]
        public ActionResult<Policy> Create(Policy policy)
        {
            _policyService.Create(policy);

            return CreatedAtRoute("GetPolicy", new { id = policy.Id.ToString() }, policy);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Policy policyIn)
        {
            var policy = _policyService.Get(id);

            if (policy == null)
            {
                return NotFound();
            }

            _policyService.Update(id, policyIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var policy = _policyService.Get(id);

            if (policy == null)
            {
                return NotFound();
            }

            _policyService.Remove(policy.Id);

            return NoContent();
        }
    }
}