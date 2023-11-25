using Confluent.Kafka;

namespace MailService.Kafka.Config
{
	public class KafkaConfig
	{
		public ConsumerConfig Config(string groupId) => new ConsumerConfig
		{
			BootstrapServers = "localhost:9092",
			GroupId = groupId,
			AutoOffsetReset = AutoOffsetReset.Earliest
		};
	}
}
