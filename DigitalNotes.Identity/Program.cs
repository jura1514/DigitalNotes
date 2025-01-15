using DigitalNotes.ServiceDefaults;
using DigitalNotes.Identity.Data;
using DigitalNotes.Identity.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using QRCoder;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<DigitalNotesIdentityDbContext>("identityDb");

builder.Services.AddDefaultIdentity<DigitalNotesUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DigitalNotesIdentityDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddSingleton(new QrCodeService(new QRCodeGenerator()));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();