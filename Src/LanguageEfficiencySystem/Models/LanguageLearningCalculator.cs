namespace LanguageEfficiencySystem.Models;

public class LanguageLearningCalculator
{
    private readonly int _averageDaysToLearn;

    public LanguageLearningCalculator(int averageDaysToLearn)
    {
        _averageDaysToLearn = averageDaysToLearn;
    }

    public Dictionary<Language, double> TimeToLearn(Developer dev, List<Language> languagesToLearn)
    {
        return languagesToLearn.ToDictionary(language => language, language => TimeToLearn(dev, language));
    }
    
    public double TimeToLearn(Developer dev, Language languageToLearn)
    {
        if (LanguageIsKnown(dev, languageToLearn))
        {
            return 0;
        }

        const double percentageMinimum = 0.2;
        var modifier = 1.0;
        modifier = AccountForCustomLanguageRules(languageToLearn, dev, modifier);
        modifier = AccountForAge(dev.Age, modifier);
        modifier = AccountForKnownLanguages(dev.Languages, modifier);
        modifier = AccountForYearsOfExperience(dev.YearsOfExperience, modifier);
        
        //Unsure what to do if the scenario goes over the limit
        //if (modifier > (1 / percentageMinimum))
        //{
        //    throw new ArithmeticException("Owen cannot do maths");
        //}

        return Math.Max(_averageDaysToLearn / modifier, _averageDaysToLearn * 0.2);
    }

    private static bool LanguageIsKnown(Developer dev, Language languageToLearn)
    {
        return dev.Languages.Contains(languageToLearn);
    }

    public int TotalTimeToLearn(Dictionary<Language, int> timesToLearn)
    {
        return timesToLearn.Sum(language => language.Value);
    }
    
    private static Dictionary<Language, int> CheckForExistingLanguages(IEnumerable<Language> knownLanguages, ICollection<Language> languagesToLearn)
    {
        
        var results = knownLanguages
            .Where(languagesToLearn.Contains)
            .ToDictionary(languages => languages, languages => 0);

        foreach (var language in results)
        {
            languagesToLearn.Remove(language.Key);
        }

        return results;
    }

    private static double AccountForAge(int age, double modifier)
    {
        const int baseAge = 25;
        const int maxAge = 45;
        const double percentageForUnder25 = 1.2;
        const double percentagePerYearOver25 = 0.01;

        var ageDifference = age - baseAge;

        if (age < baseAge)
        {
            return modifier * percentageForUnder25;
        } 
        if (age < maxAge)
        {
            return modifier * (percentageForUnder25 - (ageDifference * percentagePerYearOver25));
        }

        return modifier;
    }
    
    private static double AccountForKnownLanguages(IEnumerable<Language> knownLanguages, double modifier)
    {
        const double percentagePerLanguage = 1.08;
        var count = knownLanguages.Count();
        return modifier * Math.Pow(percentagePerLanguage, count);
    }
    
    private static double AccountForYearsOfExperience(int yearsOfExperience, double modifier)
    {
        const double percentagePerYearsOfExperience = 1.01;
        return  modifier * Math.Pow(percentagePerYearsOfExperience, yearsOfExperience);
    }
    
    private static double AccountForCustomLanguageRules(Language languageToLearn, Developer dev, double modifier)
    {
        const double percentageForCppAndJava = 2.0;
        const double percentageForCppOrJava = 1.2;
        
        //Convert to switch if this grows? and also refactor out each language to have it's own function
        if (languageToLearn is Language.CSharp)
        {
            if (dev.Languages.Contains(Language.Cpp) && dev.Languages.Contains(Language.Java))
            {
                modifier *= percentageForCppAndJava;
            }
            
            if (dev.Languages.Contains(Language.Cpp) || dev.Languages.Contains(Language.Java))
            {
                modifier *= percentageForCppOrJava;
            }
        }

        return modifier;
    }
}