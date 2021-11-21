using System;
using System.Threading.Tasks;

namespace SecureApp.Services
{
    public interface IValuesService
    {
        Task<ValuesGetDto> Get();
    }
}
