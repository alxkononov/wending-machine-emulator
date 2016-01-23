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
    class WendingController : ApiController
    {
        /// <summary>
        /// Возвращает текущее состояние вендинга 
        /// </summary>     
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
        [HttpPost]
        public HttpResponseMessage PushCoin([FromBody] Nominals nominal)
        {
            return Invoke(() => {
                WendingMachine.Instance.PushCoin(nominal);
                return null;
            });
        }

        /// <summary>
        /// Получить сдачу 
        /// </summary>
        [HttpGet]
        public HttpResponseMessage GetChange()
        {
            return Invoke(() => {

                return WendingMachine.Instance.ReturnEscrow();
            });
        }

        /// <summary>
        /// Купить 
        /// </summary>
        [HttpGet]
        public HttpResponseMessage BuyDrink(WendingDrinks drink)
        {
            return Invoke(() => {
                return WendingMachine.Instance.BuyDrink(drink);
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
                result = res == null ?
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
