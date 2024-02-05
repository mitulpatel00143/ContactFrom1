$(document).ready(function () {
    $("#MainForm").modal('show');
    $('#Name, #Number, #Email, #Description').on('input', function () {
        $(this).css('border', '');
    });

    $('#Number').keypress(function (e) {
        if (String.fromCharCode(e.keyCode).match(/[^\d]/g)) return false;
    });

    $('#FormSubmit').click(function () {
        var name = $('#Name').val().trim();
        var customer_number = $('#Customer_Number').val().trim();
        var email = $('#Email').val().trim();
        var description = $('#Description').val().trim();
        var selectedRadioValue = $("input[name='radios']:checked").val();

        if (name === '' || description === '') {

            $('#MainForm').addClass('shake');
            setTimeout(function () {
                $('#MainForm').removeClass('shake');
            }, 500);

            //toastr.error('Please fill in all required fields.');
            if (name === '') $('#Name').css('border', '1px solid #ff9191');
            if (customer_number === '') $('#Customer_Number').css('border', '1px solid #ff9191');
            if (email === '') $('#Email').css('border', '1px solid #ff9191');
            if (description === '') $('#Description').css('border', '1px solid #ff9191');
            return;
        }

        // Validate email
        if (!isValidEmail(email)) {
            toastr.error('Please enter a valid email address.');
            $('#Email').css('border', '1px solid #ff9191');
            return;
        }

        // Validate number
        if (!isValidNumber(customer_number)) {
            toastr.error('Please enter a valid number.');
            $('#Number').css('border', '1px solid #ff9191');
            return;
        }

        // Additional validation logic can be added here

        // If all validations pass, log field values to the console
        //console.log('Name:', name);
        //console.log('Number:', phone);
        //console.log('Email:', email);
        //console.log('Description:', description);
        //console.log('Selected Radio Value:', selectedRadioValue);

        // Reset borders to default
        $('#Name, #Customer_Number, #Email, #Description','#Web_QualityContactType_Id').css('border', '');

        var contactClass = {
            Name: name,
            Customer_Number: customer_number,
            Email: email,
            Description: description,
            Web_QualityContactType_Id: (selectedRadioValue == "Reclamacao" ? 1 : selectedRadioValue == "Sugestão" ? 2 : 3),
            User_IP: '10.10.10.10'
        };
        //console.log(contactClass);
        // Show success message using Toastr
        $.ajax({
            type: "POST",
            url: "/Web_Quality_Contact/Index",
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(contactClass),
            dataType: "json",
            success: function (response) {
                if (response.Success) {

                    $("#secondModal").addClass('buzz');
                    setTimeout(function () {
                        $("#secondModal").removeClass('buzz');
                    }, 500);

                    $("#secondModal").modal('show');
                    $("#MainForm").modal('hide');
                    clearDetails();
                    toastr.success('Form submitted successfully!');
                }
                else {
                    toastr.warning('Data not inserted.');
                }
            },
            error: function (errormessage) {
                console.log(errormessage);
                toastr.error("Error! While data is not add.");
            }
        });
    });

    //$('#FormSubmit').click(function () {
    //    var name = $('#Name').val().trim();
    //    var phone = $('#Number').val().trim();
    //    var email = $('#Email').val().trim();
    //    var description = $('#Description').val().trim();
    //    var selectedRadioValue = $("input[name='radios']:checked").val();

    //    if (name === '' || description === '') {

    //        $('#MainForm').addClass('shake');
    //        setTimeout(function () {
    //            $('#MainForm').removeClass('shake');
    //        }, 500);

    //        //toastr.error('Please fill in all required fields.');
    //        if (name === '') $('#Name').css('border', '1px solid #ff9191');
    //        if (phone === '') $('#Number').css('border', '1px solid #ff9191');
    //        if (email === '') $('#Email').css('border', '1px solid #ff9191');
    //        if (description === '') $('#Description').css('border', '1px solid #ff9191');
    //        return;
    //    }

    //    // Validate email
    //    if (!isValidEmail(email)) {
    //        toastr.error('Please enter a valid email address.');
    //        $('#Email').css('border', '1px solid #ff9191');
    //        return;
    //    }

    //    // Validate number
    //    if (!isValidNumber(phone)) {
    //        toastr.error('Please enter a valid number.');
    //        $('#Number').css('border', '1px solid #ff9191');
    //        return;
    //    }

    //    // Additional validation logic can be added here

    //    // If all validations pass, log field values to the console
    //    //console.log('Name:', name);
    //    //console.log('Number:', phone);
    //    //console.log('Email:', email);
    //    //console.log('Description:', description);
    //    //console.log('Selected Radio Value:', selectedRadioValue);

    //    // Reset borders to default
    //    $('#Name, #Number, #Email, #Description').css('border', '');

    //    var contactClass = {
    //        Name: name,
    //        Phone: phone,
    //        Email: email, 
    //        Description: description,
    //        Complaint: (selectedRadioValue == "Reclamacao" ? true : false),
    //        Suggestion: (selectedRadioValue == "Sugestao" ? true : false),
    //        Praise: (selectedRadioValue == "Elogio" ? true : false)
    //    };
    //    //console.log(contactClass);
    //    // Show success message using Toastr
    //    $.ajax({
    //        type: "POST",
    //        url: "/api/ContactApi/Index",
    //        contentType: "application/json;charset=utf-8",
    //        data: JSON.stringify(contactClass),
    //        dataType: "json",
    //        success: function (response) {
    //            if (response.Success) {

    //                $("#secondModal").addClass('buzz');
    //                setTimeout(function () {
    //                    $("#secondModal").removeClass('buzz');
    //                }, 500);

    //                $("#secondModal").modal('show');
    //                $("#MainForm").modal('hide');
    //                clearDetails();
    //                toastr.success('Form submitted successfully!');
    //            }
    //            else {
    //                toastr.warning('Data not inserted.');
    //            }
    //        },
    //        error: function (errormessage) {
    //            console.log(errormessage);
    //            toastr.error("Error! While data is not add.");
    //        }
    //    });
    //});

    function isValidEmail(email) {
        // Email validation regex
        var re = /^[^\s]+[^\s]+\.[^\s]+$/;
        return re.test(email);
    }

    function isValidNumber(customer_number) {
        // Number validation regex
        var rx = /^\d{1,11}$/;
        return rx.test(customer_number);
    }    

    function clearDetails() {
        $('#Name').val("");
        $('#Customer_Number').val("");
        $('#Email').val("");
        $('#Description').val("");
        //$("input[name='radios']").prop('checked', false);
        $('#radio1').prop('checked', true);
    }

    $("#ClosePopup").click(function () {
        $("#MainForm").modal('show');
    });
});