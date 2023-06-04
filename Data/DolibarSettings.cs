using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BTCPayServer.Plugins.Dolibarr.Data
{
    public class DolibarSettings
    {
        [Display(Name = "URL of your Dolibarr Rest API")]
        public string Url { get; set; }

        [Display(Name = "API Token")]
        public string Token{ get; set; }

        [Display(Name = "Main currency of your Dolibarr instance")]
        public string Currency { get; set; }

        [Display(Name = "The ID of your BTC account in Dolibarr")]
        public int BankAccountID { get; set;}
    }
}
