const qstSelectionsDiv = document.getElementById("QuestionSelections");
const selectedQstDiv = document.getElementById("SelectedQuestions");

function Initializing() {
    qstSelectionsDiv.addEventListener("dragover", QstSlctDragOver);
    qstSelectionsDiv.addEventListener("drop", QstSlctDrop);
    selectedQstDiv.addEventListener("dragover", SlctQstDragOver);
    selectedQstDiv.addEventListener("drop", SlctQstDrop);
    UpdateQuestions();
}

function SwitchPreview() {
    let qstContainer = document.getElementById("Qst-Preview-Container");
	let ansContainer = document.getElementById("Ans-Preview-Container");

    if(qstContainer.style.display === "none") {
		qstContainer.style.display = "block";
		ansContainer.style.display = "none";
	} else {
		qstContainer.style.display = "none";
		ansContainer.style.display = "block";
	}
}

/* Two list name: selectedQsts, qstSelections */
/* Expected elements in question:
    ID (int),
    Qst (str),
    Mark (numeric),
    QstImgFileName (nullable str),
    Ans (str),
    and AnsImgFileName (nullable str). */

function UpdateQuestions() {
    document.getElementById("QuestionSelections").innerHTML = "";
    document.getElementById("SelectedQuestions").innerHTML = "";
    console.log("Displaying question selections");
    DisplayQuestions(qstSelections, "QuestionSelections");
    console.log("Done display question selections");
    console.log("Displaying selected questions");
    DisplayQuestions(selectedQsts, "SelectedQuestions");
    console.log("Done display selected questions");
    console.log("Update preview");
    UpdatePreview();
    console.log("Done update preview");
}

function DisplayQuestions(questions, divID) {
    const questionSelectionDiv = document.getElementById(divID);

    questions.forEach((question) => {
        console.log("\tProcessing question ID: " + question.ID.toString());

        // Create a <li> with class "Question", equivalent to <li class="Question">:
        console.log("\t\tCreating li for ID: " + question.ID.toString());
        const questionLi = document.createElement("li");
        questionLi.className = "Question";
        questionLi.id = question.ID;
        questionLi.draggable = true;
        questionLi.addEventListener("dragstart", DragStart);
        questionLi.addEventListener("dragend", DragEnd);
        console.log("\t\tDone create li");

        // Add question:
        console.log("\t\tAdding question: " + question.Qst);
        const questionText = document.createElement("span");
        questionText.textContent = question.Qst;
        questionLi.appendChild(questionText);
        console.log("\t\tDone add question");

        // Add image if any:
        if(question.QstImgFileName){
            try{
                console.log("\t\tAdding image: " + question.QstImgFileName);
                const questionImage = document.createElement("img");
                questionImage.src = question.QstImgFileName;
                questionImage.alt = question.QstImgFileName;
                questionLi.appendChild(questionImage);
                console.log("\t\tDone add image");
            }
            catch(error) {
                console.log("\t\t\tError: " + error);
            }
        }

        // Add the whole <div> to the element with id divID:
        console.log("\t\tAdding li");
        questionSelectionDiv.appendChild(questionLi);
        console.log("\t\tDone add li");

        console.log("\tDone process question");
    });
}

function DragStart(event) {
    event.dataTransfer.setData("text/plain", event.target.id);
    event.target.classList.add("Dragging");
    console.log(event.target.id);
    console.log(event.dataTransfer);
}

function DragEnd(event) {
    event.target.classList.remove("Dragging");
}

function QstSlctDragOver(event) {
    event.preventDefault();
}

function QstSlctDrop(event) {
    console.log("QstSlctDrop: " + event);
    const draggedID = parseInt(event.dataTransfer.getData("text/plain"));
    RemoveQuestions(draggedID);
}

function SlctQstDragOver(event) {
    event.preventDefault();
}

function SlctQstDrop(event) {
    console.log("SlctQstDrop: " + event);
    const draggedID = parseInt(event.dataTransfer.getData("text/plain"));
    AddQuestions(draggedID);
}

function AddQuestions(id) {
    console.log("Moving ID: " + id.toString() + " from question selections to selected questions");
    MoveQuestion(id, qstSelections, selectedQsts);
    console.log("Done move");
}

function RemoveQuestions(id) {
    console.log("Moving ID: " + id.toString() + " from selected questions to question selections");
    MoveQuestion(id, selectedQsts, qstSelections);
    console.log("Done move");
}

function MoveQuestion(id, from, to) {
    console.log("\tOriginal :\n" + from.toString() + "\n" + to.toString());
    const index = from.findIndex(q => q.ID === id);
    if (index !== -1) {
        const selected = from[index];
        from.splice(index, 1);
        to.push(selected);
    }
    console.log("\tAfter :\n" + from.toString() + "\n" + to.toString());
    UpdateQuestions();
}

Initializing();