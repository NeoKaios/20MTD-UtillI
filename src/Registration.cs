namespace UtillI
{
    public enum PanelPosition
    {
        BottomLeft,
        BottomRight
    }
    public enum DisplayRule
    {
        Always,
        PauseOnly,
        CombatOnly,
        Never
    }
    public abstract class Registration
    {
        public PanelPosition pos { get; protected set; }
        public DisplayRule rule { get; protected set; }
        public Registration(PanelPosition pos, DisplayRule rule = DisplayRule.PauseOnly)
        {
            this.pos = pos;
            this.rule = rule;
        }
        public abstract string GetUpdatedText();
        public virtual void Init() { }
    }
}