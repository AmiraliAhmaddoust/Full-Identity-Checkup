////if ('serviceWorker' in navigator) {

////    console.log("support");
////} else {
////    console.log("dont support");
////}
//show that it support uor browser

if ('serviceWorker' in navigator) {
    var a = navigator;
    var b = a.serviceWorker;
   /* *//*console.log(b.register("/ServiceWorker.js")*//*);*/
    var c = b.register("/ServiceWorker.js");
    c.then(regestr => {
        console.log('service worker regesterd', regestr);
    }).catch(error => {
        console.log('error', error);
    })
    //register kardan service worker

    let installPromptEvent;

    window.addEventListener('beforeinstallprompt', (e) => {
        e.preventDefault();
        console.log('before install prompt event');
     //   alert("test");
        installPromptEvent = e;
    });
 
    //document.getElementById('button').addEventListener('click', (e) => {
    //    e.preventDefault();
    //    console.log(installPromptEvent);
    //    if (installPromptEvent) {
    //        installPromptEvent.prompt();

    //        installPromptEvent.userChoice
    //            .then((choiceResult) => {
    //                if (choiceResult.outcome === 'accepted') {
    //                    console.log('User Accepted');
    //                } else {
    //                    console.log('User dismissed');
    //                }

    //                installPromptEvent = null;
    //            })
    //    }
    //});
    
}


