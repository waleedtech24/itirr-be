/* ============================================
   ITIRR Admin - Component Scripts
   Reusable UI components (modals, dropdowns, etc.)
   ============================================ */

// Modal Manager
const ModalManager = {
    currentModal: null,

    open: function (modalId) {
        const modal = document.getElementById(modalId);
        const backdrop = document.querySelector('.modal-backdrop') || this.createBackdrop();

        if (modal) {
            this.currentModal = modal;
            backdrop.classList.add('show');
            modal.classList.add('show');
            document.body.style.overflow = 'hidden';
        }
    },

    close: function (modalId = null) {
        const modal = modalId ? document.getElementById(modalId) : this.currentModal;
        const backdrop = document.querySelector('.modal-backdrop');

        if (modal) {
            modal.classList.remove('show');
            backdrop?.classList.remove('show');
            document.body.style.overflow = '';
            this.currentModal = null;
        }
    },

    createBackdrop: function () {
        const backdrop = document.createElement('div');
        backdrop.className = 'modal-backdrop';
        backdrop.addEventListener('click', () => this.close());
        document.body.appendChild(backdrop);
        return backdrop;
    },

    init: function () {
        // Close modal buttons
        document.querySelectorAll('[data-modal-close]').forEach(btn => {
            btn.addEventListener('click', () => {
                const modalId = btn.getAttribute('data-modal-close');
                this.close(modalId);
            });
        });

        // Open modal buttons
        document.querySelectorAll('[data-modal-open]').forEach(btn => {
            btn.addEventListener('click', () => {
                const modalId = btn.getAttribute('data-modal-open');
                this.open(modalId);
            });
        });

        // ESC key to close
        document.addEventListener('keydown', (e) => {
            if (e.key === 'Escape' && this.currentModal) {
                this.close();
            }
        });
    }
};

// Dropdown Manager
const DropdownManager = {
    init: function () {
        document.querySelectorAll('.dropdown-toggle').forEach(toggle => {
            toggle.addEventListener('click', function (e) {
                e.stopPropagation();
                const dropdown = this.closest('.dropdown');

                // Close other dropdowns
                document.querySelectorAll('.dropdown.show').forEach(d => {
                    if (d !== dropdown) d.classList.remove('show');
                });

                dropdown.classList.toggle('show');
            });
        });

        // Close on outside click
        document.addEventListener('click', () => {
            document.querySelectorAll('.dropdown.show').forEach(d => {
                d.classList.remove('show');
            });
        });
    }
};

// Tab Manager
const TabManager = {
    init: function () {
        document.querySelectorAll('[data-tab]').forEach(tab => {
            tab.addEventListener('click', function (e) {
                e.preventDefault();
                const targetId = this.getAttribute('data-tab');
                const tabGroup = this.closest('[data-tab-group]');

                if (tabGroup) {
                    // Remove active from all tabs in group
                    tabGroup.querySelectorAll('[data-tab]').forEach(t => {
                        t.classList.remove('active');
                    });

                    // Hide all tab contents
                    document.querySelectorAll('[data-tab-content]').forEach(content => {
                        content.classList.remove('active');
                    });

                    // Show selected tab
                    this.classList.add('active');
                    const targetContent = document.getElementById(targetId);
                    if (targetContent) {
                        targetContent.classList.add('active');
                    }
                }
            });
        });
    }
};

// DataTable Helper
const DataTableHelper = {
    init: function (tableId, options = {}) {
        const table = document.getElementById(tableId);
        if (!table) return;

        const defaultOptions = {
            searchable: true,
            sortable: true,
            pagination: true,
            pageSize: 10
        };

        const config = { ...defaultOptions, ...options };

        if (config.searchable) {
            this.initSearch(table);
        }

        if (config.sortable) {
            this.initSort(table);
        }

        if (config.pagination) {
            this.initPagination(table, config.pageSize);
        }
    },

    initSearch: function (table) {
        const searchInput = document.querySelector(`[data-table-search="${table.id}"]`);
        if (!searchInput) return;

        searchInput.addEventListener('input', ITIRRAdmin.debounce(function () {
            const searchTerm = this.value.toLowerCase();
            const rows = table.querySelectorAll('tbody tr');

            rows.forEach(row => {
                const text = row.textContent.toLowerCase();
                row.style.display = text.includes(searchTerm) ? '' : 'none';
            });
        }, 300));
    },

    initSort: function (table) {
        const headers = table.querySelectorAll('th[data-sortable]');

        headers.forEach(header => {
            header.style.cursor = 'pointer';
            header.addEventListener('click', function () {
                const columnIndex = Array.from(this.parentElement.children).indexOf(this);
                const tbody = table.querySelector('tbody');
                const rows = Array.from(tbody.querySelectorAll('tr'));
                const currentOrder = this.getAttribute('data-order') || 'asc';
                const newOrder = currentOrder === 'asc' ? 'desc' : 'asc';

                rows.sort((a, b) => {
                    const aValue = a.children[columnIndex].textContent.trim();
                    const bValue = b.children[columnIndex].textContent.trim();

                    if (newOrder === 'asc') {
                        return aValue.localeCompare(bValue, undefined, { numeric: true });
                    } else {
                        return bValue.localeCompare(aValue, undefined, { numeric: true });
                    }
                });

                rows.forEach(row => tbody.appendChild(row));

                // Update sort indicators
                headers.forEach(h => h.removeAttribute('data-order'));
                this.setAttribute('data-order', newOrder);
            });
        });
    },

    initPagination: function (table, pageSize) {
        // Pagination will be implemented if needed
        console.log('Pagination initialized for', table.id);
    }
};

// File Upload Helper
const FileUploadHelper = {
    init: function (inputId, previewId) {
        const input = document.getElementById(inputId);
        const preview = document.getElementById(previewId);

        if (!input) return;

        input.addEventListener('change', function () {
            const file = this.files[0];
            if (!file) return;

            // Validate file type
            const validTypes = ['image/jpeg', 'image/png', 'image/gif', 'image/webp'];
            if (!validTypes.includes(file.type)) {
                ITIRRAdmin.showError('Please select a valid image file (JPEG, PNG, GIF, or WebP)');
                this.value = '';
                return;
            }

            // Validate file size (max 5MB)
            if (file.size > 5 * 1024 * 1024) {
                ITIRRAdmin.showError('File size must be less than 5MB');
                this.value = '';
                return;
            }

            // Show preview
            if (preview) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    preview.src = e.target.result;
                    preview.style.display = 'block';
                };
                reader.readAsDataURL(file);
            }
        });
    },

    uploadFile: function (file, uploadUrl, progressCallback, successCallback, errorCallback) {
        const formData = new FormData();
        formData.append('file', file);

        const xhr = new XMLHttpRequest();

        // Progress
        xhr.upload.addEventListener('progress', function (e) {
            if (e.lengthComputable && progressCallback) {
                const percentComplete = (e.loaded / e.total) * 100;
                progressCallback(percentComplete);
            }
        });

        // Success
        xhr.addEventListener('load', function () {
            if (xhr.status === 200) {
                const response = JSON.parse(xhr.responseText);
                if (successCallback) successCallback(response);
            } else {
                if (errorCallback) errorCallback('Upload failed');
            }
        });

        // Error
        xhr.addEventListener('error', function () {
            if (errorCallback) errorCallback('Upload failed');
        });

        xhr.open('POST', uploadUrl);
        xhr.send(formData);
    }
};

// Initialize all components
document.addEventListener('DOMContentLoaded', function () {
    ModalManager.init();
    DropdownManager.init();
    TabManager.init();
});

// Make available globally
window.ModalManager = ModalManager;
window.DropdownManager = DropdownManager;
window.TabManager = TabManager;
window.DataTableHelper = DataTableHelper;
window.FileUploadHelper = FileUploadHelper;