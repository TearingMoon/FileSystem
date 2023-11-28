namespace FileSystem.Display
{
    using System.Reflection;
    using System.Threading.Channels;
    using FileSystem.Data;
    class Menu
    {
        #region Properties and Constructor
        private int SelectedIndex;
        private MenuOption[] Options;
        private string Text;

        public Menu(string text, MenuOption[] options, int StartingIndex = 0)
        {
            Text = text;
            Options = options;
            SelectedIndex = StartingIndex;
        }
        #endregion

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
                    var attributes = GetColorInfo(current.ColorData);
                    Console.ForegroundColor = attributes != null ? attributes.ForegroundColor : ConsoleColor.Black;
                    Console.BackgroundColor = attributes != null ? attributes.BackgroundColor : ConsoleColor.Black;
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

        public static T RequestStream<T>(string text)
        {
            T result;
            //Bucle infinito de solicitud de elemento correcto
            while (true)
            {
                Console.WriteLine(text);
                var input = Console.ReadLine();
                if (input != null && input.Trim() != "" && TryParse<T>(input, out result))
                {
                    return result;
                }
                else
                {
                    Menu.Write("Incorrect value Type", ColorEnum.Error);
                }
            }
        }

        public static void Write(string text, ColorEnum color)
        {
            var attributes = GetColorInfo(color);
            Console.BackgroundColor = attributes != null ? attributes.BackgroundColor : ConsoleColor.Black;
            Console.ForegroundColor = attributes != null ? attributes.ForegroundColor : ConsoleColor.Black;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        #region Private
        private static bool TryParse<T>(string input, out T result)
        {
            try
            {
                result = (T)Convert.ChangeType(input, typeof(T));
                return true;
            }
            catch (Exception)
            {
                result = default!;
                return false;
            }
        }

        private static ColorInfoAttribute? GetColorInfo(ColorEnum colorScheme)
        {
            var memberInfo = typeof(ColorEnum).GetMember(colorScheme.ToString());
            return (ColorInfoAttribute?)Attribute.GetCustomAttribute(memberInfo[0], typeof(ColorInfoAttribute));
        }

        #endregion
    }
}