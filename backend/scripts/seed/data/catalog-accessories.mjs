// Аксесуари, живлення, фото/відео та розумний дім. Формат — див. catalog-gadgets.mjs.

const p = (t, b, price, a = {}, v = {}) => ({ t, b, price, a, v });

export default {
  powerbanks: {
    noun: "Павербанк",
    brands: ["Anker", "Xiaomi", "Baseus", "Ugreen", "Samsung"],
    schema: "brand:r battery-mah:r power-w connectivity:m color:v",
    products: [
      p("Anker PowerCore 26800", "anker", 2499, { "battery-mah": 26800, "power-w": 18, connectivity: ["usb-a", "usb-c"] }, { color: ["black"] }),
      p("Xiaomi Mi Power Bank 3 10000", "xiaomi", 799, { "battery-mah": 10000, "power-w": 18, connectivity: ["usb-a", "usb-c"] }, { color: ["black", "silver"] }),
      p("Baseus Blade 20000 100W", "baseus", 2999, { "battery-mah": 20000, "power-w": 100, connectivity: ["usb-a", "usb-c"] }, { color: ["black"] }),
      p("Ugreen Nexode 20000 130W", "ugreen", 3299, { "battery-mah": 20000, "power-w": 130, connectivity: ["usb-a", "usb-c"] }, { color: ["gray"] }),
      p("Anker 737 PowerCore 24K", "anker", 4499, { "battery-mah": 24000, "power-w": 140, connectivity: ["usb-a", "usb-c"] }, { color: ["black"] }),
      p("Samsung EB-P3400", "samsung", 1199, { "battery-mah": 10000, "power-w": 25, connectivity: ["usb-c"] }, { color: ["gray", "beige"] }),
    ],
  },

  "chargers-cables": {
    noun: "Зарядний пристрій",
    brands: ["Anker", "Apple", "Baseus", "Ugreen", "Samsung", "Belkin"],
    schema: "brand:r power-w connectivity:m color:v",
    products: [
      p("Anker 65W GaN Charger", "anker", 1599, { "power-w": 65, connectivity: ["usb-c", "usb-a"] }, { color: ["black", "white"] }),
      p("Apple 20W USB-C Charger", "apple", 899, { "power-w": 20, connectivity: ["usb-c"] }, { color: ["white"] }),
      p("Ugreen Nexode 100W 4-Port", "ugreen", 2499, { "power-w": 100, connectivity: ["usb-c", "usb-a"] }, { color: ["gray"] }),
      p("Baseus GaN5 Pro 65W", "baseus", 1299, { "power-w": 65, connectivity: ["usb-c", "usb-a"] }, { color: ["black", "white"] }),
      p("Samsung 25W Super Fast Charger", "samsung", 699, { "power-w": 25, connectivity: ["usb-c"] }, { color: ["black", "white"] }),
      p("Belkin BoostCharge USB-C to Lightning 1м", "belkin", 799, { "power-w": 20, connectivity: ["usb-c", "lightning"] }, { color: ["white"] }),
    ],
  },

  "phone-cases": {
    noun: "Чохол",
    brands: ["Spigen", "Apple", "Samsung", "OtterBox", "Ringke", "UAG"],
    schema: "brand:r compatible-model:r material:r color:v",
    products: [
      p("Spigen Rugged Armor", "spigen", 799, { "compatible-model": "iPhone 15 Pro", material: "tpu" }, { color: ["black"] }),
      p("Apple Silicone Case", "apple", 1999, { "compatible-model": "iPhone 15 Pro", material: "silicone" }, { color: ["black", "blue", "pink"] }),
      p("Samsung Silicone Case Galaxy S24 Ultra", "samsung", 1299, { "compatible-model": "Galaxy S24 Ultra", material: "silicone" }, { color: ["black", "violet"] }),
      p("OtterBox Defender iPhone 15", "otterbox", 1799, { "compatible-model": "iPhone 15", material: "plastic" }, { color: ["black"] }),
      p("Ringke Fusion Galaxy A55", "ringke", 499, { "compatible-model": "Galaxy A55", material: "tpu" }, { color: ["black"] }),
      p("UAG Monarch Pro iPhone 15 Pro Max", "uag", 2799, { "compatible-model": "iPhone 15 Pro Max", material: "leather" }, { color: ["black", "brown"] }),
    ],
  },

  "screen-protectors": {
    noun: "Захисне скло",
    brands: ["ZAGG", "Spigen", "Belkin", "BlueO", "Samsung"],
    schema: "brand:r compatible-model:r material:r",
    products: [
      p("ZAGG Glass Elite", "zagg", 699, { "compatible-model": "iPhone 15", material: "tempered-glass" }),
      p("Spigen Glas.tR Slim", "spigen", 599, { "compatible-model": "Galaxy S24", material: "tempered-glass" }),
      p("Belkin UltraGlass 2 iPhone 15 Pro", "belkin", 1199, { "compatible-model": "iPhone 15 Pro", material: "tempered-glass" }),
      p("BlueO 3D Full Cover iPhone 14", "blueo", 399, { "compatible-model": "iPhone 14", material: "tempered-glass" }),
      p("Spigen Neo Flex Galaxy S24 Ultra", "spigen", 599, { "compatible-model": "Galaxy S24 Ultra", material: "tpu" }),
      p("Samsung Anti-Reflecting Film S24 Ultra", "samsung", 799, { "compatible-model": "Galaxy S24 Ultra", material: "plastic" }),
    ],
  },

  "e-readers": {
    noun: "Електронна книга",
    brands: ["Amazon", "Kobo", "PocketBook", "Onyx Boox"],
    schema: "brand:r screen-size:r waterproof connectivity:m storage-gb:v color:v",
    products: [
      p("Amazon Kindle Paperwhite", "amazon", 6999, { "screen-size": 6.8, waterproof: true, connectivity: ["wifi", "usb-c"] }, { "storage-gb": [[16, 0]], color: ["black", "green"] }),
      p("Kobo Libra 2", "kobo", 8499, { "screen-size": 7, waterproof: true, connectivity: ["wifi", "bluetooth", "usb-c"] }, { "storage-gb": [[32, 0]], color: ["black", "white"] }),
      p("PocketBook 634 Verse Pro", "pocketbook", 6499, { "screen-size": 6, waterproof: true, connectivity: ["wifi", "bluetooth", "usb-c"] }, { "storage-gb": [[16, 0]], color: ["blue", "gray"] }),
      p("Amazon Kindle 2022", "amazon", 4499, { "screen-size": 6, waterproof: false, connectivity: ["wifi", "usb-c"] }, { "storage-gb": [[16, 0]], color: ["black", "blue"] }),
      p("PocketBook InkPad 4", "pocketbook", 9999, { "screen-size": 7.8, waterproof: true, connectivity: ["wifi", "bluetooth", "usb-c"] }, { "storage-gb": [[32, 0]], color: ["black"] }),
      p("Onyx Boox Note Air 3", "onyx-boox", 17999, { "screen-size": 10.3, waterproof: false, connectivity: ["wifi", "bluetooth", "usb-c"] }, { "storage-gb": [[64, 0]], color: ["black"] }),
    ],
  },

  drones: {
    noun: "Дрон",
    brands: ["DJI", "Autel", "Potensic"],
    schema: "brand:r video-resolution:r flight-time-min weight-g",
    products: [
      p("DJI Mini 4 Pro", "dji", 34999, { "video-resolution": "4k", "flight-time-min": 34, "weight-g": 249 }),
      p("DJI Air 3", "dji", 46999, { "video-resolution": "4k", "flight-time-min": 46, "weight-g": 720 }),
      p("DJI Mini 3", "dji", 19999, { "video-resolution": "4k", "flight-time-min": 38, "weight-g": 249 }),
      p("DJI Avata 2", "dji", 28999, { "video-resolution": "4k", "flight-time-min": 23, "weight-g": 377 }),
      p("Autel EVO Lite+", "autel", 42999, { "video-resolution": "5k", "flight-time-min": 40, "weight-g": 835 }),
      p("Potensic Atom SE", "potensic", 8999, { "video-resolution": "2k", "flight-time-min": 31, "weight-g": 249 }),
    ],
  },

  "action-cameras": {
    noun: "Екшн-камера",
    brands: ["GoPro", "DJI", "Insta360", "SJCAM"],
    schema: "brand:r video-resolution:r waterproof weight-g",
    products: [
      p("GoPro HERO12 Black", "gopro", 15999, { "video-resolution": "5k", waterproof: true, "weight-g": 154 }),
      p("DJI Osmo Action 4", "dji", 12999, { "video-resolution": "4k", waterproof: true, "weight-g": 145 }),
      p("Insta360 X4", "insta360", 21999, { "video-resolution": "5k", waterproof: true, "weight-g": 203 }),
      p("GoPro HERO11 Black", "gopro", 12999, { "video-resolution": "5k", waterproof: true, "weight-g": 153 }),
      p("Insta360 Ace Pro", "insta360", 16999, { "video-resolution": "5k", waterproof: true, "weight-g": 179 }),
      p("SJCAM SJ8 Pro", "sjcam", 6999, { "video-resolution": "4k", waterproof: false, "weight-g": 90 }),
    ],
  },

  "dslr-cameras": {
    noun: "Фотоапарат",
    brands: ["Canon", "Nikon", "Sony", "Fujifilm", "Panasonic"],
    schema: "brand:r camera-mp:r video-resolution weight-g",
    products: [
      p("Canon EOS R6 Mark II", "canon", 109999, { "camera-mp": 24, "video-resolution": "4k", "weight-g": 670 }),
      p("Nikon Z6 II", "nikon", 79999, { "camera-mp": 24, "video-resolution": "4k", "weight-g": 705 }),
      p("Sony Alpha 7 IV", "sony", 99999, { "camera-mp": 33, "video-resolution": "4k", "weight-g": 658 }),
      p("Fujifilm X-T5", "fujifilm", 74999, { "camera-mp": 40, "video-resolution": "5k", "weight-g": 557 }),
      p("Canon EOS R50 Kit 18-45mm", "canon", 33999, { "camera-mp": 24, "video-resolution": "4k", "weight-g": 375 }),
      p("Sony ZV-E10", "sony", 27999, { "camera-mp": 24, "video-resolution": "4k", "weight-g": 343 }),
      p("Panasonic Lumix S5 II", "panasonic", 79999, { "camera-mp": 24, "video-resolution": "5k", "weight-g": 740 }),
    ],
  },

  "smarthome-hubs": {
    noun: "Хаб розумного дому",
    brands: ["Google", "Amazon", "Apple", "Aqara", "Xiaomi"],
    schema: "brand:r connectivity:m color:v",
    products: [
      p("Google Nest Hub 2", "google", 3999, { connectivity: ["wifi", "bluetooth", "thread"] }, { color: ["white", "gray"] }),
      p("Amazon Echo Show 10", "amazon", 9999, { connectivity: ["wifi", "bluetooth", "zigbee"] }, { color: ["black", "white"] }),
      p("Apple HomePod mini", "apple", 4499, { connectivity: ["wifi", "bluetooth", "thread"] }, { color: ["white", "blue", "yellow"] }),
      p("Aqara Hub M2", "aqara", 2299, { connectivity: ["wifi", "zigbee", "ethernet"] }, { color: ["black"] }),
      p("Xiaomi Smart Home Hub 2", "xiaomi", 1699, { connectivity: ["wifi", "bluetooth", "zigbee"] }, { color: ["white"] }),
      p("Amazon Echo Dot 5", "amazon", 2299, { connectivity: ["wifi", "bluetooth"] }, { color: ["black", "blue", "white"] }),
    ],
  },

  "smart-lighting": {
    noun: "Розумне освітлення",
    brands: ["Philips", "Yeelight", "Xiaomi", "Nanoleaf", "TP-Link"],
    schema: "brand:r power-w connectivity:m color:v",
    products: [
      p("Philips Hue Starter Kit", "philips", 7999, { "power-w": 9, connectivity: ["zigbee", "bluetooth"] }, { color: ["white"] }),
      p("Yeelight Smart Bulb", "yeelight", 699, { "power-w": 8, connectivity: ["wifi"] }, { color: ["white"] }),
      p("Nanoleaf Shapes Hexagons (9 панелей)", "nanoleaf", 8999, { "power-w": 15, connectivity: ["wifi", "thread"] }, { color: ["white"] }),
      p("Xiaomi Smart LED Bulb Essential", "xiaomi", 399, { "power-w": 9, connectivity: ["wifi"] }, { color: ["white"] }),
      p("TP-Link Tapo L530E", "tp-link", 449, { "power-w": 9, connectivity: ["wifi"] }, { color: ["white"] }),
      p("Philips Hue Play Light Bar (2 шт.)", "philips", 6499, { "power-w": 6, connectivity: ["zigbee", "bluetooth"] }, { color: ["black", "white"] }),
    ],
  },

  "security-cameras": {
    noun: "Камера відеоспостереження",
    brands: ["Arlo", "Ring", "TP-Link", "Ezviz", "Xiaomi", "Hikvision"],
    schema: "brand:r video-resolution:r wireless night-vision waterproof color:v",
    products: [
      p("Arlo Pro 4", "arlo", 8999, { "video-resolution": "2k", wireless: true, "night-vision": true, waterproof: true }, { color: ["white"] }),
      p("Ring Stick Up Cam", "ring", 4499, { "video-resolution": "full-hd", wireless: true, "night-vision": true, waterproof: true }, { color: ["white", "black"] }),
      p("TP-Link Tapo C200", "tp-link", 1099, { "video-resolution": "full-hd", wireless: true, "night-vision": true, waterproof: false }, { color: ["white"] }),
      p("Ezviz C6N", "ezviz", 1299, { "video-resolution": "full-hd", wireless: true, "night-vision": true, waterproof: false }, { color: ["white"] }),
      p("Xiaomi Outdoor Camera AW300", "xiaomi", 1899, { "video-resolution": "2k", wireless: true, "night-vision": true, waterproof: true }, { color: ["white"] }),
      p("Hikvision DS-2CD1043G2-I", "hikvision", 2499, { "video-resolution": "2k", wireless: false, "night-vision": true, waterproof: true }, { color: ["white"] }),
    ],
  },
};
