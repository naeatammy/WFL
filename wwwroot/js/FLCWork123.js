var projecttag = document.querySelector('.myprojecttag');
projecttag.onmouseover = function() {
    projecttag.setAttribute("style", "background-color: #1a1f27;");
}
projecttag.onmouseout = function() {
    projecttag.setAttribute("style", "background-color: black");
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

var cw = document.getElementById("cw");
cw.onmouseover = function() {
    cw.setAttribute("style", "background-color: #f9f9f9;");
}
cw.onmouseout = function() {
    cw.setAttribute("style", "background-color: none");
}

var pw = document.getElementById("pw");
pw.onmouseover = function() {
    pw.setAttribute("style", "background-color: #f9f9f9;");
    document.getElementById("pw_text").setAttribute("style", "border-bottom: 5px solid black;");
}
pw.onmouseout = function() {
    pw.setAttribute("style", "background-color: none");
    document.getElementById("pw_text").setAttribute("style", "border: none");
}

var contextMenu = document.getElementById('contextMenu');
const dialog1 = document.getElementById('myDialog2');
const closeDelButton = document.getElementById('closeDelDialog');
let currentRow;


function attachRightClickEvent() {
    console.log('Attaching right-click event listeners');

    function handleRightClick(event, row) {
        event.preventDefault();
        currentRow = row;
        contextMenu.style.display = 'block';
        contextMenu.style.left = `${event.pageX}px`;
        contextMenu.style.top = `${event.pageY}px`;
    }


    document.addEventListener('click', () => {
        contextMenu.style.display = 'none';
    });

    contextMenu.addEventListener('click', (event) => {
        event.stopPropagation();
    });

    const rows = document.querySelectorAll('table tbody tr');

    if (rows.length > 0) {
        console.log(`${rows.length} rows found`);
    } else {
        console.log('No rows found');
    }

    rows.forEach(row => {
        row.addEventListener('contextmenu', (event) => handleRightClick(event, row));
    });


    const menuItems = contextMenu.querySelectorAll('.context-menu__item');
    menuItems.forEach(item => {
        item.addEventListener('click', (event) => {

            if (event.target.textContent === "Submit") {
                dialog1.showModal();
                DeleteJob();
            }

        });
    });

    closeButton.addEventListener('click', () => {
        dialog.close();
    });

    closeDelButton.addEventListener('click', () => {
        dialog1.close();
    });
    closeEditButton.addEventListener('click', () => {
        dialog3.close();
    });
}

attachRightClickEvent();

var browse = document.getElementById("Browse");
browse.onmouseover = function () {
    document.getElementById("browseBox").setAttribute("style", "visibility: unset;");
}
browse.onmouseout = function () {
    document.getElementById("browseBox").setAttribute("style", "visibility: hidden;");
}