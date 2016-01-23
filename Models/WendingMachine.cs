using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wending_machine_emulator.Models
{
    /// <summary>
    /// Класс модель вендинга singleton
    /// </summary>
    public class WendingMachine
    {
        private Lazy<WendingMachine> _Lazy = new Lazy<WendingMachine>(() => new WendingMachine());

        public WendingMachine Instance { get { return _Lazy.Value; } }

        /// <summary>
        /// Кошелек вендинга
        /// </summary>
        public Wallet Wallet { get; private set; } = new Wallet();

        /// <summary>
        /// Промежуточное хранилище из которого можно вернуть все до покупки, что было положено в вендинг
        /// </summary>
        public Wallet Escrow { get; private set; } = new Wallet();

        /// <summary>
        /// Сумма в эскроу
        /// </summary>
        private int EscrowSum { get; set; }

        /// <summary>
        /// Запсы вендинга
        /// </summary>
        public Dictionary<WendingDrinks, int> Store { get; set; } = new Dictionary<WendingDrinks, int>
            {
                {WendingDrinks.Coffee, 20 },
                {WendingDrinks.Tea, 10 },
                {WendingDrinks.Juice, 15 },
                {WendingDrinks.Latte, 20 }
            };

        /// <summary>
        /// Вставить монету в приемник
        /// </summary>        
        public void PushCoin(Nominals coin)
        {
            Escrow[coin] += 1;
            EscrowSum += (int)coin;
        }

        /// <summary>
        /// 
        /// </summary>      
        public Wallet BuyDrink(WendingDrinks drinkType)
        {
            var price = (int)drinkType;
            if (EscrowSum < price)
                throw new Exception("Недостаточно средств");

            var allCoins = new Wallet();
            allCoins.Flush(Wallet, false);
            allCoins.Flush(Escrow, false);

            var change = CountChange(allCoins, price);

            Escrow.Flush(Wallet);
            EscrowSum = 0;

            Wallet = allCoins;
            return change;
        }

        /// <summary>
        /// Посчитать сдачу
        /// </summary   
        private Wallet CountChange(Wallet allCoins, int price)
        {
            var changeSum = EscrowSum - price;
            return CountChangeRecursive(changeSum, new Wallet(), allCoins);
        }

        /// <summary>
        /// Рекурсивно посчитать сдачу минимально возможным количеством монет
        /// </summary       
        private Wallet CountChangeRecursive(int changeSum, Wallet change, Wallet allCoins)
        {
            if (changeSum == 0) return change;

            var result = (Enum.GetValues(typeof(Nominals)) as Nominals[])
                 .Select(v =>
                 {
                     var diff = changeSum - (int)v;
                     return diff >= 0 && allCoins[v] != 0 ?
                        new { Nominal = v, Diff = diff } :
                        new { Nominal = v, Diff = int.MaxValue };
                 }).Min();                                                           

            if (result.Diff == int.MaxValue)
                throw new Exception("Приносим извенения!К сожалению автомат не может выдать сдачу.");

            allCoins[result.Nominal] -= 1;
            change[result.Nominal] += 1;
            return CountChangeRecursive(result.Diff, change, allCoins);
        }

        private WendingMachine()
        {

        }
    }

    /// <summary>
    /// Товары в вендинге 
    /// </summary>
    public enum WendingDrinks
    {
        Coffee = 18,
        Tea = 13,
        Latte = 21,
        Juice = 35
    }
}


/*
1. Чай = 13 руб, 10 порций. (начальные данные)
2. Кофе = 18 руб, 20 порций.
3. Кофе с молоком = 21 руб, 20 порций.
4. Сок = 35 руб = 15 порций.*/
