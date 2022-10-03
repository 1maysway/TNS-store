document.addEventListener('DOMContentLoaded', () => {
    
    
        document.querySelectorAll('.zoomable-image img').forEach((image) => {


            image.addEventListener('mousemove', (e) => {
                if ($(window).width() > 992) {
                    const zoomWindow = e.target.parentElement;
                    e.target.style.opacity = '0';
                    const x = e.offsetX;
                    const y = e.offsetY;
                    const width = e.target.width;
                    const height = e.target.height;
                    const xPercent = Math.floor((x / width) * 100);
                    const yPercent = Math.floor((y / height) * 100);

                    const imageUrl = e.target.src;
                    //zoomWindow.style.top = `${window.scrollY + 10}px`;
                    //zoomWindow.style.left = `${width + 10}px`;
                    //zoomWindow.style.display = "flex";
                    zoomWindow.style.backgroundImage = `url(${imageUrl})`;
                    zoomWindow.style.backgroundPosition = `${xPercent}% ${yPercent}%`
                    zoomWindow.style.backgroundSize = `cover`;
                    zoomWindow.style.backgroundSize = `${width * 2.5}px ${height * 2.5}px`;
                }
            });
            image.addEventListener('mouseleave', (e) => {
                //e.target.parentElement.style.display = 'none';
                e.target.parentElement.style.backgroundImage = 'none';
                e.target.style.opacity = '1';
            });
        });
    
});
