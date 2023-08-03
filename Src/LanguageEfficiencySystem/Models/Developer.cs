namespace LanguageEfficiencySystem.Models;

//Could write a validator for this aka cant have more years of experience than age.
//Would use FluentValidation for this
public record Developer(string Name, int Age, int YearsOfExperience, IEnumerable<Language> KnownLanguages);