using System;
using System.Collections.Generic;

namespace wending_machine_emulator.Models
{
    /// <summary>
    /// Модель кошелька 
    /// </summary>
    public class Wallet
    {
        /// <summary>
        /// Словарь в котором хранятся монеты
        /// </summary>
        private Dictionary<Nominals, int> _dict = new Dictionary<Nominals, int>();

        // индексатор по номиналу
        public int this[Nominals key]
        {
            get
            {
                if (!_dict.ContainsKey(key)) return 0;
                return _dict[key];
            }
            set
            {
                if(_dict.ContainsKey(key))
                {
                    _dict[key] = value;
                    return;
                }

                _dict.Add(key, value);                
            }
        }

        /// <summary>
        /// Пересыпать монеты из другого кошелька 
        /// </summary>       
        public void Flush(Wallet wallet, bool leaveEmpty = true)
        {
            var nominals = Enum.GetValues(typeof(Nominals)) as Nominals[];
            foreach (var nominal in nominals)
            {
                this[nominal] += wallet[nominal];
                wallet[nominal] = leaveEmpty ?  0: wallet[nominal];
            }
        }
    }

}
