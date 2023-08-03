using System.Collections.ObjectModel;
using LanguageEfficiencySystem.Models;
using LanguageEfficiencySystem.Services;

namespace LanguageEfficiencySystem.Tests;

public class CandidateLanguageLearningCalculatorTests
{
    [Fact]
    public void CalculateCandidateEfficiency_WhenTheDevKnowsTheLanguage_ThenResultHasZeroTimeToLearn()
    {
        //Arrange 
        const Language languageToLearn = Language.CSharp;
        var languagesToLearn = new List<Language>() { languageToLearn };
        
        var dev = new Developer("Owen",
            24,
            5,
            new ReadOnlyCollection<Language>(new List<Language>() { languageToLearn }));
        var sut = new CandidateLanguageLearningCalculator(15, languagesToLearn);
        var expected = new CandidateLanguageLearningResult(dev,
            new Dictionary<Language, double>() { { languageToLearn, 0 } }, 0);
        
        //Act + Assert
        sut.CalculateCandidateEfficiency(dev).Should().BeEquivalentTo(expected);
    }
    
   [Fact]
   public void CalculateCandidateEfficiency_WhenTheDevHasASub20PercentAverage_ThenReturns20PercentOfAverageDaysToLearn()
   {
       //Arrange 
       var languagesToLearn = new List<Language>() { Language.CSharp };
       const int averageTimeToLearn = 15;
       var dev = new Developer("Owen",
           24,
           100,
           new ReadOnlyCollection<Language>(new List<Language>()
           {
               Language.Cobol,
               Language.Vb6,
               Language.Cpp,
               Language.Java,
               Language.Xml,
               Language.JavaScript,
               Language.Python,
               Language.Sql,
               Language.MySql,
               Language.Php,
               Language.Ruby
           }));
       var sut = new CandidateLanguageLearningCalculator(averageTimeToLearn, languagesToLearn);
       var expected =
           new CandidateLanguageLearningResult(dev, new Dictionary<Language, double>() { { Language.CSharp, 3 } }, 3);

       //Act + Assert
       sut.CalculateCandidateEfficiency(dev).Should().BeEquivalentTo(expected);
   }
   
    [Fact]
    public void CalculateCandidateEfficiency_WhenADevAndMultipleLanguages_ShouldReturnLanguagesAndTheirTimeToLearn_()
    {
        //Arrange 
        var languagesToLearn = new List<Language>() { Language.CSharp, Language.Python };
        var dev = new Developer("Owen",
            24,
            5,
            new ReadOnlyCollection<Language>(new List<Language>() { Language.Cpp, Language.Java }));
        var expected = new CandidateLanguageLearningResult(dev, new Dictionary<Language, double>
        {
            { Language.CSharp, 4.248585782137474 },
            { Language.Python, 10.196605877129937 }
        }, 14.445191659267412);
        
        var sut = new CandidateLanguageLearningCalculator(15, languagesToLearn);

        //Act + Assert
        sut.CalculateCandidateEfficiency(dev).Should().BeEquivalentTo(expected);
    }


    [Theory, MemberData(nameof(CalculateCandidateEfficiencyMemberData))]
    public void CalculateCandidateEfficiency_WhenGivenSampleDevelopersToLeanCsharpAndPython_ThenTheTimesTakenAreCorrect(Developer dev, CandidateLanguageLearningResult expected)
    {
        //Arrange 
        var languagesToLearn = new List<Language>() { Language.CSharp, Language.Python };
        var sut = new CandidateLanguageLearningCalculator(15, languagesToLearn);

        //Act + Assert
        sut.CalculateCandidateEfficiency(dev).Should().BeEquivalentTo(expected);
    }
    
    public static IEnumerable<object[]> CalculateCandidateEfficiencyMemberData => 
        new List<object[]>
        {
            new object[] { TestHelpers.Developers[0], new CandidateLanguageLearningResult(TestHelpers.Developers[0], 
                new Dictionary<Language, double>()
                {
                    {Language.CSharp , 0}, 
                    {Language.Python , 8.832915093538048}, 
                },  8.832915093538048)},
            new object[] { TestHelpers.Developers[1], new CandidateLanguageLearningResult(TestHelpers.Developers[1], 
                new Dictionary<Language, double>()
                {
                    {Language.CSharp , 8.187213706891187 }, 
                    {Language.Python , 0}, 
                },  8.187213706891187 )},
            new object[] { TestHelpers.Developers[2], new CandidateLanguageLearningResult(TestHelpers.Developers[2], 
                new Dictionary<Language, double>()
                {
                    {Language.CSharp , 10.030816834783376}, 
                    {Language.Python , 10.030816834783376 }, 
                },  20.061633669566753 )},
            new object[] { TestHelpers.Developers[3], new CandidateLanguageLearningResult(TestHelpers.Developers[3], 
                new Dictionary<Language, double>()
                {
                    {Language.CSharp , 0}, 
                    {Language.Python , 9.05575950186525}, 
                },  9.05575950186525)},
            new object[] { TestHelpers.Developers[4], new CandidateLanguageLearningResult(TestHelpers.Developers[4], 
                new Dictionary<Language, double>()
                {
                    {Language.CSharp ,  7.362029156139697}, 
                    {Language.Python , 8.834434987367636}, 
                },  16.196464143507335)},
            new object[] { TestHelpers.Developers[5], new CandidateLanguageLearningResult(TestHelpers.Developers[5], 
                new Dictionary<Language, double>()
                {
                    {Language.CSharp , 7.9974440531918605}, 
                    {Language.Python , 0}, 
                },  7.9974440531918605)},
        };
}
