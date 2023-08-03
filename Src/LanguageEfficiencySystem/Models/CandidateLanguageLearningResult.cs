namespace LanguageEfficiencySystem.Models;

//Don't know what to call this tbh?
public record CandidateLanguageLearningResult(Developer Dev, Dictionary<Language, double> TimesToLearn,
    double TotalTimeToLearn);