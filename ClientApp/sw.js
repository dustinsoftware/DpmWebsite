importScripts('dist/workbox-sw.js');

const workboxSW = new self.WorkboxSW();
workboxSW.router.registerRoute('/', workboxSW.strategies.networkFirst());
workboxSW.router.registerRoute('/fetchdata', workboxSW.strategies.networkFirst());
workboxSW.router.registerRoute('/dist/(.*)', workboxSW.strategies.cacheFirst());

workboxSW.precache([]);
