using System.ComponentModel.DataAnnotations;
using LanguageEfficiencySystem.Models;
using LanguageEfficiencySystem.Repositories;
using LanguageEfficiencySystem.Services;

namespace LanguageEfficiencySystem.Tests;

public class CandidateRankerTests
{
    private readonly IDeveloperRepository _devRepositorySub = Substitute.For<IDeveloperRepository>();

    [GwtFact(
        given: $"a {nameof(CandidateRanker)}",
        when: "constructed with no candidates",
        then: $"a {nameof(ValidationException)} is thrown")]
    public void T1()
    {
        //Arrange
        _devRepositorySub.Get().Returns(new List<Developer>());
        var languagesToLearn = new List<Language>() { Language.CSharp, Language.Python };
        var calculator = new CandidateLanguageLearningCalculator(averageDaysToLearn: 15, languagesToLearn);
        
        //Act
        var act = () => new CandidateRanker(_devRepositorySub, calculator);

        //Assert
        act.Should().Throw<ValidationException>().WithMessage("No candidates provided");
    }
    
    [GwtFact(
        given: $"a {nameof(CandidateRanker)}",
        when: "constructed with candidates",
        then: "the candidates are ordered correctly")]
    public void RankCandidates_WhenCalled_ThenAOrderedListOfCandidatesIsReturned()
    {
        //Arrange
        _devRepositorySub.Get().Returns(TestHelpers.Developers);
        var languagesToLearn = new List<Language>() { Language.CSharp, Language.Python };
        var calculator = new CandidateLanguageLearningCalculator(averageDaysToLearn: 15, languagesToLearn);
        var sut = new CandidateRanker(_devRepositorySub, calculator);
        
        //Act
        var actual = sut.RankCandidates().ToList();

        //Assert
        actual[0].Dev.Name.Should().BeEquivalentTo("Anna");
        actual[1].Dev.Name.Should().BeEquivalentTo("Lars");
        actual[2].Dev.Name.Should().BeEquivalentTo("Kari");
        actual[3].Dev.Name.Should().BeEquivalentTo("Nils");
        actual[4].Dev.Name.Should().BeEquivalentTo("Karl");
        actual[5].Dev.Name.Should().BeEquivalentTo("Johan");
    }
}