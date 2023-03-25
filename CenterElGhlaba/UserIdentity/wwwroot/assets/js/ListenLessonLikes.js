////lesten To The Server If There Is Follow Added
hub.on("AddLessonView", function (lessonId) {
    console.log("start AddLessonView")
    var views = $(`#V-${lessonId}`).html();
    console.log(views)
    $(`#V-${lessonId}`).html((parseInt(views) + 1));   
    console.log("End AddLessonView")
});

////lesten To The Server If There Is Like Removed
hub.on("RemoveLessonLike", function (lessonId) {
    
    var likes = $(`#L-${lessonId}`).html();
    $(`#L-${lessonId}`).html((parseInt(likes) - 1));
   
});

////lesten To The Server If There Is Like Added
hub.on("AddLessonLike", function (lessonId) {
   
    let beat = new Audio('/assets/Sounds/Notify.mp3');
    beat.play();

    var likes = $(`#L-${lessonId}`).html();
   
    $(`#L-${lessonId}`).html((parseInt(likes) + 1));
});

function Like(lessonId, studentId) {
    $.ajax({
        url: '/Lesson/IsLike',
        data: { lessonId: lessonId, studentId: studentId },
        success: function (result) {
            if (result) {
                $(`#data-${lessonId}`).html("<a id='Likbtn' class='btn' onclick=Remove('" + lessonId + "','" + studentId + "')>UnLike</a>")
            }
            else {
                $(`#data-${lessonId}`).html("<a id='Likbtn' class='btn' onclick=Add('" + lessonId + "','" + studentId + "')>Like</a>")
            }
        },
        error: function (xhr, status) {
            alert("Lesson Likes Hub error " + status);
        }
    });
};

function Add(lessonId, studentId) {
    $.ajax({
        url: '/Lesson/AddLike',
        data: { lessonId: lessonId, studentId: studentId },
        success: function () {
            $(`#data-${lessonId}`).html("<a id='Likbtn' class='btn' onclick=Remove('" + lessonId + "','" + studentId + "')>UnLike</a>")
        },
        error: function (xhr, status) {
            alert("Add Lesson Likes error " + status);
        }
    });
};
function Remove(lessonId, studentId) {
    $.ajax({
        url: '/Lesson/RemoveLike',
        data: { lessonId: lessonId, studentId: studentId },
        success: function (result) {
            $(`#data-${lessonId}`).html("<a id='Likbtn' class='btn' onclick=Add('" + lessonId + "','" + studentId + "')>Like</a>")

        },
        error: function (xhr, status) {
            alert("Remove Lesson Likes  error " + status);
        }
    });
};
function View(lessonId, studentId) {
    console.log("start View")
    $.ajax({
        url: '/Lesson/IsViewed',
        data: {lessonId: lessonId, studentId: studentId },
        success: function (result) {
            if (!result) {
                console.log("Adding LessonView")
                AddView(lessonId, studentId);
            }
        },
        error: function (xhr, status) {
            alert("Follow error " + status);
        }
    });
    console.log(" end View")
};


function AddView(lessonId, studentId) {
    console.log("start AddView")

    $.ajax({
        url: '/Lesson/AddView',
        data: { lessonId: lessonId, studentId: studentId },
        success: function () {
            console.log("success")
        },
        error: function (xhr, status) {
            alert("Add error " + status);
        }
    });
    console.log("end LessonView")

};

