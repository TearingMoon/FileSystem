namespace FileSystem.Display
{
    class Menu
    {
        private int SelectedIndex;
        private MenuOption[] Options;
        private string Text;

        public Menu(string text, MenuOption[] options, int StartingIndex = 0)
        {
            Text = text;
            Options = options;
            SelectedIndex = StartingIndex;
        }

        private void Display()
        {
            Console.WriteLine(Text);
            for (int i = 0; i < Options.Length; i++)
            {
                MenuOption current = Options[i];
                string symbol;

                if (i == SelectedIndex)
                {
                    symbol = ">> ";
                    Console.ForegroundColor = current.TextColor;
                    Console.BackgroundColor = current.BackgroundColor;
                }
                else
                {
                    symbol = "";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine($"{symbol}{current.Text}");
            }
            Console.ResetColor();
        }

        public int Run()
        {
            ConsoleKey PressedKey;
            do
            {
                Console.Clear();
                Display();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                PressedKey = keyInfo.Key;

                if (PressedKey == ConsoleKey.UpArrow)
                {
                    if (SelectedIndex > 0)
                    {
                        SelectedIndex--;
                    }
                }
                else if (PressedKey == ConsoleKey.DownArrow)
                {
                    if (SelectedIndex < Options.Length - 1)
                    {
                        SelectedIndex++;
                    }
                }

            } while (PressedKey != ConsoleKey.Enter && PressedKey != ConsoleKey.RightArrow);
            return SelectedIndex;
        }
    }
}