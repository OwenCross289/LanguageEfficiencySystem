namespace LanguageEfficiencySystem.Models;

//Don't know what to call this?
public record CandidateLanguageLearningResult(Developer Dev, Dictionary<Language, double> TimesToLearn,
    double TotalTimeToLearn);