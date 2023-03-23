var hub = new signalR.HubConnectionBuilder().withUrl("/lessonComment").build();

hub.start().then(function () {
    console.log("Connect Success");
});

hub.on("CommentAdded", function (username, comment, date) {

    $("#comments-Section").append(
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

    hub.invoke("AddComment", lessonID, studentID, comment, new Date());
}
