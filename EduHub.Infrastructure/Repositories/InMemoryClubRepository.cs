
using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;

namespace EduHub.Infrastructure.Repositories;

/// <summary>
/// Kho CLB lưu tạm trong bộ nhớ — dùng cho Chương 3 khi chưa có database.
/// Chương 4 sẽ thay bằng bản dùng EF Core và PostgreSQL.
/// </summary>
public class InMemoryClubRepository : IClubRepository
{
    // Dữ liệu dùng chung giữa các request.
    private static readonly List<Club> _clubs = new();

    public Task<IEnumerable<Club>> GetAllAsync(
        CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();

        // TODO 1a: Trả về bản sao danh sách.
        IEnumerable<Club> result = _clubs.ToList();

        return Task.FromResult(result);
    }

    public Task<Club?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();

        // TODO 1b: Không tìm thấy thì trả về null.
        Club? club = _clubs.FirstOrDefault(c => c.Id == id);

        return Task.FromResult(club);
    }

    public Task AddAsync(
        Club club,
        CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();

        // TODO 1c: Thêm CLB vào danh sách.
        _clubs.Add(club);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(
        Club club,
        CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();

        // TODO 1d: Tìm CLB cũ và thay thế.
        int index = _clubs.FindIndex(c => c.Id == club.Id);

        if (index == -1)
        {
            throw new KeyNotFoundException(
                $"Không tìm thấy CLB có ID: {club.Id}");
        }

        _clubs[index] = club;

        return Task.CompletedTask;
    }

    public Task DeleteAsync(
        Guid id,
        CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();

        // TODO 1e: Xóa CLB theo ID.
        Club? club = _clubs.FirstOrDefault(c => c.Id == id);

        if (club is null)
        {
            throw new KeyNotFoundException(
                $"Không tìm thấy CLB có ID: {id}");
        }

        _clubs.Remove(club);

        return Task.CompletedTask;
    }
}
