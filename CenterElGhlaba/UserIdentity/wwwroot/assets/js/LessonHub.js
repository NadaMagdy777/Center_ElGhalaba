//define Hub
var hub = new signalR.HubConnectionBuilder().withUrl("/LessonHub").build();

//start connect
hub.start().then(function () {
    console.log("Connect Success");
});

////lesten To The Server If There Is Follow Removed
hub.on("RemoveFollower", function (teacherId) {
    if (teacherId == "@Model.AppUserID.ToString()") {
        var follows = $("#Followerscount").html();
        $("#Followerscount").html((parseInt(follows) - 1));
    }
});

////lesten To The Server If There Is Follow Added
hub.on("AddFollower", function (teacherId) {
    if (teacherId == "@Model.AppUserID.ToString()") {
        let beat = new Audio('/assets/Sounds/Notify.mp3');
        beat.play();

        var follows = $("#Followerscount").html();
        $("#Followerscount").html((parseInt(follows) + 1));
    }
});

////lesten To The Server If There Is Like Removed
hub.on("RemoveLike", function (lessonId) {
    if (lessonId == "@Model.AppUserID.ToString()") {
        var follows = $("#Likescount").html();
        $("#Likescount").html((parseInt(follows) - 1));
    }
});

////lesten To The Server If There Is Like Added
hub.on("AddLike", function (teacherId) {
    if (teacherId == "@Model.AppUserID.ToString()") {
        let beat = new Audio('/assets/Sounds/Notify.mp3');
        beat.play();

        var follows = $("#Likescount").html();
        $("#Likescount").html((parseInt(follows) + 1));
    }
});

function Add(s, t) {
    $.ajax({
        url: '/Teachers/AddFollower',
        data: { studentId: s, teacherId: t },
        success: function (result) {
            $("#data1").html("<a class='btn' onclick=Remove('" + s + "','" + t + "')>UnFollow</a>")

            var follows = $("#Followerscount").html();
            $("#Followerscount").html((parseInt(follows) + 1));

        },
        error: function (xhr, status) {
            alert("Add error " + status);
        }
    });
};
function Remove(s, t) {
    $.ajax({
        url: '/Teachers/RemoveFollower',
        data: { studentId: s, teacherId: t },
        success: function (result) {
            $("#data1").html("<a class='btn' onclick=Add('" + s + "','" + t + "')>Follow</a>")

            var follows = $("#Followerscount").html();
            $("#Followerscount").html((parseInt(follows) - 1));

        },
        error: function (xhr, status) {
            alert("Remove error " + status);
        }
    });
};
function Folow(s, t) {
    $.ajax({
        url: '/Teachers/IsFolowwing',
        data: { studentId: s, teacherId: t },
        success: function (result) {
            if (result) {
                $("#data1").html("<a id='Followbtn' class='btn' onclick=Remove('" + s + "','" + t + "')>UnFollow</a>")
            }
            else {
                $("#data1").html("<a id='Followbtn' class='btn' onclick=Add('" + s + "','" + t + "')>Follow</a>")
            }
        },
        error: function (xhr, status) {
            alert("Follow error " + status);
        }
    });
};


function AddLike(s, t) {
    $.ajax({
        url: '/Teachers/AddLike',
        data: { studentId: s, teacherId: t },
        success: function (result) {
            $("#data2").html("<a class='btn' onclick=RemoveLike('" + s + "','" + t + "')>UnLike</a>")

            var follows = $("#Likescount").html();
            $("#Likescount").html((parseInt(follows) + 1));

        },
        error: function (xhr, status) {
            alert("Add error " + status);
        }
    });
};
function RemoveLike(s, t) {
    $.ajax({
        url: '/Teachers/RemoveLike',
        data: { studentId: s, teacherId: t },
        success: function (result) {
            $("#data2").html("<a class='btn' onclick=AddLike('" + s + "','" + t + "')>Like</a>")

            var follows = $("#Likescount").html();
            $("#Likescount").html((parseInt(follows) - 1));

        },
        error: function (xhr, status) {
            alert("Remove error " + status);
        }
    });
};

function Like(s, t) {
    $.ajax({
        url: '/Teachers/IsLike',
        data: { studentId: s, teacherId: t },
        success: function (result) {
            if (result) {
                $("#data2").html("<a id='Likbtn' class='btn' onclick=RemoveLike('" + s + "','" + t + "')>UnLike</a>")
            }
            else {
                $("#data2").html("<a id='Likbtn' class='btn' onclick=AddLike('" + s + "','" + t + "')>Like</a>")
            }
        },
        error: function (xhr, status) {
            alert("Like error " + status);
        }
    });
};