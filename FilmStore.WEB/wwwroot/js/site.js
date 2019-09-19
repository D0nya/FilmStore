  // Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

async function Filter(page, sort) {
  const [panelBody] = document.getElementsByClassName('panel-body');
  const [genreId, countryId, searchString, producerName, from, to] = document.getElementsByClassName('searchClass');
  const loader = '<div class="text-center"><div class="spinner-border text-secondary" role="status"><span class="sr-only">Loading...</span></div></div>';
  panelBody.innerHTML = loader;
  const response = await fetch(
    '/Order/Search?searchString=' + searchString.value +
    '&genre=' + genreId.value + '&country=' + countryId.value +
    '&producer=' + producerName.value + '&yearFrom=' + from.value + '&yearTo=' + to.value +
    '&page=' + page + '&sortOrder=' + sort + '&returnUrl=' + window.location.pathname);
  let res;
  if (response.ok) {
    res = await response.text();
    panelBody.innerHTML = res;
  } else {
    res = response.status;
    console.log(res);
  }
}

async function LoadNews() {
  const [newsFeed] = document.getElementsByClassName('news-feed');
  console.log(newsFeed);
  const loader = '<div class="text-center"><div class="spinner-border text-secondary" role="status"><span class="sr-only">Loading...</span></div></div>';
  newsFeed.innerHTML = loader;

  const response = await fetch('/News/NewsFeed');
  let res;
  if (response.ok) {
    res = await response.text();
    newsFeed.innerHTML = res;
  } else {
    res = response.status;
    console.log(res);
  }
}