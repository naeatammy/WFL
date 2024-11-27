var projecttag = document.querySelector('.myprojecttag');
projecttag.onmouseover = function () {
    projecttag.setAttribute("style", "background-color: #1a1f27; border-bottom: 3px solid white;");
}
projecttag.onmouseout = function () {
    projecttag.setAttribute("style", "background-color: black; border-bottom: none");
}


var fbtag = document.querySelector('.feedbacktag');
fbtag.onmouseover = function () {
    fbtag.setAttribute("style", "background-color: #1a1f27;");
}
fbtag.onmouseout = function () {
    fbtag.setAttribute("style", "background-color: black");
}

var listtag = document.querySelector('.listtag');
listtag.onmouseover = function () {
    listtag.setAttribute("style", "background-color: #1a1f27; border-bottom: 3px solid white;");
}
listtag.onmouseout = function () {
    listtag.setAttribute("style", "background-color: black; border-bottom: none");
}
