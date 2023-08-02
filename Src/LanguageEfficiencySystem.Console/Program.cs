using System.Collections.ObjectModel;
using System.Threading.Channels;
using LanguageEfficiencySystem.Models;

var developers = new List<Developer>
{
    new ("Kari", 32, 10, new ReadOnlyCollection<Language>(new List<Language>()
    {
        Language.CSharp,
        Language.Ruby,
        Language.Cpp,
        Language.Java
    })),
    new ("Lars", 22, 1, new ReadOnlyCollection<Language>(new List<Language>()
    {
        Language.Cpp,
        Language.JavaScript,
        Language.Python
    })),
    new ("Johan", 42, 22, new ReadOnlyCollection<Language>(new List<Language>()
    {
        Language.Sql,
        Language.JavaScript
    })),
    new ("Nils", 28, 4, new ReadOnlyCollection<Language>(new List<Language>()
    {
        Language.JavaScript,
        Language.Java,
        Language.CSharp,
        Language.Php
    })),
    new ("Karl", 52, 30, new ReadOnlyCollection<Language>(new List<Language>()
    {
        Language.Cobol,
        Language.Java,
        Language.Vb6
    })),
    new ("Anna", 38, 10, new ReadOnlyCollection<Language>(new List<Language>()
    {
        Language.Python,
        Language.Php,
        Language.MySql,
        Language.Ruby,
        Language.Xml,
        Language.Html
    })) 
};

var calculator = new LanguageLearningCalculator(15);
//var results = calculator.TimeToLearn(developers[4], new List<Language>()
//{
//    Language.CSharp,
//    Language.Python
//});

Console.WriteLine(calculator.TimeToLearn(developers[0], Language.Python));

