using LanguageEfficiencySystem.Models;

namespace LanguageEfficiencySystem.Builders;

public class LanguageLearningModifierBuilder
{
    private double _modifier;
    private readonly double _baseModifier;

    public LanguageLearningModifierBuilder(double baseModifier)
    {
        _baseModifier = baseModifier;
        _modifier = baseModifier;
    }
    
    public void BuildForAge(int age)
    {
        const int baseAge = 25;
        const int maxAge = 45;
        const double percentageForUnder25 = 1.2;
        const double percentagePerYearOver25 = 0.01;

        var ageDifference = age - baseAge;

        switch (age)
        {
            case < baseAge:
                _modifier *= percentageForUnder25;
                break;
            case < maxAge:
                _modifier *= (percentageForUnder25 - ageDifference * percentagePerYearOver25);
                break;
        }
    }

    public void BuildForKnownLanguages(IEnumerable<Language> knownLanguages)
    {
        const double percentagePerLanguage = 1.08;
        _modifier *= Math.Pow(percentagePerLanguage, knownLanguages.Count());
    }

    public void  BuildForYearsOfExperience(int yearsOfExperience)
    {
        const double percentagePerYearOfExperience = 1.01;
        _modifier *= Math.Pow(percentagePerYearOfExperience, yearsOfExperience);
    }
    
    public void BuildForCustomLanguageRules(Language languageToLearn, List<Language> knownLanguages)
    {
        const double percentageForCppAndJava = 2.0;
        const double percentageForCppOrJava = 1.2;
        
        //Convert to switch if this grows? and also refactor out each language to have it's own function
        if (languageToLearn is Language.CSharp)
        {
            if (knownLanguages.Contains(Language.Cpp) && knownLanguages.Contains(Language.Java))
            {
                _modifier *= percentageForCppAndJava;
            }
            
            else if (knownLanguages.Contains(Language.Cpp) || knownLanguages.Contains(Language.Java))
            {
                _modifier *= percentageForCppOrJava;
            }
        }
    }

    public double GetModifier()
    {
        var result = _modifier;
        Reset();
        return result;
    }

    private void Reset()
    {
        _modifier = _baseModifier;
    }
}