using System;

namespace wending_machine_emulator.Models
{
    /// <summary>
    /// Служебная структура для подсчета сдачи
    /// </summary>
    public class ChangeDiff : IComparable<ChangeDiff>
    {
        public Nominals Nominal { get; set; }
        public int Diff { get; set; }

        public int CompareTo(ChangeDiff other)
        {
            return Diff - other.Diff;
        }
    }
}
