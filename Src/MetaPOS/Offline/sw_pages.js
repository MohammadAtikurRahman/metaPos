const cacheName = 'metapos';

const cacheAssets = [
  '/offline',
  '/offline/',
  '/offline/index.html',
  '/offline/customer.html',
  '/offline/errors.html',
  '/offline/slip.html',
  '/offline/stock.html',
  '/offline/favicon.ico',
  '/offline/styles/invoice.css',
  '/offline/styles/bootstrap.min.css',
  '/offline/styles/customer.css',
  '/offline/styles/extras.1.1.0.min.css',
  '/offline/styles/font_all.css',
  '/offline/styles/shards-dashboards.1.1.0.css',
  '/offline/styles/shards-dashboards.1.1.0.min.css',
  '/offline/styles/accents/danger.1.1.0.css',
  '/offline/styles/accents/danger.1.1.0.min.css',
  '/offline/styles/accents/info.1.1.0.css',
  '/offline/styles/accents/info.1.1.0.min.css',
  '/offline/styles/accents/secondary.1.1.0.css',
  '/offline/styles/accents/secondary.1.1.0.min.css',
  '/offline/styles/accents/success.1.1.0.css',
  '/offline/styles/accents/success.1.1.0.min.css',
  '/offline/styles/accents/warning.1.1.0.css',
  '/offline/styles/accents/warning.1.1.0.min.css',
  '/offline/styles/jquery.toast.css',
  '/offline/styles/slip.css',
  '/offline/styles/stock.css',
  '/offline/styles/font-awesome-all.css',
  '/offline/styles/icon.css',
  '/offline/scripts/app/app.js',
  '/offline/scripts/app/customer.js',
  '/offline/scripts/app/index.js',
  '/offline/scripts/app/slip.js',
  '/offline/scripts/app/stock.js',
  '/offline/jquery-flexdatalist-2.2.4/jquery.flexdatalist.css',
  '/offline/jquery-flexdatalist-2.2.4/jquery.flexdatalist.js',
  '/offline/jquery-flexdatalist-2.2.4/jquery.flexdatalist.min.css',
  '/offline/jquery-flexdatalist-2.2.4/jquery.flexdatalist.min.js',
  '/offline/scripts/jquery-3.3.1.min.js',
  '/offline/scripts/popper.min.js',
  '/offline/scripts/bootstrap.min.js',
  '/offline/scripts/shards.min.js',
  '/offline/scripts/jquery.sharrre.min.js',
  '/offline/scripts/extras.1.1.0.min.js',
  '/offline/scripts/jquery.cookie.js',
  '/offline/scripts/lodash.min.js',
  '/offline/scripts/moment.min.js',
  '/Offline/scripts/moment-timezone-with-data.min.js',
  '/offline/scripts/jquery.toast.js',
  '/offline/images/avatars/0.jpg',
  '/offline/images/avatars/1.jpg',
  '/offline/images/avatars/2.jpg',
  '/offline/images/avatars/3.jpg',
  '/offline/images/icons/delete.png',
  '/offline/images/icons/shopping-cart.png',
  '/offline/images/icons/shopping-cart.svg',
  '/offline/images/logo.png',
  '/offline/webfonts/fa-brands-400.eot',
  '/offline/webfonts/fa-brands-400.svg',
  '/offline/webfonts/fa-brands-400.ttf',
  '/offline/webfonts/fa-brands-400.woff',
  '/offline/webfonts/fa-brands-400.woff2',
  '/offline/webfonts/fa-regular-400.eot',
  '/offline/webfonts/fa-regular-400.svg',
  '/offline/webfonts/fa-regular-400.ttf',
  '/offline/webfonts/fa-regular-400.woff',
  '/offline/webfonts/fa-regular-400.woff2',
  '/offline/webfonts/fa-solid-900.eot',
  '/offline/webfonts/fa-solid-900.svg',
  '/offline/webfonts/fa-solid-900.ttf',
  '/offline/webfonts/fa-solid-900.woff',
  '/offline/webfonts/fa-solid-900.woff2',
  '/offline/webfonts/icon-font.woff2',
  '/offline/invoice.html',
  '/offline/invoice.html?type=0',
  '/offline/invoice.html?type=1',
  '/offline/styles/report-print.css',
  '/offline/scripts/app/invoice-print-offline.js?v=0.005'
];

// Call Install Event
self.addEventListener('install', e => {
    console.log('Service Worker: Installed');

e.waitUntil(
  caches
    .open(cacheName)
    .then(cache => {
        console.log('Service Worker: Caching Files');
cache.addAll(cacheAssets);
})
      .then(() => self.skipWaiting())
  );
});

// Call Activate Event
self.addEventListener('activate', e => {
    console.log('Service Worker: Activated');
// Remove unwanted caches
e.waitUntil(
  caches.keys().then(cacheNames => {
      return Promise.all(
        cacheNames.map(cache => {
            if (cache !== cacheName) {
            console.log('Service Worker: Clearing Old Cache');
return caches.delete(cache);
}
})
      );
})
  );
});

// Call Fetch Event
self.addEventListener('fetch', e => {
    console.log('Service Worker: Fetching');
e.respondWith(fetch(e.request).catch(() => caches.match(e.request)));
});