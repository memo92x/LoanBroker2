using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ServiceLibrary
{
    /// <summary>
    /// Summary description for RuleBase
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RuleBase : System.Web.Services.WebService
    {

        [WebMethod]
        public List<Bank> GetBanks(int creditScore, double amount, int duration)
        {
            //onkel
            //xml, json, messaging, ws
            Bank bank1 = new Bank("CPHBusinessBank", "cph_exchange", "normalizerXML_queue", "XML");
            Bank bank2 = new Bank("NordeaBank", "nordea_exchange", "normalizerJSON_queue", "JSON");
            Bank bank3 = new Bank("DanskeBank", "danske_exchange", "normalizerMESSAGING_queue", "MESSAGING");
            Bank bank4 = new Bank("Vivus", "vivus_exchange", "normalizerWS_queue", "WS");

            List<Bank> banks = new List<Bank>();

            if(creditScore >= 200 && amount <= 20000 && duration <= 3)
            {
                banks.Add(bank2);
                banks.Add(bank3);
                banks.Add(bank4);
            }
            else if(creditScore <= 200 && amount <= 15000 && duration >= 6)
            {
                banks.Add(bank4);
            }
            else if(creditScore > 700 && amount >= 100000 && duration >= 1)
            {
                banks.Add(bank2);
                banks.Add(bank3);
            }
            else
            {
                banks.Add(bank1);
                banks.Add(bank2);
                banks.Add(bank3);
                banks.Add(bank4);
            }
            

            return banks;
        }
    }
}
