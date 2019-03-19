// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $(".ratingStar").hover(function () {
        $(".ratingStar").addClass("far").removeClass("fas");

        $(this).addClass("fas").removeClass("far");
        $(this).prevAll(".ratingStar").addClass("fas").removeClass("far");
    });

    $(".ratingStar").click(function () {
        var starValue = $(this).attr("data-value")
        $("#ratingsValue").val(starValue);
    });
});