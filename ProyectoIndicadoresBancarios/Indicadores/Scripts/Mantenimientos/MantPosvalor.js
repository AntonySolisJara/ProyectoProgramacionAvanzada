function GetProducts(index) {

    AjaxDemo.GetProductsByCategoryID(index, GetProducts_CallBack);
}

function GetProducts_CallBack(response) {
    var ds = response.value;
    var html = new Array();

    if (ds != null && typeof (ds) == "object" && ds.Tables != null) {
        for (var i = 0; i < ds.Tables[0].Rows.length; i++) {

            html[html.length] = "<option value=" + ds.Tables[0].Rows[i].ProductID + ">" + ds.Tables[0].Rows[i].ProductName + "</option>";

        }

        document.getElementById("Display1").innerHTML = "<select id=\"sel1\">" + html.join("") + "</select>";
    }
}
