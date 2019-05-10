// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(function () {
    $(".ratingStar").click(function () {
        $(".ratingStar").addClass("far").removeClass("fas");

        $(this).addClass("fas").removeClass("far");
        $(this).prevAll(".ratingStar").addClass("fas").removeClass("far");

        var starValue = $(this).attr('data-value')
        $('#ratingsValue').val(starValue);
    });

    $(".ratingStar2").click(function () {
        $(".ratingStar2").addClass("far").removeClass("fas");

        $(this).addClass("fas").removeClass("far");
        $(this).prevAll(".ratingStar2").addClass("fas").removeClass("far");

        var starValue = $(this).attr('data-value')
        $('#ratingsValue2').val(starValue);
    });

    $(".ratingStar3").click(function () {
        $(".ratingStar3").addClass("far").removeClass("fas");

        $(this).addClass("fas").removeClass("far");
        $(this).prevAll(".ratingStar3").addClass("fas").removeClass("far");

        var starValue = $(this).attr('data-value')
        $('#ratingsValue3').val(starValue);
    });

    $(".ratingStar4").click(function () {
        $(".ratingStar4").addClass("far").removeClass("fas");

        $(this).addClass("fas").removeClass("far");
        $(this).prevAll(".ratingStar4").addClass("fas").removeClass("far");

        var starValue = $(this).attr('data-value')
        $('#ratingsValue4').val(starValue);
    });

    $(".ratingStar5").click(function () {
        $(".ratingStar5").addClass("far").removeClass("fas");

        $(this).addClass("fas").removeClass("far");
        $(this).prevAll(".ratingStar5").addClass("fas").removeClass("far");

        var starValue = $(this).attr('data-value')
        $('#ratingsValue5').val(starValue);
    });
});