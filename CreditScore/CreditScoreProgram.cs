using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditScore
{
    class CreditScoreProgram
    {
        static void Main(string[] args)
        {
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
                channel.QueueDeclare(queue: "credit_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
                channel.ExchangeDeclare("credit_exchange", ExchangeType.Topic);
                channel.QueueBind("credit_queue", "credit_exchange", "");

                Console.WriteLine("--Creditservice ready to receive request--");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    string[] messageArray = message.Split('&');
                    string ssn = messageArray[0];
                    int duration = Int32.Parse(messageArray[2]);
                    double amount = Convert.ToDouble(messageArray[1]);
                    
                    // start
                    CreditService.CreditScoreServiceClient creditService = new CreditService.CreditScoreServiceClient();
                    int creditScore = creditService.creditScore(ssn);
                    
                    Console.WriteLine(" [x] Received {0}", creditScore + "SSN: " + ssn + "Duration: "+ duration + "Amount: " + amount);
                };
                channel.BasicConsume(queue: "credit_queue", noAck: true, consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
