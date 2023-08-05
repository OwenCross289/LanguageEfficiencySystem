//Would use host builder for a more complex solution
using System.Collections.ObjectModel;
using LanguageEfficiencySystem.Console;
using LanguageEfficiencySystem.Models;
using LanguageEfficiencySystem.Services;

//Could swap this for a db to allow for inputting devs without recompilation (Repository pattern)
var developers = new List<Developer>
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

//These values should come in from a config file or user input
const int averageDaysToLearn = 15;
const int developersRequired = 3;
var languagesToLearn = new List<Language> { Language.CSharp, Language.Python };

var calculator = new CandidateLanguageLearningCalculator(averageDaysToLearn, languagesToLearn);
var ranker = new CandidateRanker(developers, calculator);
var teamBuilder = new TeamBuilder(developersRequired, ranker);

var questionWriter = new QuestionWriter(teamBuilder, languagesToLearn);

questionWriter.QuestionOne();
questionWriter.QuestionTwo();
questionWriter.QuestionThree();
