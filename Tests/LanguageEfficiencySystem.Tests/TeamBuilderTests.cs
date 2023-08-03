using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using LanguageEfficiencySystem.Models;

namespace LanguageEfficiencySystem.Tests;

public class TeamBuilderTests
{
    private readonly List<Developer> _developers = new()
    {
        new("Kari", 32, 10, new ReadOnlyCollection<Language>(new List<Language>
        {
            Language.CSharp,
            Language.Ruby,
            Language.Cpp,
            Language.Java
        })),
        new("Lars", 22, 1, new ReadOnlyCollection<Language>(new List<Language>
        {
            Language.Cpp,
            Language.JavaScript,
            Language.Python
        })),
        new("Johan", 42, 22, new ReadOnlyCollection<Language>(new List<Language>
        {
            Language.Sql,
            Language.JavaScript
        })),
        new("Nils", 28, 4, new ReadOnlyCollection<Language>(new List<Language>
        {
            Language.JavaScript,
            Language.Java,
            Language.CSharp,
            Language.Php
        })),
        new("Karl", 52, 30, new ReadOnlyCollection<Language>(new List<Language>
        {
            Language.Cobol,
            Language.Java,
            Language.Vb6
        })),
        new("Anna", 38, 10, new ReadOnlyCollection<Language>(new List<Language>
        {
            Language.Python,
            Language.Php,
            Language.MySql,
            Language.Ruby,
            Language.Xml,
            Language.Html
        }))
    };
    
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Ctor_WhenCreatedTooManyDevelopers_ThenTheRequiredNumberOfDevelopersInTeamAndRestInRunnersUp(int requiredDevelopers)
    {
        //Arrange + Act
        var sut = new TeamBuilder(requiredDevelopers,
            _developers,
            new LanguageLearningCalculator(15, new List<Language>() { Language.CSharp }));

        //Assert
        sut.Team.Count().Should().Be(requiredDevelopers);
        sut.RunnersUp.Count().Should().Be(_developers.Count - sut.Team.Count());
        sut.Team.Should().NotContain(sut.RunnersUp);
    }
    
    [Fact]
    public void Ctor_WhenCreatedWithTheExactAmountOfDevelopers_ThenTheRequiredNumberOfDevelopersInTeamAndRunnerUpEmpty()
    {
        //Arrange + Act
        var sut = new TeamBuilder(_developers.Count(),
            _developers,
            new LanguageLearningCalculator(15, new List<Language>() { Language.CSharp, Language.Python }));

        //Assert
        sut.Team.Count().Should().Be(_developers.Count());
        sut.RunnersUp.Should().BeEmpty();
    }
    
    [Theory]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(99)]
    [InlineData(475)]
    [InlineData(105484)]
    public void Ctor_WhenCreatedWithNotEnoughDevelopers_ThenAllCandidatesInTeamAndNoneInRunnersUp(int requiredDevelopers)
    {
        //Arrange + Act
        var act = () => new TeamBuilder(requiredDevelopers,
            _developers,
            new LanguageLearningCalculator(15, new List<Language>() { Language.CSharp, Language.Python }));

        //Assert
        act.Should().Throw<ValidationException>().WithMessage("Not enough developers to build the team");
    }
    
    [Fact]
    public void Ctor_WhenCreated_ThenTimeBeforeOperationalIsTheLongestMemberOfTeam()
    {
        //Arrange + Act
        var sut = new TeamBuilder(3,
            _developers,
            new LanguageLearningCalculator(15, new List<Language>() { Language.CSharp, Language.Python }));

        //Assert
        sut.TimeBeforeOperational.Should().Be(8.832915093538048);
    }
}