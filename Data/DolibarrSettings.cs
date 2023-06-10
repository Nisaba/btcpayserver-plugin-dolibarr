using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BTCPayServer.Plugins.Dolibarr.Data
{
    public class DolibarrSettings
    {
        [Display(Name = "URL of your Dolibarr Rest API")]
        [Url]
        [Required]
        public string Url { get; set; }

        [Display(Name = "API Token")]
        [Required]
        public string Token{ get; set; }

        [Display(Name = "Main currency of your Dolibarr instance")]
        [StringLength(3)]
        [Required]
        public string Currency { get; set; }

        [Display(Name = "The ID of your BTC bank account in Dolibarr")]
        [Required]
        public int BankAccountID { get; set;}

        [Display(Name = "Dolibarr plugin enabled")] 
        public bool IsEnabled { get; set; }

    }
}
