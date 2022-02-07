function getInfoAdmin() {

    $.ajax("/Tables/GetInfo")
        .done(function (data) {

            var count = data.length;
            console.log(count);
            $('#tabel').remove();

            for (var i = 0; i < count; i++) {
                var element = $("<tr id=" + data[i]['id'] +"><td>" + data[i]['id'] +
                    "</td><td>" + data[i]['fullName'] +
                    "</td><td>" + data[i]['items'] +
                    '<td><button class="btn btn-info btn-sm" onclick="Edit(' + data[i]['id'] +');">Edit</button></td>' +
                    "</td></tr>");
                $("#tab").append(element);
            }
        });
}
function getInfoUser() {
    $.ajax("/Tables/GetInfo")
        .done(function (data) {
            $('#tabel').remove();
            var count = data.length;

            for (var i = 0; i < count; i++) {
                var element = $("<tr id=" + data[i]['id'] +"><td>" + data[i]['id'] +
                    "</td><td>" + data[i]['fullName'] +
                    "</td><td>" + data[i]['items'] +
                    "</td></tr>");
                $("#tab").append(element);
            }
        });
}

function Edit(id) {
    var url = "/Tables/Edit/" + id;
    $(location).attr('href', url);
}

function Create() {
    var url = "/Tables/Create";
    $(location).attr('href', url);
}

function ShowOrderTable(id)
{
    $.post("/Tables/GetOrderInfo", { id: id}).done(
        function (orders) {
            var count = orders.length;

            if (count != 0) {
                for (var i = 0; i < count; i++) {
                    var element = $(
                        "<tr><td>" + orders[i]['id'] +
                        "</td><td>" + orders[i]['item']['name'] +
                        "</td><td>" + orders[i]['price'] +
                        "</td ><td>" + orders[i]['dateTimeOrder'] +
                        "</td ><td>" + '<button class="btn btn-info btn-sm" onclick="EditOrder(' + orders[i]['id'] + ');">Edit</button>' +
                        "</td></tr>");
                    $("#tab").append(element);
                }
            }
            else
            {
                $("#empty").append("No orders")
            }
        }
    )
}

function AddOrder(id) {
    var url = "/Order/Add/" + id;
    $(location).attr('href', url);
}

function SaveOrder() {
    var url = "/Order/Save/";
    orderId = undefined;
    $(location).attr('href', url);
}
