using System.ComponentModel.DataAnnotations;
using LanguageEfficiencySystem.Models;
using LanguageEfficiencySystem.Services;

namespace LanguageEfficiencySystem.Tests;

public class TeamBuilderTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Ctor_WhenCreatedWithTooManyCandidates_ThenTheRequiredNumberOfCandidatesAreInTeamAndTheRestInRunnersUp(int requiredDevelopers)
    {
        //Arrange
        var calculator = new CandidateLanguageLearningCalculator(15, new List<Language>() { Language.CSharp });
        var ranker = new CandidateRanker(TestHelpers.Developers, calculator);
        
        //Act
        var sut = new TeamBuilder(requiredDevelopers, ranker);

        //Assert
        sut.Team.Count().Should().Be(requiredDevelopers);
        sut.RunnersUp.Count().Should().Be(TestHelpers.Developers.Count - sut.Team.Count());
        sut.Team.Should().NotContain(sut.RunnersUp);
    }
    
    [Fact]
    public void Ctor_WhenCreatedWithTheExactAmountOfCandidates_ThenTheRequiredNumberOfCandidatesInTeamAndRunnerUpEmpty()
    {
        //Arrange 
        var calculator = new CandidateLanguageLearningCalculator(15, new List<Language>() { Language.CSharp, Language.Python });
        var ranker = new CandidateRanker(TestHelpers.Developers, calculator);
        
        //Act
        var sut = new TeamBuilder(TestHelpers.Developers.Count(), ranker);

        //Assert
        sut.Team.Count().Should().Be(TestHelpers.Developers.Count());
        sut.RunnersUp.Should().BeEmpty();
    }
    
    [Theory]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(99)]
    [InlineData(475)]
    [InlineData(105484)]
    public void Ctor_WhenCreatedWithNotEnoughCandidates_ThenValidationExceptionThrown(int requiredDevelopers)
    {
        //Arrange 
        var calculator = new CandidateLanguageLearningCalculator(15, new List<Language>() { Language.CSharp, Language.Python });
        var ranker = new CandidateRanker(TestHelpers.Developers, calculator);
        
        //Act
        var act = () => new TeamBuilder(requiredDevelopers, ranker);

        //Assert
        act.Should().Throw<ValidationException>().WithMessage("Not enough developers to build the team");
    }

    [Fact]
    public void Ctor_WhenCreated_ThenTimeBeforeOperationalIsTheLongestMemberOfTeam()
    {
        //Arrange 
        var calculator = new CandidateLanguageLearningCalculator(15, new List<Language>() { Language.CSharp, Language.Python });
        var ranker = new CandidateRanker(TestHelpers.Developers, calculator);
        
        //Act
        var sut = new TeamBuilder(3, ranker);

        //Assert
        sut.TimeBeforeOperational.Should().Be(8.832915093538048);
    }
}