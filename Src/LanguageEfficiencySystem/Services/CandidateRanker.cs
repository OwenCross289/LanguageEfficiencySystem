using System.ComponentModel.DataAnnotations;
using LanguageEfficiencySystem.Models;
using LanguageEfficiencySystem.Repositories;

namespace LanguageEfficiencySystem.Services;

public class CandidateRanker
{
    private readonly IEnumerable<Developer> _candidates;
    private readonly CandidateLanguageLearningCalculator _calculator;

    public CandidateRanker(IDeveloperRepository devRepository, CandidateLanguageLearningCalculator calculator)
    {
        _candidates = devRepository.Get();
        _calculator = calculator;
        
        //Any is awful for performance and should be avoided as of .NET 8 analyzers
        if (_candidates.Count() < 1)
        {
            throw new ValidationException("No candidates provided");
        }
    }

    public IOrderedEnumerable<CandidateLanguageLearningResult> RankCandidates()
    {
        return _candidates
            .Select(candidate => _calculator.CalculateCandidateEfficiency(candidate))
            .OrderBy(candidate => candidate.TotalTimeToLearn);
    }
}