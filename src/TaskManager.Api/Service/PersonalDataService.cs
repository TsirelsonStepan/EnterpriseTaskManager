using System.Threading.Tasks;
using TaskManager.Api.DTO;
using TaskManager.Api.Repository;

namespace TaskManager.Api.Service;

public class PersonalDataService
{
    readonly UserRepository _userRepository;

    public PersonalDataService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string> GetUserData(string username)
    {
        UserEntity userEntity = await _userRepository.SearchByUsername(username);
        return userEntity.Data!; //FIX
    }
}