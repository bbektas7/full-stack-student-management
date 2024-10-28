using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Student.API.Repositories
{
    public interface IImageRepository
    {
        Task<string> Uploud(IFormFile file, string fileName);
    }
}
