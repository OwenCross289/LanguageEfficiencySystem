//Would use host builder for a more complex solution
using LanguageEfficiencySystem.Console;
using LanguageEfficiencySystem.Models;
using LanguageEfficiencySystem.Repositories;
using LanguageEfficiencySystem.Services;

var devRepository = new DeveloperRepository();

//These values should come in from a config file or user input
const int averageDaysToLearn = 15;
const int developersRequired = 3;
var languagesToLearn = new List<Language> { Language.CSharp, Language.Python };

var calculator = new CandidateLanguageLearningCalculator(averageDaysToLearn, languagesToLearn);
var ranker = new CandidateRanker(devRepository, calculator);
var teamBuilder = new TeamBuilder(developersRequired, ranker);

var questionWriter = new QuestionWriter(teamBuilder, languagesToLearn);

questionWriter.QuestionOne();
questionWriter.QuestionTwo();
questionWriter.QuestionThree();
