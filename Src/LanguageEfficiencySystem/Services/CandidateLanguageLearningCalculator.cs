using LanguageEfficiencySystem.Builders;
using LanguageEfficiencySystem.Models;

namespace LanguageEfficiencySystem.Services;

public class CandidateLanguageLearningCalculator
{
    private readonly int _averageDaysToLearn;
    private readonly IEnumerable<Language> _languagesToLearn;

    public CandidateLanguageLearningCalculator(int averageDaysToLearn, IEnumerable<Language> languagesToLearn)
    {
        _averageDaysToLearn = averageDaysToLearn;
        _languagesToLearn = languagesToLearn;
    }

    public CandidateLanguageLearningResult CalculateCandidateEfficiency(Developer dev)
    {
        var timesToLearn = _languagesToLearn.ToDictionary(language => language, language => CalculateTimeToLearn(dev, language));
        return new CandidateLanguageLearningResult(dev, timesToLearn, TotalTimeToLearn(timesToLearn));
    }

    private double CalculateTimeToLearn(Developer dev, Language languageToLearn)
    {
        if (LanguageIsKnown(dev, languageToLearn))
        {
            return 0;
        }
        
        var modifier = new LanguageLearningModifierBuilder(1);
        modifier.BuildForCustomLanguageRules(languageToLearn, dev.KnownLanguages.ToList());
        modifier.BuildForAge(dev.Age);
        modifier.BuildForKnownLanguages(dev.KnownLanguages);
        modifier.BuildForYearsOfExperience(dev.YearsOfExperience);

        //Assumed to round if they are the next Uncle Bob
        const double percentageMinimum = 0.2;
        return Math.Max(_averageDaysToLearn / modifier.GetModifier(), _averageDaysToLearn * percentageMinimum);
    }

    private static bool LanguageIsKnown(Developer dev, Language languageToLearn)
    {
        return dev.KnownLanguages.Contains(languageToLearn);
    }

    private static double TotalTimeToLearn(Dictionary<Language, double> timesToLearn)
    {
        return timesToLearn.Sum(language => language.Value);
    }
}