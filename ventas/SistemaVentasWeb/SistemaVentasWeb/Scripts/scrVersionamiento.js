var strRuta = "";
var js = document.querySelectorAll('script');
for (var i = 0; i < js.length; i++) {
    strRuta = js[i].src;
    if (strRuta.includes("scr", 0)) {
        js[i].src = js[i].src + '?ver=' + Date.now();
        //alert(js[i].src);
    }
}
var css = document.querySelectorAll('link');
for (var i = 0; i < css.length; i++) {
    strRuta = css[i].href;
    if (strRuta.includes("pag", 0)) {
        css[i].href = css[i].href + '?ver=' + Date.now();
        //alert(css[i].href);
    }
}