namespace LanguageEfficiencySystem.Models;

public class LanguageLearningCalculator
{
    private readonly int _averageDaysToLearn;
    private readonly IEnumerable<Language> _languagesToLearn;

    public LanguageLearningCalculator(int averageDaysToLearn, IEnumerable<Language> languagesToLearn)
    {
        _averageDaysToLearn = averageDaysToLearn;
        _languagesToLearn = languagesToLearn;
    }

    public LanguageLearningCalculatorResult TimesToLearn(Developer dev)
    {
        var timesToLearn = _languagesToLearn.ToDictionary(language => language, language => TimeToLearn(dev, language));
        return new LanguageLearningCalculatorResult(dev, timesToLearn, TotalTimeToLearn(timesToLearn));
    }

    private double TimeToLearn(Developer dev, Language languageToLearn)
    {
        if (LanguageIsKnown(dev, languageToLearn)) return 0;

        var modifier = 1.0;
        modifier = AccountForCustomLanguageRules(languageToLearn, dev, modifier);
        modifier = AccountForAge(dev.Age, modifier);
        modifier = AccountForKnownLanguages(dev.KnownLanguages, modifier);
        modifier = AccountForYearsOfExperience(dev.YearsOfExperience, modifier);

        //Unsure what to do if the scenario goes over the limit
        //if (modifier > (1 / percentageMinimum))
        //{
        //    throw new ArithmeticException("Owen cannot do maths");
        //}

        const double percentageMinimum = 0.2;
        return Math.Max(_averageDaysToLearn / modifier, _averageDaysToLearn * percentageMinimum);
    }

    private static bool LanguageIsKnown(Developer dev, Language languageToLearn)
    {
        return dev.KnownLanguages.Contains(languageToLearn);
    }

    private static double TotalTimeToLearn(Dictionary<Language, double> timesToLearn)
    {
        return timesToLearn.Sum(language => language.Value);
    }

    private static double AccountForAge(int age, double modifier)
    {
        const int baseAge = 25;
        const int maxAge = 45;
        const double percentageForUnder25 = 1.2;
        const double percentagePerYearOver25 = 0.01;

        var ageDifference = age - baseAge;

        return age switch
        {
            < baseAge => modifier * percentageForUnder25,
            < maxAge => modifier * (percentageForUnder25 - ageDifference * percentagePerYearOver25),
            _ => modifier
        };
    }

    private static double AccountForKnownLanguages(IEnumerable<Language> knownLanguages, double modifier)
    {
        const double percentagePerLanguage = 1.08;
        var count = knownLanguages.Count();
        return modifier * Math.Pow(percentagePerLanguage, count);
    }

    private static double AccountForYearsOfExperience(int yearsOfExperience, double modifier)
    {
        const double percentagePerYearOfExperience = 1.01;
        return modifier * Math.Pow(percentagePerYearOfExperience, yearsOfExperience);
    }

    private static double AccountForCustomLanguageRules(Language languageToLearn, Developer dev, double modifier)
    {
        const double percentageForCppAndJava = 2.0;
        const double percentageForCppOrJava = 1.2;

        //Convert to switch if this grows? and also refactor out each language to have it's own function
        if (languageToLearn is Language.CSharp)
        {
            if (dev.KnownLanguages.Contains(Language.Cpp) && dev.KnownLanguages.Contains(Language.Java))
            {
                modifier *= percentageForCppAndJava;
            }
            
            if (dev.KnownLanguages.Contains(Language.Cpp) || dev.KnownLanguages.Contains(Language.Java))
            {
                modifier *= percentageForCppOrJava;
            }
        }

        return modifier;
    }
}