enum ColorEnum
{
    [ColorInfo(ConsoleColor.Black, ConsoleColor.White)]
    Default,

    [ColorInfo(ConsoleColor.Red, ConsoleColor.Black)]
    Error,

    [ColorInfo(ConsoleColor.Green, ConsoleColor.Black)]
    Success,

    [ColorInfo(ConsoleColor.White, ConsoleColor.Black)]
    Important,



}

#region Atributes
[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class ColorInfoAttribute : Attribute
{
    public ConsoleColor BackgroundColor { get; }
    public ConsoleColor ForegroundColor { get; }

    public ColorInfoAttribute(ConsoleColor backgroundColor, ConsoleColor foregroundColor)
    {
        BackgroundColor = backgroundColor;
        ForegroundColor = foregroundColor;
    }
}
#endregion