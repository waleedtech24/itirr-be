
// TOGGLER JS //
$(document).ready(function () {
    $("#loader").hide();
    $(document).on("click", ".toggle-sidebar-btn", function () {
        $("body").toggleClass("toggle-sidebar");
    });
});


document.querySelectorAll(".dropdown-btn").forEach(btn => {
    btn.addEventListener("click", function () {
        this.classList.toggle("active");

        // rotate icon
        const icon = this.querySelector("i");
        icon.classList.toggle("bi-chevron-down");
        icon.classList.toggle("bi-chevron-up");
    });
});


//

// document.addEventListener("DOMContentLoaded", function () {
//     // ========= First Upload (Background) =========
//     const fileInput1 = document.getElementById("upload_image_background");
//     const fieldset1 = document.querySelector(".upload_dropZone");
//     const gallery1 = fieldset1.querySelector(".upload_gallery");

//     const removeBtn1 = document.createElement("button");
//     removeBtn1.innerHTML = "✖";
//     removeBtn1.classList.add("remove-btn");
//     removeBtn1.style.display = "none";
//     fieldset1.appendChild(removeBtn1);

//     fileInput1.addEventListener("change", function () {
//         if (fileInput1.files.length > 0) {
//             const file = fileInput1.files[0];
//             if (file.type.startsWith("image/")) {
//                 const reader = new FileReader();
//                 reader.onload = function (e) {
//                     fieldset1.style.backgroundImage = `url(${e.target.result})`;
//                     fieldset1.style.backgroundSize = "cover";
//                     fieldset1.style.backgroundPosition = "center";
//                     fieldset1.style.backgroundRepeat = "no-repeat";

//                     Array.from(fieldset1.children).forEach(child => {
//                         if (child !== removeBtn1 && !child.classList.contains("upload_gallery")) {
//                             child.style.display = "none";
//                         }
//                     });

//                     removeBtn1.style.display = "block";
//                     gallery1.innerHTML = "";
//                 };
//                 reader.readAsDataURL(file);
//             }
//         }
//     });

//     removeBtn1.addEventListener("click", function () {
//         fieldset1.style.backgroundImage = "";
//         Array.from(fieldset1.children).forEach(child => {
//             if (child !== removeBtn1) {
//                 child.style.display = "";
//             }
//         });
//         removeBtn1.style.display = "none";
//     });


//     // ========= Second Upload (Exterior Images) =========
//     const fileInput2 = document.getElementById("upload_exterior_image");
//     const fieldset2 = fileInput2.closest(".upload_dropZone"); // target its own fieldset
//     const gallery2 = fieldset2.querySelector(".upload_gallery");

//     const removeBtn2 = document.createElement("button");
//     removeBtn2.innerHTML = "✖";
//     removeBtn2.classList.add("remove-btn");
//     removeBtn2.style.display = "none";
//     fieldset2.appendChild(removeBtn2);

//     fileInput2.addEventListener("change", function () {
//         if (fileInput2.files.length > 0) {
//             const file = fileInput2.files[0];
//             if (file.type.startsWith("image/")) {
//                 const reader = new FileReader();
//                 reader.onload = function (e) {
//                     fieldset2.style.backgroundImage = `url(${e.target.result})`;
//                     fieldset2.style.backgroundSize = "cover";
//                     fieldset2.style.backgroundPosition = "center";
//                     fieldset2.style.backgroundRepeat = "no-repeat";

//                     Array.from(fieldset2.children).forEach(child => {
//                         if (child !== removeBtn2 && !child.classList.contains("upload_gallery")) {
//                             child.style.display = "none";
//                         }
//                     });

//                     removeBtn2.style.display = "block";
//                     gallery2.innerHTML = "";
//                 };
//                 reader.readAsDataURL(file);
//             }
//         }
//     });

//     removeBtn2.addEventListener("click", function () {
//         fieldset2.style.backgroundImage = "";
//         Array.from(fieldset2.children).forEach(child => {
//             if (child !== removeBtn2) {
//                 child.style.display = "";
//             }
//         });
//         removeBtn2.style.display = "none";
//     });
// });


document.addEventListener("DOMContentLoaded", function () {

    function setupImageUpload(inputId, rowClass) {
        const input = document.getElementById(inputId);
        const row = document.querySelector(rowClass);
        const uploadCol = row.querySelector(".upload-col");

        input.addEventListener("change", function () {
            Array.from(input.files).forEach(file => {
                if (file.type.startsWith("image/")) {
                    const reader = new FileReader();
                    reader.onload = function (e) {
                        // Create wrapper
                        const col = document.createElement("div");
                        col.className = "col-md-3 col-12";

                        const wrapper = document.createElement("div");
                        wrapper.className = "image-wrapper";

                        const img = document.createElement("img");
                        img.src = e.target.result;

                        const delBtn = document.createElement("button");
                        delBtn.className = "delete-btn";
                        delBtn.innerHTML = "✖";

                        delBtn.addEventListener("click", function () {
                            col.remove();
                            row.appendChild(uploadCol); // move fieldset last
                        });

                        wrapper.appendChild(img);
                        wrapper.appendChild(delBtn);
                        col.appendChild(wrapper);

                        // Insert before upload fieldset
                        row.insertBefore(col, uploadCol);
                    };
                    reader.readAsDataURL(file);
                }
            });

            // reset input (so same file can be re-added)
            input.value = "";
        });
    }

    // ✅ apply separately for both
    setupImageUpload("upload_interior_image", ".interior-row");
    setupImageUpload("upload_exterior_image", ".exterior-row");

});

// booking Clender//

