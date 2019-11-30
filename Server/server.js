const io = require('socket.io')(process.env.PORT || 3000);
var shortid = require('shortid');

console.log('server started');

var players = [];

io.on('connection', (socket) => {
  console.log('client Connected');

  var thisPlayerId = shortid.generate();
  
  players.push(thisPlayerId);

  console.log('client conencted id:', thisPlayerId);

  socket.broadcast.emit('spawn', {id: thisPlayerId});

  //players who connected lately can see other players who connected before 
  players.forEach((playerId) => {
    if (playerId == thisPlayerId)
      return;

    socket.emit('spawn', {id: playerId});

    console.log('sending spawn to new player for id :', playerId);

  })

  socket.on('move', (data) => {
    data.id = thisPlayerId;
    console.log('client moved', JSON.stringify(data));
    socket.broadcast.emit('move', data);

  });

  socket.on('disconnect', () => {
    console.log('client Disconnected');
    players.splice(players.indexOf(thisPlayerId),1);
    socket.broadcast.emit('disconnected',{id: thisPlayerId});


  })

})
