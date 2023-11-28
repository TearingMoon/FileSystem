namespace FileSystem.Display
{
    class MenuOption
    {
        public ConsoleColor BackgroundColor;
        public ConsoleColor TextColor;
        public string Text;

        public MenuOption(string text, ConsoleColor backgrounColor = ConsoleColor.White, ConsoleColor textColor = ConsoleColor.Black)
        {
            Text = text;
            BackgroundColor = backgrounColor;
            TextColor = textColor;
        }

    }
}