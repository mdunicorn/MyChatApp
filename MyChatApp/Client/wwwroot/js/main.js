function focusElement(element) {
    element.focus();
}

function createEmojiPicker(button, editable, dotNetObjRef, onUpdatedMehodName) {
    const picker = new EmojiButton();
    picker.on('emoji', emoji => {
        insertAtCursor(editable, emoji);
        dotNetObjRef.invokeMethodAsync(onUpdatedMehodName, editable.value);
        setTimeout(() => editable.focus(), 0);
    });

    button.addEventListener('click', () => {
        picker.togglePicker(button);
    });
}

function insertAtCursor(element, text) {
    //IE support
    if (document.selection) {
        element.focus();
        sel = document.selection.createRange();
        sel.text = text;
    }
    //MOZILLA and others
    else if (element.selectionStart || element.selectionStart == '0') {
        var startPos = element.selectionStart;
        var endPos = element.selectionEnd;
        element.value = element.value.substring(0, startPos)
            + text
            + element.value.substring(endPos, element.value.length);
        element.selectionStart = element.selectionEnd = startPos + text.length;
    } else {
        element.value += text;
    }
}
