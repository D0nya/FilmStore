console.log('loaded');
$('div.card-footer>input').on(
  {
    'input': function () {
      console.log('input');

      var num = $(this).val();
      var ref = $(this).siblings('a').attr('href');
      var index = ref.indexOf('count=') + 'count='.length;
      ref = ref.substr(0, index) + num + ref.substr(ref.indexOf('&'), ref.length);
      $(this).siblings('a').attr('href', ref);
    }
  });