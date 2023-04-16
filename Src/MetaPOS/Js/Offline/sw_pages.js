const cacheName = 'metapos';

const cacheAssets = [
  '/application/Offline/',
  '/application/Offline/index.html',
  '/application/Offline/customer.html',
  '/application/Offline/errors.html',
  '/application/Offline/slip.html',
  '/application/Offline/stock.html',
  '/application/Offline/favicon.ico',
  '/application/Offline/styles/invoice.css',
  '/application/Offline/styles/bootstrap.min.css',
  '/application/Offline/styles/customer.css',
  '/application/Offline/styles/extras.1.1.0.min.css',
  '/application/Offline/styles/font_all.css',
  '/application/Offline/styles/shards-dashboards.1.1.0.css',
  '/application/Offline/styles/shards-dashboards.1.1.0.min.css',
  '/application/Offline/styles/accents/danger.1.1.0.css',
  '/application/Offline/styles/accents/danger.1.1.0.min.css',
  '/application/Offline/styles/accents/info.1.1.0.css',
  '/application/Offline/styles/accents/info.1.1.0.min.css',
  '/application/Offline/styles/accents/secondary.1.1.0.css',
  '/application/Offline/styles/accents/secondary.1.1.0.min.css',
  '/application/Offline/styles/accents/success.1.1.0.css',
  '/application/Offline/styles/accents/success.1.1.0.min.css',
  '/application/Offline/styles/accents/warning.1.1.0.css',
  '/application/Offline/styles/accents/warning.1.1.0.min.css',
  '/application/Offline/styles/slip.css',
  '/application/Offline/styles/stock.css',
  '/application/Offline/scripts/app/app.js',
  '/application/Offline/scripts/app/customer.js',
  '/application/Offline/scripts/app/index.js',
  '/application/Offline/scripts/app/slip.js',
  '/application/Offline/scripts/app/stock.js',
  '/application/Offline/jquery-flexdatalist-2.2.4/jquery.flexdatalist.css',
  '/application/Offline/jquery-flexdatalist-2.2.4/jquery.flexdatalist.js',
  '/application/Offline/images/avatars/0.jpg',
  '/application/Offline/images/avatars/1.jpg',
  '/application/Offline/images/avatars/2.jpg',
  '/application/Offline/images/avatars/3.jpg',
  '/application/Offline/images/icons/delete.png',
  '/application/Offline/images/icons/shopping-cart.png',
  '/application/Offline/images/icons/shopping-cart.svg',
  '/application/Offline/images/logo.png',
  '/application/Offline/webfonts/fa-brands-400.eot',
  '/application/Offline/webfonts/fa-brands-400.svg',
  '/application/Offline/webfonts/fa-brands-400.ttf',
  '/application/Offline/webfonts/fa-brands-400.woff',
  '/application/Offline/webfonts/fa-brands-400.woff2',
  '/application/Offline/webfonts/fa-regular-400.eot',
  '/application/Offline/webfonts/fa-regular-400.svg',
  '/application/Offline/webfonts/fa-regular-400.ttf',
  '/application/Offline/webfonts/fa-regular-400.woff',
  '/application/Offline/webfonts/fa-regular-400.woff2',
  '/application/Offline/webfonts/fa-solid-900.eot',
  '/application/Offline/webfonts/fa-solid-900.svg',
  '/application/Offline/webfonts/fa-solid-900.ttf',
  '/application/Offline/webfonts/fa-solid-900.woff',
  '/application/Offline/webfonts/fa-solid-900.woff2',
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