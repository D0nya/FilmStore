// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

async function Filter(n) {
  const [panelBody] = document.getElementsByClassName('panel-body');
  const [genreId, countryId, searchString, producerName, from, to] = document.getElementsByClassName('searchClass');
  const loader = '<div class="text-center"><div class="spinner-border text-secondary" role="status"><span class="sr-only">Loading...</span></div></div>';
  panelBody.innerHTML = loader;
  const response = await fetch(
    '/Order/Search?searchString=' + searchString.value +
    '&genreId=' + genreId.value + '&countryId=' + countryId.value +
    '&producerName=' + producerName.value + '&yearFrom=' + from.value + '&yearTo=' + to.value + '&sort=' + n);
  let res;
  if (response.ok) {
    res = await response.text();
    panelBody.innerHTML = res;
  } else {
    res = response.status;
    console.log(res);
  }
}