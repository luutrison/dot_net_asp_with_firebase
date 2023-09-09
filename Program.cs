using BAN_BANH.Method;
using BAN_BANH.Model;
using BAN_BANH.Pages.Product;
using Google.Api;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Caching.Memory;

Environment.SetEnvironmentVariable(VARIBLE.GOOGLE_APPLICATION_CREDENTIALS, VARIBLE.API_FIRESTORE_CODER_WRITER);
Environment.SetEnvironmentVariable(VARIBLE.DATE_TIME_FORMAT, "dd/MM/yyyy - HH:mm:ss");

var Builder = WebApplication.CreateBuilder(args);


// Add services to the container.
Builder.Services.AddRazorPages();
Builder.Services.AddResponseCompression();

var Api = Builder.Build();

// Configure the HTTP request pipeline.
METHOD.ENVIROMENT_WEB(Api);

Api.UseHttpsRedirection();
Api.UseRouting();
Api.MapRazorPages();
Api.UseEndpoints(endpoints => METHOD.ENDPOINT(endpoints));




Api.Run();



