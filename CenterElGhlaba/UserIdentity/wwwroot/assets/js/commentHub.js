var commentHub = new signalR.HubConnectionBuilder().withUrl("/lessonComment").build();

commentHub.start().then(function () {
    console.log("Comment Connect Success");
});

commentHub.on("CommentAdded", function (username, comment, date) {


    $("#comments-Section").prepend(
        `
            <div class="vid">
                <div>
                    <span class="gray">${username}: </span>
                    <span class="gray">${date}</span>
                    <p>${comment}</p >
                </div>
            </div>
        `
    )
});

function AddComment(lessonID, studentID) {
   
    let comment = $("#comment").val();
    $("#comment").val('');
    
    commentHub.invoke("AddNewComment", lessonID, studentID, comment, new Date());
   
}
