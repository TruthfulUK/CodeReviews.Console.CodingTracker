using Spectre.Console;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace CodingTracker.Helpers;
internal class InputHelpers
{
    public static Dictionary<string, TEnum> GetMenuOptions<TEnum>()
        where TEnum : struct, Enum
    {
        return Enum.GetValues<TEnum>()
            .ToDictionary(option => GetEnumDisplayName(option), option => option);
    }

    public static string GetEnumDisplayName(Enum value)
    {
        return value.GetType()
            .GetMember(value.ToString())[0]
            .GetCustomAttribute<DisplayAttribute>()?.Name
            ?? value.ToString();
    }

    public static TEnum SelectionPrompt<TEnum>(Dictionary<string, TEnum> options)
        where TEnum : struct, Enum
    {
        var selectedKey = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Please select an [blue]option[/]:")
            .AddChoices(options.Keys));

        return options[selectedKey];
    }

    public static DateOnly DatePrompt()
    {
        var dateString = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter date (DD/MM/YY):")
            .DefaultValue(DateTime.Now.ToString("dd/MM/yy"))
            .Validate(input =>
            {
                return DateTime.TryParseExact(input, "dd/MM/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                ? Spectre.Console.ValidationResult.Success()
                : Spectre.Console.ValidationResult.Error("\n[red]Invalid date, please use DD/MM/YY[/]\n");
            })
        );
        return DateOnly.ParseExact(dateString, "dd/MM/yy", CultureInfo.InvariantCulture);
    }

    public static TimeOnly TimePrompt()
    {
        var timeString = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter time (24 hour format - HH:MM):")
            .DefaultValue(DateTime.Now.ToString("HH:mm"))
            .Validate(input =>
            {
                return TimeOnly.TryParseExact(input, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                ? Spectre.Console.ValidationResult.Success()
                : Spectre.Console.ValidationResult.Error("\n[red]Invalid time, please use (HH:MM) and enter a valid 24 hour value (e.g., 23:25 for 11:25PM)[/]\n");
            })
        );
        return TimeOnly.ParseExact(timeString, "HH:mm", CultureInfo.InvariantCulture);
    }

    public static int RowIdPrompt(List<int> rowIds)
    {
        var inputId = AnsiConsole.Prompt(
            new TextPrompt<int>("Enter an ID # from the above table:")
            .Validate(input =>
            {
                return rowIds.Contains(input)
                ? Spectre.Console.ValidationResult.Success()
                : Spectre.Console.ValidationResult.Error("\n[red]Invalid ID # - please enter an ID # from the table displayed above[/]\n");
            })
        );

        return inputId;
    }

    public static int PeriodLengthPrompt(string text)
    {
        var period = AnsiConsole.Prompt(
            new TextPrompt<int>(text)
            .Validate(input =>
                input > 0
                ? Spectre.Console.ValidationResult.Success()
                : Spectre.Console.ValidationResult.Error("\n[red]Invalid number, please enter a value greater than 0.[/]\n"))
        );
        return period;
    }

    public static bool ConfirmationPrompt(string text, bool defaultValue)
    {
        var confirmaiton = AnsiConsole.Prompt(
            new TextPrompt<bool>(text)
            .AddChoice(true)
            .AddChoice(false)
            .DefaultValue(defaultValue)
            .WithConverter(choice => choice ? "Y" : "N"));

        return confirmaiton;
    }

    public static void DisplayHeader(string title)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(
            new FigletText("Coding Tracker")
                .Centered()
                .Color(Color.Blue));

        var rule = 
            new Rule($"{title}")
                .RuleStyle("blue dim")
                .Centered();

        AnsiConsole.Write(rule);
    }

    public static void PressKeyToContinue()
    {
        var rule = new Rule().RuleStyle("blue dim");
        AnsiConsole.Write(rule);

        var paddedContinueText =
            new Text("Display paused - press any key to continue",
            new Style(Color.Blue));
        var paddedContinue = new Padder(paddedContinueText).PadTop(1);
        AnsiConsole.Write(paddedContinue);
        Console.ReadKey();
    }
}
