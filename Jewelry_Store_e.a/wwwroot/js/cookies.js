function setCookie(cname, cvalue, exdays) {
  var d = new Date();
  d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
  var expires = "expires="+d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires;
  setCookiesCount();
}

function getCookie(cname) {
  var name = cname + "=";
  var ca = document.cookie.split(';');
  for(var i = 0; i < ca.length; i++) {
    var c = ca[i];
    while (c.charAt(0) == ' ') {
      c = c.substring(1);
    }
    if (c.indexOf(name) == 0) {
      return c.substring(name.length, c.length);
    }
  }
  return "";
}

function removeCookie(number) {
    Cookies.remove(`SDSC${number}`);
    var registry = Cookies.get("SDregistery").split("~");
    registry.splice(registry.indexOf(number.toString()), 1);
    var newRegistry = registry.join("~")
    Cookies.set("SDregistery", newRegistry);
}
function clearCookies() {
    for (var cookie in Cookies.get()) {
        if (cookie.startsWith("SD")) {
            Cookies.remove(cookie);
        }
    }
}

function setCookiesCount()
{
    if (Cookies.get("SDregistery")) {
        let arr = Cookies.get('SDregistery').split("~")
        let count = arr.length
        if (count == 1 && arr[0] == "")
            count = 0
        if (count > 0) $("#btnCart").html(`My Cart <span class="badge badge-pill badge-info"> ${count} </span>`)
    }
}
$(document).ready(setCookiesCount);