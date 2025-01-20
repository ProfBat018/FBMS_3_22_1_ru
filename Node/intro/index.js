// Node js first lesson

// const http = require('http');

// const server = http.createServer((req, res) => {
//     res.end('Hello World');
// }
// );

// server.listen(3000, () => {
//     console.log('Server is running on port 3000');
// }
// );

// CommonJs

global.foo = function() {
    console.log('foo');
}
const { add, subtract } = require("./math");
console.log(add(1, 2));

// global.a = 1;


// console.log(a);
// foo();

// console.log(global);
