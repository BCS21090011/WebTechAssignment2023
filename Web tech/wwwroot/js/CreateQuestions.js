window.addEventListener("resize", function (event) {
	calcOuterPreviewWidth();
});

function initializing() {
	updateQuestions();
	calcOuterPreviewWidth();
}

function calcOuterPreviewWidth() {
	const screenHeight = window.innerHeight;
	const outerPreviewDiv = document.getElementById("Outer-preview");
	const outerPreviewWidth = screenHeight / 1.414;
	outerPreviewDiv.style.width = outerPreviewWidth + "px";

	/* This is for testing purpose only */
	console.log(outerPreviewWidth);
}

function displayQuestions(questions, divId, buttonText, buttonClickFunction) {
	const questionSelectionDiv = document.getElementById(divId);

	questions.forEach((question, index) => {
		const questionDiv = document.createElement("div");
		questionDiv.classList.add("question");
		questionDiv.id = question.id; // set the id attribute

		const questionText = document.createElement("span");
		questionText.textContent = question.text; // use the text property
		questionDiv.appendChild(questionText);

		const btn = document.createElement("button");
		btn.textContent = buttonText;
		btn.classList.add("qstAct-button");
		btn.addEventListener("click", () => {
			buttonClickFunction(question.id); // pass the ID to the button click function
		});
		questionDiv.appendChild(btn);

		questionSelectionDiv.appendChild(questionDiv);
	});
}

function updateQuestions() {
	document.getElementById("QuestionSelection").innerHTML = "";
	document.getElementById("SelectedQuestions").innerHTML = "";
	displayQuestions(qstSelection, "QuestionSelection", "Add", addButtonClickFunction);
	displayQuestions(selectedQst, "SelectedQuestions", "Remove", removeButtonClickFunction);
}

/*qstSelection should be obtained from database*/
let qstSelection = [
	{ id: "1", text: "What is your name?" },
	{ id: "2", text: "What is your favorite color?" },
	{ id: "3", text: "What is the meaning of life?" },
	{ id: "4", text: "This is just a very very very very very very very very very very very very very very very very very very very very very very very long question." },
	{ id: "6", text: "Auto generated Q6" },
	{ id: "7", text: "Auto generated Q7" },
	{ id: "8", text: "Auto generated Q8" },
	{ id: "9", text: "Auto generated Q9" },
	{ id: "10", text: "Auto generated Q10" },
	{ id: "11", text: "Auto generated Q11" },
	{ id: "12", text: "Auto generated Q12" },
	{ id: "13", text: "Auto generated Q13" },
	{ id: "14", text: "Auto generated Q14" },
	{ id: "15", text: "Auto generated Q15" },
	{ id: "16", text: "Auto generated Q16" },
	{ id: "17", text: "Auto generated Q17" },
	{ id: "18", text: "Auto generated Q18" },
	{ id: "19", text: "Auto generated Q19" },
];
const addButtonClickFunction = (id) => {
	const selected = qstSelection.find(q => q.id === id);
	qstSelection = qstSelection.filter(q => q.id !== id);
	selectedQst.push(selected);
	updateQuestions();

	/*These console.log is just for testing*/
	console.log("Add button clicked");
	console.log(qstSelection)
	console.log(selectedQst)
}

/*selectedQst should initially empty*/
let selectedQst = [
	{ id: "5", text: "Question sample" }
];
const removeButtonClickFunction = (id) => {
	const selected = selectedQst.find(q => q.id === id);
	selectedQst = selectedQst.filter(q => q.id !== id);
	qstSelection.push(selected);
	updateQuestions();

	/*These console.log is just for testing*/
	console.log("Remove button clicked");
	console.log(qstSelection)
	console.log(selectedQst)
}

initializing();