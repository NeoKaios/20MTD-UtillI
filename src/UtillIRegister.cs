using System.Collections.Generic;

namespace UtillI
{
    public static class UtillIRegister
    {
        public static List<Registration> registrations { get; private set; } = new List<Registration>();
        public static void Register(Registration reg)
        {
            registrations.Add(reg);
        }
    }
}