# Тема урока: Express.js

`Express.js` - это минималистичный и гибкий веб-фреймворк для приложений Node.js, предоставляющий обширный набор функций для мобильных и веб-приложений. С помощью `Express.js` можно создавать веб-сервера и веб-приложения, а также API.

Самое главное что вам нужно для работы с `express.js` и в целом с любым `js` фреймворком или библиотекой - это то, что вам по-любому придется скачивать сторониие библиотеки для работы.

В моем примере, я написал `authApp`, который регистрирует и авторизует пользователей. Для этого я использовал библиотеку `bcryptjs`, которая шифрует пароли пользователей, а также `jsonwebtoken`, который создает токены для авторизации пользователей.

Структура проекта такая: 

.
├── config
│   ├── dbContext.js
│   └── jwt.js
├── controllers
│   └── authController.js
├── dtos
│   └── userDTO.js
├── index.js
├── middleware
│   └── authMiddleware.js
├── models
│   └── userModel.js
├── package-lock.json
├── package.json
├── readme.md
├── routes
│   ├── accountRoutes.js
│   └── authRoutes.js
├── services
│   ├── accountService.js
│   └── userService.js
└── validators
    └── authValidator.js

В папке `config` находятся файлы конфигурации для подключения к базе данных и для создания токенов. Для работы с базой данных мы используем `MongoDb`. Чтобы подключиться к базе данных, мы скачиваем пакет `mongoose`. Все конфигурации должны находиться в `.env` файле. Для того чтобы достать оттуда данные мы используем пакет `dotenv`. 

Вот пример файла `dbContext.js`:

```javascript

const mongoose = require("mongoose");

const connectDB = async () => {
  try {
    await mongoose.connect(process.env.MONGO_URI);
    console.log("✅ MongoDB подключена");
  } catch (err) {
    console.error("❌ Ошибка подключения к MongoDB:", err.message);
    process.exit(1);
  }
};

module.exports = connectDB;

```

В папке `controllers` находятся контроллеры, которые обрабатывают запросы от клиента. В контроллерах мы используем сервисы, которые обрабатывают логику приложения.

```javascript

const { validationResult } = require("express-validator");
const userService = require("../services/userService");
const { UserDTO } = require("../dtos/userDTO");
const { plainToInstance } = require("class-transformer");

const registerUser = async (req, res) => {
  const errors = validationResult(req);
  if (!errors.isEmpty()) {
    return res.status(400).json({ errors: errors.array() });
  }

  try {
    const { user } = await userService.createUser(
      req.body
    );

    const userObject = user.toObject ? user.toObject() : user;

    const userDto = plainToInstance(UserDTO, userObject, {
      excludeExtraneousValues: true,
    });

    res.status(201).json({ user: userDto, refreshToken });
  } catch (err) {
    res.status(400).json({ message: err.message });
  }
};

module.exports = {
  registerUser,
};

```

Для валидации данных, который пользователь ввел, я использую пакет `express-validator`. В папке `validators` находятся валидаторы, которые проверяют данные, которые пришли от клиента.

```javascript
// Валидация регистрации
const registerValidation = (req, res, next) => {
  const { username, email, password, confirmPassword } = req.body;

  if (!username || !email || !password || !confirmPassword) {
    return res.status(400).json({ message: "Все поля обязательны" });
  }

  const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
  if (!emailRegex.test(email)) {
    return res.status(400).json({ message: "Некорректный email" });
  }

  if (password.length < 6) {
    return res
      .status(400)
      .json({ message: "Пароль должен содержать хотя бы 6 символов" });
  }

  if (password !== confirmPassword) {
    return res.status(400).json({ message: "Пароли не совпадают" });
  }

  next();
};
```

в userService мы обрабатываем логику приложения. В данном случае, мы создаем пользователя и сохраняем его в базу данных.

```javascript
const bcrypt = require("bcryptjs");
const User = require("../models/userModel");
const { UserDTO } = require("../dtos/userDTO");
const { generateAccessToken, generateRefreshToken } = require("../config/jwt");
const { plainToInstance } = require("class-transformer");

const createUser = async (userData) => {
  const { username, email, password } = userData;

  const existingUser = await User.findOne({ email });
  if (existingUser) throw new Error("Пользователь уже зарегистрирован");

  const salt = await bcrypt.genSalt(10);
  const hashedPassword = await bcrypt.hash(password, salt);

  const newUser = new User({ username, email, password: hashedPassword });
  await newUser.save(); // Обязательно сохраняем пользователя в БД

  console.log(newUser.toObject());

  return {
    user: plainToInstance(UserDTO, newUser.toObject()),
  };
};

```

`Bcryptjs` - это библиотека, которая шифрует пароли пользователей. В данном случае, мы шифруем пароль пользователя и сохраняем его в базу данных.
`User` - это `mongoose` схема или же моя модель пользователя. В данном случае, мы создаем нового пользователя и сохраняем его в базу данных.

`UserDTO` - это класс, в котором я использую декораторы `@Expose` с помощью `Babel`. Эти декораторы делают из моих полей свойства для сериализации и десериализации.

```javascript
const { Expose } = require("class-transformer");

// @Expose - это декоратор, который позволяет указать,
// что свойство должно быть включено в процесс сериализации/десериализации.

class UserDTO {
  @Expose()
  username;

  @Expose()
  email;
}

module.exports = { UserDTO };
```

Дело в том что, js не поддерживает декораторы, поэтому я использую `Babel`, чтобы транспилировать мой код. Но для того чтобы использовать `Babel` нужно его скачать, написать ему конфигурацию и запускать программу слеюущим образом:

```json

"scripts": {
    "start": "nodemon --exec babel-node index.js",
    "build": "babel src --out-dir dist"
  },

```

или же 

```json
node -r @babel/register index.js   
```

Все это нужно для `plainToInstance` метода, который преобразует объект в экземпляр класса.
Получается что он берет ключи из `JS` объекта и вставляет в тот самый `UserDTO` класс.

Пути для своих контроллеров я прописываю в папке `routes`. В данном случае, я создал два роута: `authRoutes` и `accountRoutes`. В `authRoutes` я обрабатываю запросы для регистрации и авторизации пользователей, а в `accountRoutes` я обрабатываю запросы для работы с аккаунтом пользователя.

```javascript
const express = require("express");
const {
  registerUser,
  loginUser,
  getUsers,
  refreshToken,
} = require("../controllers/authController");

const {
  registerValidation,
  loginValidation,
} = require("../validators/authValidator");

const { protect } = require("../middleware/authMiddleware");

const router = express.Router();

router.post("/register", registerValidation, registerUser);
router.post("/login", loginValidation, loginUser);
router.post("/refresh", refreshToken);
router.get("/", protect, getUsers); // Только для авторизованных

module.exports = router;

```

В свои `get` и `post` методы я передаю сразу валидацию(как middleware), чтобы проверить данные, которые пришли от клиента. Если данные не прошли валидацию, то я возвращаю ошибку.

Ключевое слово `protect` - это middleware, который проверяет токен пользователя. Если токен не прошел проверку, то я возвращаю ошибку.






