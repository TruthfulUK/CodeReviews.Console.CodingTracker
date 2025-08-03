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
                : Spectre.Console.ValidationResult.Error("[red]Invalid date, please use DD/MM/YY[/]");
            })
        );
        return DateOnly.ParseExact(dateString, "dd/MM/yy", CultureInfo.InvariantCulture);
    }

    public static TimeOnly TimePrompt()
    {
        var timeString = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter time (24 hour format - HH:mm):")
            .DefaultValue(DateTime.Now.ToString("HH:mm"))
            .Validate(input =>
            {
                return TimeOnly.TryParseExact(input, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                ? Spectre.Console.ValidationResult.Success()
                : Spectre.Console.ValidationResult.Error("[red]Invalid time, please use HH:MM[/]");
            })
        );
        return TimeOnly.ParseExact(timeString, "HH:mm", CultureInfo.InvariantCulture);
    }

    public static void PressKeyToContinue()
    {
        var rule = new Rule().RuleStyle("blue dim");
        AnsiConsole.Write(rule);

        var paddedContinueText =
            new Text("Press [Any Key] to return to the menu...",
            new Style(Color.Blue));
        var paddedContinue = new Padder(paddedContinueText).PadTop(2).PadBottom(2).PadLeft(0);
        AnsiConsole.Write(paddedContinue);
        Console.ReadKey();
    }
}
