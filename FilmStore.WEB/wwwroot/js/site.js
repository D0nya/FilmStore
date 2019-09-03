// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('.btn-genre').on('click', function () {
  
  $(this).children('input').attr('checked', !$(this).children('input').attr('checked'));
});