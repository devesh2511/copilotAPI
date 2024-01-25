using API1.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API1.Services;

public class UserService
{
    private readonly IMongoCollection<User> _userCollection;

    public UserService(
        IOptions<UserDatabaseSettings> UserDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            UserDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            UserDatabaseSettings.Value.DatabaseName);

        _userCollection = mongoDatabase.GetCollection<User>(
            UserDatabaseSettings.Value.UserCollectionName);
    }

    public async Task<List<User>> GetAsync() =>
        await _userCollection.Find(_ => true).ToListAsync();

    public async Task<User> GetAsync(string username) =>
        await _userCollection.Find(x => x.Username == username).FirstOrDefaultAsync();

    public async Task CreateAsync(User newUser) =>
        await _userCollection.InsertOneAsync(newUser);

    public async Task UpdateAsync(string username, User updatedUser) =>
        await _userCollection.ReplaceOneAsync(x => x.Username == username, updatedUser);

    public async Task RemoveAsync(string username) =>
        await _userCollection.DeleteOneAsync(x => x.Username == username);
}