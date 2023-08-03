using System.ComponentModel.DataAnnotations;
using LanguageEfficiencySystem.Models;
using LanguageEfficiencySystem.Services;

namespace LanguageEfficiencySystem.Tests;

public class CandidateRankerTests
{
    [Fact]
    public void Ctor_WhenCreatedWithNoCandidates_ThenValidationExceptionThrown()
    {
        //Arrange 
        var calculator = new CandidateLanguageLearningCalculator(15, new List<Language>() { Language.CSharp, Language.Python });
        
        //Act
        var act = () => new CandidateRanker(new List<Developer>(), calculator);

        //Assert
        act.Should().Throw<ValidationException>().WithMessage("No candidates provided");
    }
    
    [Fact]
    public void RankCandidates_WhenCalled_ThenAOrderedListOfCandidatesIsReturned()
    {
        //Arrange 
        var calculator = new CandidateLanguageLearningCalculator(15, new List<Language>() { Language.CSharp, Language.Python });
        var sut = new CandidateRanker(TestHelpers.Developers, calculator);
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