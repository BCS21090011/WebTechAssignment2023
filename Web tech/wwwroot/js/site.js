var mysql = require('mysql');

var con = mysql.createConnection({
    host: "localhost",
    user: "root",
    password: "741852song"
});

con.connect(function (err) {
    if (err) throw err;
    console.log("Connected!");
});