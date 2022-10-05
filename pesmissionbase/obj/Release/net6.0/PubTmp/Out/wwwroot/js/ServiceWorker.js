
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
