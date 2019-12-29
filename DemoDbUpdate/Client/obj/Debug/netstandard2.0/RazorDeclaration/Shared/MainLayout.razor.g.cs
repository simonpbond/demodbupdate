#pragma checksum "C:\Users\Simon Bond\source\repos\DemoDbUpdate\DemoDbUpdate\Client\Shared\MainLayout.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a78ddfdd2aac472478d2ef3dad71d78977db63b3"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace DemoDbUpdate.Client.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#line 1 "C:\Users\Simon Bond\source\repos\DemoDbUpdate\DemoDbUpdate\Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#line 2 "C:\Users\Simon Bond\source\repos\DemoDbUpdate\DemoDbUpdate\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#line 3 "C:\Users\Simon Bond\source\repos\DemoDbUpdate\DemoDbUpdate\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#line 4 "C:\Users\Simon Bond\source\repos\DemoDbUpdate\DemoDbUpdate\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#line 5 "C:\Users\Simon Bond\source\repos\DemoDbUpdate\DemoDbUpdate\Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#line 6 "C:\Users\Simon Bond\source\repos\DemoDbUpdate\DemoDbUpdate\Client\_Imports.razor"
using DemoDbUpdate.Client;

#line default
#line hidden
#line 7 "C:\Users\Simon Bond\source\repos\DemoDbUpdate\DemoDbUpdate\Client\_Imports.razor"
using DemoDbUpdate.Client.Shared;

#line default
#line hidden
#line 2 "C:\Users\Simon Bond\source\repos\DemoDbUpdate\DemoDbUpdate\Client\Shared\MainLayout.razor"
using Microsoft.AspNetCore.SignalR.Client;

#line default
#line hidden
    public partial class MainLayout : LayoutComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#line 17 "C:\Users\Simon Bond\source\repos\DemoDbUpdate\DemoDbUpdate\Client\Shared\MainLayout.razor"
       

    public static Action DataChanged;

    protected override async Task OnInitializedAsync()
    {

        var connection = new HubConnectionBuilder()
        .WithUrl("http://localhost:44329/notificationHub")
        .Build();

        connection.Closed += async (error) =>
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await connection.StartAsync();
        };
        connection.On("datachanged", () =>
        {

            DataChanged.Invoke();


        });

        try
        {
            await connection.StartAsync();
            Console.WriteLine("Connection to Signr Hub Started");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        
    }


#line default
#line hidden
    }
}
#pragma warning restore 1591