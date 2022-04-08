﻿using Microsoft.AspNetCore.Components.Web;
﻿using System.Globalization;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Net.Http;
using MudBlazor.Docs.Wasm;
using MudBlazor.Docs.Extensions;
using MudBlazor.Docs.Services;
using MudBlazor.Docs.Services.Notifications;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.TryAddDocsViewServices();
builder.Services.AddLocalization();

var build = builder.Build();

CultureInfo culture = new CultureInfo("pl-PL");

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

var notificationService = build.Services.GetService<INotificationService>();
if (notificationService is InMemoryNotificationService inmemoryService)
{
    inmemoryService.Preload();
}

await build.RunAsync();
