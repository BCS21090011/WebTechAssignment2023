function AppendQuestionsList(list, id, qst, mark, subject, difficulty, qstImgFileName, ans, ansImgFileName) {
    let question = {
        ID: id,
        Qst: decodeHtmlEntities(qst),
        Mark: mark,
        Subject: subject,
        Difficulty: difficulty,
        QstImgFileName: qstImgFileName,
        Ans: decodeHtmlEntities(ans),
        AnsImgFileName: ansImgFileName
    };

    list.push(question);
}

function decodeHtmlEntities(text) {
    var elem = document.createElement('textarea');
    elem.innerHTML = text;
    return elem.value;
}