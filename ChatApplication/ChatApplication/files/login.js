$(document).ready(function () {
    if (localStorage.getItem("UID")) {
        window.location.href = "Default.aspx"
    }
    //Login Functions 
    var socketServer = new WebSocket("ws://10.10.10.57:8181");

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
                localStorage.setItem('UID', info.userID)
                localStorage.setItem('username', info.username)
                window.location.href = "Default.aspx"
            } else {
                alert("The username or password is incorrect”");
            }
        } else {
            if (info.function == "signupReturn") {
                if (info.code == "success") {
                    alert("Registration is successfull.");
                    window.location.href = "./Login.aspx"
                } else {
                    alert(code)
                }
            }
        }
        
        
    };

    $(".signupBtn").click(function () {
        var email = $(".emailreg").val();
        var username = $(".usernamereg").val();
        var password = $(".passwordreg").val();

        var jsonS = '{"function":"signup", "email":"' + email + '","username":"' + username + '","password":"' + password + '"}';

        socketServer.send(jsonS);

    });

})