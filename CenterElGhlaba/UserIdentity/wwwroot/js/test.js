//Window.onload = function () {
    console.log("hhhhhhhhh");
    if (Folow(@user.Id.ToString(), @Model.AppUserID.ToString())) {
        alert("Following");
        document.getElementById("data").innerText = "hello"
    }
    else {
        alert("Not Following");
        document.getElementById("data").innerHTML = "hello2"

    }
//}



