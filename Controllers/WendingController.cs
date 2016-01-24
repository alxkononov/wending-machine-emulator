using System.Net.Http;
using System.Text;
using System.Web.Http;
using wending_machine_emulator.Models;
using Newtonsoft.Json;
using System;

namespace wending_machine_emulator.Controllers
{
    /// <summary>
    /// Контроллер доступа к функциям вендинга 
    /// </summary>
    public class WendingController : ApiController
    {
        /// <summary>
        /// Возвращает текущее состояние вендинга 
        /// </summary>     
        [Route("getstate")]
        [HttpGet]   
        public HttpResponseMessage GetState()
        {
            return Invoke(() =>
            {
                return WendingMachine.Instance.GetState();
            });         
        }

        /// <summary>
        /// Засунуть монету
        /// </summary>    
        [Route("pushcoin/{nominal}")]
        [HttpPost]
        public HttpResponseMessage PushCoin(int nominal)
        {
            return Invoke(() => {
                if (!Enum.IsDefined(typeof(Nominals), nominal))
                    throw new Exception("Монеты такого номинала не принимаются вендингом");
                
                WendingMachine.Instance.PushCoin((Nominals)nominal);
                return null;
            });
        }

        /// <summary>
        /// Получить сдачу 
        /// </summary>
        [Route("getchange")]
        [HttpGet]
        public HttpResponseMessage GetChange()
        {
            return Invoke(() => {
                return WendingMachine.Instance.ReturnEscrow().ToArray();
            });
        }

        /// <summary>
        /// Купить 
        /// </summary>
        [Route("buydrink/{drink}")]
        [HttpGet]
        public HttpResponseMessage BuyDrink(WendingDrinks drink)
        {
            return Invoke(() => {
                var change = WendingMachine.Instance.BuyDrink(drink);
                return change.ToArray();
            });
        }

        /// <summary>
        /// Инвокер с обработкой ошибок 
        /// </summary>      
        private HttpResponseMessage Invoke(Func<object> func)
        {
            string result;
            try
            {
                var res = func();
                result =  res == null ?
                        JsonConvert.SerializeObject(new  { error = 0}):
                        JsonConvert.SerializeObject(res);
            }
            catch (Exception ex)
            {
                result = JsonConvert.SerializeObject(new { error = 1, msg = ex.Message });
            }


            var resp = new HttpResponseMessage();
            resp.Content = new StringContent(result, Encoding.UTF8, "application/json");
            return resp;
        }
    }

}
