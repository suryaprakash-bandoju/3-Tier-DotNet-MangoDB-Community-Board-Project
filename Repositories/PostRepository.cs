using CommunityBoard.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CommunityBoard.Repositories;

public class PostRepository : IPostRepository
{
    private readonly IMongoCollection<Post> _posts;

    public PostRepository(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _posts = database.GetCollection<Post>(settings.Value.CollectionName);
    }

    public async Task<List<Post>> GetAllAsync() =>
        await _posts.Find(FilterDefinition<Post>.Empty)
            .SortByDescending(x => x.CreatedAt)
            .ToListAsync();

    public async Task<Post?> GetByIdAsync(string id)
    {
        if (!ObjectId.TryParse(id, out _)) return null;
        return await _posts.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Post post) => await _posts.InsertOneAsync(post);

    public async Task UpdateAsync(Post post) =>
        await _posts.ReplaceOneAsync(x => x.Id == post.Id, post);

    public async Task DeleteAsync(string id) =>
        await _posts.DeleteOneAsync(x => x.Id == id);
}
