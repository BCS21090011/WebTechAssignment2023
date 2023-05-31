function FilterQsts(target, object) {
    // Create a Set of IDs from the object array for faster lookup
    const objectIds = new Set(object.map(item => item.ID));

    // Filter the target array based on the IDs
    const filteredTarget = target.filter(item => !objectIds.has(item.ID));

    // Return the filtered target array
    return filteredTarget;
}

function FilterQstsByAttr(target, attr, value) {
    // Filter the target array based on the attribute and value
    const filteredTarget = target.filter(item => item[attr] === value);

    // Return the filtered target array
    return filteredTarget;
}
