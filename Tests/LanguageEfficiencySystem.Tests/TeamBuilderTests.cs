using System.ComponentModel.DataAnnotations;
using LanguageEfficiencySystem.Models;
using LanguageEfficiencySystem.Repositories;
using LanguageEfficiencySystem.Services;

namespace LanguageEfficiencySystem.Tests;

public class TeamBuilderTests
{
    private readonly IDeveloperRepository _devRepositorySub = Substitute.For<IDeveloperRepository>();
    
    [GwtTheory(
        given: $"a {nameof(TeamBuilder)}",
        when: "created with more candidates than required",
        then: $"the required number of candidates are in {nameof(TeamBuilder.Team)}, the rest in {nameof(TeamBuilder.RunnersUp)}")]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void T1(int requiredDevelopers)
    {
        //Arrange
        _devRepositorySub.Get().Returns(TestHelpers.Developers);
        var languagesToLearn = new List<Language>() { Language.CSharp };
        var calculator = new CandidateLanguageLearningCalculator(averageDaysToLearn: 15, languagesToLearn);
        var ranker = new CandidateRanker(_devRepositorySub, calculator);
        
        //Act
        var sut = new TeamBuilder(requiredDevelopers, ranker);

        //Assert
        sut.Team.Count.Should().Be(requiredDevelopers);
        sut.RunnersUp.Count.Should().Be(TestHelpers.Developers.Count - sut.Team.Count);
        sut.Team.Should().NotContain(sut.RunnersUp);
    }
    
    [GwtFact(
        given: $"a {nameof(TeamBuilder)}",
        when: "constructed with the exact amount of candidates",
        then: $"all candidates in {nameof(TeamBuilder.Team)}, {nameof(TeamBuilder.RunnersUp)} empty")]
    public void T2()
    {
        //Arrange
        _devRepositorySub.Get().Returns(TestHelpers.Developers);
        var languagesToLearn = new List<Language>() { Language.CSharp, Language.Python };
        var calculator = new CandidateLanguageLearningCalculator(averageDaysToLearn: 15, languagesToLearn);
        var ranker = new CandidateRanker(_devRepositorySub, calculator);
        
        //Act
        var sut = new TeamBuilder(TestHelpers.Developers.Count, ranker);

        //Assert
        sut.Team.Count.Should().Be(TestHelpers.Developers.Count);
        sut.RunnersUp.Should().BeEmpty();
    }
    
    [GwtTheory(
        given: $"a {nameof(TeamBuilder)}",
        when: "created with less candidates than required",
        then: $"a {nameof(ValidationException)} is thrown")]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(99)]
    [InlineData(475)]
    [InlineData(105484)]
    public void T3(int requiredDevelopers)
    {
        //Arrange
        _devRepositorySub.Get().Returns(TestHelpers.Developers);
        var languagesToLearn = new List<Language>() { Language.CSharp, Language.Python };
        var calculator = new CandidateLanguageLearningCalculator(averageDaysToLearn: 15, languagesToLearn);
        var ranker = new CandidateRanker(_devRepositorySub, calculator);
        
        //Act
        var act = () => new TeamBuilder(requiredDevelopers, ranker);

        //Assert
        act.Should().Throw<ValidationException>().WithMessage("Not enough developers to build the team");
    }

    [GwtFact(
        given: $"a {nameof(TeamBuilder)}",
        when: "constructed",
        then: $"{nameof(TeamBuilder.TimeBeforeOperational)} is the same as the longest on the team")]
    public void T4()
    {
        //Arrange
        _devRepositorySub.Get().Returns(TestHelpers.Developers);
        var languagesToLearn = new List<Language>() { Language.CSharp, Language.Python };
        var calculator = new CandidateLanguageLearningCalculator(averageDaysToLearn: 15, languagesToLearn);
        var ranker = new CandidateRanker(_devRepositorySub, calculator);
        
        //Act
        var sut = new TeamBuilder(numberOfDevelopersRequired: 3, ranker);

        //Assert
        sut.TimeBeforeOperational.Should().Be(8.832915093538048);
    }
}