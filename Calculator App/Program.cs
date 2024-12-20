using Calculator_App;
using Microsoft.Extensions.DependencyInjection;
using AppContext = Calculator_App.AppContext;


var startup = new StartUp();

var serviceCollection = new ServiceCollection();

startup.ConfigureServices(serviceCollection);

var serviceProvider = serviceCollection.BuildServiceProvider();

using var context = serviceProvider.GetRequiredService<AppContext>();


var tableMethod = serviceProvider.GetRequiredService<IUserAction>();
tableMethod.GetUserInput();


