# Demo UI Update with Signalr
Demonstrates using Signalr to update data automatically in the Blazor client.  
  
    
  
  
### Client Side
There is currently no SignalR .NET Client available for Blazor WebAssembly, this will be ready in May 2020.  
Because of this, we add the SignalR javascript library to the Client project using these instructions in the section named **Add the SignalR client library** [found here](https://docs.microsoft.com/en-us/aspnet/core/tutorials/signalr?tabs=visual-studio&view=aspnetcore-3.1).  
  
Then in the client we add our own javascript file `notifications.js` which will start the signalr connection.  
Make sure `signalr.min.js` and `notifications.js` is added inside the `Index.html` of your client project.  

Inside `notifications.js` we have the following code:
```
connection.on("datachanged", function () {
    console.log("Javascript received a datachanged message from the SignalR Server");
    DotNet.invokeMethodAsync('DemoDbUpdate.Client', 'DataChanged');
});
```
The above code listen's for the "`datachanged`" message from the SignalR Server.  
Whenever it receives this message, it will call a .NET Method named `DataChanged` in the `DemoDbUpdate.Client` assembly, [how this works can be found here](https://docs.microsoft.com/en-us/aspnet/core/blazor/javascript-interop?view=aspnetcore-3.1) under the heading **Invoke .NET methods from JavaScript functions**  

The DataChanged method is located inside a class we have made named `InteropMethods.cs`. This is the .NET method that the javascript function calls. The code inside this method is as follows:
```
        public static Action OnDataChanged;

        [JSInvokable]
        public static void DataChanged()
        {
            Console.WriteLine("Successfully received notification that data has changed from Server SignalR Hub");
            OnDataChanged.Invoke();
        }

    }
```
We have an Action named `OnDataChanged` that we raise with `OnDataChanged.Invoke()`.

Then inside our `FetchData.razor` page we can subscribe to the `OnDataChanged` Action and whenever the Action is raised we can refresh our data in the component.
```
    protected override async Task OnInitializedAsync()
    {
        Interop.InteropMethods.OnDataChanged += async () =>
        {
            Console.WriteLine("Notification received from server that data has changed.");
            await UpdateData();
            StateHasChanged();
        };

        await UpdateData();


    }
```
### Server Side  
To setup signal on the server, we create a new class named `NotificationHub.cs`.  
As this class will be a SignalR Hub we inherit AspNetCore.SignalR.Hub:  
`public class NotificationHub : Hub`  
  
  
Then inside our server's `Startup.cs` we add:  
`services.AddSignalR();` inside `ConfigureServices`.  
`endpoints.MapHub<NotificationHub>("/notificationHub");` inside `Configure`.
  
Lastly, inside our `WeatherForecastController` we need to inject the HubContext, so that we can access HubContext to send Signalr messages from within our contoller.  

```
        private readonly ILogger<WeatherForecastController> logger;
        private readonly IHubContext<NotificationHub> _hubContext;


        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHubContext<NotificationHub> hubContext)
        {
            this.logger = logger;
            _hubContext = hubContext;
        }
```  
  
Then inside our WeatherForecast controller's `AddData()` method that saves data to the database, we use the `_hubContext` to send the notification that data has changed to all the connected signalr clients.  

`await _hubContext.Clients.All.SendAsync("datachanged");`
  
The complete method:  
```
       [HttpGet("adddata")]
        public async Task<IActionResult> AddData()
        {
            var rng = new Random();
            var newData = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });

            MockDatabase.SaveToDatabase(newData);
            await _hubContext.Clients.All.SendAsync("datachanged");

            return Ok(string.Format("{0} - New data added to the server's database.", DateTime.Now.ToString()));
        }
```  
  
  
Notice that we specify '`datachanged`' and this matches the message type we are listening for inside our `notifications.js` file in the Client.  
  
This is a quick demonstration only and not meant to show the perfect architecture of a real world application. 
 



