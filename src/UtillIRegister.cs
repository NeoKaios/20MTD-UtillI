using System.Collections.Generic;

namespace UtillI
{
    public enum PanelPosition
    {
        BottomLeft,
        BottomRight
    }

    public struct Registration
    {
        public PanelPosition pos { get; }
        public TextUpdater updater { get; }
        public int id { get; }
        public Registration(PanelPosition pos, TextUpdater updater)
        {
            this.id = UtillIRegister.id++;
            this.pos = pos;
            this.updater = updater;
        }
    }
    public static class UtillIRegister
    {
        public static int id = 0;
        public static List<Registration> registrations { get; private set; } = new List<Registration>();
        public static void Register(PanelPosition pos, TextUpdater updater)
        {
            registrations.Add(new Registration(pos, updater));
        }
    }
}