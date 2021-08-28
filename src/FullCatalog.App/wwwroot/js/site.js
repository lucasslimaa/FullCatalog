function AjaxModal() {

    $(document).ready(function () {
        $(function () {
            $.ajaxSetup({ cache: false });

            $("a[data-modal]").on("click",
                function (e) {
                    $('#myModalContent').load(this.href,
                        function () {
                            $('#myModal').modal({
                                keyboard: true
                            },
                                'show');
                            bindForm(this);
                        });
                    return false;
                });
        });

        function bindForm(dialog) {
            $('form', dialog).submit(function () {
                $.ajax({
                    url: this.action,
                    type: this.method,
                    data: $(this).serialize(),
                    success: function (result) {
                        if (result.success) {
                            $('#myModal').modal('hide');
                            $('#AddressTarget').load(result.url);
                        } else {
                            $('#myModalContent').html(result);
                            bindForm(dialog);
                        }
                    }
                });
                return false;
            });
        }
    });
}

function SearchZipCode() {
    $(document).ready(function () {

        function clear_zipcode_form() {
            $("#Address_Street").val("");
            $("#Address_Neighborhood").val("");
            $("#Address_City").val("");
            $("#Address_State").val("");
        }

        $("#Address_ZipCode").blur(function () {

            var zipcode = $(this).val().replace(/\D/g, '');

            if (zipcode != "") {

                var validatezipcode = /^[0-9]{8}$/;

                if (validatezipcode.test(zipcode)) {

                    $("#Address_Street").val("...");
                    $("#Address_Neighborhood").val("...");
                    $("#Address_City").val("...");
                    $("#Address_State").val("...");

                    $.getJSON("https://viacep.com.br/ws/" + zipcode + "/json/?callback=?",
                        function (data) {

                            if (!("erro" in data)) {
                                $("#Address_Street").val(data.logradouro);
                                $("#Address_Neighborhood").val(data.bairro);
                                $("#Address_City").val(data.localidade);
                                $("#Address_State").val(data.uf);
                            } 
                            else {
                                clear_zipcode_form();
                                alert("ZipCode not found =s");
                            }
                        });
                } 
                else {

                    clear_zipcode_form();
                    alert("ZipCode format is invalid");
                }
            }
            else {
                clear_zipcode_form();
            }
        });
    });
}

$(document).ready(function () {
    $("#msg_box").fadeOut(2500);
});