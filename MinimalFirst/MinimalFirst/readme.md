# Введение в MinimalAPI 

`MinimalAPI` - это новый подход к созданию веб-приложений в ASP.NET Core 6.0. 
Он позволяет создавать веб-приложения с минимальным количеством кода и настроек.


## Разбор базового шаблона

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

В этой строке создается экземпляр `WebApplicationBuilder`, который используется для настройки приложения.
Вспомним паттерн `builder`, который позволяет создавать сложные объекты с множеством настроек поэтапно. 

```csharp

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
```

Тут мы добавляем сервисы и Middleware в наш `IOC` контейнер.  

```csharp
var app = builder.Build();
```

Создаем экземпляр приложения. 

```csharp
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
```

Если программа находится в режиме разработки, то добавляем Swagger UI и Swagger в Middleware. 
Проверить режим можно в `Properties/launchSettings.json` в поле `ASPNETCORE_ENVIRONMENT`.

```csharp

app.UseHttpsRedirection();

app.Run();
```

Добавляем Middleware для перенаправления с `http` на `https` и запускаем приложение.




