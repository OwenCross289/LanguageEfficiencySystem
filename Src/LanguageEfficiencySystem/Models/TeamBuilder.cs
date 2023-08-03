using System.ComponentModel.DataAnnotations;

namespace LanguageEfficiencySystem.Models;

public class TeamBuilder
{
    //Could remove theis? and just do it all in ctor?
    private readonly LanguageLearningCalculator _calculator;

    public IReadOnlyCollection<LanguageLearningCalculatorResult> Team { get; }
    public IReadOnlyCollection<LanguageLearningCalculatorResult> RunnersUp { get; }
    public double TimeBeforeOperational { get; }

    //Can add validation if there are not enough developers for the requirements
    public TeamBuilder(int numberOfDevelopersRequired, IEnumerable<Developer> candidates,
        LanguageLearningCalculator calculator)
    {
        _calculator = calculator;
        candidates = candidates.ToList();
        
        if (numberOfDevelopersRequired > candidates.Count())
        {
            throw new ValidationException(message: "Not enough developers to build the team");
        }

        var orderedCandidates = OrderCandidates(candidates).ToList();
        Team = orderedCandidates.Take(numberOfDevelopersRequired).ToList().AsReadOnly();
        RunnersUp = orderedCandidates.Skip(numberOfDevelopersRequired).ToList().AsReadOnly();
        TimeBeforeOperational = Team.Select(dev => dev.TotalTimeToLearn).Max();
    }

    private IEnumerable<LanguageLearningCalculatorResult> OrderCandidates(IEnumerable<Developer> candidates)
    {
        return candidates.Select(candidate => _calculator.TimesToLearn(candidate))
            .OrderBy(d => d.TotalTimeToLearn);
    }
}