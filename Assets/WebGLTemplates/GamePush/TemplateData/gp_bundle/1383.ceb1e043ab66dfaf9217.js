(self.webpackChunkgame_score_sdk=self.webpackChunkgame_score_sdk||[]).push([[1383],{1383:function(e,t){var n,o,r,p;function i(e){return(i="function"==typeof Symbol&&"symbol"==typeof Symbol.iterator?function(e){return typeof e}:function(e){return e&&"function"==typeof Symbol&&e.constructor===Symbol&&e!==Symbol.prototype?"symbol":typeof e})(e)}p=function(e){"use strict";var t=function(){return(t=Object.assign||function(e){for(var t,n=1,o=arguments.length;n<o;n++)for(var r in t=arguments[n])Object.prototype.hasOwnProperty.call(t,r)&&(e[r]=t[r]);return e}).apply(this,arguments)};function n(){for(var e=0,t=0,n=arguments.length;t<n;t++)e+=arguments[t].length;var o=Array(e),r=0;for(t=0;t<n;t++)for(var p=arguments[t],i=0,a=p.length;i<a;i++,r++)o[r]=p[i];return o}var o,r,p,a,s,u="undefined"!=typeof window,d=Boolean(u&&window.AndroidBridge),l=Boolean(u&&window.webkit&&window.webkit.messageHandlers&&window.webkit.messageHandlers.VKWebAppClose),b=Boolean(u&&window.ReactNativeWebView&&"function"==typeof window.ReactNativeWebView.postMessage),c=u&&!d&&!l,f=c&&/(^\?|&)vk_platform=mobile_web(&|$)/.test(location.search),A=c?"message":"VKWebAppEvent",m=n(["VKWebAppInit","VKWebAppGetCommunityAuthToken","VKWebAppAddToCommunity","VKWebAppAddToHomeScreenInfo","VKWebAppClose","VKWebAppCopyText","VKWebAppCreateHash","VKWebAppGetUserInfo","VKWebAppSetLocation","VKWebAppSendToClient","VKWebAppGetClientVersion","VKWebAppGetPhoneNumber","VKWebAppGetEmail","VKWebAppGetGroupInfo","VKWebAppGetGeodata","VKWebAppGetCommunityToken","VKWebAppGetConfig","VKWebAppGetLaunchParams","VKWebAppSetTitle","VKWebAppGetAuthToken","VKWebAppCallAPIMethod","VKWebAppJoinGroup","VKWebAppLeaveGroup","VKWebAppAllowMessagesFromGroup","VKWebAppDenyNotifications","VKWebAppAllowNotifications","VKWebAppOpenPayForm","VKWebAppOpenApp","VKWebAppShare","VKWebAppShowWallPostBox","VKWebAppScroll","VKWebAppShowOrderBox","VKWebAppShowLeaderBoardBox","VKWebAppShowInviteBox","VKWebAppShowRequestBox","VKWebAppAddToFavorites","VKWebAppShowCommunityWidgetPreviewBox","VKWebAppShowStoryBox","VKWebAppStorageGet","VKWebAppStorageGetKeys","VKWebAppStorageSet","VKWebAppFlashGetInfo","VKWebAppSubscribeStoryApp","VKWebAppOpenWallPost","VKWebAppCheckAllowedScopes","VKWebAppCheckNativeAds","VKWebAppShowNativeAds","VKWebAppRetargetingPixel","VKWebAppConversionHit","VKWebAppShowSubscriptionBox","VKWebAppCheckSurvey","VKWebAppShowSurvey","VKWebAppScrollTop","VKWebAppScrollTopStart","VKWebAppScrollTopStop"],c&&!f?["VKWebAppResizeWindow","VKWebAppAddToMenu","VKWebAppShowInstallPushBox","VKWebAppGetFriends"]:["VKWebAppShowImages"]),w=u?window.AndroidBridge:void 0,W=l?window.webkit.messageHandlers:void 0,V=c?parent:void 0;function K(e,t){var n=t||{bubbles:!1,cancelable:!1,detail:void 0},o=document.createEvent("CustomEvent");return o.initCustomEvent(e,!!n.bubbles,!!n.cancelable,n.detail),o}(o=e.EAdsFormats||(e.EAdsFormats={})).REWARD="reward",o.INTERSTITIAL="interstitial",(r=e.EGrantedPermission||(e.EGrantedPermission={})).CAMERA="camera",r.LOCATION="location",r.PHOTO="photo",(p=e.EGetLaunchParamsResponseLanguages||(e.EGetLaunchParamsResponseLanguages={})).RU="ru",p.UK="uk",p.UA="ua",p.EN="en",p.BE="be",p.KZ="kz",p.PT="pt",p.ES="es",(a=e.EGetLaunchParamsResponseGroupRole||(e.EGetLaunchParamsResponseGroupRole={})).ADMIN="admin",a.EDITOR="editor",a.MEMBER="member",a.MODER="moder",a.NONE="none",(s=e.EGetLaunchParamsResponsePlatforms||(e.EGetLaunchParamsResponsePlatforms={})).DESKTOP_WEB="desktop_web",s.MOBILE_WEB="mobile_web",s.MOBILE_ANDROID="mobile_android",s.MOBILE_ANDROID_MESSENGER="mobile_android_messenger",s.MOBILE_IPHONE="mobile_iphone",s.MOBILE_IPHONE_MESSENGER="mobile_iphone_messenger",s.MOBILE_IPAD="mobile_ipad","undefined"==typeof window||window.CustomEvent||(window.CustomEvent=(K.prototype=Event.prototype,K));var v=function(e){var o=void 0,r=[];function p(e){r.push(e)}function a(){return l||d}function s(){return c&&window.parent!==window}function u(){return a()||s()}function f(e){if(l||d)return n(r).map((function(t){return t.call(null,e)}));var t=null==e?void 0:e.data;if(c&&t){if(b&&"string"==typeof t)try{t=JSON.parse(t)}catch(t){}var p=t.type,i=t.data,a=t.frameId;p&&("SetSupportedHandlers"!==p?"VKWebAppSettings"!==p?n(r).map((function(e){return e({detail:{type:p,data:i}})})):o=a:i.supportedHandlers)}}b&&/(android)/i.test(navigator.userAgent)?document.addEventListener(A,f):"undefined"!=typeof window&&"addEventListener"in window&&window.addEventListener(A,f);var K=function(e,n){var o,r,p=(o={current:0,next:function(){return++this.current}},r={},{add:function(e,t){var n=null!=t?t:o.next();return r[n]=e,n},resolve:function(e,t,n){var o=r[e];o&&(n(t)?o.resolve(t):o.reject(t),r[e]=null)}});return n((function(e){if(e.detail&&e.detail.data&&"object"==i(e.detail.data)&&"request_id"in e.detail.data){var t=e.detail.data,n=t.request_id,o=function(e,t){var n={};for(var o in e)Object.prototype.hasOwnProperty.call(e,o)&&t.indexOf(o)<0&&(n[o]=e[o]);if(null!=e&&"function"==typeof Object.getOwnPropertySymbols){var r=0;for(o=Object.getOwnPropertySymbols(e);r<o.length;r++)t.indexOf(o[r])<0&&Object.prototype.propertyIsEnumerable.call(e,o[r])&&(n[o[r]]=e[o[r]])}return n}(t,["request_id"]);n&&p.resolve(n,o,(function(e){return!("error_type"in e)}))}})),function(n,o){return void 0===o&&(o={}),new Promise((function(r,i){var a=p.add({resolve:r,reject:i},o.request_id);e(n,t(t({},o),{request_id:a}))}))}}((function(e,t){w&&w[e]?w[e](JSON.stringify(t)):W&&W[e]&&"function"==typeof W[e].postMessage?W[e].postMessage(t):b?window.ReactNativeWebView.postMessage(JSON.stringify({handler:e,params:t})):V&&"function"==typeof V.postMessage&&V.postMessage({handler:e,params:t,type:"vk-connect",webFrameId:o,connectVersion:"2.7.0"},"*")}),p);return{send:K,sendPromise:K,subscribe:p,unsubscribe:function(e){var t=r.indexOf(e);-1<t&&r.splice(t,1)},supports:function(e){return d?!(!w||"function"!=typeof w[e]):l?!(!W||!W[e]||"function"!=typeof W[e].postMessage):c&&-1<m.indexOf(e)},isWebView:a,isIframe:s,isEmbedded:u,isStandalone:function(){return!u()}}}();e.applyMiddleware=function e(){for(var n=[],o=0;o<arguments.length;o++)n[o]=arguments[o];return n.includes(void 0)||n.includes(null)?e.apply(void 0,n.filter((function(e){return"function"==typeof e}))):function(e){if(0===n.length)return e;var o,r={subscribe:e.subscribe,send:function(){for(var t=[],n=0;n<arguments.length;n++)t[n]=arguments[n];return e.send.apply(e,t)}};return o=n.filter((function(e){return"function"==typeof e})).map((function(e){return e(r)})).reduce((function(e,t){return function(n){return e(t(n))}}))(e.send),t(t({},e),{send:o})}},e.default=v,Object.defineProperty(e,"__esModule",{value:!0})},"object"==i(t)?p(t):(o=[t],void 0===(r="function"==typeof(n=p)?n.apply(t,o):n)||(e.exports=r))}}]);