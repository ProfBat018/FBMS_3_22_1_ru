# �������� � MinimalAPI 

`MinimalAPI` - ��� ����� ������ � �������� ���-���������� � ASP.NET Core 6.0. 
�� ��������� ��������� ���-���������� � ����������� ����������� ���� � ��������.


## ������ �������� �������

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
```

```csharp
var builder = WebApplication.CreateBuilder(args);
```

� ���� ������ ��������� ��������� `WebApplicationBuilder`, ������� ������������ ��� ��������� ����������.
�������� ������� `builder`, ������� ��������� ��������� ������� ������� � ���������� �������� ��������. 

```csharp

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
```

��� �� ��������� ������� � Middleware � ��� `IOC` ���������.  

```csharp
var app = builder.Build();
```

������� ��������� ����������. 

```csharp
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
```

���� ��������� ��������� � ������ ����������, �� ��������� Swagger UI � Swagger � Middleware. 
��������� ����� ����� � `Properties/launchSettings.json` � ���� `ASPNETCORE_ENVIRONMENT`.

```csharp

app.UseHttpsRedirection();

app.Run();
```

��������� Middleware ��� ��������������� � `http` �� `https` � ��������� ����������.




