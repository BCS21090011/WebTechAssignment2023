var QstPDFdoc; // Global pdf doc for questions.
var AnsPDFdoc; // Global pdf doc for answers.

// Define page size and margins:
var pageWidth = 210;    // A4 width in mm.
var pageHeight = 297;   // A4 height in mm.
var marginTop = 20;
var marginBottom = 20;
var marginLeft = 20;
var marginRight = 20;

// Define values for text, images, and other:
var spaceAfterText = 10;    // The space after each question or answer.
var imageHeight = 50;
var imageWidth = 50;
var pageNumPositionX = 100;
var pageNumPositionY = 289;
var textNumLength = 10;
var textContentLengthPerLine = pageWidth - marginLeft - marginRight - textNumLength;
var textContentHeight = pageHeight - marginTop - marginBottom;

// Define font and font size:
var fontName = 'Helvetica';
var fontSize = 12;

function UpdatePreview() {
    UpdateQstPreview();
    UpdateAnsPreview();
}

function UpdateQstPreview() {
    GenerateQstPDF();
    DisplayQstPreview();
}

function UpdateAnsPreview() {
    GenerateAnsPDF();
    DisplayAnsPreview();
}

function GenerateQstPDF() {
    console.log("Generating Question PDF");
    QstPDFdoc = GeneratePDF("Qst", "QstImgFileName", "Mark");
    console.log("Done generate Question PDF");
}

function GenerateAnsPDF() {
    console.log("Generating Answer PDF");
    AnsPDFdoc = GeneratePDF("Ans", "AnsImgFileName");
    console.log("Done generate Answer PDF");
}

function DisplayQstPreview() {
    console.log("Displaying Question preview");
    DisplayPreview(QstPDFdoc, "Qst-Preview-Container");
    console.log("Done display Question preview");
}

function DisplayAnsPreview() {
    console.log("Displaying Answer preview");
    DisplayPreview(AnsPDFdoc, "Ans-Preview-Container");
    console.log("Done display Question preview");
}

function GeneratePDF(textAttr, imgFileNameAttr, markAttr) {
    var PDFdoc = new window.jspdf.jsPDF();   // Create new PDF document object.
    questions = JSON.parse(JSON.stringify(selectedQsts));   // Deep copy it.

    // Set font:
    PDFdoc.setFont(fontName, "normal");
    PDFdoc.setFontSize(fontSize);

    // Add questions to PDF:
    var pageNum = 1;
    var offSetY = marginTop;
    AddPageNum(PDFdoc, pageNum);

    for (var i = 0; i < questions.length; i++) {
        console.log("\tProcessing " + textAttr + " number: " + i.toString());

        let part = selectedQsts[i];

        text = part[textAttr];

        // If need mark:
        if (markAttr) {
            // If it has mark:
            if (part[markAttr]) {
                text += "\n[Mark: " + part[markAttr].toString() + "]"; // Add in the mark.
            }
        }

        let splittedText = SplitText(PDFdoc, text);
        let textHeight = GetTextHeight(PDFdoc, splittedText);
        let height = textHeight + spaceAfterText;

        // If it has image:
        if (part[imgFileNameAttr]) {
            height += imageHeight;
        }

        // If exceed page height:
        if (ExceedHeight(offSetY, height) === true) {
            PDFdoc.addPage();
            pageNum += 1;
            AddPageNum(PDFdoc, pageNum);
            offSetY = marginTop;    // Reset offset y value.
        }

        AddTextAndImg(PDFdoc, i + 1, splittedText, part[imgFileNameAttr], offSetY, textHeight);
        offSetY += height;

        console.log("\tDone process " + textAttr + " number: " + i.toString());
    }

    return PDFdoc;
}

function AddPageNum(PDFdoc, pageNum) {
    PDFdoc.text("Page " + pageNum.toString(), pageNumPositionX, pageNumPositionY);
}

function AddTextAndImg(PDFdoc, textNum, splittedText, imgFileName, offSetY, textHeight) {
    console.log("\t\tAdding text:\n\t\t\t" + splittedText);
    AddText(PDFdoc, textNum, splittedText, offSetY);
    console.log("\t\tDone add text");

    // Add image if any:
    if (imgFileName) {
        console.log("\t\tAdding image: " + imgFileName);
        AddImg(PDFdoc, imgFileName, offSetY + textHeight);
        console.log("\t\tDone add image");
    }
}

function AddText(PDFdoc, textNum, splittedText, offSetY) {
    PDFdoc.text(textNum.toString() + '.', marginLeft, offSetY);
    PDFdoc.text(splittedText, marginLeft + textNumLength, offSetY);
}

function AddImg(PDFdoc, imgFileName, y) {
    try {
        let img = new Image();
        img.src = imgFileName;
        PDFdoc.addImage(img, (pageWidth - imageWidth) / 2, y, imageWidth, imageHeight);
    }
    catch (error) {
        console.log("\t\t\tAn error occured:\n\t\t\t\t" + error.message);
    }
}

// Split text so that it won't exceed the page width with all the margins:
function SplitText(PDFdoc, text) {
    return PDFdoc.splitTextToSize(text, textContentLengthPerLine);
}

function GetTextHeight(PDFdoc, splittedText) {
    var splittedTextHeight = PDFdoc.getTextDimensions(splittedText).h;
    return splittedTextHeight;
}

// true if exceed page height:
function ExceedHeight(offSetY, height) {
    if (offSetY + height > textContentHeight) {
        return true;
    }
    else {
        return false;
    }
}

function DisplayPreview(PDFdoc, previewContainerTagID) {
    var pdfDataUri = PDFdoc.output("datauristring");
    var previewContainer = document.getElementById(previewContainerTagID);
    previewContainer.innerHTML = '<embed src="' + pdfDataUri + '" type="application/pdf" width="100%" height="100%" zoom="true#page=' + 1 + '">';
}

// UpdatePreview();