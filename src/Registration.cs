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
        private static int maxId = 0;
        public PanelPosition pos { get; protected set; }
        public DisplayRule rule { get; protected set; }
        public int id { get; }
        public Registration(PanelPosition pos, DisplayRule rule)
        {
            this.id = maxId++;
            this.pos = pos;
            this.rule = rule;
        }
        public abstract string GetUpdatedText();
        public virtual void Init() { }
    }
}