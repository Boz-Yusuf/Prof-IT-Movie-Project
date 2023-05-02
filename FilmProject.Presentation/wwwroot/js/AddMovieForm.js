
$(document).ready(function () {
    $.get("https://localhost:7269/Category/AllCategories", function (data) {
        var select = $(".chosen-select");

        $.each(data, function (index, category) {
            select.append('<option value="' + category.id + '">' + category.categoryName + '</option>');
        });
        select.trigger("chosen:updated");
    });
    $('.chosen-select').chosen();
    $("#movieForm").validate({
        errorClass: "text-danger",
        rules: {
            "MovieName": {
                required: true
            },
            "MovieSummary": {
                required: true
            },
            "DirectorName": {
                required: true
            },
            "RatingAge": {
                required: true
            },
            "PublishYear": {
                required: true
            },
            "Photo": {
                required: true
            },
            "ImdbUrl": {
                required: true,
                url: true
            },
            "MovieLanguage": {
                required: true
            },
            "CategoriesId": {
                required: true,
                minlength: 1
            }
        },
        messages: {
            "MovieName": {
                required: "Film ismi zorunludur."
            },
            "MovieSummary": {
                required: "Film �zeti zorunludur."
            },
            "DirectorName": {
                required: "Filmin y�netmen bilgisi zorunludur."
            },
            "RatingAge": {
                required: "Filmin izleyici kitle bilgisi zorunludur."
            },
            "PublishYear": {
                required: "Filmin tarih bilgisi zorunludur."
            },
            "Photo": {
                required: "Filmin afi�ini y�kleyin."
            },
            "ImdbUrl": {
                required: "Filmin IMDB adresini girin.",
                url: "Ge�erli bir adres bilgisi girilmelidir."
            },
            "MovieLanguage": {
                required: "L�tfen filmin dilini girin."
            },
            "CategoriesId": {
                required: "L�tfen kategori ekleyiniz.",
                minlength: "L�tfen kategori ekleyiniz."
            }
        }
    });



    $("form").on("submit", function (e) {
        if (Array.from(document.querySelectorAll('select[name="CategoriesId"] option:checked')).map(option => option.value).length == 0) {
            e.preventDefault();
            $("#errorSelect").text("L�tfen kategori bilgisi girin.");
        }
    });




});

