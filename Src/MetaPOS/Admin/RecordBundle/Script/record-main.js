
$(function() {
    checkSubBranhOperation();
});



// Generate random id
function Generator() { };
Generator.prototype.rand = Math.floor(Math.random() * 26) + Date.now();
Generator.prototype.getId = function () {
    return this.rand++;
};

var GenRandom = {

    Stored: [],

    Job: function () {
        var newId = Date.now().toString().substr(6); // or use any method that you want to achieve this string

        if (this.Check(newId)) {
            this.Job();
        }

        this.Stored.push(newId);
        return newId; // or store it in sql database or whatever you want

    },

    Check: function (id) {
        for (var i = 0; i < this.Stored.length; i++) {
            if (this.Stored[i] == id) return true;
        }
        return false;
    }

};

$('.btnAddRecord').click(function() {

});




var branchType;
function checkSubBranhOperation() {

    var jsonData = {
        "select": "roleID,branchType",
        "from": "RoleInfo",
        "where": {
            "active": '1'
        },
        "column": "roleID",
        "dir": "desc"
    };
    $.ajax({
        type: "Post",
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/getDataListAction",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var outData = JSON.parse(data.d)[0];
            branchType = outData.branchType;
            if (branchType == "sub") {
                $('.subbranch-hide').addClass("disNone");
            }
        },
        failure: function (response) {
            console.log(response);
            alert(response);
        },
        error: function (response) {
            console.log(response);
            alert(response);
        }
    });
}

