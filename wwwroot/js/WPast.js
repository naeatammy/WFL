var projecttag = document.querySelector('.myprojecttag');
projecttag.onmouseover = function() {
    projecttag.setAttribute("style", "background-color: #1a1f27;");
}
projecttag.onmouseout = function() {
    projecttag.setAttribute("style", "background-color: black");
}

var fbtag = document.querySelector('.feedbacktag');
fbtag.onmouseover = function() {
    fbtag.setAttribute("style", "background-color: #1a1f27; border-bottom: 3px solid white;");
}
fbtag.onmouseout = function() {
    fbtag.setAttribute("style", "background-color: black; border-bottom: none");
}
fbtag.onclick = function() {
    window.location.href = "Feedback.html";
}

var listtag = document.querySelector('.listtag');
listtag.onmouseover = function() {
    listtag.setAttribute("style", "background-color: #1a1f27; border-bottom: 3px solid white;");
}
listtag.onmouseout = function() {
    listtag.setAttribute("style", "background-color: black; border-bottom: none");
}
listtag.onclick = function() {
    window.location.href = "Favourite.html";
}

var wp = document.getElementById("wp");
wp.onmouseover = function() {
    wp.setAttribute("style", "background-color: #f9f9f9;");
}
wp.onmouseout = function() {
    wp.setAttribute("style", "background-color: none");
}

var op = document.getElementById("op");
op.onmouseover = function() {
    wp.setAttribute("style", "background-color: #f9f9f9;");
    document.getElementById("op_text").setAttribute("style", "border-bottom: 5px solid black;");
}
op.onmouseout = function() {
    wp.setAttribute("style", "background-color: none");
    document.getElementById("op_text").setAttribute("style", "border: none");
}


var pp = document.getElementById("pp");
pp.onmouseover = function() {
    pp.setAttribute("style", "background-color: #f9f9f9;");
    document.getElementById("pp_text").setAttribute("style", "border-bottom: 5px solid black;");
}
pp.onmouseout = function() {
    pp.setAttribute("style", "background-color: none");
    document.getElementById("pp_text").setAttribute("style", "border: none");
}


