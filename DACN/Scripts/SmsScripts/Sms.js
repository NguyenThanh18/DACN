var sendOTP = function () {
    var phoneNumber = document.getElementById("PhoneNumber").value;
    $.ajax({
        url: "/Sms/sendOTP",
        type: "POST",
        data: { phoneNumber: phoneNumber },
        success: function (res) {
        }
    });
}

var verifyOTP = function () {
    var phoneNumber = document.getElementById("PhoneNumber").value;
    var OTPCode = document.getElementById("OTPCode").value;

    $.ajax({
        url: "/Sms/verifyOTP",
        type: "POST",
        data: { phoneNumber: phoneNumber, OTP: OTPCode },
        success: function (res) {
            if (res == "approved") {
                document.getElementById("verifyOTP").innerHTML = "Xác thực không thành công!";
                document.getElementById("verifyOTP1").innerHTML = "Quay lại trang đăng tin!";
                document.getElementById('btnSubmit').disabled = false;
                $(btnSubmit1).show();
                $(btnSubmit).hide();
            } else {
                document.getElementById("verifyOTP").innerHTML = "Xác thực không thành công!";
                document.getElementById('btnSubmit').disabled = true;
            }
        }
    });
}