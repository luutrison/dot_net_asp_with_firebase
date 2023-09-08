using BAN_BANH.Method;
using BAN_BANH.Model;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Caching.Memory;

Environment.SetEnvironmentVariable(VARIBLE.GOOGLE_APPLICATION_CREDENTIALS, VARIBLE.API_FIRESTORE_CODER_WRITER);
Environment.SetEnvironmentVariable(VARIBLE.DATE_TIME_FORMAT, "dd/MM/yyyy - HH:mm:ss");


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddResponseCompression();
var app = builder.Build();

// Configure the HTTP request pipeline.
METHOD.ENVIROMENT_WEB(app);

app.UseHttpsRedirection();
app.UseRouting();
app.MapRazorPages();
app.UseEndpoints(endpoints => METHOD.ENDPOINT(endpoints));
app.Run();



