
//console.log(self);

self.addEventListener('install', (event) => {
    console.log('installing service worker', event);
})

self.addEventListener('activate', (event) => {
    console.log('activating service worker', event);
  
});

self.addEventListener('fetch', (event) => {
    console.log('fetch data', event);
});
//self.addEventListener('fetch', (e) => {
//    console.log(e.request.url);
//    e.respondWith(
//        caches.match(e.request).then((response) => response || fetch(e.request)),
//    );
//});
// Caution! Be sure you understand the caveats before publishing an application with
// offline support. See https://aka.ms/blazor-offline-considerations

