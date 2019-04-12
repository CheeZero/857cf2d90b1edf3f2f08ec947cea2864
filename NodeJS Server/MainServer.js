const app = require('express')(); 
const server = require('http').Server(app); 
const io = require('socket.io')(server); 

server.listen(4567); 


app.get('/', (req, res) => { 
console.log(req); 
res.sendFile(__dirname + '/index.html'); 
}); 
io.on('connection', socket => { 
console.log('hi'); 
socket.emit('boop', {boop: 123}); 


socket.on("UpdateData", data =>{
    console.log('Data has beed receieved -> ');
    console.log(data);
})


socket.on("Prize", data=> {
        console.log('Data has been received ->');
        console.log(data);
});

socket.on("CurrentInformation", data=> {
    console.log('Data has been received ->');
    console.log(data);
})

socket.emit('testData', {test: 321}); 
socket.on('close', () => { 
console.log('closed'); 
}); 
}); 

/*io.on('disconnect', socket => { 
console.log('poshel nahui'); 
socket.on('close', data => { 
console.log('clossssed'); 
}); 
});*/