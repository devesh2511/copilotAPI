using API1.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API1.Services
{
    public class ReviewsServices
    {
        private readonly IMongoCollection<Reviews> _reviewsCollection;
        internal object ReviewId;

        public ReviewsServices(
            IOptions<ReviewsDatabaseSettings> ReviewsDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                ReviewsDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ReviewsDatabaseSettings.Value.DatabaseName);

            _reviewsCollection = mongoDatabase.GetCollection<Reviews>(
                ReviewsDatabaseSettings.Value.ReviewsCollectionName);
        }

        public async Task<List<Reviews>> GetAsync() =>
            await _reviewsCollection.Find(_ => true).ToListAsync();

        public async Task<Reviews?> GetAsync(string id) =>
            await _reviewsCollection.Find(x => x.ReviewId == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Reviews newReview) =>
            await _reviewsCollection.InsertOneAsync(newReview);

        public async Task UpdateAsync(string id, Reviews updatedReview) =>
            await _reviewsCollection.ReplaceOneAsync(x => x.ReviewId == id, updatedReview);

        public async Task RemoveAsync(string id) =>
            await _reviewsCollection.DeleteOneAsync(x => x.ReviewId == id);

        internal Task CreateAsync(ReviewsServices newReviewService)
        {
            throw new NotImplementedException();
        }
    }
}
