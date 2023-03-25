//define Hub
var hub = new signalR.HubConnectionBuilder().withUrl("/LessonLikesHub").build();

//start connect
hub.start().then(function () {
    console.log("Connect Success Likes Hub");
});

