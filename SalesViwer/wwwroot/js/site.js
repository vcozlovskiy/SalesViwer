function ShowClientsTable() {

    $.post("/Client/ClientsJson")
        .done(function (data) {

            var count = data.length;
            console.log(count);
            $('table').remove();

            var table = $(
                "<table id='table'>" +
                "<colgroup>" +
                "<col span = '2' style = 'background:Khaki'>" +
                "<col style='background - color: LightCyan'>" +
                "</colgroup>" +
                "<tr>" +
                "<td> Id</td>" +
                "<td> Client name</td>" +
                "<td>Order</td>" +
                "</tr>" +
                "<table>");
            $("main").append(table);

            for (var i = 0; i < count; i++) {
                var ordesName = "";
                for (var j = 0; j < data[i]['orders'].length; j++)
                {
                    ordesName += data[i]['orders'][j]['item']['name'] + ',';
                }
                var tablePart = $("<tr id=" + data[i]['id'] + "><td>" + data[i]['id'] +
                    "</td><td>" + data[i]['fullName'] +
                    "</td><td>" + ordesName +
                    '<td><button class="btn btn-info btn-sm" onclick="Edit(' + data[i]['id'] + ');">Edit</button></td>' +
                    "</td></tr>");
                $("#table").append(tablePart);
            }

            $("main").append(sortParametrs);
        });
}

function getInfoUser() {
    $.post("/Client/ClientsJson")
        .done(function (data) {

            var count = data.length;
            console.log(count);
            $('table').remove();

            var table = $(
                "<table id='table'>" +
                "<colgroup>" +
                "<col span = '2' style = 'background:Khaki'>" +
                "<col style='background - color: LightCyan'>" +
                "</colgroup>" +
                "<tr>" +
                "<td> Id</td>" +
                "<td> Client name</td>" +
                "<td>Order</td>" +
                "</tr>" +
                "<table>");
            $("main").append(table);

            for (var i = 0; i < count; i++) {
                var ordesName = "";
                for (var j = 0; j < data[i]['orders'].length; j++) {
                    ordesName += data[i]['orders'][j]['item']['name'] + ',';
                }
                var tablePart = $("<tr id=" + data[i]['id'] + "><td>" + data[i]['id'] +
                    "</td><td>" + data[i]['fullName'] +
                    "</td><td>" + ordesName +
                    "</td></tr>");
                $("#table").append(tablePart);
            }
        });
}

function Edit(id) {
    var url = "/Tables/Edit/" + id;
    $(location).attr('href', url);
}

function CreateNewUser() {
    var url = "/Tables/Create";
    $(location).attr('href', url);
}

function ShowOrderTable(id) {
    $.post("/Tables/GetOrderInfo", { id: id }).done(
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
            else {
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

function ShowAllOrders() {
    $.post("/Order/JsonOrders").done(
        function (orders) {
            var count = orders.length;
            $('table').remove();

            var table = $(
                "<table id='table'>" +
                "<colgroup>" +
                "<col span = '2' style = 'background:Khaki'>" +
                "<col style='background - color: LightCyan'>" +
                "</colgroup>" +
                "<tr>" +
                "<td> Id</td>" +
                "<td> Item</td>" +
                "<td>Price</td>" +
                "<td>Date time</td>" +
                "</tr>" +
                "<table>");

            $("main").append(table);
            if (count != 0) {
                for (var i = 0; i < count; i++) {
                    var element = $(
                        "<tr><td>" + orders[i]['id'] +
                        "</td><td>" + orders[i]['item']['name'] +
                        "</td><td>" + orders[i]['price'] +
                        "</td ><td>" + orders[i]['dateTimeOrder'] +
                        "</td ><td>" + '<button class="btn btn-info btn-sm" onclick="EditOrder(' + orders[i]['id'] + ');">Edit</button>' +
                        "</td></tr>");
                    $("#table").append(element);
                }
            }
            else {
                $("#empty").append("No orders")
            }
        }
    )
}

function ShowManagetTable() {
    $.post("/Manager/JsonManagers").done(
        function (managerJson) {
            var count = managerJson.length;
            $('table').remove();

            var table = $(
                "<table id='table'>" +
                "<colgroup>" +
                "<col span = '2' style = 'background:Khaki'>" +
                "<col style='background - color: LightCyan'>" +
                "</colgroup>" +
                "<tr>" +
                "<td> Id</td>" +
                "<td> Full name</td>" +
                "<td> Count orders</td>" +
                "</tr>" +
                "<table>");
            $("main").append(table);

            for (var i = 0; i < count; i++) {
                var element = $(
                    "<tr><td>" + managerJson[i]['id'] +
                    "</td><td>" + managerJson[i]['fullName'] +
                    "</td><td>" + managerJson[i]['orders'].length +
                    "</td ><td>" + '<button class="btn btn-info btn-sm" onclick="EditManager(' + managerJson[i]['id'] + ');">Edit</button>' +
                    "</td></tr>");
                $("#table").append(element);
            }
        }
    )
}

var sortParametrs = $("<form action='a()'>" +
    "<label for='parametre'>Full name</label>" + 
      "<select name='parametre' id='lang'>" +
        "<option value='Ascending'>Ascending</option>" +
        "<option value='Descending'>Descending</option>" +
      "</select>" +
      "<input type='submit' value='Submit' />" + 
    "</form >")

function a(param) {
    console.log("Work" + param)
}