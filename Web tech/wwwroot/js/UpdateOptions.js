function UpdateOptions(options, selectID) {
    const select = document.getElementById(selectID);
    select.innerHTML = "";

    options.forEach((option) => {
        const opt = document.createElement("option");
        opt.value = option.Value;
        opt.textContent = option.Text;
        select.appendChild(opt);
    });
}