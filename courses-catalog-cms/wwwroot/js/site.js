document.addEventListener("DOMContentLoaded", function () {

    const imageInput = document.getElementById('imageFile');

    if (imageInput) {
        imageInput.addEventListener('change', function (event) {
            const file = event.target.files[0];

            if (file) {
                if (file.type.match('image.*')) {
                    const reader = new FileReader();

                    reader.onload = function (e) {
                        // 1. Tworzymy ciemne tło (overlay) na cały ekran
                        const overlay = document.createElement('div');
                        overlay.style.position = 'fixed';
                        overlay.style.top = '0';
                        overlay.style.left = '0';
                        overlay.style.width = '100vw';
                        overlay.style.height = '100vh';
                        overlay.style.backgroundColor = 'rgba(0, 0, 0, 0.7)';
                        overlay.style.display = 'flex';
                        overlay.style.alignItems = 'center';
                        overlay.style.justifyContent = 'center';
                        overlay.style.zIndex = '9999'; // Żeby było nad wszystkim innym
                        overlay.id = 'imagePreviewOverlay';

                        // 2. Tworzymy biały kafelek na środku
                        const card = document.createElement('div');
                        card.style.backgroundColor = '#fff';
                        card.style.padding = '25px';
                        card.style.borderRadius = '10px';
                        card.style.textAlign = 'center';
                        card.style.boxShadow = '0 4px 20px rgba(0,0,0,0.3)';
                        card.style.maxWidth = '90%';
                        card.style.width = '500px';

                        // 3. Nagłówek kafelka
                        const title = document.createElement('h4');
                        title.innerText = 'Podgląd wybranego zdjęcia';
                        title.style.marginBottom = '20px';

                        // 4. Sam obrazek
                        const img = document.createElement('img');
                        img.src = e.target.result;
                        img.style.maxWidth = '100%';
                        img.style.maxHeight = '50vh';
                        img.style.borderRadius = '5px';
                        img.style.marginBottom = '20px';
                        img.style.objectFit = 'contain';

                        // 5. Kontener na przyciski
                        const btnContainer = document.createElement('div');
                        btnContainer.style.display = 'flex';
                        btnContainer.style.justifyContent = 'space-between';
                        btnContainer.style.gap = '15px';

                        // 6. Przycisk Akceptuj (zielony z Bootstrapa)
                        const acceptBtn = document.createElement('button');
                        acceptBtn.innerText = 'OK, zostaw to zdjęcie';
                        acceptBtn.className = 'btn btn-success flex-grow-1';
                        acceptBtn.onclick = function (e) {
                            e.preventDefault(); // Zapobiega wysłaniu formularza
                            document.body.removeChild(overlay); // Zamyka popup
                        };

                        // 7. Przycisk Anuluj (czerwony z Bootstrapa)
                        const cancelBtn = document.createElement('button');
                        cancelBtn.innerText = 'Odrzuć';
                        cancelBtn.className = 'btn btn-outline-danger flex-grow-1';
                        cancelBtn.onclick = function (e) {
                            e.preventDefault();
                            imageInput.value = ''; // Czyści input!
                            document.body.removeChild(overlay); // Zamyka popup
                        };

                        // 8. Składamy to wszystko w całość jak klocki Lego
                        btnContainer.appendChild(cancelBtn);
                        btnContainer.appendChild(acceptBtn);

                        card.appendChild(title);
                        card.appendChild(img);
                        card.appendChild(btnContainer);

                        overlay.appendChild(card);
                        document.body.appendChild(overlay);
                    }

                    reader.readAsDataURL(file);
                } else {
                    alert("Wybrany plik nie jest zdjęciem!");
                    imageInput.value = '';
                }
            }
        });
    }
});