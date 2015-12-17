using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceLibrary
{
    public class Bank
    {
        private string bankname;
        private string exchange;
        private string normalizer;
        private string translatorType;

        public Bank(string bankname, string exchange, string normalizer, string translatorType)
        {
            this.bankname = bankname;
            this.exchange = exchange;
            this.normalizer = normalizer;
            this.translatorType = translatorType;
        }

        public Bank()
        {

        }

        public Bank(string bankname, string exchange)
        {
            this.bankname = bankname;

            this.exchange = exchange;
        }

        public string Bankname {
            get { return bankname; }
            set { bankname = value; }
        }

        public string Exchange {
            get { return exchange; }
            set { exchange = value; }
        }

        public string Normalizer
        {
            get { return normalizer; }
            set { normalizer = value; }
        }

        public string TranslatorType
        {
            get { return translatorType; }
            set { translatorType = value; }
        }



    }
}