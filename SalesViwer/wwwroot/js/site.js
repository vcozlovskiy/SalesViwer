function getInfo(id) {

    $.ajax("/Tables/GetInfo")
        .done(function (data) {

            var count = data.length;
            console.log(count);

            for (var i = 0; i < count; i++) {
                var element = $("<tr><td>" + data[i]['id'] + "</td><td>" + data[i]['fullName'] + "</td><td>" + data[i]['orders'] + "</td></tr>");
                $("#tab").append(element);
            }
        });
}
