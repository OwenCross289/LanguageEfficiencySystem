using System.Collections.ObjectModel;
using LanguageEfficiencySystem.Models;
using LanguageEfficiencySystem.Services;

namespace LanguageEfficiencySystem.Tests;

public class CandidateLanguageLearningCalculatorTests
{
    [GwtFact(
        given: $"a {nameof(CandidateLanguageLearningCalculator.CalculateCandidateEfficiency)}",
        when: "a candidate knows the language",
        then: "total time to learn is 0")]
    public void T1()
    {
        //Arrange 
        const Language languageToLearn = Language.CSharp;
        var languagesToLearn = new List<Language>() { languageToLearn };
        
        var candidate = new Developer("Owen",
            Age: 24,
            YearsOfExperience: 5,
            KnownLanguages: new ReadOnlyCollection<Language>(new List<Language>() { languageToLearn }));
        var sut = new CandidateLanguageLearningCalculator(15, languagesToLearn);
        var expected = new CandidateLanguageLearningResult(
            candidate,
            TimesToLearn: new Dictionary<Language, double>() { { languageToLearn, 0 } },
            TotalTimeToLearn: 0);
        
        //Act 
        var actual = sut.CalculateCandidateEfficiency(candidate);
        
        //Assert
        actual.Should().BeEquivalentTo(expected);
    }
    
    [GwtFact(
        given: $"a {nameof(CandidateLanguageLearningCalculator.CalculateCandidateEfficiency)}",
        when: "a candidate has less than 20% of the maximum time to learn",
        then: "value is rounded up to 20%")]
   public void T2()
   {
       //Arrange 
       var languagesToLearn = new List<Language>() { Language.CSharp };
       var candidate = new Developer("Owen",
           Age: 24,
           YearsOfExperience: 100,
           KnownLanguages: new ReadOnlyCollection<Language>(new List<Language>()
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
       var sut = new CandidateLanguageLearningCalculator(averageDaysToLearn: 15, languagesToLearn);
       var expected =new CandidateLanguageLearningResult(
           candidate, 
           TimesToLearn: new Dictionary<Language, double>() { { Language.CSharp, 3 } }, 
           TotalTimeToLearn: 3);

       //Act
       var actual = sut.CalculateCandidateEfficiency(candidate);
       
       //Assert
       actual.Should().BeEquivalentTo(expected);
   }
   
   [GwtFact(
       given: $"a {nameof(CandidateLanguageLearningCalculator.CalculateCandidateEfficiency)}",
       when: "a candidate knows multiple languages",
       then: "a dictionary of languages and their total time to learn is returned with the sum of the dictionary")]
    public void T3()
    {
        //Arrange 
        var languagesToLearn = new List<Language>() { Language.CSharp, Language.Python };
        var candidate = new Developer("Owen",
            Age: 24,
            YearsOfExperience: 5,
            KnownLanguages: new ReadOnlyCollection<Language>(new List<Language>() { Language.Cpp, Language.Java }));
        var expected = new CandidateLanguageLearningResult(
            candidate, 
            TimesToLearn: new Dictionary<Language, double>
            {
                { Language.CSharp, 4.248585782137474 },
                { Language.Python, 10.196605877129937 }
            },
            TotalTimeToLearn: 14.445191659267412);
        
        var sut = new CandidateLanguageLearningCalculator(averageDaysToLearn: 15, languagesToLearn);

        //Act
        var actual = sut.CalculateCandidateEfficiency(candidate);
        
        //Assert
        actual.Should().BeEquivalentTo(expected);
    }

    
    [GwtTheory(
         given: $"a {nameof(CandidateLanguageLearningCalculator.CalculateCandidateEfficiency)}, with multiple candidates",
         when: "efficiency is calculated for CSharp and Python",
         then: "expected values are returned"), 
     MemberData(nameof(CalculateCandidateEfficiencyMemberData))]
    public void T4(Developer dev, CandidateLanguageLearningResult expected)
    {
        //Arrange 
        var languagesToLearn = new List<Language>() { Language.CSharp, Language.Python };
        var sut = new CandidateLanguageLearningCalculator(averageDaysToLearn: 15, languagesToLearn);

        //Act
        var actual = sut.CalculateCandidateEfficiency(dev);
        
        //Assert
        actual.Should().BeEquivalentTo(expected);
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
