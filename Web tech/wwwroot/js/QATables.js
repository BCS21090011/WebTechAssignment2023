function CreateQATable(qsts, containerID) {
    function CreateRowContent(type, textCenterAlign, content, img) {
        const elem = document.createElement(type);
        elem.classList.add("QAValue");
        
        const p = document.createElement("p");
        p.textContent = content;
        
        if(textCenterAlign == true) {
            p.style.textAlign = "center";
        }

        elem.appendChild(p);
        
        if(img) {
            const imgElem = document.createElement("img");
            imgElem.src = img;
            imgElem.alt = img;
            elem.appendChild(imgElem);
        }

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
            Text: "ID",
            Width: "5%"
        },
        {
            Text: "Question",
            Width: "30%"
        },
        {
            Text: "Answer",
            Width: "30%"
        },
        {
            Text: "Subject",
            Width: "10%"
        },
        {
            Text: "Mark",
            Width: "10%"
        },
        {
            Text: "Difficulty",
            Width: "15%"
        }
    ].forEach((head) => {
        const th = CreateRowContent("th", true, head.Text);
        th.style.width = head.Width;
        headRow.appendChild(th);
    });
    table.appendChild(headRow);

    qsts.forEach((qst) => {
        const row = document.createElement("tr");
        row.classList.add("QARow");
        row.id = qst.ID;
        
        // ID:
        row.appendChild(CreateRowContent("td", true, qst.ID));

        // Question:
        row.appendChild(CreateRowContent("td", false, qst.Qst, qst.QstImgFileName));

        // Answer:
        row.appendChild(CreateRowContent("td", false, qst.Ans, qst.AnsImgFileName));

        // Subject:
        row.appendChild(CreateRowContent("td", true, qst.Subject));

        // Mark:
        row.appendChild(CreateRowContent("td", true, qst.Mark));

        // Difficulty:
        row.appendChild(CreateRowContent("td", true, qst.Difficulty));

        table.appendChild(row);
    });

    container.appendChild(table);
}