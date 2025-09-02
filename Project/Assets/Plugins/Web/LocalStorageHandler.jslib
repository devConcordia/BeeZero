mergeInto(LibraryManager.library, {
    SaveToLocalStorage: function (key, value) {
        window.localStorage.setItem(UTF8ToString(key), UTF8ToString(value));
    },
    LoadFromLocalStorage: function (key) {
        var result = window.localStorage.getItem(UTF8ToString(key));
        if (result !== null) {
            return stringToNewUTF8(result);
        }
        return stringToNewUTF8("");
    }
});