// Гаджети, ТВ, консолі, ПК і монітори.
// schema: "ключ:прапорці", де r — обов'язковий, v — варіантний, m — множинний.
// p(назва, бренд, ціна ₴, атрибути товару, варіанти).
// Варіанти: { ключ: [значення | [значення, надбавка до ціни]] } — будуються всі комбінації.

const p = (t, b, price, a = {}, v = {}) => ({ t, b, price, a, v });
const CONN_PHONE = ["wifi", "bluetooth", "nfc", "5g", "usb-c"];

export default {
  smartphones: {
    noun: "Смартфон",
    brands: ["Apple", "Samsung", "Xiaomi", "Google", "Motorola", "OnePlus", "Nothing", "Realme"],
    schema: "brand:r operating-system:r screen-size:r ram-gb:r camera-mp battery-mah connectivity:m color:v storage-gb:vr",
    products: [
      p("Samsung Galaxy S24 Ultra", "samsung", 52999, { "operating-system": "android", "screen-size": 6.8, "ram-gb": 12, "camera-mp": 200, "battery-mah": 5000, connectivity: CONN_PHONE }, { color: ["titanium", "black"], "storage-gb": [[256, 0], [512, 6000]] }),
      p("Apple iPhone 15 Pro", "apple", 44999, { "operating-system": "ios", "screen-size": 6.1, "ram-gb": 8, "camera-mp": 48, "battery-mah": 3274, connectivity: CONN_PHONE }, { color: ["titanium", "black"], "storage-gb": [[128, 0], [256, 5000], [512, 13000]] }),
      p("Samsung Galaxy A55 5G", "samsung", 16999, { "operating-system": "android", "screen-size": 6.6, "ram-gb": 8, "camera-mp": 50, "battery-mah": 5000, connectivity: CONN_PHONE }, { color: ["black", "light-blue"], "storage-gb": [[128, 0], [256, 2500]] }),
      p("Xiaomi Redmi Note 13 Pro", "xiaomi", 13499, { "operating-system": "android", "screen-size": 6.67, "ram-gb": 8, "camera-mp": 200, "battery-mah": 5100, connectivity: ["wifi", "bluetooth", "nfc", "usb-c"] }, { color: ["black", "violet", "white"], "storage-gb": [[256, 0], [512, 2000]] }),
      p("Google Pixel 8", "google", 27999, { "operating-system": "android", "screen-size": 6.2, "ram-gb": 8, "camera-mp": 50, "battery-mah": 4575, connectivity: CONN_PHONE }, { color: ["black", "pink"], "storage-gb": [[128, 0], [256, 3500]] }),
      p("Motorola Edge 50 Fusion", "motorola", 14999, { "operating-system": "android", "screen-size": 6.7, "ram-gb": 8, "camera-mp": 50, "battery-mah": 5000, connectivity: CONN_PHONE }, { color: ["blue", "pink"], "storage-gb": [[256, 0]] }),
      p("OnePlus 12", "oneplus", 34999, { "operating-system": "android", "screen-size": 6.82, "ram-gb": 12, "camera-mp": 50, "battery-mah": 5400, connectivity: CONN_PHONE }, { color: ["green", "black"], "storage-gb": [[256, 0], [512, 4000]] }),
      p("Apple iPhone 13", "apple", 22999, { "operating-system": "ios", "screen-size": 6.1, "ram-gb": 4, "camera-mp": 12, "battery-mah": 3240, connectivity: ["wifi", "bluetooth", "nfc", "5g", "lightning"] }, { color: ["black", "white", "blue"], "storage-gb": [[128, 0], [256, 4000]] }),
      p("Nothing Phone (2a)", "nothing", 13999, { "operating-system": "android", "screen-size": 6.7, "ram-gb": 8, "camera-mp": 50, "battery-mah": 5000, connectivity: CONN_PHONE }, { color: ["black", "white"], "storage-gb": [[128, 0], [256, 1500]] }),
    ],
  },

  laptops: {
    noun: "Ноутбук",
    brands: ["Apple", "Dell", "Lenovo", "ASUS", "HP", "Acer", "MSI"],
    schema: "brand:r operating-system:r screen-size:r storage-type:r cpu-model:r gpu-model weight-kg color:v ram-gb:vr storage-gb:vr",
    products: [
      p("Dell XPS 15", "dell", 89999, { "operating-system": "windows", "screen-size": 15.6, "storage-type": "nvme", "cpu-model": "Intel Core i7-13700H", "gpu-model": "NVIDIA GeForce RTX 4050", "weight-kg": 1.86 }, { "ram-gb": [[16, 0], [32, 12000]], "storage-gb": [[512, 0], [1024, 5000]] }),
      p("Apple MacBook Air M3", "apple", 49999, { "operating-system": "macos", "screen-size": 13.6, "storage-type": "nvme", "cpu-model": "Apple M3 (8 ядер)", "gpu-model": "Apple M3 GPU (10 ядер)", "weight-kg": 1.24 }, { color: ["silver", "gray"], "ram-gb": [[8, 0], [16, 8000]], "storage-gb": [[256, 0], [512, 8000]] }),
      p("Lenovo IdeaPad Slim 5 14", "lenovo", 32999, { "operating-system": "windows", "screen-size": 14, "storage-type": "nvme", "cpu-model": "AMD Ryzen 7 7730U", "gpu-model": "AMD Radeon Graphics", "weight-kg": 1.46 }, { color: ["gray"], "ram-gb": [[16, 0]], "storage-gb": [[512, 0], [1024, 3000]] }),
      p("ASUS ROG Strix G16", "asus", 69999, { "operating-system": "windows", "screen-size": 16, "storage-type": "nvme", "cpu-model": "Intel Core i7-13650HX", "gpu-model": "NVIDIA GeForce RTX 4060", "weight-kg": 2.5 }, { "ram-gb": [[16, 0], [32, 6000]], "storage-gb": [[1024, 0]] }),
      p("HP Pavilion 15", "hp", 27999, { "operating-system": "windows", "screen-size": 15.6, "storage-type": "nvme", "cpu-model": "Intel Core i5-1335U", "gpu-model": "Intel Iris Xe Graphics", "weight-kg": 1.75 }, { color: ["silver"], "ram-gb": [[8, 0], [16, 2500]], "storage-gb": [[512, 0]] }),
      p("Acer Aspire 5 A515", "acer", 21999, { "operating-system": "no-os", "screen-size": 15.6, "storage-type": "nvme", "cpu-model": "Intel Core i5-12450H", "gpu-model": "Intel UHD Graphics", "weight-kg": 1.76 }, { "ram-gb": [[16, 0]], "storage-gb": [[512, 0]] }),
      p("Lenovo Legion 5 16IRX9", "lenovo", 64999, { "operating-system": "windows", "screen-size": 16, "storage-type": "nvme", "cpu-model": "Intel Core i7-14650HX", "gpu-model": "NVIDIA GeForce RTX 4060", "weight-kg": 2.4 }, { "ram-gb": [[16, 0], [32, 5000]], "storage-gb": [[1024, 0]] }),
      p("Apple MacBook Pro 14 M3 Pro", "apple", 99999, { "operating-system": "macos", "screen-size": 14.2, "storage-type": "nvme", "cpu-model": "Apple M3 Pro (11 ядер)", "gpu-model": "Apple M3 Pro GPU (14 ядер)", "weight-kg": 1.61 }, { color: ["silver", "black"], "ram-gb": [[18, 0]], "storage-gb": [[512, 0], [1024, 12000]] }),
      p("MSI Thin GF63 12VE", "msi", 36999, { "operating-system": "windows", "screen-size": 15.6, "storage-type": "nvme", "cpu-model": "Intel Core i5-12450H", "gpu-model": "NVIDIA GeForce RTX 4050", "weight-kg": 1.86 }, { "ram-gb": [[16, 0]], "storage-gb": [[512, 0], [1024, 2500]] }),
    ],
  },

  tablets: {
    noun: "Планшет",
    brands: ["Apple", "Samsung", "Xiaomi", "Lenovo", "Huawei"],
    schema: "brand:r operating-system:r screen-size:r battery-mah connectivity:m color:v storage-gb:vr",
    products: [
      p("Samsung Galaxy Tab S9", "samsung", 34999, { "operating-system": "android", "screen-size": 11, "battery-mah": 8400, connectivity: ["wifi", "bluetooth", "usb-c"] }, { color: ["graphite", "beige"], "storage-gb": [[128, 0], [256, 4000]] }),
      p("Apple iPad Pro 12.9", "apple", 54999, { "operating-system": "ipados", "screen-size": 12.9, "battery-mah": 10758, connectivity: ["wifi", "bluetooth", "usb-c"] }, { color: ["silver", "gray"], "storage-gb": [[256, 0], [512, 8000]] }),
      p("Apple iPad 10.9 (2022)", "apple", 17999, { "operating-system": "ipados", "screen-size": 10.9, "battery-mah": 7606, connectivity: ["wifi", "bluetooth", "usb-c"] }, { color: ["blue", "pink", "silver"], "storage-gb": [[64, 0], [256, 5500]] }),
      p("Xiaomi Pad 6", "xiaomi", 13999, { "operating-system": "android", "screen-size": 11, "battery-mah": 8840, connectivity: ["wifi", "bluetooth", "usb-c"] }, { color: ["gray", "light-blue"], "storage-gb": [[128, 0], [256, 1500]] }),
      p("Lenovo Tab P12", "lenovo", 15999, { "operating-system": "android", "screen-size": 12.7, "battery-mah": 10200, connectivity: ["wifi", "bluetooth", "usb-c"] }, { color: ["gray"], "storage-gb": [[128, 0]] }),
      p("Samsung Galaxy Tab A9+", "samsung", 9499, { "operating-system": "android", "screen-size": 11, "battery-mah": 7040, connectivity: ["wifi", "bluetooth", "usb-c"] }, { color: ["graphite", "silver"], "storage-gb": [[64, 0], [128, 1200]] }),
      p("Huawei MatePad 11.5", "huawei", 11999, { "operating-system": "harmonyos", "screen-size": 11.5, "battery-mah": 7700, connectivity: ["wifi", "bluetooth", "usb-c"] }, { color: ["gray"], "storage-gb": [[128, 0]] }),
    ],
  },

  smartwatches: {
    noun: "Смарт-годинник",
    brands: ["Apple", "Samsung", "Garmin", "Xiaomi", "Huawei", "Amazfit"],
    schema: "brand:r operating-system screen-size case-size-mm battery-life-hours waterproof connectivity:m color:v",
    products: [
      p("Apple Watch Series 9", "apple", 17999, { "operating-system": "watchos", "screen-size": 1.9, "case-size-mm": 45, "battery-life-hours": 18, waterproof: true, connectivity: ["bluetooth", "wifi", "nfc", "gps"] }, { color: ["black", "silver", "pink"] }),
      p("Samsung Galaxy Watch 6", "samsung", 11999, { "operating-system": "wear-os", "screen-size": 1.5, "case-size-mm": 44, "battery-life-hours": 40, waterproof: true, connectivity: ["bluetooth", "wifi", "nfc", "gps"] }, { color: ["graphite", "silver"] }),
      p("Garmin Forerunner 265", "garmin", 18999, { "screen-size": 1.3, "case-size-mm": 46, "battery-life-hours": 312, waterproof: true, connectivity: ["bluetooth", "wifi", "gps"] }, { color: ["black", "white"] }),
      p("Xiaomi Smart Band 8", "xiaomi", 1999, { "screen-size": 1.62, "battery-life-hours": 384, waterproof: true, connectivity: ["bluetooth"] }, { color: ["black", "gold"] }),
      p("Huawei Watch GT 4", "huawei", 9999, { "operating-system": "harmonyos", "screen-size": 1.43, "case-size-mm": 46, "battery-life-hours": 336, waterproof: true, connectivity: ["bluetooth", "gps", "nfc"] }, { color: ["black", "brown"] }),
      p("Amazfit GTR 4", "amazfit", 6999, { "screen-size": 1.45, "case-size-mm": 46, "battery-life-hours": 336, waterproof: true, connectivity: ["bluetooth", "gps", "wifi"] }, { color: ["black", "silver"] }),
    ],
  },

  headphones: {
    noun: "Навушники",
    brands: ["Sony", "Bose", "Apple", "Samsung", "JBL", "Sennheiser", "Marshall", "Xiaomi"],
    schema: "brand:r headphone-type:r wireless anc battery-life-hours connectivity:m color:v",
    products: [
      p("Sony WH-1000XM5", "sony", 13999, { "headphone-type": "over-ear", wireless: true, anc: true, "battery-life-hours": 30, connectivity: ["bluetooth", "usb-c", "jack-35"] }, { color: ["black", "silver"] }),
      p("Bose QuietComfort 45", "bose", 10999, { "headphone-type": "over-ear", wireless: true, anc: true, "battery-life-hours": 24, connectivity: ["bluetooth", "usb-c", "jack-35"] }, { color: ["black", "white"] }),
      p("Apple AirPods Pro 2", "apple", 9999, { "headphone-type": "in-ear", wireless: true, anc: true, "battery-life-hours": 6, connectivity: ["bluetooth", "usb-c"] }, { color: ["white"] }),
      p("Samsung Galaxy Buds 2 Pro", "samsung", 6999, { "headphone-type": "in-ear", wireless: true, anc: true, "battery-life-hours": 5, connectivity: ["bluetooth", "usb-c"] }, { color: ["graphite", "white", "violet"] }),
      p("JBL Tune 770NC", "jbl", 3999, { "headphone-type": "over-ear", wireless: true, anc: true, "battery-life-hours": 44, connectivity: ["bluetooth", "usb-c", "jack-35"] }, { color: ["black", "blue", "white"] }),
      p("Sennheiser Momentum 4 Wireless", "sennheiser", 12999, { "headphone-type": "over-ear", wireless: true, anc: true, "battery-life-hours": 60, connectivity: ["bluetooth", "usb-c", "jack-35"] }, { color: ["black", "white"] }),
      p("Marshall Major IV", "marshall", 5499, { "headphone-type": "on-ear", wireless: true, anc: false, "battery-life-hours": 80, connectivity: ["bluetooth", "usb-c", "jack-35"] }, { color: ["black", "brown"] }),
      p("Xiaomi Redmi Buds 5 Pro", "xiaomi", 2799, { "headphone-type": "in-ear", wireless: true, anc: true, "battery-life-hours": 10, connectivity: ["bluetooth", "usb-c"] }, { color: ["black", "white"] }),
    ],
  },

  "wireless-speakers": {
    noun: "Бездротова колонка",
    brands: ["JBL", "Sony", "Marshall", "Bose", "Ultimate Ears", "Harman Kardon"],
    schema: "brand:r wireless waterproof battery-life-hours power-w connectivity:m color:v",
    products: [
      p("JBL Charge 5", "jbl", 6499, { wireless: true, waterproof: true, "battery-life-hours": 20, "power-w": 40, connectivity: ["bluetooth", "usb-c"] }, { color: ["black", "blue", "red"] }),
      p("Sony SRS-XB33", "sony", 4999, { wireless: true, waterproof: true, "battery-life-hours": 24, connectivity: ["bluetooth", "usb-c"] }, { color: ["black", "blue"] }),
      p("JBL Flip 6", "jbl", 4499, { wireless: true, waterproof: true, "battery-life-hours": 12, "power-w": 30, connectivity: ["bluetooth", "usb-c"] }, { color: ["black", "blue", "red"] }),
      p("Marshall Emberton II", "marshall", 6999, { wireless: true, waterproof: true, "battery-life-hours": 30, "power-w": 20, connectivity: ["bluetooth", "usb-c"] }, { color: ["black"] }),
      p("Bose SoundLink Flex", "bose", 5999, { wireless: true, waterproof: true, "battery-life-hours": 12, connectivity: ["bluetooth", "usb-c"] }, { color: ["black", "blue"] }),
      p("Ultimate Ears BOOM 3", "ultimate-ears", 5499, { wireless: true, waterproof: true, "battery-life-hours": 15, connectivity: ["bluetooth"] }, { color: ["black", "blue"] }),
      p("Harman Kardon Onyx Studio 8", "harman-kardon", 8999, { wireless: true, waterproof: false, "battery-life-hours": 8, "power-w": 50, connectivity: ["bluetooth", "usb-c"] }, { color: ["black"] }),
    ],
  },

  televisions: {
    noun: "Телевізор",
    brands: ["Samsung", "LG", "Sony", "Philips", "Xiaomi", "TCL", "Hisense"],
    schema: "brand:r screen-size:r resolution-width:r resolution-height:r panel-type:r refresh-rate-hz smart-tv operating-system",
    products: [
      p("LG OLED C3 55", "lg", 54999, { "screen-size": 55, "resolution-width": 3840, "resolution-height": 2160, "panel-type": "oled", "refresh-rate-hz": 120, "smart-tv": true, "operating-system": "webos" }),
      p("Samsung QN90B 65", "samsung", 69999, { "screen-size": 65, "resolution-width": 3840, "resolution-height": 2160, "panel-type": "mini-led", "refresh-rate-hz": 120, "smart-tv": true, "operating-system": "tizen" }),
      p("Sony Bravia XR-55A80L", "sony", 62999, { "screen-size": 55, "resolution-width": 3840, "resolution-height": 2160, "panel-type": "oled", "refresh-rate-hz": 120, "smart-tv": true, "operating-system": "google-tv" }),
      p("Philips 55PUS8818 Ambilight", "philips", 27999, { "screen-size": 55, "resolution-width": 3840, "resolution-height": 2160, "panel-type": "led", "refresh-rate-hz": 60, "smart-tv": true, "operating-system": "google-tv" }),
      p("Xiaomi TV A Pro 55", "xiaomi", 17999, { "screen-size": 55, "resolution-width": 3840, "resolution-height": 2160, "panel-type": "qled", "refresh-rate-hz": 60, "smart-tv": true, "operating-system": "google-tv" }),
      p("TCL 65C745", "tcl", 32999, { "screen-size": 65, "resolution-width": 3840, "resolution-height": 2160, "panel-type": "qled", "refresh-rate-hz": 144, "smart-tv": true, "operating-system": "google-tv" }),
      p("Samsung UE50CU7100", "samsung", 15999, { "screen-size": 50, "resolution-width": 3840, "resolution-height": 2160, "panel-type": "led", "refresh-rate-hz": 60, "smart-tv": true, "operating-system": "tizen" }),
      p("Hisense 43A6K", "hisense", 10999, { "screen-size": 43, "resolution-width": 3840, "resolution-height": 2160, "panel-type": "led", "refresh-rate-hz": 60, "smart-tv": true }),
    ],
  },

  "gaming-consoles": {
    noun: "Ігрова консоль",
    brands: ["Sony", "Microsoft", "Nintendo", "Valve"],
    schema: "brand:r storage-gb connectivity:m color:v",
    products: [
      p("Sony PlayStation 5", "sony", 21999, { "storage-gb": 1000, connectivity: ["wifi", "bluetooth", "ethernet", "hdmi", "usb-c"] }, { color: ["white", "black"] }),
      p("Microsoft Xbox Series X", "microsoft", 22999, { "storage-gb": 1000, connectivity: ["wifi", "bluetooth", "ethernet", "hdmi", "usb-a"] }, { color: ["black"] }),
      p("Nintendo Switch OLED", "nintendo", 13999, { "storage-gb": 64, connectivity: ["wifi", "bluetooth", "usb-c", "hdmi"] }, { color: ["white", "red"] }),
      p("Microsoft Xbox Series S", "microsoft", 12999, { "storage-gb": 512, connectivity: ["wifi", "bluetooth", "ethernet", "hdmi"] }, { color: ["white", "black"] }),
      p("Valve Steam Deck OLED", "valve", 24999, { "storage-gb": 512, connectivity: ["wifi", "bluetooth", "usb-c"] }, { color: ["black"] }),
      p("Sony PlayStation Portal", "sony", 9999, { connectivity: ["wifi", "usb-c"] }, { color: ["white"] }),
    ],
  },

  "desktop-pcs": {
    noun: "Комп’ютер",
    brands: ["HP", "Lenovo", "Apple", "ASUS", "Dell", "ARTLINE", "MSI"],
    schema: "brand:r operating-system cpu-model:r gpu-model ram-gb:vr storage-gb:vr",
    products: [
      p("HP Omen 30L", "hp", 64999, { "operating-system": "windows", "cpu-model": "Intel Core i7-13700K", "gpu-model": "NVIDIA GeForce RTX 4070" }, { "ram-gb": [[16, 0], [32, 6000]], "storage-gb": [[512, 0], [1024, 3000]] }),
      p("Lenovo Legion Tower 5", "lenovo", 59999, { "operating-system": "windows", "cpu-model": "AMD Ryzen 7 7700", "gpu-model": "NVIDIA GeForce RTX 4060 Ti" }, { "ram-gb": [[16, 0], [32, 6000]], "storage-gb": [[1024, 0], [2048, 4000]] }),
      p("Apple Mac mini M2", "apple", 27999, { "operating-system": "macos", "cpu-model": "Apple M2 (8 ядер)", "gpu-model": "Apple M2 GPU (10 ядер)" }, { "ram-gb": [[8, 0], [16, 8000]], "storage-gb": [[256, 0], [512, 8000]] }),
      p("ARTLINE Gaming X75", "artline", 52999, { "operating-system": "no-os", "cpu-model": "AMD Ryzen 7 5700X", "gpu-model": "NVIDIA GeForce RTX 4060" }, { "ram-gb": [[32, 0]], "storage-gb": [[1024, 0]] }),
      p("ASUS ROG Strix G13CH", "asus", 74999, { "operating-system": "windows", "cpu-model": "Intel Core i7-14700F", "gpu-model": "NVIDIA GeForce RTX 4070 SUPER" }, { "ram-gb": [[32, 0]], "storage-gb": [[1024, 0]] }),
      p("Dell OptiPlex 7010 SFF", "dell", 24999, { "operating-system": "windows", "cpu-model": "Intel Core i5-13500", "gpu-model": "Intel UHD Graphics 770" }, { "ram-gb": [[8, 0], [16, 2000]], "storage-gb": [[512, 0]] }),
    ],
  },

  monitors: {
    noun: "Монітор",
    brands: ["LG", "Dell", "Samsung", "ASUS", "MSI", "AOC", "Philips"],
    schema: "brand:r screen-size:r resolution-width:r resolution-height:r panel-type:r refresh-rate-hz",
    products: [
      p("LG UltraGear 27", "lg", 13999, { "screen-size": 27, "resolution-width": 2560, "resolution-height": 1440, "panel-type": "ips", "refresh-rate-hz": 144 }),
      p("Dell UltraSharp U2723QE", "dell", 24999, { "screen-size": 27, "resolution-width": 3840, "resolution-height": 2160, "panel-type": "ips", "refresh-rate-hz": 60 }),
      p("Samsung Odyssey G5 32", "samsung", 11999, { "screen-size": 32, "resolution-width": 2560, "resolution-height": 1440, "panel-type": "va", "refresh-rate-hz": 165 }),
      p("ASUS TUF Gaming VG27AQ1A", "asus", 10999, { "screen-size": 27, "resolution-width": 2560, "resolution-height": 1440, "panel-type": "ips", "refresh-rate-hz": 170 }),
      p("MSI MAG 274QRF-QD", "msi", 11499, { "screen-size": 27, "resolution-width": 2560, "resolution-height": 1440, "panel-type": "ips", "refresh-rate-hz": 180 }),
      p("AOC 24G2SP", "aoc", 5999, { "screen-size": 24, "resolution-width": 1920, "resolution-height": 1080, "panel-type": "ips", "refresh-rate-hz": 165 }),
      p("Philips 243V7QDSB", "philips", 3799, { "screen-size": 23.8, "resolution-width": 1920, "resolution-height": 1080, "panel-type": "ips", "refresh-rate-hz": 75 }),
      p("Samsung Odyssey OLED G8 34", "samsung", 44999, { "screen-size": 34, "resolution-width": 3440, "resolution-height": 1440, "panel-type": "qd-oled", "refresh-rate-hz": 175 }),
    ],
  },
};
