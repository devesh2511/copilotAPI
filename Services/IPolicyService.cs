using System.Collections.Generic;
using API1.Models;

namespace API1.Services
{
    public interface IPolicyService
    {
        List<Policy> Get();
        Policy Get(string id);
        Policy Create(Policy policy);
        void Update(string id, Policy policyIn);
        void Remove(Policy policyIn);
        void Remove(string id);
    }
}