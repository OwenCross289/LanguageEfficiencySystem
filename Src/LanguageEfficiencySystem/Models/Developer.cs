namespace LanguageEfficiencySystem.Models;

public record Developer(string Name, 
    int Age, 
    int YearsOfExperience, 
    IReadOnlyCollection<Language> Languages);