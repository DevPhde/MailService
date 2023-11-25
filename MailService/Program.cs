using dotenv.net;
using MailService;
using MailService.Kafka.Consumers;
using MailService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Infrastrucuture
builder.Services.AddInfrastructure(builder.Configuration);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//Kafka Configuration
var serviceProvider = builder.Services.BuildServiceProvider();
using (var scope = serviceProvider.CreateScope())
{
	var scopedServices = scope.ServiceProvider;
	var emailService = scopedServices.GetRequiredService<IEmailService>();

	var kafkaConsumer = new KafkaConsumer(emailService);
	var topicName = "teste";

	var consumer = new Thread(() => kafkaConsumer.StartConsumer(topicName));
	_ = Task.Run(() =>
	{
		consumer.Start();
		consumer.Join();
	});
}

// End Kafka Configuration

app.MapControllers();

app.Run();

