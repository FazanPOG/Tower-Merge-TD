(self.webpackChunkgame_score_sdk=self.webpackChunkgame_score_sdk||[]).push([[4552],{5709:(t,e)=>{"use strict";e.Ko=void 0;e.Ko=function(){if("undefined"==typeof navigator)return"sberBox";switch(!0){case function(){if("undefined"==typeof navigator)return!1;var t=navigator.userAgent.toLowerCase();return t.includes("sberportal")||t.includes("stargate")}():return"sberPortal";case function(){if("undefined"==typeof navigator)return!1;var t=navigator.userAgent.toLowerCase();return t.includes("satellite")||t.includes("sberbox top")}():case"undefined"!=typeof navigator&&navigator.userAgent.toLowerCase().includes("sberbox"):case function(){if("undefined"==typeof navigator)return!1;var t=navigator.userAgent.toLowerCase();return t.includes("(tv; tv)")||t.includes("(tv; huawei)")||t.includes("(huawei-tv; huawei)")||t.includes("(huawei-tv; huawei tv)")}():return"sberBox";default:return"mobile"}}},4340:(t,e,i)=>{"use strict";i.d(e,{D:()=>r});const r=()=>Promise.resolve({success:!1,rating:0,error:""})},9:(t,e,i)=>{"use strict";i.d(e,{Z:()=>s});var r=i(4340),n=function(t,e,i,r){return new(i||(i=Promise))((function(n,s){function o(t){try{u(r.next(t))}catch(t){s(t)}}function a(t){try{u(r.throw(t))}catch(t){s(t)}}function u(t){var e;t.done?n(t.value):(e=t.value,e instanceof i?e:new i((function(t){t(e)}))).then(o,a)}u((r=r.apply(t,e||[])).next())}))};class s{constructor(){this.canAddShortcut=!1,this.canRequestReview=!1,this.isAlreadyReviewed=!1}addShortcut(){return n(this,void 0,void 0,(function*(){return!1}))}requestReview(){return(0,r.D)()}requestGameUrl(){return n(this,void 0,void 0,(function*(){}))}}},5572:(t,e,i)=>{"use strict";i.d(e,{VK:()=>r,yl:()=>n});const r={success:!0,payload:{}},n={success:!1,payload:{}}},2712:(t,e,i)=>{"use strict";i.d(e,{aD:()=>l,FU:()=>f,hc:()=>v});const r=(t,e)=>({type:t,getLink:e}),n=r("facebook",(t=>`//www.facebook.com/sharer/sharer.php?u=${t.url}`)),s=r("whatsapp",(t=>`//api.whatsapp.com/send?text=${t.text}%20${t.url}`)),o=r("telegram",(t=>`//t.me/share/url?url=${t.url}&text=${t.text}`)),a=r("vkontakte",(t=>`//vk.com/share.php?url=${t.url}&title=${t.text}&image=${t.image}`)),u=r("twitter",(t=>`//twitter.com/share?text=${t.text}&url=${t.url}`)),c=r("odnoklassniki",(t=>`//connect.ok.ru/offer?url=${t.url}&title=${t.text}&imageUrl=${t.image}`)),h=r("viber",(t=>`viber://forward?text=${t.text}%20${t.url}`)),d=r("moymir",(t=>`//connect.mail.ru/share?url=${t.url}&title=${t.text}&image_url=${t.image}`)),l=[s,o,a,c,h,d],f=[n,u,o,s,h],v=[n,u,o,s,h,a,c,d]},6390:(t,e,i)=>{"use strict";function r(){try{return window.top.location.href||location.href}catch(t){return location.href}}i.d(e,{T:()=>r})},5092:(t,e,i)=>{"use strict";i.d(e,{M:()=>r});const r=(t,e,i,r)=>{var n;const s=(window.innerWidth-i)/2,o=(window.innerHeight-r)/2,a=window.open(t,e,`scrollbars=yes,\n        width=${i},\n        height=${r},\n        top=${o},\n        left=${s}\n        `);return null===(n=a.focus)||void 0===n||n.call(a),a}},9991:(t,e,i)=>{"use strict";function r(t,e=0){return new Promise(((i,r)=>{let n=0;t(window)?i():(n=setInterval((function(){t(window)&&(clearInterval(n),i())}),100),e>0&&setTimeout((()=>{clearInterval(n),r(new Error("Timeout reached"))}),e))}))}i.d(e,{Z:()=>r})},5192:(t,e,i)=>{"use strict";i.r(e),i.d(e,{default:()=>$});var r,n,s,o,a=i(5709),u=i(6390),c=i(4917),h=i(6558),d=i(5092),l=i(8293),f=i(9991),v=function(t,e,i,r){return new(i||(i=Promise))((function(n,s){function o(t){try{u(r.next(t))}catch(t){s(t)}}function a(t){try{u(r.throw(t))}catch(t){s(t)}}function u(t){var e;t.done?n(t.value):(e=t.value,e instanceof i?e:new i((function(t){t(e)}))).then(o,a)}u((r=r.apply(t,e||[])).next())}))},p=function(t,e,i,r,n){if("m"===r)throw new TypeError("Private method is not writable");if("a"===r&&!n)throw new TypeError("Private accessor was defined without a setter");if("function"==typeof e?t!==e||!n:!e.has(t))throw new TypeError("Cannot write private member to an object whose class did not declare it");return"a"===r?n.call(t,i):n?n.value=i:e.set(t,i),i},y=function(t,e,i,r){if("a"===i&&!r)throw new TypeError("Private accessor was defined without a getter");if("function"==typeof e?t!==e||!r:!e.has(t))throw new TypeError("Cannot read private member from an object whose class did not declare it");return"m"===i?r:"a"===i?r.call(t):r?r.value:e.get(t)};class w{constructor(t,e){var i,u;this.config=t,this.gp=e,this.deviceType=(0,a.Ko)(),this.isSupportsNativePayment=!1,this.isOnSberRu=!1,r.set(this,(0,l._)({timeout:15e3})),n.set(this,{userId:"",signature:""}),s.set(this,null),o.set(this,null),this.isSupportsNativePayment=Array.isArray(window.appInitialData)&&window.appInitialData.some((t=>"app_context"===(null==t?void 0:t.type))),window.addEventListener("message",(t=>{"https://sber.ru"===t.origin&&(this.isSupportsNativePayment=!1,this.isOnSberRu=!0)})),null===(u=null===(i=window.top)||void 0===i?void 0:i.postMessage)||void 0===u||u.call(i,JSON.stringify({type:"ready"}),"*")}get appUrl(){return(0,u.T)()}init(){return v(this,void 0,void 0,(function*(){const t=(0,l._)();return this.ready=t.ready,Promise.all([(0,c.Z)({src:"https://cdn-app.sberdevices.ru/shared-static/0.0.0/polyfills/cookie-ls-polyfill.min.js"}),(0,c.Z)({src:"https://unpkg.com/@salutejs/client@1.32.3/umd/assistant.production.min.js",check:t=>"assistant"in t})]).catch(h.kg.error).finally(t.done),t.ready.then((()=>{const{assistant:t,SberDevicesAdSDK:e}=window;this.assistant=t.createAssistant({getState:()=>{}}),this.assistant.on("data",(t=>{if("smart_app_data"!==t.type)return;const e=t.smart_app_data;switch(this.handleOnCheckPurchase(e),this.handleOnInvoiceCreated(e),this.handleOnGsRequestPayload(e),e.type){case"sub":e.gsPayload&&p(this,n,e.gsPayload,"f"),y(this,r,"f").done();break;case"GS_INVOICE_ASK_PAYMENT":this.handleInvoiceAskPayment(e);break;case"error":this.cancelPayment(e.error),h.kg.error(e.error)}}))})),this.ready}))}getPayload(){return v(this,void 0,void 0,(function*(){return yield y(this,r,"f").ready,{userId:y(this,n,"f").userId,signature:y(this,n,"f").signature}}))}getPlayer(){return v(this,void 0,void 0,(function*(){const t=(0,l._)();return t.done({id:0,name:"",photoSmall:"",photoMedium:"",photoLarge:""}),t.ready}))}showRewardedVideo(){return v(this,void 0,void 0,(function*(){return!1}))}showPreloader(){return v(this,void 0,void 0,(function*(){return!1}))}showFullscreen(){return v(this,void 0,void 0,(function*(){return!1}))}showSticky(){return Promise.resolve(!1)}closeSticky(){}refreshSticky(){return this.closeSticky(),this.showSticky()}sendAction(t,e){const i=(0,l._)();return this.assistant.sendAction({type:t,payload:e},i.done,i.abort),i.ready}purchase(t){return this.cancelPayment(),p(this,o,(0,l._)(),"f"),y(this,o,"f").ready.finally((()=>{p(this,o,null,"f"),p(this,s,null,"f")})),this.sendAction("GS_PAYMENTS_PURCHASE",{product:t}).then(this.handleOnInvoiceCreated.bind(this)).catch(this.cancelPayment.bind(this)),y(this,o,"f").ready}handleOnInvoiceCreated(t){if("GS_INVOICE_CREATED"===t.type){const e="https://gs.eponesh.com/sdk/static/pages/payment-result.html";p(this,s,t.payload,"f"),this.sendAction("GS_INVOICE_CREATED_HANDLED",Object.assign(Object.assign({},y(this,s,"f")),{isSupportsNativePayment:this.isSupportsNativePayment,successUrl:`${e}?gsPaymentStatus=success`,failedUrl:`${e}?gsPaymentStatus=failed`}))}"error"===t.type&&this.cancelPayment(t.error)}handleOnCheckPurchase(t){switch(t.type){case"GS_PAYMENT_SUCCESS":y(this,o,"f").done({invoiceId:t.invoiceId});break;case"GS_PAYMENT_WAITING":{let t=window.setTimeout((()=>this.checkPurchase()),1e4);y(this,o,"f").ready.finally((()=>clearTimeout(t)));break}case"GS_PAYMENT_FAILED":case"error":this.cancelPayment(t.error)}}handleOnGsRequestPayload(t){"GS_REQUEST_PAYLOAD_RESULT"===t.type&&p(this,n,t.gsPayload,"f")}checkPurchase(){return v(this,void 0,void 0,(function*(){if(y(this,s,"f"))return this.sendAction("GS_PAYMENTS_CHECK_PURCHASE",y(this,s,"f")).then(this.handleOnCheckPurchase.bind(this)).catch(this.cancelPayment.bind(this))}))}cancelPayment(t="cancelled"){var e;null===(e=y(this,o,"f"))||void 0===e||e.abort(t),p(this,o,null,"f"),p(this,s,null,"f")}handleInvoiceAskPayment({formUrl:t}){return v(this,void 0,void 0,(function*(){let e,i=!1;const r=t=>{try{if("GS_PAYMENT_RESULT_MESSAGE"!==JSON.parse(t.data).type)return;i=!0,this.checkPurchase()}catch(t){}};window.addEventListener("message",r),y(this,o,"f").ready.finally((()=>{p(this,o,null,"f"),p(this,s,null,"f"),e.close(),window.removeEventListener("message",r)})),e=(0,d.M)(t,"",400,600),yield(0,f.Z)((()=>e.closed)),i||this.cancelPayment()}))}}r=new WeakMap,n=new WeakMap,s=new WeakMap,o=new WeakMap;var m=function(t,e,i,r){return new(i||(i=Promise))((function(n,s){function o(t){try{u(r.next(t))}catch(t){s(t)}}function a(t){try{u(r.throw(t))}catch(t){s(t)}}function u(t){var e;t.done?n(t.value):(e=t.value,e instanceof i?e:new i((function(t){t(e)}))).then(o,a)}u((r=r.apply(t,e||[])).next())}))};class g{constructor(t){this.sdk=t,this.hasCredetials=!1,this.userId="",this.isAuthorizedAtPlatform=!0}getPlayerAuthInfo(){return m(this,void 0,void 0,(function*(){yield this.sdk.ready;const t=yield this.sdk.getPayload();return this.userId=t.userId,this.hasCredetials=!!t.userId&&!!t.signature,t}))}getPlayerContext(){return m(this,void 0,void 0,(function*(){return{platformData:yield this.getPlayerAuthInfo(),key:""}}))}loginPlayer(){return m(this,void 0,void 0,(function*(){return this.sdk.getPlayer()}))}logoutPlayer(){return m(this,void 0,void 0,(function*(){return!1}))}getPlayer(){return m(this,void 0,void 0,(function*(){return this.sdk.getPlayer()}))}publishRecord(t){}isPlatformAvatar(){return!1}setCredentials(t){}}var P=function(t,e,i,r){return new(i||(i=Promise))((function(n,s){function o(t){try{u(r.next(t))}catch(t){s(t)}}function a(t){try{u(r.throw(t))}catch(t){s(t)}}function u(t){var e;t.done?n(t.value):(e=t.value,e instanceof i?e:new i((function(t){t(e)}))).then(o,a)}u((r=r.apply(t,e||[])).next())}))};class k{constructor(t){this.sdk=t,this.isStickyAvailable=!1,this.stickyBannerConfig={isOverlapping:!1,height:0},this.isFullscreenAvailable=!0,this.isRewardedAvailable=!0,this.isPreloaderAvailable=!0,this.needToLeaveFullscreenBeforeAds=!1,this.canShowFullscreenBeforeGamePlay=!1}showPreloader(){return P(this,void 0,void 0,(function*(){return yield this.sdk.ready,this.sdk.showPreloader().catch((()=>!1))}))}showFullscreen(){return P(this,void 0,void 0,(function*(){return yield this.sdk.ready,this.sdk.showFullscreen().catch((()=>!1))}))}showRewardedVideo(){return P(this,void 0,void 0,(function*(){return yield this.sdk.ready,this.sdk.showRewardedVideo().catch((()=>!1))}))}showSticky(){return P(this,void 0,void 0,(function*(){return yield this.sdk.ready,this.sdk.showSticky().catch((()=>!1))}))}refreshSticky(){return P(this,void 0,void 0,(function*(){return yield this.sdk.ready,this.sdk.refreshSticky().catch((()=>!1))}))}closeSticky(){return P(this,void 0,void 0,(function*(){return yield this.sdk.ready,this.sdk.closeSticky()}))}}var S=i(2712),A=i(5942),b=i(5572),_=function(t,e,i,r){return new(i||(i=Promise))((function(n,s){function o(t){try{u(r.next(t))}catch(t){s(t)}}function a(t){try{u(r.throw(t))}catch(t){s(t)}}function u(t){var e;t.done?n(t.value):(e=t.value,e instanceof i?e:new i((function(t){t(e)}))).then(o,a)}u((r=r.apply(t,e||[])).next())}))};class E{constructor(t){this.sdk=t,this.hasIntegratedAuth=!1,this.isExternalLinksAllowed=!1,this.isSecretCodeAuthAvailable=!0,this._hasAuthModal=!1,this.isLogoutAvailable=!1,this.type=A.z.SMARTMARKET,this.socialNetworks=S.aD,this.isSupportsImageUploading=!1,this.hasAccountLinkingFeature=!1,this.isSupportsRemoteVariables=!1,this.isSupportsCloudSaves=!1}getSDK(){return this.sdk}getNativeSDK(){return this.sdk}requestPermissions(){return _(this,void 0,void 0,(function*(){return b.VK}))}uploadImage(){return null}getVariables(){return _(this,void 0,void 0,(function*(){return{}}))}}class C{constructor(t){this.sdk=t,this.isSupportsShare=!1,this.isSupportsNativeShare=!1,this.isSupportsNativePosts=!1,this.isSupportsNativeInvite=!1,this.isSupportsNativeCommunityJoin=!1,this.canJoinCommunity=!0,this.isSupportShareParams=!1}get shareParams(){return{}}share(t){return Promise.resolve(!1)}post(t){return Promise.resolve(!1)}invite(t){return Promise.resolve(!1)}getCommunityLink(t){return t}joinCommunity(){return Promise.resolve(!1)}addShareParamsToUrl(t,e){return t}makeShareUrl(t){return""}getShareParam(t){return""}}var I=i(9503),x=function(t,e,i,r){return new(i||(i=Promise))((function(n,s){function o(t){try{u(r.next(t))}catch(t){s(t)}}function a(t){try{u(r.throw(t))}catch(t){s(t)}}function u(t){var e;t.done?n(t.value):(e=t.value,e instanceof i?e:new i((function(t){t(e)}))).then(o,a)}u((r=r.apply(t,e||[])).next())}))};const T={[I.Uo.RU]:"Руб",[I.Uo.EN]:"Rub"};class N{constructor(t){this.sdk=t,this.isSupportsSubscriptions=!1,this.isOneTimeSubscription=!0,this.isServerValidation=!0,this.isNeedAuthorizeBeforePurchase=!0}get isSupportsPayments(){return!this.sdk.isOnSberRu}mapProducts(t,e){return x(this,void 0,void 0,(function*(){return e.map((e=>Object.assign(Object.assign({},e),{currencySymbol:T[t.language]||T[I.Uo.EN],currency:"RUB"})))}))}consumeAllExpired(t,e,i){return x(this,void 0,void 0,(function*(){}))}fetchPurchases(){return x(this,void 0,void 0,(function*(){return{payload:{},purchases:[]}}))}purchase(t){return x(this,void 0,void 0,(function*(){return yield this.sdk.ready,this.sdk.purchase(t)}))}consume(t){return x(this,void 0,void 0,(function*(){return{}}))}subscribe(t,e){return x(this,void 0,void 0,(function*(){return{}}))}unsubscribe(t,e){return x(this,void 0,void 0,(function*(){return{}}))}}var R=i(9);function $(t){return e=this,i=void 0,n=function*(){const e=new w({},t.gp),[,,i]=yield Promise.all([e.init(),t.setupStorage([]),t.fetchConfig()]),r=new g(e);return{adsAdapter:new k(e),appAdapter:new R.Z,playerAdapter:r,platformAdapter:new E(e),socialsAdapter:new C(e),paymentsAdapter:new N(e),projectConfig:i}},new((r=void 0)||(r=Promise))((function(t,s){function o(t){try{u(n.next(t))}catch(t){s(t)}}function a(t){try{u(n.throw(t))}catch(t){s(t)}}function u(e){var i;e.done?t(e.value):(i=e.value,i instanceof r?i:new r((function(t){t(i)}))).then(o,a)}u((n=n.apply(e,i||[])).next())}));var e,i,r,n}}}]);