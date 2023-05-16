function CreateHistoryTable(histories, containerID) {
    function CreateRowTxtContent(type, textCenterAlign, text) {
        const elem = document.createElement(type);
        elem.classList.add("QAValue");

        const p = document.createElement('p');
        p.textContent = text;
        
        if(textCenterAlign == true) {
            p.style.textAlign = 'center';
        }

        elem.appendChild(p);

        return elem;
    }

    let container = document.getElementById(containerID);
    container.innerHTML = '';
    
    const table = document.createElement("table");
    table.classList.add("QATable");

    const headRow = document.createElement("tr");
    headRow.classList.add("QARow");
    [
        {
            Text: "Date time created",
            Width: "50%"
        },
        {
            Text: "Subject",
            Width: "30%"
        },
        {
            Text: "Actions",
            Width: "20%"
        }
    ].forEach((head) => {
        const th = CreateRowTxtContent("th", true, head.Text);
        th.style.width = head.Width;
        headRow.appendChild(th);
    });
    table.appendChild(headRow);

    histories.forEach((history) => {
        const row = document.createElement("tr");
        row.classList.add("QARow");
        row.id = history.ID;

        // Datetime:
        row.appendChild(CreateRowTxtContent("td", false, history.GeneratedTime));

        // Subject:
        row.appendChild(CreateRowTxtContent("td", true, history.Subject));

        // Actions:
        const actionTd = document.createElement("td");
        const editBtn = document.createElement("button");
        editBtn.textContent = "Edit";
        editBtn.style.width = "50%";
        editBtn.addEventListener("click", () => {
            EditBtnFunc(history.ID);
        });
        actionTd.appendChild(editBtn);

        const removeBtn = document.createElement("button");
        removeBtn.textContent = "Remove";
        removeBtn.style.width = "50%";
        removeBtn.addEventListener("click", () => {
            RemoveBtnFunc(history.ID);
        });
        actionTd.appendChild(removeBtn);

        row.appendChild(actionTd);
        table.appendChild(row);
    });

    container.appendChild(table);
}

function EditBtnFunc(id) {
    console.log(id);
}

function RemoveBtnFunc(id) {
    console.log(id);
}