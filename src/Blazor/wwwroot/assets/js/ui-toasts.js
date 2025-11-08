/**
 * UI Toasts
 */

'use strict';

(function () {
    const toastPlacementExample = document.querySelector('.toast-placement-ex'),
        toastPlacementBtn = document.querySelector('#showToastPlacement');
    let selectedType, selectedPlacement, toastPlacement;

    function toastDispose(toast) {
        if (toast && toast._element !== null) {
            if (toastPlacementExample) {
                toastPlacementExample.classList.remove(selectedType);
                DOMTokenList.prototype.remove.apply(toastPlacementExample.classList, selectedPlacement);
            }
            toast.dispose();
        }
    }

    // Main show-toast logic
    function showToast() {
        if (toastPlacement) {
            toastDispose(toastPlacement);
        }

        selectedType = 'bg-primary';
        selectedPlacement = ['bottom-0', 'end-0'];

        toastPlacementExample.classList.add(selectedType);
        DOMTokenList.prototype.add.apply(toastPlacementExample.classList, selectedPlacement);

        toastPlacement = new bootstrap.Toast(toastPlacementExample);
        toastPlacement.show();
    }

    // Button inside the page (optional)
    if (toastPlacementBtn) {
        toastPlacementBtn.onclick = showToast;
    }

    // ✅ Expose the function globally
    window.popToast = showToast;
})();
