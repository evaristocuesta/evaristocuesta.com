//
// Scripts
// 

window.addEventListener('DOMContentLoaded', event => {

    // Activate Bootstrap scrollspy on the main nav element
    const sideNav = document.body.querySelector('#sideNav');
    if (sideNav) {
        new bootstrap.ScrollSpy(document.body, {
            target: '#sideNav',
            offset: 74,
        });
    };

    // Collapse responsive navbar when toggler is visible
    const navbarToggler = document.body.querySelector('.navbar-toggler');
    const responsiveNavItems = [].slice.call(
        document.querySelectorAll('#navbarResponsive .nav-link')
    );
    responsiveNavItems.map(function (responsiveNavItem) {
        responsiveNavItem.addEventListener('click', () => {
            if (window.getComputedStyle(navbarToggler).display !== 'none') {
                navbarToggler.click();
            }
        });
    });

    // Close navbar when clicking outside of it (accessibility improvement)
    const navbarCollapse = document.body.querySelector('#navbarResponsive');
    if (navbarToggler && navbarCollapse) {
        document.addEventListener('click', (event) => {
            // Check if navbar is expanded and toggler is visible (mobile view)
            const isNavbarExpanded = navbarCollapse.classList.contains('show');
            const isTogglerVisible = window.getComputedStyle(navbarToggler).display !== 'none';

            if (isNavbarExpanded && isTogglerVisible) {
                // Check if click is outside the navbar
                const isClickInsideNav = sideNav.contains(event.target);

                if (!isClickInsideNav) {
                    navbarToggler.click();
                }
            }
        });
    }

    contactInfo();
});

function contactInfo() {
    const emailAdress = atob('Y29udGFjdEBldmFyaXN0b2N1ZXN0YS5jb20=');
    const emails = document.querySelectorAll('a.email-contact-info');
    emails.forEach(email => {
        email.children[1].innerHTML = emailAdress;
        email.setAttribute('href', 'mailto:' + emailAdress);
    });

    const phoneNumber = atob('KzM0IDYzNyA1MyA0NSA2MQ==');
    const phones = document.querySelectorAll('a.phone-contact-info');
    phones.forEach(phone => {
        phone.children[1].innerHTML = phoneNumber;
        phone.setAttribute('href', 'tel:' + phoneNumber);
    });
}
