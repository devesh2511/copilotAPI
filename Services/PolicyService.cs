using API1.Services;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using API1.Models;

namespace API1.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IMongoCollection<Policy> _policies;

        public PolicyService(IYourDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _policies = database.GetCollection<Policy>(settings.PoliciesCollectionName);
        }

        public List<Policy> Get() =>
            _policies.Find(policy => true).ToList();

        public Policy Get(string id) =>
            _policies.Find<Policy>(policy => policy.Id == id).FirstOrDefault();

        public Policy Create(Policy policy)
        {
            _policies.InsertOne(policy);
            return policy;
        }

        public void Update(string id, Policy policyIn) =>
            _policies.ReplaceOne(policy => policy.Id == id, policyIn);

        public void Remove(Policy policyIn) =>
            _policies.DeleteOne(policy => policy.Id == policyIn.Id);

        public void Remove(string id) =>
            _policies.DeleteOne(policy => policy.Id == id);
    }

    public interface IYourDatabaseSettings
    {
        MongoClientSettings ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string PoliciesCollectionName { get; set; }
    }
}