namespace FileSystem.Display
{
    using System.Reflection;
    using System.Threading.Channels;
    using FileSystem.Data;
    public static class Menu
    {
        #region Properties and Constructor
        private static int SelectedIndex = 0;
        #endregion

        public static int Run(string header, MenuOption[] options)
        {
            ConsoleKey PressedKey;
            SelectedIndex = 0;
            do
            {
                Console.Clear();
                Display(header, options);

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
                    if (SelectedIndex < options.Length - 1)
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
            while (true)
            {
                Write(text, ColorEnum.Important);
                var input = Console.ReadLine();
                if (input != null && input.Trim() != "" && TryParse<T>(input, out result))
                {
                    return result;
                }
                else
                {
                    Write("Incorrect value Type you must enter a " + typeof(T).Name, ColorEnum.ErrorNoBg);
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

        private static void Display(string header, MenuOption[] options)
        {
            Console.ResetColor();
            Console.WriteLine(header);
            for (int i = 0; i < options.Length; i++)
            {
                MenuOption current = options[i];
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

        #endregion
    }
}