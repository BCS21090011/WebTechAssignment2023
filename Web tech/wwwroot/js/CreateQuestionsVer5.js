const questionSelectionsUl = document.getElementById("QuestionSelections");
const selectedQuestionsUl = document.getElementById("SelectedQuestions");

function Initializing() {
    selectedQuestionsUl.addEventListener("dragover", SelectedQuestionsDragOverFunc);
    UpdateQuestions();
}

function SwitchPreview() {
    let qstContainer = document.getElementById("Qst-Preview-Container");
    let ansContainer = document.getElementById("Ans-Preview-Container");

    if (qstContainer.style.display === "none") {
        qstContainer.style.display = "block";
        ansContainer.style.display = "none";
    } else {
        qstContainer.style.display = "none";
        ansContainer.style.display = "block";
    }
}

/*
Two list name: selectedQsts, qstSelections
Expected elements in question:
    ID (int),
    Qst (str),
    Mark (numeric),
    QstImgFileName (nullable str),
    Ans (str),
    and AnsImgFileName (nullable str).
*/

function UpdateQuestions() {
    document.getElementById("QuestionSelections").innerHTML = "";
    document.getElementById("SelectedQuestions").innerHTML = "";
    console.log("Displaying question selections");
    DisplayQuestions(qstSelections, "QuestionSelections", "Add", AddButtonClickFunction, false);
    console.log("Done display question selections");
    console.log("Displaying selected questions");
    DisplayQuestions(selectedQsts, "SelectedQuestions", "Remove", RemoveButtonClickFunction, true);
    console.log("Done display selected questions");
    console.log("Update preview");
    UpdatePreview();
    console.log("Done update preview");
    console.log("Adding ExtendHideButton to QuestionSelections");
    UlAddExtendHideButton("QuestionSelections");
    console.log("Done add ExtendHideButton to QuestionSelections");
    console.log("Adding ExtendHideButton to SelectedQuestions");
    UlAddExtendHideButton("SelectedQuestions");
    console.log("Done add ExtendHideButton to SelectedQuestions");
}

function DisplayQuestions(questions, ulID, buttonText, buttonClickFunction, draggable) {
    const questionSelectionUl = document.getElementById(ulID);

    questions.forEach((question) => {
        console.log("\tProcessing question ID: " + question.ID.toString());

        // Create a <li> with class "Question", equivalent to <li class="Question">:
        console.log("\t\tCreating li for ID: " + question.ID.toString());
        const questionLi = document.createElement("li");
        questionLi.className = "Question";
        questionLi.id = question.ID;
        questionLi.draggable = draggable;
        if (draggable === true) {
            questionLi.style.cursor = "pointer";
        }
        questionLi.addEventListener("dragstart", () => {
            questionLi.classList.add("Dragging");
        });
        questionLi.addEventListener("dragend", () => {
            questionLi.classList.remove("Dragging");
            UpdateSelectedQstsLst();
            UpdatePreview();
        });
        console.log("\t\tDone create li");

        // Add question:
        console.log("\t\tAdding question: " + question.Qst);
        const questionText = document.createElement("span");
        questionText.textContent = question.Qst;
        questionLi.appendChild(questionText);
        console.log("\t\tDone add question");

        // Add image if any:
        if (question.QstImgFileName) {
            try {
                console.log("\t\tAdding image: " + question.QstImgFileName);
                const questionImage = document.createElement("img");
                questionImage.src = question.QstImgFileName;
                questionImage.alt = question.QstImgFileName;
                questionLi.appendChild(questionImage);
                console.log("\t\tDone add image");
            }
            catch (error) {
                console.log("\t\t\tError: " + error);
            }
        }

        // Add button with function:
        console.log("\t\tAdding \"" + buttonText + "\" button with function: " + buttonClickFunction);
        const btn = document.createElement("button");
        btn.textContent = buttonText;
        btn.className = "QstAct-Button";
        btn.addEventListener("click", () => {
            buttonClickFunction(question.ID);   // Pass the ID to the button click function.
        });
        questionLi.appendChild(btn);
        console.log("\t\tDone add button with function");

        // Add the whole <li> to the element with id ulID:
        console.log("\t\tAdding li");
        questionSelectionUl.appendChild(questionLi);
        console.log("\t\tDone add li");

        console.log("\tDone process question");
    });
}

function UlAddExtendHideButton(ulID) {
    const questionSelectionUl = document.getElementById(ulID);
    const questions = [...questionSelectionUl.querySelectorAll(".Question")];

    console.log("Scanning through questions");
    questions.forEach((question) => {
        const text = question.querySelector("span");
        const isOverFlowed = IsOverFlowed(text);
        const hasNewLine = text.textContent.indexOf('\n') !== -1;
        const img = question.querySelector("img");

        console.log("\tQuestion: " + question.id);
        console.log("\t\tIsOverFlowed: " + isOverFlowed);
        console.log("\t\tHasNewLine: " + hasNewLine);
        console.log("\t\timg: " + img);
        if (isOverFlowed || img || hasNewLine) {
            console.log("\t\tAdding extend button");
            const extendButton = document.createElement("button");
            extendButton.textContent = "Extend";
            extendButton.classList.add("ExtendHideButton");
            extendButton.addEventListener("click", () => {
                ExtendHideButtonFunc(ulID, question.id);
            });
            question.insertBefore(extendButton, question.querySelector(".QstAct-Button"));
            console.log("\t\tDone add extend button");
        }
    });
}

function AddButtonClickFunction(id) {
    console.log("Moving ID: " + id.toString() + " from question selections to selected questions");
    MoveQuestion(id, qstSelections, selectedQsts, true);
    console.log("Done move");
}

function RemoveButtonClickFunction(id) {
    console.log("Moving ID: " + id.toString() + " from selected questions to question selections");
    MoveQuestion(id, selectedQsts, qstSelections, false);
    console.log("Done move");
}

function MoveQuestion(id, from, to, draggable) {
    console.log("\tOriginal :\n" + from.toString() + "\n" + to.toString());
    const index = from.findIndex(q => q.ID === id);
    if (index !== -1) {
        const selected = from[index];
        selected.draggable = draggable;
        from.splice(index, 1);
        to.push(selected);
    }
    console.log("\tAfter :\n" + from.toString() + "\n" + to.toString());
    UpdateQuestions();
}

function IsOverFlowed(element) {
    if (element.scrollWidth > element.offsetWidth) {
        return true;
    }
    else {
        return false;
    }
}

function ExtendHideButtonFunc(outterID, id) {
    const outter = document.getElementById(outterID);
    const qst = outter.querySelector('#' + CSS.escape(id));

    if (qst.classList.contains("Question")) {    // Just in case.
        const btn = qst.querySelector(".ExtendHideButton");
        if (qst.classList.contains("Extended")) {
            // Shrink it:
            console.log("Shrink Question: " + id.toString() + " in id " + outterID.toString());
            qst.classList.remove("Extended");
            btn.textContent = "Extend";
        }
        else {
            // Extend it:
            console.log("Extend Question id " + id.toString() + " in id " + outterID.toString());
            qst.classList.add("Extended");
            btn.textContent = "Shrink";
        }
    }
}

function SelectedQuestionsDragOverFunc(event) {
    event.preventDefault();

    // Get the currently dragging .Question:
    const draggingItem = selectedQuestionsUl.querySelector(".Dragging");
    console.log("Dragging item: " + draggingItem);

    // Get all the .Question in selectedQuestions that are not currently dragging:
    const siblings = [...selectedQuestionsUl.querySelectorAll(".Question:not(.Dragging)")];
    console.log("Siblings: " + siblings);

    let nextSibling = siblings.find(sibling => {
        console.log("\tEvent.clientY: " + event.clientY);
        console.log("\tSibling: " + ((sibling.offsetTop + sibling.offsetHeight / 2) - selectedQuestionsUl.scrollTop));
        console.log("\tScroll range: " + selectedQuestionsUl.scrollTop);
        return event.clientY <= (sibling.offsetTop + sibling.offsetHeight / 2) - selectedQuestionsUl.scrollTop;
    });
    console.log("Next sibling: " + nextSibling);

    selectedQuestionsUl.insertBefore(draggingItem, nextSibling);
}

function MoveLstVal(lst, targetIndex, destinationIndex) {
    const tmp = lst.splice(targetIndex, 1)[0];
    lst.splice(destinationIndex - 1, 0, tmp);
}

function UpdateSelectedQstsLst() {
    let tmpLst = [];
    const questions = [...selectedQuestionsUl.querySelectorAll(".Question")];

    console.log("Updating selectedQsts list");
    questions.forEach((question) => {
        const index = selectedQsts.findIndex((qst) => {
            return question.id == qst.ID;
        });

        tmpLst.push(selectedQsts[index]);
    });

    selectedQsts = tmpLst;
    console.log(selectedQsts);
    console.log("Done update selectedQsts list");
}

Initializing();