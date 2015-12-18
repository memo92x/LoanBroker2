using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GetBanks
{
    class GetBanksProgram
    {
        static void Main(string[] args)
        {
            Console.Title = "Get Banks";
            Control c = new Control();
            ConnectionFactory factory = new ConnectionFactory()
            {
                //commit
                HostName = "datdb.cphbusiness.dk",
                Port = 5672,
                UserName = "student",
                Password = "cph"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "banks_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
                channel.ExchangeDeclare("banks_exchange", ExchangeType.Topic);
                channel.QueueBind("banks_queue", "banks_exchange", "");

                Console.WriteLine("--Getting banks--");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    string[] messageArray = message.Split('&');
                    string ssn = messageArray[0];
                    int duration = Int32.Parse(messageArray[2]);
                    double amount = Convert.ToDouble(messageArray[1]);
                    int creditScore = Convert.ToInt32(messageArray[3]);

                    List<Bank> listBanks = GetBanks(creditScore, amount, duration);
                    // start
                    string sendMessage = ssn + "&" + amount + "&" + duration + "&" + creditScore + "&" + JsonConvert.SerializeObject(listBanks);
                    string println = "SSN:" + ssn + " AMOUNT:" + amount + " DURATION" + duration + " CREDITSCORE" + creditScore + " BANKS{" + BankNamesToString(listBanks) + "}";

                    c.sendMessage("recipientlist_exchange", sendMessage, println);
                };
                channel.BasicConsume(queue: "banks_queue", noAck: true, consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        private static List<Bank> GetBanks(int creditScore, double amount, int duration)
        {
            RuleBase.RuleBaseSoapClient ruleBaseCLient = new RuleBase.RuleBaseSoapClient();
            List<Bank> listBanks = new List<Bank>();
            foreach(var bank in ruleBaseCLient.GetBanks(creditScore, amount, duration))
            {
                listBanks.Add(new Bank(bank.Bankname, bank.Exchange, bank.Normalizer, bank.TranslatorType));
            }
            return listBanks;
        }

        private static string BankNamesToString(List<Bank> listBanks)
        {
            string bankNames = "";
            foreach(Bank bank in listBanks)
            {
                bankNames += bank.Bankname + ",";
            }
            return bankNames.TrimEnd(',');
        }
    }
}
