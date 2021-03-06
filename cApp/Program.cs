using ConsoleTables;
using Microsoft.Extensions.Configuration;

namespace cApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //Creating host for using DI in this console app
            using var host = DI.CreateHostBuilder(args).Build();

            //Adding configuration builder for accessing the data in appsettings.json
            var builder = new ConfigurationBuilder()
               .AddJsonFile($"appsettings.json", true, true);

            var config = builder.Build();

            //Getting the top 5 products from the API
            IndexViewDTO model = await OrderUtility.getTopFiveProducts(host.Services);

            if (string.IsNullOrEmpty(model.Message))
            {
                //Preparing the data for ConsoleTable package for showing the data in Readable Table in the Console Output
                var rows = Enumerable.Repeat(model.products, model.products.Count());

                Console.WriteLine("Top 5 sold products orders that are in progress: ");

                //Printing the Data in Table version in Output from ConsoleTable package
                ConsoleTable
                    .From<ProductDTO>(model.products.AsEnumerable())
                    .Configure(o => o.NumberAlignment = Alignment.Right)
                    .Write(Format.Alternative);
            }
            else
            {
                //Returning The Error Message from Api
                Console.WriteLine(model.Message);
            }

            //Letting know what is happening to the user
            Console.WriteLine("Updating stock of top sold product to 25");

            //Updating The Stock of the first product retrieved from api to 25. Using 25 is for this Assessment
            //the quantity can come from the user.
            Console.WriteLine(await productsUtilities.updateQuantity(host.Services));
        }
    }
}





