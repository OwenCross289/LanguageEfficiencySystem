using LanguageEfficiencySystem.Models;

namespace LanguageEfficiencySystem.Repositories;

public interface IDeveloperRepository
{
    IEnumerable<Developer> Get();
}