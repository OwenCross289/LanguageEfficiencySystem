using System.ComponentModel.DataAnnotations;
using LanguageEfficiencySystem.Models;

namespace LanguageEfficiencySystem.Services;

public class TeamBuilder
{
    public IReadOnlyCollection<CandidateLanguageLearningResult> Team { get; }
    public IReadOnlyCollection<CandidateLanguageLearningResult> RunnersUp { get; }
    public double TimeBeforeOperational { get; }
    
    public TeamBuilder(int numberOfDevelopersRequired, CandidateRanker ranker)
    {
        var orderedCandidates = ranker.RankCandidates().ToList();
        
        if (numberOfDevelopersRequired > orderedCandidates.Count)
        {
            throw new ValidationException("Not enough developers to build the team");
        }
        
        Team = orderedCandidates.Take(numberOfDevelopersRequired).ToList().AsReadOnly();
        RunnersUp = orderedCandidates.Skip(numberOfDevelopersRequired).ToList().AsReadOnly();
        TimeBeforeOperational = Team.Select(dev => dev.TotalTimeToLearn).Max();
    }
}