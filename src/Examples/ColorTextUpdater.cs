
namespace UtillI.Examples
{
    public class ColorTextUpdater : TextUpdater
    {
        private int i = 0;
        private string color;
        public ColorTextUpdater(string color = "white")
        {
            this.color = color;
        }
        public string GetUpdatedText()
        {
            return $"<color={color}>{i++}";
        }
    }
}