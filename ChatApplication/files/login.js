$(document).ready(function () {
    if (localStorage.getItem("UID")) {
        window.location.href = "Default.aspx"
    }
    //Login Functions 
    var socketServer = new WebSocket("ws://10.10.10.140:8181");

    $(".loginBtn").click(function () {

        var username = $(".username").val().trim();
        var password = $(".password").val();

        var json = '{"function":"login", "username":"' + username + '", "password":"' + password + '"}';

        socketServer.send(json);

        
        

    });
    console.log(socketServer);
    socketServer.onmessage = function (e) {

            
        var info = JSON.parse(e.data.toString().replace(/\n|\r/g, "").trim());
        if (!info.function) {
            console.log(info);
            if (info.returned && info.returned == "true") {
                alert("logged in");
                localStorage.setItem('UID', info.userID)
                localStorage.setItem('username', info.username)

                window.location.href = "Default2.aspx"
            } else {
                alert("there is an error");
            }
        }
        
        
    };

})