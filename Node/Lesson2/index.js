// Объявляем функцию `baz`, которая выводит в консоль "baz"
const baz = () => console.log("baz");

// Объявляем функцию `foo`, которая выводит в консоль "foo"
const foo = () => console.log("foo");

// Объявляем функцию `zoo`, которая выводит в консоль "zoo"
const zoo = () => console.log("zoo");

// Основная функция `start`
const start = () => {
  console.log("start"); // Сразу выводим "start" в консоль

  // Добавляем функцию `baz` в очередь выполнения setImmediate
  setImmediate(baz);

  // Создаем новый Promise, который сразу разрешается значением "bar"
  new Promise((resolve, reject) => {
    resolve("bar");
  }).then((resolve) => {
    console.log(resolve); // Выводим значение "bar", когда Promise выполнится

    // Добавляем функцию `zoo` в очередь выполнения process.nextTick
    process.nextTick(zoo);
  });

  // Добавляем функцию `foo` в очередь выполнения process.nextTick
  process.nextTick(foo);
};

// Вызываем функцию `start`
start();