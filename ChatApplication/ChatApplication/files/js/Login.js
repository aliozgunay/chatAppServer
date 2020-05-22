$(document).ready(function ())
{
    var socketServer = new WebSocket("ws://10.10.10.109:8181");
    $('signupbtn').click(function ())
    {
        var username = $('.username').val().trim();
        var password = $('.password').val();

        var json = '{"function":"login","username":"' + username + '","password":"' + password + '"}';
        socketServer.onmessage
    }

    
}