function checkLogin() {
    var username = $("#username").val();
    var password = $("#password").val();

    if (!username || !password) {
        alert("Please enter username and password");
        return;
    }

    let model = { UserName: username, Password: password }

    $.ajax({
        url: "/Home/Login",
        type: "POST",
        data: model,
        success: function (response) {
            if (response.Success) {
                //window.location.href = "/Home/Index";
            }
            else {
                alert(response.Message);
            }
        },
        error: function (xhr, status, error) {
            console.error("AJAX Error:", error);
            alert("Something went wrong. Please try again.");
        }
    });
}


function getAllMyAppointmentAsDoctor()
{
}