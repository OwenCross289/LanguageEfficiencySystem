using System.Collections.ObjectModel;
using System.Globalization;
using LanguageEfficiencySystem.Models;
using Spectre.Console;

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

var languagesToLearn = new ReadOnlyCollection<Language>(new List<Language> { Language.CSharp, Language.Python });
var calculator = new LanguageLearningCalculator(15, languagesToLearn);
var teamBuilder = new TeamBuilder(3, developers, calculator);

Console.WriteLine(
    $"Code4Cash AS has been given an assignment that requires 3 developers to work with both C# and Python.{Environment.NewLine}");
Question1();
Question2();
Question3();

void Question1()
{
    AnsiConsole.WriteLine("1. Which employees should be moved to this new team?");

    var table = BuildTableHeaders(languagesToLearn);
    BuildTableRows(teamBuilder.Team, table);
    AnsiConsole.Write(table);
}

void Question2()
{
    AnsiConsole.WriteLine($"{Environment.NewLine}2. How long will it take before the team is operational?");
    AnsiConsole.WriteLine($"It will take the team {teamBuilder.TimeBeforeOperational} days to get operational.");
}

void Question3()
{
    AnsiConsole.WriteLine(
        $@"{Environment.NewLine}3. In what order should the remaining employees be considered if the team is to be expanded?");
    AnsiConsole.WriteLine("The order for the rest of the developers to considered to join the team are: ");
    var table = BuildTableHeaders(languagesToLearn);
    BuildTableRows(teamBuilder.RunnersUp, table);
    AnsiConsole.Write(table);
}

Table BuildTableHeaders(IEnumerable<Language> readOnlyCollection)
{
    var table = new Table();
    table.Border(TableBorder.Rounded);
    table.AddColumns("Name", "Age", "Experience (years)");
    foreach (var language in readOnlyCollection) table.AddColumn($"{language.ToString()}");

    table.AddColumn("Total time to learn (days)");
    return table;
}

void BuildTableRows(IEnumerable<LanguageLearningCalculatorResult> languageLearningCalculatorResults, Table table)
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