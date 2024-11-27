var projecttag = document.querySelector('.myprojecttag');
projecttag.onmouseover = function () {
    projecttag.setAttribute("style", "background-color: #1a1f27;");
}
projecttag.onmouseout = function () {
    projecttag.setAttribute("style", "background-color: black");
}


var listtag = document.querySelector('.listtag');
listtag.onmouseover = function () {
    listtag.setAttribute("style", "background-color: #1a1f27; border-bottom: 3px solid white;");
}
listtag.onmouseout = function () {
    listtag.setAttribute("style", "background-color: black; border-bottom: none");
}
listtag.onclick = function () {
    window.location.href = "Favourite.html";
}

var op = document.getElementById("op");
op.onmouseover = function () {
    op.setAttribute("style", "background-color: #f9f9f9;");
}
op.onmouseout = function () {
    op.setAttribute("style", "background-color: none");
}

var wp = document.getElementById("wp");
wp.onmouseover = function () {
    wp.setAttribute("style", "background-color: #f9f9f9;");
    document.getElementById("wp_text").setAttribute("style", "border-bottom: 5px solid black;");
}
wp.onmouseout = function () {
    wp.setAttribute("style", "background-color: none");
    document.getElementById("wp_text").setAttribute("style", "border: none");
}



var pp = document.getElementById("pp");
pp.onmouseover = function () {
    pp.setAttribute("style", "background-color: #f9f9f9;");
    document.getElementById("pp_text").setAttribute("style", "border-bottom: 5px solid black;");
}
pp.onmouseout = function () {
    pp.setAttribute("style", "background-color: none");
    document.getElementById("pp_text").setAttribute("style", "border: none");
}


var browse = document.getElementById("Browse");
browse.onmouseover = function () {
    document.getElementById("browseBox").setAttribute("style", "visibility: unset;");
}
browse.onmouseout = function () {
    document.getElementById("browseBox").setAttribute("style", "visibility: hidden;");
}

var contextMenu = document.getElementById('contextMenu');
const dialog = document.getElementById('myDialog1');
const dialog1 = document.getElementById('myDialog2');
const dialog3 = document.getElementById('myDialog3');
const closeButton = document.getElementById('closeDialog');
const closeDelButton = document.getElementById('closeDelDialog');
const closeEditButton = document.getElementById('closeEditDialog');
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
            if (event.target.textContent === "Review") {
                dialog.showModal();
            }
            if (event.target.textContent === "Delete") {
                dialog1.showModal();
                DeleteJob();
            }
            if (event.target.textContent === "Edit") {
                const id = currentRow.cells[0].innerText;
                document.getElementById("id_input").value = id;
                const des = currentRow.cells[3].innerText;
                document.getElementById("des").value = des;
                const expduration = currentRow.cells[4].innerText;
                const arr = expduration.split(" ");
                const duration1 = document.getElementById("duration1");
                duration1.value = arr[0];
                const duration2 = document.getElementById("duration2");
                duration2.value = arr[1];
                const name = currentRow.cells[2].innerText;
                document.getElementById("name_input").value = name;
                const paymentAmount = currentRow.cells[5].innerText;
                const paymentType = currentRow.cells[6].innerText;
                document.getElementById("paymentType").value = paymentType;
                document.getElementById("amount_text").value = paymentAmount;
                dialog3.showModal();
            }
            contextMenu.style.display = 'none';
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


var profile_menu = document.getElementById("profile_name");
profile_menu.onmouseover = function() {
    document.getElementById("profile_menu_box").setAttribute("style", "visibility: unset;");
}

profile_menu.onmouseout = function() {
    document.getElementById("profile_menu_box").setAttribute("style", "visibility: hidden");
}
