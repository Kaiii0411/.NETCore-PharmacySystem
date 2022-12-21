function removeUnicode(str) {
    return str.normalize("NFD").replaceAll('đ', 'd').replaceAll('Đ', 'D').replace(/\p{Diacritic}/gu, "");
}