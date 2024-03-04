namespace Consid.Domain.Interfaces;

public interface IRandomJsonDataService
{
    Task<string> GetRandomJsonData();
}