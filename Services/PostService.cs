using CommunityBoard.Models;
using CommunityBoard.Repositories;

namespace CommunityBoard.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _repository;

    public PostService(IPostRepository repository) => _repository = repository;

    public Task<List<Post>> GetAllAsync() => _repository.GetAllAsync();
    public Task<Post?> GetByIdAsync(string id) => _repository.GetByIdAsync(id);

    public async Task CreateAsync(Post post)
    {
        post.Title = post.Title.Trim();
        post.Description = post.Description.Trim();
        if (string.IsNullOrWhiteSpace(post.Title)) throw new ArgumentException("Title is required.");
        if (string.IsNullOrWhiteSpace(post.Description)) throw new ArgumentException("Description is required.");
        post.CreatedAt = DateTime.UtcNow;
        post.Status = "Open";
        await _repository.CreateAsync(post);
    }

    public async Task<bool> UpdateAsync(string id, Post post)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null) return false;

        existing.Title = post.Title.Trim();
        existing.Category = post.Category.Trim();
        existing.Description = post.Description.Trim();
        existing.Status = post.Status;

        if (string.IsNullOrWhiteSpace(existing.Title) || string.IsNullOrWhiteSpace(existing.Description))
            throw new ArgumentException("Title and description are required.");

        await _repository.UpdateAsync(existing);
        return true;
    }

    public Task DeleteAsync(string id) => _repository.DeleteAsync(id);
}
