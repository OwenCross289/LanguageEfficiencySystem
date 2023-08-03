namespace LanguageEfficiencySystem.Tests;

//public class LanguageLearningCalculatorTests
//{
//    [Fact]
//    public void TimeToLearn_ShouldReturnZero_WhenTheDevKnowsTheLanguage()
//    {
//        //Arrange 
//        const Language languageToLearn = Language.CSharp;
//        var dev = new Developer("Owen",
//            24,
//            5,
//            new ReadOnlyCollection<Language>(new List<Language>() { languageToLearn }));
//        var sut = new LanguageLearningCalculator(15);
//
//        //Act + Assert
//        sut.TimeToLearn(dev, languageToLearn).Should().Be(0);
//    }
//    
//    [Fact]
//    public void TimeToLearn_ShouldReturn20PercentOfAverageDaysToLearn_WhenTheDevHasASub20PercentAverage()
//    {
//        //Arrange 
//        const Language languageToLearn = Language.CSharp;
//        var dev = new Developer("Owen",
//            24,
//            100,
//            new ReadOnlyCollection<Language>(new List<Language>()
//            {
//                Language.Cobol,
//                Language.Vb6,
//                Language.Cpp,
//                Language.Java,
//                Language.Xml,
//                Language.JavaScript,
//                Language.Python,
//                Language.Sql,
//                Language.MySql,
//                Language.Php,
//                Language.Ruby
//            }));
//        var sut = new LanguageLearningCalculator(15);
//
//        //Act + Assert
//        sut.TimeToLearn(dev, languageToLearn).Should().Be(15 * 0.2);
//    }
//    
//    [Fact]
//    public void TimesToLearn_ShouldReturnLanguagesAndTheirTimeToLearn_WhenADevAndMultipleLanguages()
//    {
//        //Arrange 
//        var dev = new Developer("Owen",
//            24,
//            5,
//            new ReadOnlyCollection<Language>(new List<Language>() { Language.Cpp, Language.Java }));
//        
//        var sut = new LanguageLearningCalculator(15);
//
//        var expected = new Dictionary<Language, double>()
//        {
//            { Language.CSharp, 4.248585782137474 },
//            { Language.Python, 10.196605877129937 }
//        };
//        
//        //Act + Assert
//        sut.TimesToLearn(dev, new List<Language>() { Language.CSharp, Language.Python }).Should().Equal(expected);
//    }
//
//    [Fact]
//    public void TotalTimeToLearn_ShouldReturnTheTotalTimeToLearn_WhenCalled()
//    {
//        //Arrange 
//        var timesToLearn = new Dictionary<Language, double>()
//        {
//            { Language.Cobol, 6.6 },
//            { Language.CSharp, 2.76 },
//            { Language.Java, 8.4 },
//            { Language.Cpp, 15.0 },
//        };
//        
//        var sut = new LanguageLearningCalculator(15);
//        
//        //Act + Assert
//        LanguageLearningCalculator.TotalTimeToLearn(timesToLearn).Should().Be(32.76);
//    }
//}