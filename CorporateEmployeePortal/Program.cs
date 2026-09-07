using System.IO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

//Custom branding headers
app.Use(async(context, next) => {
  context.Response.Headers["X-Company"] = "TechCorp Solutions";
  context.Response.Headers["X-Portal-Version"] =  "1.0";
  context.Response.Headers["X-Environment"] = "Corporate Portal";
  await next();
});
// Access logging
app.Use(async(context, next) => {
  var currentTimeStamp = DateTime.Now.ToString();
  var method = context.Request.Method;
  var path = context.Request.Path;
  var userAgent = context.Request.Headers["User-Agent"].FirstOrDefault() ?? "unknown";
  File.AppendAllText("log.txt", $"log message with [{currentTimeStamp}] {method} {path} - UserAgent :{userAgent}\n");
  await next();
  File.AppendAllText("log.txt", $"log message with [{currentTimeStamp}] {method} {path} - Status: {context.Response.StatusCode}\n");
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

//Terminal middleware for emergency alert
app.Run(async context =>
{
    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync(@"
    <html>
    <head>
      <title>Emergency Alert - TechCorp Portal</title>
      <style>
          body { 
              font-family: Arial, sans-serif; 
              margin: 0; 
              padding: 20px; 
              background-color: #f8f9fa; 
          }
          .alert-container { 
              max-width: 800px; 
              margin: 0 auto; 
              background-color: #dc3545; 
              color: white; 
              padding: 30px; 
              border-radius: 8px; 
              text-align: center; 
              box-shadow: 0 4px 6px rgba(0,0,0,0.1);
          }
          h1 { 
              font-size: 2.5em; 
              margin-bottom: 20px; 
              font-weight: bold;
          }
          p { 
              font-size: 1.2em; 
              line-height: 1.6; 
              margin-bottom: 15px;
          }
          .company-logo {
              background-color: white;
              color: #dc3545;
              padding: 10px 20px;
              border-radius: 4px;
              display: inline-block;
              margin-top: 20px;
              font-weight: bold;
          }
      </style>
    </head>
    <body>
      <div class='alert-container'>
          <h1>CRITICAL SECURITY UPDATE</h1>
          <p><strong>ATTENTION ALL EMPLOYEES:</strong></p>
          <p>The TechCorp employee portal is currently undergoing emergency maintenance due to a critical security update.</p>
          <p>All employees must complete mandatory security training by <strong>Friday, 5:00 PM</strong>.</p>
          <p>Please contact IT support at ext. 2525 for immediate assistance.</p>
          <p><strong>Do not attempt to access sensitive company data until this alert is resolved.</strong></p>
          <div class='company-logo'>TechCorp Solutions - IT Security Team</div>
      </div>
    </body>
    </html>
      ");
});

app.Run();


