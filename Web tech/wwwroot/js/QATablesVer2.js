function CreateQATable(qsts, containerID) {
    function CreateRowContent(type, textCenterAlign, content, img) {
        const elem = document.createElement(type);
        elem.classList.add("QAValue");

        const p = document.createElement("p");
        p.textContent = content;

        if (textCenterAlign == true) {
            p.style.textAlign = "center";
        }

        elem.appendChild(p);

        if (img) {
            const imgElem = document.createElement("img");
            imgElem.src = img;
            imgElem.alt = img;
            elem.appendChild(imgElem);
        }

        return elem;
    }

    let container = document.getElementById(containerID);

    console.log("Clearing container");
    container.innerHTML = '';
    console.log("Done clear container");

    console.log("Creating table");
    const table = document.createElement("table");
    table.classList.add("QATable");

    console.log("\tCreating header row");
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
    console.log("\tDone create header row");

    console.log("\tCreating content row");
    qsts.forEach((qst) => {
        console.log("\t\tCreating row for content id: " + qst.ID + "\n");
        console.log(qst);

        const row = document.createElement("tr");
        row.addEventListener("click", () => {
            console.log("Clicked");

            if (row.classList.contains("Extended")) {
                row.classList.remove("Extended");
                console.log("\tShrink");
            }
            else {
                row.classList.add("Extended");
                console.log("\tExtend");
            }

            console.log(row);
        })

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

        console.log("\t\tDone create row for content");
    });
    console.log("\tDone create content row");

    container.appendChild(table);
    AddExtendFunction(containerID);
}

function AddExtendFunction(containerID) {
    console.log("Adding extend function");

    const container = document.getElementById(containerID);
    const rows = [...container.querySelectorAll("tr")];

    rows.forEach((row) => {
        console.log("\tProcessing row id: " + row.id + "\n");
        console.log(row);

        const texts = [...row.querySelectorAll("p")];
        const img = [...row.querySelectorAll("img")];

        var isOverFlowed = false;

        console.log("\t\tChecking all texts in row");
        texts.forEach((text) => {
            console.log("\t\t\tChecking text:\n" + text.textContent);

            if (IsOverFlowed(text)) {
                console.log("\t\t\t\tIs overflowed");
                isOverFlowed = true;
            }

            console.log("\t\t\tDone check text");
        });
        console.log("\t\tDone checking all texts in row");

        console.log("\t\tIs Overflowed: " + isOverFlowed);
        console.log("\t\timg: " + img.length > 0);
        if (isOverFlowed || img.length > 0) {
            console.log("\t\tMarking row as overflowed");
            row.classList.add("OverFlowed");
        }

        console.log("\tDone process row");
    });

    console.log("Done add extend function");
}

function IsOverFlowed(text) {
    if (text.scrollWidth > text.offsetWidth || text.textContent.indexOf('\n') !== -1) {
        return true;
    }
    else {
        return false;
    }
}