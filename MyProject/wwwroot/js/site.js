// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $(".ratingStar").hover(function () {
        $(".ratingStar").addClass("far").removeClass("fas");

        $(this).addClass("fas").removeClass("far");
        $(this).prevAll(".ratingStar").addClass("fas").removeClass("far");
    });

    $(".ratingStar2").hover(function () {
        $(".ratingStar2").addClass("far").removeClass("fas");

        $(this).addClass("fas").removeClass("far");
        $(this).prevAll(".ratingStar2").addClass("fas").removeClass("far");
    });

    $(".ratingStar3").hover(function () {
        $(".ratingStar3").addClass("far").removeClass("fas");

        $(this).addClass("fas").removeClass("far");
        $(this).prevAll(".ratingStar3").addClass("fas").removeClass("far");
    });

    $(".ratingStar4").hover(function () {
        $(".ratingStar4").addClass("far").removeClass("fas");

        $(this).addClass("fas").removeClass("far");
        $(this).prevAll(".ratingStar4").addClass("fas").removeClass("far");
    });

    $(".ratingStar5").hover(function () {
        $(".ratingStar5").addClass("far").removeClass("fas");

        $(this).addClass("fas").removeClass("far");
        $(this).prevAll(".ratingStar5").addClass("fas").removeClass("far");
    });

    $(".ratingStar").click(function () {
        var starValue = $(this).attr("data-value")
        $("#ratingsValue1").val(starValue);
    });

    $(".ratingStar2").click(function () {
        var starValue = $(this).attr("data-value")
        $("#ratingsValue2").val(starValue);
    });

    $(".ratingStar3").click(function () {
        var starValue = $(this).attr("data-value")
        $("#ratingsValue3").val(starValue);
    });

    $(".ratingStar4").click(function () {
        var starValue = $(this).attr("data-value")
        $("#ratingsValue4").val(starValue);
    });

    $(".ratingStar5").click(function () {
        var starValue = $(this).attr("data-value")
        $("#ratingsValue5").val(starValue);
    });
});