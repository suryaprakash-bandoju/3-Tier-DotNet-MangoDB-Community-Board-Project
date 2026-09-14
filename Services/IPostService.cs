using CommunityBoard.Models;

namespace CommunityBoard.Services;

public interface IPostService
{
    Task<List<Post>> GetAllAsync();
    Task<Post?> GetByIdAsync(string id);
    Task CreateAsync(Post post);
    Task<bool> UpdateAsync(string id, Post post);
    Task DeleteAsync(string id);
}
