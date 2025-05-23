// Custom JS code for homepage

// Handle missing images by replacing with a placeholder
document.addEventListener("DOMContentLoaded", function () {
  // Base64 encoded small gray placeholder image
  const placeholderImage =
    "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAASwAAAEsCAMAAABOo35HAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyNpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuNi1jMTQwIDc5LjE2MDQ1MSwgMjAxNy8wNS8wNi0wMTowODoyMSAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENDIDIwMTggKFdpbmRvd3MpIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOjJEOTJCRTYyRkRCMzExRTlCODVCQzlCNkE0OTlCOUYzIiB4bXBNTTpEb2N1bWVudElEPSJ4bXAuZGlkOjJEOTJCRTYzRkRCMzExRTlCODVCQzlCNkE0OTlCOUYzIj4gPHhtcE1NOkRlcml2ZWRGcm9tIHN0UmVmOmluc3RhbmNlSUQ9InhtcC5paWQ6MkQ5MkJFNjBGREIzMTFFOUI4NUJDOUIyQTQ5OUI5RjMiIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6MkQ5MkJFNjFGREIzMTFFOUI4NUJDOUIyQTQ5OUI5RjMiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz7RAkKFAAAABlBMVEX////MzMxmxeRhAAAAH0lEQVR42uzBMQEAAADCIPuntscGAAAAAAAAAAAAAADuDA1HAAH0xoa0AAAAAElFTkSuQmCC";

  // Find all images with onerror attribute referring to no-image.jpg
  const images = document.querySelectorAll("img[onerror]");

  // Replace the onerror attribute with our custom handler
  images.forEach(function (img) {
    img.onerror = function () {
      this.src = placeholderImage;
      this.onerror = null; // Prevent infinite loop

      // Add CSS to maintain aspect ratio and styling
      this.style.backgroundColor = "#f3f4f6";
      this.style.display = "flex";
      this.style.alignItems = "center";
      this.style.justifyContent = "center";

      // Add "No Image" text overlay
      const parent = this.parentNode;
      if (parent && !parent.querySelector(".no-image-text")) {
        const textOverlay = document.createElement("div");
        textOverlay.className = "no-image-text";
        textOverlay.style.position = "absolute";
        textOverlay.style.top = "50%";
        textOverlay.style.left = "50%";
        textOverlay.style.transform = "translate(-50%, -50%)";
        textOverlay.style.color = "#9ca3af";
        textOverlay.style.fontSize = "1rem";
        textOverlay.style.fontWeight = "bold";
        textOverlay.textContent = "No Image";

        // Make sure parent has position relative for absolute positioning
        if (getComputedStyle(parent).position === "static") {
          parent.style.position = "relative";
        }
        parent.appendChild(textOverlay);
      }
    };
  });

  // Initialize Swiper if it exists
  if (typeof Swiper !== "undefined" && document.querySelector("#hero-slider")) {
    new Swiper("#hero-slider", {
      loop: true,
      autoplay: {
        delay: 5000,
        disableOnInteraction: false,
      },
      pagination: {
        el: ".swiper-pagination",
        clickable: true,
      },
      navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
      },
    });
  }
});
