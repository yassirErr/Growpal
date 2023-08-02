
//Layouts
function blockLink() {
    // Use sweetalert to show a custom alert message
    swal({
        title: "This link will come soon",
     
        icon: "warning",
        buttons: {
            confirm: {
                text: "OK",
                value: true,
                visible: true,
                className: "",
                closeModal: true
            }
        }
    });
}