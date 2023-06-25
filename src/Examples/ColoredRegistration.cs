
namespace UtillI.Examples
{
    public class ColoredRegistration : Registration
    {
        private int i = 0;
        private string color;
        public ColoredRegistration(PanelPosition pos, DisplayRule rule, string color = "white") : base(pos, rule)
        {
            this.color = color;
        }
        override public string GetUpdatedText()
        {
            return $"<color={color}>{i++}";
        }
    }
}