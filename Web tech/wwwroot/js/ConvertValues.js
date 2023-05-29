function ConvertAttrToValue(list, attr, value) {
    for (var i = 0; i < list.length; i++) {
        var item = list[i];
        var attrValue = item[attr];

        if (value.hasOwnProperty(attrValue)) {
            item[attr] = value[attrValue];
        }
    }
}