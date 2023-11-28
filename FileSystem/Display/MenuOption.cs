namespace FileSystem.Display
{
    class MenuOption
    {
        public ColorEnum ColorData;
        public string Text;

        public MenuOption(string text, ColorEnum colorData)
        {
            Text = text;
            ColorData = colorData;
        }

        public MenuOption(string text){
            Text = text;
            ColorData = ColorEnum.Important;
        }

    }
}