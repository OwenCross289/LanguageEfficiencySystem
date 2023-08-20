using LanguageEfficiencySystem.Builders;
using LanguageEfficiencySystem.Models;

namespace LanguageEfficiencySystem.Tests;

public class LanguageLearningModifierBuilderTests
{
    [GwtFact(given: $"a {nameof(LanguageLearningModifierBuilder)}",
        when: "constructed",
        then: $"{nameof(LanguageLearningModifierBuilder.GetModifier)} returns what was passed in")]
    public void T1()
    {
        //Arrange
        var sut = new LanguageLearningModifierBuilder(baseModifier: 1);
        const double expected = 1.0;
        
        //Act
        var actual = sut.GetModifier();

        //Assert
        actual.Should().Be(expected);
    }
    
    [GwtFact(given: $"a {nameof(LanguageLearningModifierBuilder)}",
        when: "constructed and the modifier is increased",
        then: $"{nameof(LanguageLearningModifierBuilder.GetModifier)} returns what was passed in and resets the modifier")]
    public void T2()
    {
        //Arrange
        var sut = new LanguageLearningModifierBuilder(baseModifier: 1);
        const double preResetExpected = 1.1046221254112045;
        const double postResetExpected = 1.0;
        
        //Act
        sut.BuildForYearsOfExperience(yearsOfExperience: 10);
        var preResetActual = sut.GetModifier();
        var postResetActual = sut.GetModifier();
        
        //Assert
        preResetActual.Should().Be(preResetExpected);
        postResetActual.Should().Be(postResetExpected);
    }
    
    [GwtTheory(given: $"a {nameof(LanguageLearningModifierBuilder)}",
        when: $"{nameof(LanguageLearningModifierBuilder.BuildForAge)} is called",
        then: $"{nameof(LanguageLearningModifierBuilder.GetModifier)} returns correct values")]
    [InlineData(1, 1.2)]
    [InlineData(10, 1.2)]
    [InlineData(24, 1.2)]
    [InlineData(25, 1.2)]
    [InlineData(26, 1.19)]
    [InlineData(30, 1.15)]
    [InlineData(40, 1.05)]
    [InlineData(44, 1.01)]
    [InlineData(45, 1.0)]
    [InlineData(46, 1.0)]
    public void T3(int age, double expectedModifier)
    {
        //Arrange
        var sut = new LanguageLearningModifierBuilder(baseModifier: 1);
        
        //Act
        sut.BuildForAge(age);
        var actual = sut.GetModifier();
        
        //Assert
        actual.Should().Be(expectedModifier);
    }
    
    [GwtTheory(given: $"a {nameof(LanguageLearningModifierBuilder)}",
        when: $"{nameof(LanguageLearningModifierBuilder.BuildForYearsOfExperience)} is called",
        then: $"{nameof(LanguageLearningModifierBuilder.GetModifier)} returns correct values")]
    [InlineData(1, 1.01)]
    [InlineData(2, 1.0201)]
    [InlineData(3, 1.0303010000000001)]
    [InlineData(4, 1.04060401)]
    [InlineData(5, 1.0510100501000001)]
    [InlineData(6, 1.0615201506010001)]
    [InlineData(7, 1.07213535210701)]
    [InlineData(8, 1.0828567056280802)]
    [InlineData(9, 1.0936852726843609)]
    [InlineData(10, 1.1046221254112045)]
    [InlineData(15, 1.1609689553699987)]
    [InlineData(20, 1.220190039947967)]
    [InlineData(30, 1.347848915332906)]
    [InlineData(40, 1.4888637335882213)]
    [InlineData(50, 1.6446318218438827)]
    public void T4(int yearsOfExperience, double expectedModifier)
    {
        //Arrange
        var sut = new LanguageLearningModifierBuilder(baseModifier: 1);
        
        //Act
        sut.BuildForYearsOfExperience(yearsOfExperience);
        var actual = sut.GetModifier();
        
        //Assert
        actual.Should().Be(expectedModifier);
    }
    
    [GwtTheory(given: $"a {nameof(LanguageLearningModifierBuilder)}",
        when: $"{nameof(LanguageLearningModifierBuilder.BuildForCustomLanguageRules)} is called",
        then: $"{nameof(LanguageLearningModifierBuilder.GetModifier)} returns correct values")]
    [MemberData(nameof(BuildForCustomLanguageRulesMemberData))]
    public void T5(Language languageToLearn, List<Language> knownLanguages, double expectedModifier)
    {
        //Arrange
        var sut = new LanguageLearningModifierBuilder(baseModifier: 1);
        
        //Act
        sut.BuildForCustomLanguageRules(languageToLearn, knownLanguages);
        var actual = sut.GetModifier();
        
        //Assert
        actual.Should().Be(expectedModifier);
    }
    
    [GwtTheory(given: $"a {nameof(LanguageLearningModifierBuilder)}",
        when: $"{nameof(LanguageLearningModifierBuilder.BuildForKnownLanguages)} is called",
        then: $"{nameof(LanguageLearningModifierBuilder.GetModifier)} returns correct values")]
    [MemberData(nameof(BuildForKnownLanguagesMemberData))]
    public void T6(IEnumerable<Language> languages, double expectedModifier)
    {
        //Arrange
        var sut = new LanguageLearningModifierBuilder(baseModifier: 1);
        
        //Act
        sut.BuildForKnownLanguages(languages);
        var actual = sut.GetModifier();
        
        //Assert
        actual.Should().Be(expectedModifier);
    }

    public static IEnumerable<object[]> BuildForCustomLanguageRulesMemberData =>
        new List<object[]>
        {
            new object[]
            {
                Language.CSharp, 
                new List<Language>(){Language.Cpp, Language.Java},
                2.0
            },
            new object[]
            {
                Language.CSharp, 
                new List<Language>(){Language.Java},
                1.2
            },
            new object[]
            {
                Language.CSharp, 
                new List<Language>(){Language.Cpp},
                1.2
            }
        };
    
    public static IEnumerable<object[]> BuildForKnownLanguagesMemberData => 
        new List<object[]>
        {
            new object[] { new List<Language>(){ Language.CSharp,
                Language.Java,
                Language.Cpp,
                Language.Ruby,
                Language.JavaScript,
                Language.Python,
                Language.Sql,
                Language.MySql,
                Language.Php,
                Language.Cobol,
                Language.Vb6,
                Language.Xml,
                Language.Html}, 2.719623726164499},
            new object[] { new List<Language>(){Language.Java,
                Language.Cpp,
                Language.Ruby,
                Language.JavaScript,
                Language.Python,
                Language.Sql,
                Language.MySql,
                Language.Php,
                Language.Cobol,
                Language.Vb6,
                Language.Xml,
                Language.Html }, 2.5181701168189803},
            new object[] { new List<Language>(){ Language.Cpp,
                Language.Ruby,
                Language.JavaScript,
                Language.Python,
                Language.Sql,
                Language.MySql,
                Language.Php,
                Language.Cobol,
                Language.Vb6,
                Language.Xml,
                Language.Html}, 2.331638997054611},
            new object[] { new List<Language>(){Language.Ruby,
                Language.JavaScript,
                Language.Python,
                Language.Sql,
                Language.MySql,
                Language.Php,
                Language.Cobol,
                Language.Vb6,
                Language.Xml,
                Language.Html }, 2.158924997272788},
            new object[] { new List<Language>(){Language.JavaScript,
                Language.Python,
                Language.Sql,
                Language.MySql,
                Language.Php,
                Language.Cobol,
                Language.Vb6,
                Language.Xml,
                Language.Html }, 1.9990046271044333},
            new object[] { new List<Language>(){Language.Python,
                Language.Sql,
                Language.MySql,
                Language.Php,
                Language.Cobol,
                Language.Vb6,
                Language.Xml,
                Language.Html }, 1.8509302102818825},
            new object[] { new List<Language>(){ Language.Sql,
                Language.MySql,
                Language.Php,
                Language.Cobol,
                Language.Vb6,
                Language.Xml,
                Language.Html }, 1.7138242687795209},
            new object[] { new List<Language>(){ Language.MySql,
                Language.Php,
                Language.Cobol,
                Language.Vb6,
                Language.Xml,
                Language.Html}, 1.5868743229440005},
            new object[] { new List<Language>(){Language.Php,
                Language.Cobol,
                Language.Vb6,
                Language.Xml,
                Language.Html }, 1.4693280768000005},
            new object[] { new List<Language>(){Language.Cobol,
                Language.Vb6,
                Language.Xml,
                Language.Html }, 1.3604889600000003},
            new object[] { new List<Language>(){ Language.Vb6,
                Language.Xml,
                Language.Html}, 1.2597120000000002},
            new object[] { new List<Language>(){ Language.Xml,
                Language.Html}, 1.1664},
            new object[] { new List<Language>(){Language.Html}, 1.08}
  };
}