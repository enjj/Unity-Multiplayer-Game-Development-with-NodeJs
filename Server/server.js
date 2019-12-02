const io = require('socket.io')(process.env.PORT || 3000);
var shortid = require('shortid');

console.log('server started');

var players = [];

io.on('connection', (socket) => {
  console.log('client Connected');

  var thisPlayerId = shortid.generate();
  
  var player = {
    id: thisPlayerId,
    x:0, 
    y:0
  }

  players[thisPlayerId] = player;

  console.log('client conencted id:', thisPlayerId);
  socket.emit('register', { id: thisPlayerId});
  socket.broadcast.emit('spawn', {id: thisPlayerId});
  socket.broadcast.emit('requestPosition' ); 

  //players who connected lately can see other players who connected before 

  for(var playerId in players){
    if (playerId == thisPlayerId)
      continue;

    socket.emit('spawn', players[playerId]);

    console.log('sending spawn to new player for id :', playerId);
  }
 

  socket.on('move', (data) => {
    data.id = thisPlayerId;
    console.log('client moved', JSON.stringify(data));

    player.x = data.x;
    player.y = data.y;
    
    socket.broadcast.emit('move', data);

  });

  socket.on('updatePosition', (data) => {
    console.log("update position: ", data);

    data.id = thisPlayerId;

    socket.broadcast.emit('updatePosition' , data);
  });

 

  socket.on('follow', (data) => {
    console.log("follow request: ", data);

    data.id = thisPlayerId;

    socket.broadcast.emit('follow' , data);
  });

  socket.on('attack', (data) => {
    console.log("attack request: ", data);

    data.id = thisPlayerId;

    io.emit('attack' , data);
  });

  socket.on('disconnect', () => {
    console.log('client Disconnected');

    delete players[thisPlayerId];

    socket.broadcast.emit('disconnected',{id: thisPlayerId});
  })

})
