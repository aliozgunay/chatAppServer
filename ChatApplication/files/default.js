var sound = new Howl({
    src: ['notifysound.mp3']
});
$(document).ready(function () {
    if (!localStorage.getItem("UID")) {
        window.location.href = "Login.aspx"
    }
    //Login Functions 
    var socketServer = new WebSocket("ws://10.10.10.140:8181");
    var json = '{"function":"syncID", "id":"' + localStorage.getItem("UID") + '"}';
    var onlineUserJson = '{"function":"getOnlineUsers", "username":"' + localStorage.getItem("username") + '", "uid":"' + localStorage.getItem("UID") + '"}';
    var getMyGroupsJson = '{"function":"getGroups", "uid":"' + localStorage.getItem("UID") + '"}';


    socketServer.onopen = function () {
        socketServer.send(json);

    }
    $(".chatArea").html("");

    setInterval(function () {
        socketServer.send(onlineUserJson)
    }, 1000)

    setInterval(function () {
        socketServer.send(getMyGroupsJson)
    }, 1000)

    $('.inputMessage').keydown(function (e) {
        if (e.keyCode == 13) {
            var client = location.hash.substr(1);
            if (isNaN(client)) {
                // Group 
                var message = $(this).val();
                var sendData = '{"function":"sendMessageGroup", "myUid":"' + localStorage.getItem("UID") + '", "groupName":"' + client + '","message":"' + message + '","username":"' + localStorage.getItem("username") + '"}';
                socketServer.send(sendData);
                $(".chatArea").append('<div class="m-widget3"> <div class="m-widget3__item"> <div class="m-widget3__header"> <div class="m-widget3__user-img">  </div> <div class="m-widget3__info"> <span class="m-widget3__username"> @' + localStorage.getItem("username") + ' </span><br> <span class="m-widget3__time"> a few seconds ago </span> </div> <span class="m-widget3__status m--font-info"> Delivered </span> </div> <div class="m-widget3__body"> <p class="m-widget3__text"> ' + message + ' </p> </div> </div> </div><hr />');
                var messageBody = document.querySelector('.chatArea');
                messageBody.scrollTop = messageBody.scrollHeight - messageBody.clientHeight;
                e.preventDefault()
                $(this).val('')
                var json = '{"function":"readMessagesGroup", "uid":"' + localStorage.getItem("UID") + '", "client":"' + client + '"}';
                socketServer.send(json);
            } else {
                // Client
                var message = $(this).val();
                var sendData = '{"function":"sendMessage", "myUid":"' + localStorage.getItem("UID") + '", "client":"' + client + '","message":"' + message + '","username":"' + localStorage.getItem("username") + '"}';
                socketServer.send(sendData);
                $(".chatArea").append('<div class="m-widget3"> <div class="m-widget3__item"> <div class="m-widget3__header"> <div class="m-widget3__user-img">  </div> <div class="m-widget3__info"> <span class="m-widget3__username"> @' + localStorage.getItem("username") + ' </span><br> <span class="m-widget3__time"> a few seconds ago </span> </div> <span class="m-widget3__status m--font-info"> Delivered </span> </div> <div class="m-widget3__body"> <p class="m-widget3__text"> ' + message + ' </p> </div> </div> </div><hr />');
                var messageBody = document.querySelector('.chatArea');
                messageBody.scrollTop = messageBody.scrollHeight - messageBody.clientHeight;
                e.preventDefault()
                $(this).val('')
                var json = '{"function":"readMessages", "uid":"' + localStorage.getItem("UID") + '", "client":"' + client + '"}';
                socketServer.send(json);
            }
            
        }
    })

    $("body").on("click", ".userLi", function () {
        $(".chatName").html($(this).find(".m-menu__link-text").text());
        var thisHref = $(this).attr("href").replace("#", "");
        var sendData = '{"function":"getHistory", "myUid":"' + localStorage.getItem("UID") + '", "client":"' + thisHref + '"}';
        socketServer.send(sendData);

    });

    $("body").on("click", ".groupLi", function () {
        $(".chatName").html($(this).find(".m-menu__link-text").text());
        var thisHref = $(this).attr("href").substr(1);
        var sendData = '{"function":"getHistoryGroup", "myUid":"' + localStorage.getItem("UID") + '", "groupName":"' + thisHref + '"}';
        socketServer.send(sendData);

    });

    $(".inputMessage").click(function () {

        var thisHref = location.hash.substr(1);
        //var json = '{"function":"readMessages", "uid":"' + localStorage.getItem("UID") + '", "client":"' + thisHref + '"}';
        socketServer.send(json);

    })

    socketServer.onmessage = function (e) {


        var info = JSON.parse(e.data.toString().replace(/\n|\r/g, "").trim());
        if (info.function) {
            console.log(info);
            if (info.function == "loadHistory") {
                $(".chatArea").animate({ scrollTop: 0 }, 100);
                $(".chatArea").html("");
                $(".chatArea").focus();

                var messages = info.messages;
                for (var i = 0; i < messages.length; i++) {
                    $(".chatArea").prepend('<div class="m-widget3"> <div class="m-widget3__item"> <div class="m-widget3__header"> <div class="m-widget3__user-img">  </div> <div class="m-widget3__info"> <span class="m-widget3__username"> @' + messages[i][1] + ' </span><br> <span class="m-widget3__time"> a few seconds ago </span> </div> <span class="m-widget3__status m--font-info"> Delivered </span> </div> <div class="m-widget3__body"> <p class="m-widget3__text"> ' + messages[i][0] + ' </p> </div> </div> </div><hr />');


                    $(".chatArea").animate({ scrollTop: $(".chatArea").height() }, 100);

                }

                var thisHref = location.hash.substr(1);
                var json = '{"function":"readMessages", "uid":"' + localStorage.getItem("UID") + '", "client":"' + thisHref + '"}';
                socketServer.send(json);

            }

            if (info.function == "loadHistoryGroup") {
                $(".chatArea").animate({ scrollTop: 0 }, 100);
                $(".chatArea").html("");
                $(".chatArea").focus();

                var messages = info.messages;
                for (var i = 0; i < messages.length; i++) {
                    $(".chatArea").prepend('<div class="m-widget3"> <div class="m-widget3__item"> <div class="m-widget3__header"> <div class="m-widget3__user-img">  </div> <div class="m-widget3__info"> <span class="m-widget3__username"> @' + messages[i][1] + ' </span><br> <span class="m-widget3__time"> a few seconds ago </span> </div> <span class="m-widget3__status m--font-info"> Delivered </span> </div> <div class="m-widget3__body"> <p class="m-widget3__text"> ' + messages[i][0] + ' </p> </div> </div> </div><hr />');


                    $(".chatArea").animate({ scrollTop: $(".chatArea").height() }, 100);

                }

                var thisHref = location.hash.substr(1);
                var json = '{"function":"readMessagesGroup", "uid":"' + localStorage.getItem("UID") + '", "groupName":"' + thisHref + '"}';
                socketServer.send(json);

            }

            if (info.function == "getOnlineUsers") {
                $(".onlineUserListDiv").html("");
                var count = info.listUser.length;
                for (var i = 0; i < count; i++) {

                    var badgeUsername = "<span class='badge badge-danger'>" + info.convClient[i] + "</span>"

                    $(".onlineUserListDiv").append('<li class="m-menu__item m-menu__item--active" aria-haspopup="true"><a href="#' + info.listClient[i] + '" class="m-menu__link userLi"><i class="m-menu__link-icon flaticon-user"></i><span class="m-menu__link-title"> <span class="m-menu__link-wrap"> <span class="m-menu__link-text">  @' + info.listUser[i] + '</span> ' + (info.convClient[i] > 0 ? badgeUsername : "") + ' </span></span></a></li>');


                }

            }

            if (info.function == "getMyGroups") {
                $(".groupListDiv").html("");
                var count = info.listGroup.length;

                var htmlInner = "";
                for (var i = 0; i < count; i++) {

                    htmlInner += '<li class="m-menu__item m-menu__item--active" aria-haspopup="true"><a href="#' + info.listGroup[i] + '" class="m-menu__link groupLi"><i class="m-menu__link-icon flaticon-users"></i><span class="m-menu__link-title"> <span class="m-menu__link-wrap"> <span class="m-menu__link-text">  ' + info.listGroup[i] + '</span> </span></span></a></li>';


                }
                $(".groupListDiv").html(htmlInner);

            }

            if (info.function == "receiveMessage") {

                var client = location.hash.substr(1);
                if (info.senderID == client) {


                    $(".chatArea").append('<div class="m-widget3" data-uniq="'+info.uniq+'"> <div class="m-widget3__item"> <div class="m-widget3__header"> <div class="m-widget3__user-img">  </div> <div class="m-widget3__info"> <span class="m-widget3__username"> @' + info.fromUname + ' </span><br> <span class="m-widget3__time"> a few seconds ago </span> </div> <span class="m-widget3__status m--font-info"> Delivered </span> </div> <div class="m-widget3__body"> <p class="m-widget3__text"> ' + info.message + ' </p> </div> </div> </div><hr />');

                    var x = "jello"
                    $('[data-uniq="' + info.uniq +'"]').addClass(x + ' animated').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {

                    });

                    var messageBody = document.querySelector('.chatArea');
                    messageBody.scrollTop = messageBody.scrollHeight - messageBody.clientHeight;


                } else {
                    sound.play();
                }

                var x = "rubberBand"
                $('[href="#' + info.senderID + '"]').addClass(x + ' animated').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {

                });
            }

            if (info.function == "receiveMessageGroup") {

                var client = location.hash.substr(1);
                if (info.groupName == client) {


                    $(".chatArea").append('<div class="m-widget3" data-uniq="' + info.uniq +'"> <div class="m-widget3__item"> <div class="m-widget3__header"> <div class="m-widget3__user-img">  </div> <div class="m-widget3__info"> <span class="m-widget3__username"> @' + info.fromUname + ' </span><br> <span class="m-widget3__time"> a few seconds ago </span> </div> <span class="m-widget3__status m--font-info"> Delivered </span> </div> <div class="m-widget3__body"> <p class="m-widget3__text"> ' + info.message + ' </p> </div> </div> </div><hr />');

                    var x = "jello"
                    $('[data-uniq="' + info.uniq + '"]').addClass(x + ' animated').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {

                    });

                    var messageBody = document.querySelector('.chatArea');
                    messageBody.scrollTop = messageBody.scrollHeight - messageBody.clientHeight;


                } else {
                    sound.play();
                }

                var x = "rubberBand"
                $('[href="#' + info.groupName + '"]').addClass(x + ' animated').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                   
                });
            }


        }


    };


});