using TaskManager.Api.Domain;
using TaskManager.Api.DTO;

namespace TaskManager.Api.Mapping;

public class Mapper
{
    public Mapper() {}

    public User GetUser(UserEntity userEntity)
    {
        return new User(
            name: userEntity.Name ?? throw new System.ArgumentException("Username is required"),
            data: userEntity.Data
        );
    }
}