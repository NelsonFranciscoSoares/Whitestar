using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Arrow.DeveloperTest.Services;
using Arrow.DeveloperTest.Factories;
using Arrow.DeveloperTest.Data;
using Arrow.DeveloperTest.Types;

namespace Arrow.DeveloperTest.Runner
{
    class Program
    {
        static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var task = host.RunAsync();

            Main(host.Services);

            return task;
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped<IPaymentService, PaymentService>();
                    services.AddScoped<IPaymentSchemeFactory, PaymentSchemeFactory>();
                    services.AddSingleton<IAccountDataStore, AccountDataStore>();
                });
        }

        static void Main(IServiceProvider serviceProvider)
        {
            while (true)
            {
                Console.WriteLine($"How kind of payment do you intend to make?");
                Console.WriteLine("========================================");
                Console.WriteLine("For Faster Payments writes 0 and press Enter");
                Console.WriteLine("For Backs writes writes 1 and press Enter");
                Console.WriteLine("For Chaps writes writes 2 and press Enter");
                string paymentType = Console.ReadLine();
                int parsedPaymentType = int.Parse(paymentType);


                var valueIsDefinedInEnum = Enum.IsDefined(typeof(PaymentScheme), parsedPaymentType);
                if (valueIsDefinedInEnum == false)
                {
                    Console.WriteLine("Inserted informaction is incorrect");
                    continue;
                }

                Enum.TryParse(paymentType, out PaymentScheme paymentScheme);

                Console.WriteLine("What is your account number");
                var accountNumber = Console.ReadLine();

                Console.WriteLine("What is the amount value");
                var amountValue = Console.ReadLine();

                var makePaymentRequest = new MakePaymentRequest
                {
                    Amount = Decimal.Parse(amountValue),
                    DebtorAccountNumber = accountNumber,
                    PaymentDate = DateTime.UtcNow,
                    PaymentScheme = paymentScheme
                };

                using var scope = serviceProvider.CreateScope();
                var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
                var makePaymentResponse = paymentService.MakePayment(makePaymentRequest);

                if(makePaymentResponse.Success)
                {
                    Console.WriteLine("Operation executed with success!");
                }
                else
                {
                    Console.WriteLine("Problem with this operation!");
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("========================================");
            }
        }
    }
}
