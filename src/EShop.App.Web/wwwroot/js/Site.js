function customAlert(type, title, message) {
    window.notificationService.notify({
        // title
        title: title,
        // notification message
        text: message,
        // 'success', 'warning', 'error'
        type: type,
        // 'top-right', 'bottom-right', 'top-left', 'bottom-left'
        position: 'bottom-right',
        // auto close
        autoClose: true,
        // 5 seconds
        duration: 3000,
        // shows close button
        showRemoveButton: true
    });
}