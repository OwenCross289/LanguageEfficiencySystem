namespace LanguageEfficiencySystem.Models;

public record LanguageLearningCalculatorResult(Developer Dev, Dictionary<Language, double> TimesToLearn,
    double TotalTimeToLearn);