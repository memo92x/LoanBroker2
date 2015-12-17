using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;

namespace LoanBroker
{
    /// <summary>
    /// Summary description for LoanRequestService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class LoanRequestService : System.Web.Services.WebService
    {

        [WebMethod]
        public void StartLoanRequest(double amount, int lånetid, string SSN)
        {
            string message = SSN + "&" + amount + "&" + lånetid;
            string print = SSN + "&" + amount + "&" + lånetid;
            sendMessage("credit_exchange", message, print);


        }
        public void sendMessage(string EXCHANGE_NAME, string message, string println)
        {

            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = "datdb.cphbusiness.dk",
                Port = 5672,
                UserName = "student",
                Password = "cph"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                var body = Encoding.UTF8.GetBytes(message);

                channel.ExchangeDeclare(EXCHANGE_NAME, ExchangeType.Topic);
                channel.BasicPublish(exchange: EXCHANGE_NAME, routingKey: "", basicProperties: null, body: body);

                Console.WriteLine(" [x] Sent {0}", println);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

        }
    }
}
