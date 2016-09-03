$(document).ready(function () {
    $('.selectpicker').selectpicker({
        style: 'btn-default',
        liveSearchStyle: 'startsWith',
        width: '100%',
        size: '10'
    });

    var kategori = $("#hfKategori").val();
    var sehir = $("#hfSehir").val();

    $("#ddlKategori").val(kategori).selectpicker('refresh');
    $("#ddlSehirler").val(sehir).selectpicker('refresh');

});

function UrlGit(sehirSeo) {
    window.location.href = "/hava-durumu/hava-durumu/" + sehirSeo + "-hava-durumu";
}

function BtnGit() {
    var sehir = $("#ddlSehirler option:selected").val();
    var kategori = $("#ddlKategori option:selected").val();

    $.ajax({
        type: "POST",
        url: "/hava-durumu/hava-durumu.aspx/AramaUrlDon",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{kategori:" + JSON.stringify(kategori) + ", il:" + JSON.stringify(sehir) + "}",
        async: true,
        success: function (result) {
            window.location.href = result.d;
        },
        error: function (xhr, status, error) {
            bootbox.alert(error);
        }
    });
}
