namespace ArchitectureProject.Infrastructure.Interfaces
{
    public interface IPasswordHasherService
    {
        public string GenerateHash(string password, string salt);
        public bool CompareHash(string target, string source, string salt);
    }
}
