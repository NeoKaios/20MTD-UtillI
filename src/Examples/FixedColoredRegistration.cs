
namespace UtillI.Examples
{
    public class FixedColoredRegistration : Registration
    {
        private int i = 1000;
        private string color = "white";
        public FixedColoredRegistration(string color = "white") : base(PanelPosition.BottomLeft)
        {
            this.color = color;
        }
        override public string GetUpdatedText()
        {
            return $"<color={color}>{i--}";
        }
    }
}