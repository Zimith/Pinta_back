
using Pinta.Domain.FileSystem;

namespace Pinta.DAL.interfaces.FileSystem;

public interface IFileRepository
{
    Task<Pinta.Domain.FileSystem.File?> GetById(long id);
    Task<Domain.FileSystem.File?> GetByName(string id);
}