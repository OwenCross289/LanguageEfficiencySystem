using System.ComponentModel.DataAnnotations;
using LanguageEfficiencySystem.Models;

namespace LanguageEfficiencySystem.Services;

public class CandidateRanker
{
    private readonly IEnumerable<Developer> _candidates;
    private readonly CandidateLanguageLearningCalculator _calculator;

    public CandidateRanker(IEnumerable<Developer> candidates, CandidateLanguageLearningCalculator calculator)
    {
        _candidates = candidates;
        _calculator = calculator;
        
        //Any is awful for performance and should be avoided as of .NET 8 analyzers
        if (_candidates.Count() < 1)
        {
            throw new ValidationException("No candidates provided");
        }
    }

    public IOrderedEnumerable<CandidateLanguageLearningResult> RankCandidates()
    {
        return _candidates.Select(candidate => _calculator.CalculateCandidateEfficiency(candidate))
            .OrderBy(d => d.TotalTimeToLearn);
    }
}