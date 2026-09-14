using CommunityBoard.Models;

namespace CommunityBoard.Repositories;

public interface IPostRepository
{
    Task<List<Post>> GetAllAsync();
    Task<Post?> GetByIdAsync(string id);
    Task CreateAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(string id);
}
