// Комплектуючі, периферія та мережеве обладнання. Формат — див. catalog-gadgets.mjs.

const p = (t, b, price, a = {}, v = {}) => ({ t, b, price, a, v });

export default {
  processors: {
    noun: "Процесор",
    brands: ["Intel", "AMD"],
    schema: "brand:r cpu-socket:r cores:r threads base-clock-ghz boost-clock-ghz tdp-w",
    products: [
      p("Intel Core i9-13900K", "intel", 21999, { "cpu-socket": "lga1700", cores: 24, threads: 32, "base-clock-ghz": 3.0, "boost-clock-ghz": 5.8, "tdp-w": 125 }),
      p("AMD Ryzen 9 7950X", "amd", 23999, { "cpu-socket": "am5", cores: 16, threads: 32, "base-clock-ghz": 4.5, "boost-clock-ghz": 5.7, "tdp-w": 170 }),
      p("AMD Ryzen 7 7800X3D", "amd", 16999, { "cpu-socket": "am5", cores: 8, threads: 16, "base-clock-ghz": 4.2, "boost-clock-ghz": 5.0, "tdp-w": 120 }),
      p("Intel Core i5-14600K", "intel", 12999, { "cpu-socket": "lga1700", cores: 14, threads: 20, "base-clock-ghz": 3.5, "boost-clock-ghz": 5.3, "tdp-w": 125 }),
      p("AMD Ryzen 5 7600", "amd", 7499, { "cpu-socket": "am5", cores: 6, threads: 12, "base-clock-ghz": 3.8, "boost-clock-ghz": 5.1, "tdp-w": 65 }),
      p("Intel Core i5-12400F", "intel", 4299, { "cpu-socket": "lga1700", cores: 6, threads: 12, "base-clock-ghz": 2.5, "boost-clock-ghz": 4.4, "tdp-w": 65 }),
      p("AMD Ryzen 5 5600", "amd", 3899, { "cpu-socket": "am4", cores: 6, threads: 12, "base-clock-ghz": 3.5, "boost-clock-ghz": 4.4, "tdp-w": 65 }),
      p("Intel Core Ultra 7 265K", "intel", 17999, { "cpu-socket": "lga1851", cores: 20, threads: 20, "base-clock-ghz": 3.9, "boost-clock-ghz": 5.5, "tdp-w": 125 }),
    ],
  },

  "graphics-cards": {
    noun: "Відеокарта",
    brands: ["NVIDIA", "AMD", "ASUS", "MSI", "Gigabyte", "Palit", "Sapphire"],
    schema: "brand:r gpu-chip:r vram-gb:r tdp-w",
    products: [
      p("NVIDIA GeForce RTX 4070", "nvidia", 27999, { "gpu-chip": "GeForce RTX 4070", "vram-gb": 12, "tdp-w": 200 }),
      p("AMD Radeon RX 7800 XT", "amd", 23999, { "gpu-chip": "Radeon RX 7800 XT", "vram-gb": 16, "tdp-w": 263 }),
      p("ASUS Dual GeForce RTX 4060 OC 8GB", "asus", 13999, { "gpu-chip": "GeForce RTX 4060", "vram-gb": 8, "tdp-w": 115 }),
      p("MSI GeForce RTX 4070 Ti SUPER Ventus 3X 16G", "msi", 41999, { "gpu-chip": "GeForce RTX 4070 Ti SUPER", "vram-gb": 16, "tdp-w": 285 }),
      p("Gigabyte Radeon RX 7600 Gaming OC 8G", "gigabyte", 11499, { "gpu-chip": "Radeon RX 7600", "vram-gb": 8, "tdp-w": 165 }),
      p("Palit GeForce RTX 4060 Ti Dual 8GB", "palit", 17499, { "gpu-chip": "GeForce RTX 4060 Ti", "vram-gb": 8, "tdp-w": 160 }),
      p("Sapphire Pulse Radeon RX 7900 XT", "sapphire", 36999, { "gpu-chip": "Radeon RX 7900 XT", "vram-gb": 20, "tdp-w": 315 }),
    ],
  },

  motherboards: {
    noun: "Материнська плата",
    brands: ["ASUS", "MSI", "Gigabyte", "ASRock"],
    schema: "brand:r cpu-socket:r chipset:r form-factor:r memory-type:r",
    products: [
      p("ASUS ROG STRIX Z790", "asus", 15999, { "cpu-socket": "lga1700", chipset: "Intel Z790", "form-factor": "atx", "memory-type": "ddr5" }),
      p("MSI MAG B650 TOMAHAWK", "msi", 9499, { "cpu-socket": "am5", chipset: "AMD B650", "form-factor": "atx", "memory-type": "ddr5" }),
      p("Gigabyte B760M DS3H DDR4", "gigabyte", 4499, { "cpu-socket": "lga1700", chipset: "Intel B760", "form-factor": "matx", "memory-type": "ddr4" }),
      p("ASRock B550M Pro4", "asrock", 4199, { "cpu-socket": "am4", chipset: "AMD B550", "form-factor": "matx", "memory-type": "ddr4" }),
      p("ASUS TUF Gaming B650-PLUS WIFI", "asus", 8999, { "cpu-socket": "am5", chipset: "AMD B650", "form-factor": "atx", "memory-type": "ddr5" }),
      p("MSI MPG Z790I Edge WiFi", "msi", 13999, { "cpu-socket": "lga1700", chipset: "Intel Z790", "form-factor": "mini-itx", "memory-type": "ddr5" }),
    ],
  },

  "ram-memory": {
    noun: "Оперативна пам’ять",
    brands: ["Corsair", "Kingston", "G.Skill", "Crucial", "TeamGroup"],
    schema: "brand:r memory-type:r frequency-mhz ram-gb:vr",
    products: [
      p("Corsair Vengeance DDR5 32GB", "corsair", 4699, { "memory-type": "ddr5", "frequency-mhz": 6000 }, { "ram-gb": [[32, 0]] }),
      p("Kingston Fury Beast 16GB", "kingston", 1899, { "memory-type": "ddr4", "frequency-mhz": 3200 }, { "ram-gb": [[16, 0]] }),
      p("G.Skill Trident Z5 RGB DDR5-6400", "g-skill", 6499, { "memory-type": "ddr5", "frequency-mhz": 6400 }, { "ram-gb": [[32, 0], [64, 6500]] }),
      p("Crucial Pro DDR5-5600", "crucial", 3999, { "memory-type": "ddr5", "frequency-mhz": 5600 }, { "ram-gb": [[32, 0], [64, 4800]] }),
      p("Kingston Fury Beast DDR5-5200", "kingston", 2199, { "memory-type": "ddr5", "frequency-mhz": 5200 }, { "ram-gb": [[16, 0], [32, 1700]] }),
      p("TeamGroup T-Force Vulcan Z DDR4-3600", "teamgroup", 1699, { "memory-type": "ddr4", "frequency-mhz": 3600 }, { "ram-gb": [[16, 0], [32, 1600]] }),
    ],
  },

  "ssd-drives": {
    noun: "SSD-накопичувач",
    brands: ["Samsung", "Western Digital", "Kingston", "Crucial", "Lexar", "Goodram"],
    schema: "brand:r storage-type:r read-speed-mbps write-speed-mbps storage-gb:vr",
    products: [
      p("Samsung 980 PRO 1TB", "samsung", 4299, { "storage-type": "nvme", "read-speed-mbps": 7000, "write-speed-mbps": 5000 }, { "storage-gb": [[1000, 0]] }),
      p("WD Black SN850X 2TB", "western-digital", 6999, { "storage-type": "nvme", "read-speed-mbps": 7300, "write-speed-mbps": 6600 }, { "storage-gb": [[2000, 0]] }),
      p("Kingston NV2", "kingston", 1599, { "storage-type": "nvme", "read-speed-mbps": 3500, "write-speed-mbps": 2800 }, { "storage-gb": [[500, 0], [1000, 1000], [2000, 3400]] }),
      p("Samsung 870 EVO", "samsung", 2399, { "storage-type": "ssd", "read-speed-mbps": 560, "write-speed-mbps": 530 }, { "storage-gb": [[500, 0], [1000, 1700]] }),
      p("Crucial P3 Plus", "crucial", 2899, { "storage-type": "nvme", "read-speed-mbps": 5000, "write-speed-mbps": 4200 }, { "storage-gb": [[1000, 0], [2000, 2800]] }),
      p("Goodram CX400 Gen.2", "goodram", 1299, { "storage-type": "ssd", "read-speed-mbps": 550, "write-speed-mbps": 500 }, { "storage-gb": [[512, 0], [1024, 1000]] }),
      p("Lexar NM790", "lexar", 3499, { "storage-type": "nvme", "read-speed-mbps": 7400, "write-speed-mbps": 6500 }, { "storage-gb": [[1000, 0], [2000, 2700]] }),
    ],
  },

  "hdd-drives": {
    noun: "Жорсткий диск",
    brands: ["Seagate", "Western Digital", "Toshiba"],
    schema: "brand:r rpm storage-gb:vr",
    products: [
      p("Seagate Barracuda 4TB", "seagate", 3599, { rpm: 5400 }, { "storage-gb": [[4000, 0]] }),
      p("WD Blue 2TB", "western-digital", 2599, { rpm: 7200 }, { "storage-gb": [[2000, 0]] }),
      p("Seagate IronWolf NAS", "seagate", 4699, { rpm: 5400 }, { "storage-gb": [[4000, 0], [8000, 4500]] }),
      p("Toshiba P300", "toshiba", 1899, { rpm: 7200 }, { "storage-gb": [[1000, 0], [2000, 700], [4000, 2500]] }),
      p("WD Red Plus", "western-digital", 4999, { rpm: 5400 }, { "storage-gb": [[4000, 0], [6000, 2400]] }),
    ],
  },

  "power-supplies": {
    noun: "Блок живлення",
    brands: ["Corsair", "Seasonic", "be quiet!", "Chieftec", "DeepCool", "Cooler Master"],
    schema: "brand:r power-w:r psu-efficiency:r modular",
    products: [
      p("Corsair RM850x 850W", "corsair", 5999, { "power-w": 850, "psu-efficiency": "gold", modular: true }),
      p("Seasonic Focus GX-750", "seasonic", 4999, { "power-w": 750, "psu-efficiency": "gold", modular: true }),
      p("be quiet! Pure Power 12 M 750W", "be-quiet", 4599, { "power-w": 750, "psu-efficiency": "gold", modular: true }),
      p("Chieftec Proton 650W", "chieftec", 2299, { "power-w": 650, "psu-efficiency": "bronze", modular: true }),
      p("DeepCool PK550D", "deepcool", 1699, { "power-w": 550, "psu-efficiency": "bronze", modular: false }),
      p("Cooler Master MWE Gold 1050 V2", "cooler-master", 6999, { "power-w": 1050, "psu-efficiency": "gold", modular: true }),
    ],
  },

  "pc-cases": {
    noun: "Корпус",
    brands: ["Fractal Design", "NZXT", "Lian Li", "be quiet!", "DeepCool", "Corsair"],
    schema: "brand:r form-factor:r color:v",
    products: [
      p("Fractal Design Meshify C", "fractal-design", 3999, { "form-factor": "atx" }, { color: ["black", "white"] }),
      p("NZXT H510", "nzxt", 3499, { "form-factor": "atx" }, { color: ["black", "white"] }),
      p("Lian Li O11 Dynamic EVO", "lian-li", 6999, { "form-factor": "e-atx" }, { color: ["black", "white"] }),
      p("be quiet! Pure Base 500DX", "be-quiet", 4499, { "form-factor": "atx" }, { color: ["black", "white"] }),
      p("DeepCool CH560", "deepcool", 3799, { "form-factor": "e-atx" }, { color: ["black", "white"] }),
      p("Corsair 4000D Airflow", "corsair", 4199, { "form-factor": "atx" }, { color: ["black", "white"] }),
    ],
  },

  "cooling-systems": {
    noun: "Система охолодження",
    brands: ["Noctua", "DeepCool", "be quiet!", "Arctic", "NZXT", "ID-Cooling"],
    schema: "brand:r cooling-type:r tdp-w",
    products: [
      p("Noctua NH-D15", "noctua", 4999, { "cooling-type": "air", "tdp-w": 250 }),
      p("Deepcool AK620", "deepcool", 2299, { "cooling-type": "air", "tdp-w": 260 }),
      p("Arctic Liquid Freezer III 360", "arctic", 4799, { "cooling-type": "aio", "tdp-w": 300 }),
      p("be quiet! Dark Rock Pro 5", "be-quiet", 4599, { "cooling-type": "air", "tdp-w": 270 }),
      p("NZXT Kraken 240", "nzxt", 5499, { "cooling-type": "aio", "tdp-w": 250 }),
      p("ID-Cooling SE-224-XTS", "id-cooling", 899, { "cooling-type": "air", "tdp-w": 220 }),
    ],
  },

  keyboards: {
    noun: "Клавіатура",
    brands: ["Logitech", "Keychron", "Razer", "HyperX", "A4Tech", "SteelSeries"],
    schema: "brand:r wireless mechanical connectivity:m color:v",
    products: [
      p("Logitech MX Keys", "logitech", 4499, { wireless: true, mechanical: false, connectivity: ["bluetooth", "usb-c"] }, { color: ["graphite", "white"] }),
      p("Keychron K2", "keychron", 3999, { wireless: true, mechanical: true, connectivity: ["bluetooth", "usb-c"] }, { color: ["black"] }),
      p("Razer BlackWidow V4", "razer", 6499, { wireless: false, mechanical: true, connectivity: ["usb-c"] }, { color: ["black"] }),
      p("HyperX Alloy Origins Core", "hyperx", 3299, { wireless: false, mechanical: true, connectivity: ["usb-c"] }, { color: ["black"] }),
      p("Logitech K380", "logitech", 1499, { wireless: true, mechanical: false, connectivity: ["bluetooth"] }, { color: ["graphite", "pink", "white"] }),
      p("A4Tech Bloody B820R", "a4tech", 1799, { wireless: false, mechanical: true, connectivity: ["usb-a"] }, { color: ["black"] }),
      p("SteelSeries Apex 3 TKL", "steelseries", 2499, { wireless: false, mechanical: false, connectivity: ["usb-a"] }, { color: ["black"] }),
    ],
  },

  "mice-pads": {
    noun: "Миша / килимок",
    brands: ["Logitech", "Razer", "SteelSeries", "HyperX", "A4Tech"],
    schema: "brand:r wireless dpi color:v",
    products: [
      p("Logitech G840 XL", "logitech", 1599, {}, { color: ["black"] }),
      p("Razer Goliathus Extended", "razer", 1499, {}, { color: ["black"] }),
      p("Logitech G Pro X Superlight 2", "logitech", 6499, { wireless: true, dpi: 32000 }, { color: ["black", "white", "pink"] }),
      p("Razer DeathAdder V3", "razer", 2999, { wireless: false, dpi: 30000 }, { color: ["black"] }),
      p("Logitech MX Master 3S", "logitech", 4299, { wireless: true, dpi: 8000 }, { color: ["graphite", "gray"] }),
      p("SteelSeries QcK Heavy XXL", "steelseries", 1999, {}, { color: ["black"] }),
      p("HyperX Pulsefire Haste 2", "hyperx", 1899, { wireless: false, dpi: 26000 }, { color: ["black", "white"] }),
    ],
  },

  webcams: {
    noun: "Вебкамера",
    brands: ["Logitech", "Razer", "Elgato", "A4Tech"],
    schema: "brand:r video-resolution:r color:v",
    products: [
      p("Logitech C920 HD Pro", "logitech", 2499, { "video-resolution": "full-hd" }, { color: ["black"] }),
      p("Razer Kiyo Pro", "razer", 4499, { "video-resolution": "full-hd" }, { color: ["black"] }),
      p("Logitech Brio 4K", "logitech", 6999, { "video-resolution": "4k" }, { color: ["black"] }),
      p("Elgato Facecam MK.2", "elgato", 5999, { "video-resolution": "full-hd" }, { color: ["black"] }),
      p("Logitech C922 Pro Stream", "logitech", 3299, { "video-resolution": "full-hd" }, { color: ["black"] }),
      p("A4Tech PK-940HA", "a4tech", 1199, { "video-resolution": "full-hd" }, { color: ["black"] }),
    ],
  },

  microphones: {
    noun: "Мікрофон",
    brands: ["Blue", "HyperX", "Rode", "Shure", "Fifine", "Razer"],
    schema: "brand:r connectivity:mr color:v",
    products: [
      p("Blue Yeti USB Microphone", "blue", 5499, { connectivity: ["usb-a", "jack-35"] }, { color: ["black", "silver"] }),
      p("HyperX QuadCast S", "hyperx", 6499, { connectivity: ["usb-c", "jack-35"] }, { color: ["black"] }),
      p("Rode PodMic USB", "rode", 8999, { connectivity: ["usb-c", "xlr"] }, { color: ["black"] }),
      p("Shure SM58", "shure", 4999, { connectivity: ["xlr"] }, { color: ["black"] }),
      p("Fifine K669B", "fifine", 1299, { connectivity: ["usb-a"] }, { color: ["black"] }),
      p("Rode NT-USB Mini", "rode", 3999, { connectivity: ["usb-a", "jack-35"] }, { color: ["black"] }),
      p("Razer Seiren Mini", "razer", 1999, { connectivity: ["usb-a"] }, { color: ["black", "pink"] }),
    ],
  },

  routers: {
    noun: "Маршрутизатор",
    brands: ["ASUS", "TP-Link", "Xiaomi", "Keenetic", "Mercusys", "Netgear"],
    schema: "brand:r wireless-technology:r connectivity:m",
    products: [
      p("ASUS RT-AX88U", "asus", 10999, { "wireless-technology": "wifi-6", connectivity: ["wifi", "ethernet", "usb-a"] }),
      p("TP-Link Archer AX73", "tp-link", 4999, { "wireless-technology": "wifi-6", connectivity: ["wifi", "ethernet", "usb-a"] }),
      p("TP-Link Deco X50 (3 шт.)", "tp-link", 8999, { "wireless-technology": "wifi-6", connectivity: ["wifi", "ethernet"] }),
      p("Keenetic Hopper KN-3810", "keenetic", 4499, { "wireless-technology": "wifi-6", connectivity: ["wifi", "ethernet", "usb-a"] }),
      p("Xiaomi Router AX3000T", "xiaomi", 1999, { "wireless-technology": "wifi-6", connectivity: ["wifi", "ethernet"] }),
      p("ASUS RT-BE88U", "asus", 17999, { "wireless-technology": "wifi-7", connectivity: ["wifi", "ethernet", "usb-a"] }),
      p("Mercusys MR70X", "mercusys", 1699, { "wireless-technology": "wifi-6", connectivity: ["wifi", "ethernet"] }),
      p("Netgear Nighthawk RAX50", "netgear", 7999, { "wireless-technology": "wifi-6", connectivity: ["wifi", "ethernet", "usb-a"] }),
    ],
  },

  "external-drives": {
    noun: "Зовнішній накопичувач",
    brands: ["Samsung", "Western Digital", "Seagate", "SanDisk", "Transcend"],
    schema: "brand:r storage-type:r connectivity:m storage-gb:vr color:v",
    products: [
      p("Samsung T7 1TB", "samsung", 3999, { "storage-type": "ssd", connectivity: ["usb-c"] }, { "storage-gb": [[1000, 0]], color: ["gray", "blue"] }),
      p("WD My Passport 4TB", "western-digital", 4299, { "storage-type": "hdd", connectivity: ["usb-a"] }, { "storage-gb": [[4000, 0]], color: ["black", "blue"] }),
      p("SanDisk Extreme Portable SSD", "sandisk", 4499, { "storage-type": "nvme", connectivity: ["usb-c"] }, { "storage-gb": [[1000, 0], [2000, 3000]] }),
      p("Seagate Expansion Portable", "seagate", 2899, { "storage-type": "hdd", connectivity: ["usb-a"] }, { "storage-gb": [[2000, 0], [5000, 2500]] }),
      p("Samsung T9", "samsung", 5999, { "storage-type": "nvme", connectivity: ["usb-c"] }, { "storage-gb": [[1000, 0], [2000, 3500]] }),
      p("Transcend StoreJet 25M3", "transcend", 2399, { "storage-type": "hdd", connectivity: ["usb-a"] }, { "storage-gb": [[1000, 0], [2000, 800]] }),
    ],
  },
};
