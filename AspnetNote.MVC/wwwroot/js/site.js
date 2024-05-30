// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// id로 했다면 $(".editor") class로 했다면 하단

$(".editor").trumbowyg({
    lang: 'ko',
    // 버튼이 명시가 되야되므로 버튼즈 앞에 btnsDef
    btnsDef: {
        imageUpload: {
            dropdown: ['insertImage', 'upload', 'base64'],
            ico:'insertImage'
        }
    },
    btns: [
        ['viewHTML'],
        ['undo', 'redo'], // Only supported in Blink browsers
        ['formatting'],
        ['strong', 'em', 'del'],
        ['superscript', 'subscript'],
        ['link'],
        ['imageUpload'],
        ['justifyLeft', 'justifyCenter', 'justifyRight', 'justifyFull'],
        ['unorderedList', 'orderedList'],
        ['horizontalRule'],
        ['removeformat'],
        ['fullscreen']
    ]
});