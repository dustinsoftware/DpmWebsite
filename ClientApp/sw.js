importScripts('dist/workbox-shim.js');

const workboxSW = new self.workboxSW();
workboxSW.router.registerRoute('/', workboxSW.strategies.cacheFirst());
workboxSW.router.registerRoute('/counter', workboxSW.strategies.cacheFirst());
workboxSW.router.registerRoute('/fetchdata', workboxSW.strategies.cacheFirst());
workboxSW.router.registerRoute('/dist/(.*)', workboxSW.strategies.cacheFirst());

workboxSW.precache([]);
