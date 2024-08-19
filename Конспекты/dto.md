# Тема урока: DTO (Data Transfer Object)

Для работы с данными в приложении часто используются объекты, которые представляют собой простые структуры данных.

DTO (Data Transfer Object) - это объект, который используется для передачи данных между слоями приложения. DTO содержит только данные и методы доступа к ним, но не содержит бизнес-логики.

Предположим у меня есть модель пользователя в C#, которая работает с EF Core: 

```csharp

public class User 
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsEmailConfirmed { get; set; } = false;
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}
```

DTO для этой модели может выглядеть так:

```csharp

public class UserDto
{
    public string Username { get; set; }
    public string Email { get; set; }
}
```

DTO позволяет скрыть детали реализации модели и передавать только необходимые данные. Это упрощает взаимодействие между слоями приложения и уменьшает связанность между ними.

Такой тип мы можем использовать для передачи данных между контроллерами и сервисами, между сервисами и репозиториями и т.д.

Например для входа пользователя в систему, мы можем использовать DTO для передачи данных между контроллером и сервисом:

```csharp

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}
```

Или же для обновления токена пользователя:

```csharp

public class RefreshTokenDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}
```