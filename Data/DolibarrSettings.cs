﻿using System;
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
        [Required]
        public string Url { get; set; }

        [Display(Name = "API Token")]
        [Required]
        public string Token{ get; set; }

        [Display(Name = "Main currency of your Dolibarr instance")]
        [MaxLength(3)]
        [Required]
        public string Currency { get; set; }

        [Display(Name = "The ID of your BTC account in Dolibarr")]
        [Required]
        public int BankAccountID { get; set;}

    }
}