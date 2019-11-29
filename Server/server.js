const io = require('socket.io')(process.env.PORT || 3000);
var shortid = require('shortid');

console.log('server started');

var players = [];

io.on('connection', (socket) => {
  console.log('client Connected');

  var thisClientId = shortid.generate();
  
  players.push(thisClientId);

  console.log('client conencted id:', thisClientId);

  socket.broadcast.emit('spawn');

  //players who connected lately can see other players who connected before 
  players.forEach((playerId) => {
    if (playerId == thisClientId)
      return;

    socket.emit('spawn', {
      id: playerId
    });

    console.log('sending spawn to new player for id :', playerId);

  })

  socket.on('move', (data) => {
    data.id = thisClientId;
    console.log('client moved', JSON.stringify(data));
    socket.broadcast.emit('move', data);

  });

  socket.on('disconnect', () => {
    console.log('client Disconnected');


  })

})
