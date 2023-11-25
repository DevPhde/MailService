using Confluent.Kafka;
using MailService.Entities;
using MailService.Kafka.Config;
using MailService.Model;
using MailService.Services;
using Newtonsoft.Json.Linq;

namespace MailService.Kafka.Consumers
{
	public class KafkaConsumer
	{
		private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
		private readonly IEmailService _emailService;

		public KafkaConsumer(IEmailService emailService)
		{
			_emailService = emailService;
		}

		public void StartConsumer(string topic)
		{
			Console.WriteLine($"Started consumer for topic: {topic}");

			try
			{
				var kafkaConfig = new KafkaConfig();
				using (var consumer = new ConsumerBuilder<Ignore, string>(kafkaConfig.Config(topic)).Build())
				{
					consumer.Subscribe(topic);

					try
					{
						while (true)
						{
							var cr = consumer.Consume(cancellationTokenSource.Token);
							Console.WriteLine($"Message Read ({topic}): {cr.Message.Value}");
							JObject json = JObject.Parse(cr.Message.Value);
							Mail mail = new Mail
							(
								(int)json["UserId"],
								(string)json["Name"],
								(string)json["Email"],
								(EmailTypeEnum)(int)json["MailType"],
								(string)json["Message"]
							);

							_emailService.SendMail(mail);
						}
					}
					catch (OperationCanceledException)
					{
						consumer.Close();
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
	}
}
