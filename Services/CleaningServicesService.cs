using API1.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API1.Services;

public class CleaningServicesService
{
    private readonly IMongoCollection<CleaningServices> _cleaningservicesCollection;

    public CleaningServicesService(
        IOptions<CleaningServicesDatabaseSettings> CleaningServicesDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            CleaningServicesDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            CleaningServicesDatabaseSettings.Value.DatabaseName);

        _cleaningservicesCollection = mongoDatabase.GetCollection<CleaningServices>(
            CleaningServicesDatabaseSettings.Value.ServicesCollectionName);
    }

    public async Task<List<CleaningServices>> GetAsync() =>
        await _cleaningservicesCollection.Find(_ => true).ToListAsync();

    public async Task<CleaningServices?> GetAsync(string id) =>
        await _cleaningservicesCollection.Find(x => x.ServiceId == id).FirstOrDefaultAsync();

    public async Task CreateAsync(CleaningServices newBook) =>
        await _cleaningservicesCollection.InsertOneAsync(newBook);

    public async Task UpdateAsync(string id, CleaningServices updatedBook) =>
        await _cleaningservicesCollection.ReplaceOneAsync(x => x.ServiceId == id, updatedBook);

    public async Task RemoveAsync(string id) =>
        await _cleaningservicesCollection.DeleteOneAsync(x => x.ServiceId == id);
}