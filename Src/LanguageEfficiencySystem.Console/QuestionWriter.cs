using System.Globalization;
using LanguageEfficiencySystem.Models;
using LanguageEfficiencySystem.Services;
using Spectre.Console;

namespace LanguageEfficiencySystem.Console;

public class QuestionWriter
{
    private readonly TeamBuilder _team;
    private readonly IEnumerable<Language> _languagesToLearn;

    public QuestionWriter(TeamBuilder team, IEnumerable<Language> languagesToLearn)
    {
        _team = team;
        _languagesToLearn = languagesToLearn;
        AnsiConsole.WriteLine(
            $"Code4Cash AS has been given an assignment that requires 3 developers to work with both C# and Python.{Environment.NewLine}");
    }

    public void QuestionOne()
    {
        AnsiConsole.WriteLine("1. Which employees should be moved to this new team?");
        var table = BuildTableHeaders(_languagesToLearn);
        BuildTableRows(_team.Team, table);
        AnsiConsole.Write(table);
    }

    public void QuestionTwo()
    {
        AnsiConsole.WriteLine($"{Environment.NewLine}2. How long will it take before the team is operational?");
        AnsiConsole.WriteLine($"It will take the team {_team.TimeBeforeOperational} days to get operational.");
    }
    
    public void QuestionThree()
    {
        AnsiConsole.WriteLine(
            $@"{Environment.NewLine}3. In what order should the remaining employees be considered if the team is to be expanded?");
        AnsiConsole.WriteLine("The order for the rest of the developers to considered to join the team are: ");
    
        var table = BuildTableHeaders(_languagesToLearn);
        BuildTableRows(_team.RunnersUp, table);
        AnsiConsole.Write(table);
    }
    
    private static Table BuildTableHeaders(IEnumerable<Language> languages)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumns("Name", "Age", "Experience (years)");
        foreach (var language in languages)
        {
            table.AddColumn($"{language.ToString()}");
        }

        table.AddColumn("Total time to learn (days)");
        return table;
    }

    private static void BuildTableRows(IEnumerable<CandidateLanguageLearningResult> languageLearningCalculatorResults, Table table)
    {
        foreach (var member in languageLearningCalculatorResults)
        {
            var results = new List<string>
            {
                member.Dev.Name,
                member.Dev.Age.ToString(),
                member.Dev.YearsOfExperience.ToString()
            };
            results.AddRange(member.TimesToLearn.Select(timesToLearn =>
                timesToLearn.Value.ToString(CultureInfo.InvariantCulture)));
            results.Add(member.TotalTimeToLearn.ToString(CultureInfo.InvariantCulture));

            table.AddRow(results.ToArray());
        }
    }
}