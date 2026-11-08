// Побутова техніка, догляд за собою, електротранспорт і зарядні станції.
// Формат — див. catalog-gadgets.mjs.

const p = (t, b, price, a = {}, v = {}) => ({ t, b, price, a, v });

export default {
  "robot-vacuums": {
    noun: "Робот-пилосос",
    brands: ["Roborock", "iRobot", "Dreame", "Xiaomi", "Ecovacs"],
    schema: "brand:r suction-pa:r battery-life-minutes mopping color:v",
    products: [
      p("Roborock S8", "roborock", 26999, { "suction-pa": 6000, "battery-life-minutes": 180, mopping: true }, { color: ["black", "white"] }),
      p("iRobot Roomba j7+", "irobot", 21999, { "suction-pa": 2200, "battery-life-minutes": 75, mopping: false }, { color: ["black"] }),
      p("Dreame L10s Ultra", "dreame", 29999, { "suction-pa": 5300, "battery-life-minutes": 180, mopping: true }, { color: ["white"] }),
      p("Xiaomi Robot Vacuum S10+", "xiaomi", 14999, { "suction-pa": 4000, "battery-life-minutes": 180, mopping: true }, { color: ["white"] }),
      p("Ecovacs Deebot T30 Omni", "ecovacs", 32999, { "suction-pa": 11000, "battery-life-minutes": 180, mopping: true }, { color: ["black", "white"] }),
      p("Roborock Q7 Max", "roborock", 11999, { "suction-pa": 4200, "battery-life-minutes": 180, mopping: true }, { color: ["black", "white"] }),
    ],
  },

  "air-purifiers": {
    noun: "Очищувач повітря",
    brands: ["Philips", "Xiaomi", "Dyson", "Levoit", "Sharp"],
    schema: "brand:r room-area-m2:r power-w color:v",
    products: [
      p("Philips Air Purifier 3000i", "philips", 15999, { "room-area-m2": 104, "power-w": 56 }, { color: ["white"] }),
      p("Xiaomi Mi Air Purifier 4", "xiaomi", 6999, { "room-area-m2": 48, "power-w": 30 }, { color: ["white"] }),
      p("Dyson Purifier Cool TP07", "dyson", 24999, { "room-area-m2": 81, "power-w": 40 }, { color: ["white", "silver"] }),
      p("Levoit Core 300S", "levoit", 4999, { "room-area-m2": 40, "power-w": 24 }, { color: ["white"] }),
      p("Sharp UA-HD40E-L", "sharp", 9499, { "room-area-m2": 28, "power-w": 26 }, { color: ["white"] }),
      p("Xiaomi Smart Air Purifier Elite", "xiaomi", 12999, { "room-area-m2": 125, "power-w": 72 }, { color: ["white"] }),
    ],
  },

  "coffee-makers": {
    noun: "Кавоварка",
    brands: ["DeLonghi", "Philips", "Saeco", "Krups", "Nespresso", "Siemens"],
    schema: "brand:r water-tank-liters pressure-bar power-w color:v",
    products: [
      p("DeLonghi Magnifica S", "delonghi", 13999, { "water-tank-liters": 1.8, "pressure-bar": 15, "power-w": 1450 }, { color: ["black", "silver"] }),
      p("Philips 3200 LatteGo", "philips", 16999, { "water-tank-liters": 1.8, "pressure-bar": 15, "power-w": 1500 }, { color: ["black", "white"] }),
      p("Nespresso Vertuo Next", "nespresso", 4999, { "water-tank-liters": 1.1, "power-w": 1500 }, { color: ["black", "gray"] }),
      p("DeLonghi Dedica EC685", "delonghi", 7499, { "water-tank-liters": 1.1, "pressure-bar": 15, "power-w": 1300 }, { color: ["silver", "black", "red"] }),
      p("Saeco GranAroma", "saeco", 29999, { "water-tank-liters": 1.8, "pressure-bar": 15, "power-w": 1500 }, { color: ["black"] }),
      p("Siemens EQ.6 plus s300", "siemens", 21999, { "water-tank-liters": 1.7, "pressure-bar": 19, "power-w": 1500 }, { color: ["black"] }),
      p("Krups Evidence One EA895", "krups", 24999, { "water-tank-liters": 2.3, "pressure-bar": 15, "power-w": 1450 }, { color: ["black"] }),
    ],
  },

  microwaves: {
    noun: "Мікрохвильова піч",
    brands: ["Samsung", "Bosch", "LG", "Gorenje", "Panasonic", "Electrolux"],
    schema: "brand:r capacity-liters:r power-w grill color:v",
    products: [
      p("Samsung Microwave 23L", "samsung", 4299, { "capacity-liters": 23, "power-w": 800, grill: false }, { color: ["black", "white"] }),
      p("Bosch Serie 4 Microwave", "bosch", 7999, { "capacity-liters": 20, "power-w": 800, grill: false }, { color: ["silver", "black"] }),
      p("LG MH6535GIS", "lg", 4999, { "capacity-liters": 25, "power-w": 1000, grill: true }, { color: ["silver"] }),
      p("Gorenje MO20E1W", "gorenje", 2799, { "capacity-liters": 20, "power-w": 800, grill: false }, { color: ["white"] }),
      p("Panasonic NN-ST34HM", "panasonic", 3999, { "capacity-liters": 25, "power-w": 800, grill: false }, { color: ["silver"] }),
      p("Electrolux LMS2203EMX", "electrolux", 6999, { "capacity-liters": 20, "power-w": 700, grill: false }, { color: ["silver"] }),
    ],
  },

  "electric-kettles": {
    noun: "Електрочайник",
    brands: ["Bosch", "Philips", "Tefal", "Xiaomi", "Gorenje", "Russell Hobbs"],
    schema: "brand:r capacity-liters:r power-w color:v",
    products: [
      p("Bosch TWK3A011 Kettle", "bosch", 1199, { "capacity-liters": 1.7, "power-w": 2400 }, { color: ["black", "white"] }),
      p("Philips HD9350 Kettle", "philips", 1499, { "capacity-liters": 1.7, "power-w": 2200 }, { color: ["silver"] }),
      p("Xiaomi Electric Kettle 2", "xiaomi", 899, { "capacity-liters": 1.7, "power-w": 1800 }, { color: ["white"] }),
      p("Tefal KO2601 Express", "tefal", 999, { "capacity-liters": 1.7, "power-w": 2200 }, { color: ["white"] }),
      p("Russell Hobbs Attentiv 26200-70", "russell-hobbs", 2499, { "capacity-liters": 1.7, "power-w": 3000 }, { color: ["silver"] }),
      p("Gorenje K17WE", "gorenje", 799, { "capacity-liters": 1.7, "power-w": 2200 }, { color: ["white"] }),
    ],
  },

  "air-fryers": {
    noun: "Аерогриль",
    brands: ["Philips", "Cosori", "Tefal", "Xiaomi", "Ninja"],
    schema: "brand:r capacity-liters:r power-w color:v",
    products: [
      p("Philips Airfryer XXL", "philips", 9999, { "capacity-liters": 7.3, "power-w": 2225 }, { color: ["black"] }),
      p("Cosori Air Fryer 5.8QT", "cosori", 4999, { "capacity-liters": 5.5, "power-w": 1700 }, { color: ["black", "white"] }),
      p("Ninja Foodi Dual Zone AF300EU", "ninja", 8999, { "capacity-liters": 7.6, "power-w": 2470 }, { color: ["black"] }),
      p("Tefal Easy Fry Precision EY4018", "tefal", 3299, { "capacity-liters": 4.2, "power-w": 1500 }, { color: ["black"] }),
      p("Xiaomi Smart Air Fryer 6.5L", "xiaomi", 3499, { "capacity-liters": 6.5, "power-w": 1800 }, { color: ["white", "black"] }),
      p("Philips Airfryer 3000 Series L", "philips", 3999, { "capacity-liters": 4.1, "power-w": 1400 }, { color: ["black", "white"] }),
    ],
  },

  refrigerators: {
    noun: "Холодильник",
    brands: ["Samsung", "LG", "Bosch", "Gorenje", "Beko", "Whirlpool"],
    schema: "brand:r capacity-liters:r energy-class no-frost color:v",
    products: [
      p("Samsung RS68A8830SL", "samsung", 49999, { "capacity-liters": 609, "energy-class": "F", "no-frost": true }, { color: ["silver"] }),
      p("LG GBB72NSDFN", "lg", 32999, { "capacity-liters": 384, "energy-class": "D", "no-frost": true }, { color: ["gray"] }),
      p("Bosch KGN39VLCT", "bosch", 28999, { "capacity-liters": 363, "energy-class": "C", "no-frost": true }, { color: ["silver"] }),
      p("Gorenje NRK6192AXL4", "gorenje", 17999, { "capacity-liters": 300, "energy-class": "E", "no-frost": true }, { color: ["silver"] }),
      p("Beko RCNA406I30W", "beko", 15999, { "capacity-liters": 362, "energy-class": "E", "no-frost": true }, { color: ["white"] }),
      p("Whirlpool W7X 82I W", "whirlpool", 19999, { "capacity-liters": 335, "energy-class": "E", "no-frost": true }, { color: ["white"] }),
    ],
  },

  "washing-machines": {
    noun: "Пральна машина",
    brands: ["Bosch", "LG", "Samsung", "Electrolux", "Beko", "Whirlpool"],
    schema: "brand:r capacity-kg:r energy-class spin-speed-rpm color:v",
    products: [
      p("Bosch Serie 6 Washing Machine", "bosch", 24999, { "capacity-kg": 9, "energy-class": "A", "spin-speed-rpm": 1400 }, { color: ["white"] }),
      p("LG F4WV709P2 Washing Machine", "lg", 21999, { "capacity-kg": 9, "energy-class": "A", "spin-speed-rpm": 1400 }, { color: ["white"] }),
      p("Samsung WW80T534DAW", "samsung", 16999, { "capacity-kg": 8, "energy-class": "A", "spin-speed-rpm": 1400 }, { color: ["white"] }),
      p("Electrolux EW6SN426BI", "electrolux", 12999, { "capacity-kg": 6, "energy-class": "C", "spin-speed-rpm": 1200 }, { color: ["white"] }),
      p("Beko WUE6612DBA", "beko", 9999, { "capacity-kg": 6, "energy-class": "D", "spin-speed-rpm": 1200 }, { color: ["white"] }),
      p("Whirlpool FFB 8458 WV", "whirlpool", 14999, { "capacity-kg": 8, "energy-class": "B", "spin-speed-rpm": 1400 }, { color: ["white"] }),
    ],
  },

  "vacuum-cleaners": {
    noun: "Пилосос",
    brands: ["Dyson", "Philips", "Samsung", "Xiaomi", "Rowenta", "Bosch"],
    schema: "brand:r power-w wireless battery-life-minutes color:v",
    products: [
      p("Dyson V15 Detect", "dyson", 29999, { "power-w": 660, wireless: true, "battery-life-minutes": 60 }, { color: ["yellow", "silver"] }),
      p("Philips PowerPro Expert", "philips", 7999, { "power-w": 2100, wireless: false }, { color: ["blue"] }),
      p("Xiaomi Vacuum Cleaner G11", "xiaomi", 8999, { "power-w": 450, wireless: true, "battery-life-minutes": 60 }, { color: ["white"] }),
      p("Samsung Jet 75", "samsung", 13999, { "power-w": 550, wireless: true, "battery-life-minutes": 60 }, { color: ["graphite"] }),
      p("Rowenta X-Force Flex 9.60", "rowenta", 10999, { "power-w": 185, wireless: true, "battery-life-minutes": 45 }, { color: ["blue"] }),
      p("Bosch BGLS4X201", "bosch", 5499, { "power-w": 700, wireless: false }, { color: ["red"] }),
    ],
  },

  "shavers-trimmers": {
    noun: "Електробритва",
    brands: ["Philips", "Braun", "Panasonic", "Xiaomi", "Remington"],
    schema: "brand:r wireless battery-life-minutes waterproof color:v",
    products: [
      p("Philips Series 5000 Shaver", "philips", 3999, { wireless: true, "battery-life-minutes": 60, waterproof: true }, { color: ["black"] }),
      p("Braun Series 9 Pro", "braun", 11999, { wireless: true, "battery-life-minutes": 60, waterproof: true }, { color: ["silver", "graphite"] }),
      p("Philips OneBlade Pro 360", "philips", 2999, { wireless: true, "battery-life-minutes": 90, waterproof: true }, { color: ["black"] }),
      p("Panasonic ES-LV6U", "panasonic", 7999, { wireless: true, "battery-life-minutes": 45, waterproof: true }, { color: ["silver"] }),
      p("Xiaomi Mijia Electric Shaver S500", "xiaomi", 1299, { wireless: true, "battery-life-minutes": 60, waterproof: true }, { color: ["black"] }),
      p("Remington HC5150 Trimmer", "remington", 1499, { wireless: true, "battery-life-minutes": 40, waterproof: false }, { color: ["black"] }),
    ],
  },

  "hair-dryers": {
    noun: "Фен",
    brands: ["Dyson", "Philips", "Rowenta", "BaByliss", "Remington", "Xiaomi"],
    schema: "brand:r power-w:r modes color:v",
    products: [
      p("Dyson Supersonic", "dyson", 17999, { "power-w": 1600, modes: 4 }, { color: ["pink", "gray"] }),
      p("Philips BHD500 Hair Dryer", "philips", 1499, { "power-w": 2100, modes: 6 }, { color: ["black"] }),
      p("Rowenta Pro Expert CV8830", "rowenta", 3499, { "power-w": 2200, modes: 6 }, { color: ["black"] }),
      p("BaByliss 6704E", "babyliss", 1799, { "power-w": 2000, modes: 6 }, { color: ["black"] }),
      p("Xiaomi Mi Ionic Hair Dryer H300", "xiaomi", 1399, { "power-w": 1600, modes: 3 }, { color: ["pink"] }),
      p("Remington Shine Therapy AC5999", "remington", 1999, { "power-w": 2300, modes: 6 }, { color: ["violet"] }),
    ],
  },

  "electric-toothbrushes": {
    noun: "Електрична зубна щітка",
    brands: ["Philips", "Oral-B", "Xiaomi", "Panasonic"],
    schema: "brand:r battery-life-days modes color:v",
    products: [
      p("Philips Sonicare 9900", "philips", 11999, { "battery-life-days": 14, modes: 4 }, { color: ["black", "pink"] }),
      p("Oral-B iO Series 9", "oral-b", 12999, { "battery-life-days": 14, modes: 7 }, { color: ["black", "pink"] }),
      p("Oral-B Pro 3 3000", "oral-b", 1999, { "battery-life-days": 14, modes: 3 }, { color: ["pink", "blue"] }),
      p("Philips Sonicare 4100", "philips", 1699, { "battery-life-days": 14, modes: 1 }, { color: ["white", "blue"] }),
      p("Xiaomi Mi Electric Toothbrush T500", "xiaomi", 999, { "battery-life-days": 18, modes: 3 }, { color: ["white"] }),
      p("Panasonic EW-DC12", "panasonic", 2499, { "battery-life-days": 20, modes: 3 }, { color: ["white"] }),
    ],
  },

  "electric-scooters": {
    noun: "Електросамокат",
    brands: ["Xiaomi", "Segway", "Kugoo", "Inmotion", "Proove"],
    schema: "brand:r max-speed-kmh:r range-km:r battery-capacity-wh max-load-kg color:v",
    products: [
      p("Xiaomi Mi Electric Scooter Pro 2", "xiaomi", 17999, { "max-speed-kmh": 25, "range-km": 45, "battery-capacity-wh": 446, "max-load-kg": 100 }, { color: ["black"] }),
      p("Segway Ninebot Max G30", "segway", 22999, { "max-speed-kmh": 30, "range-km": 65, "battery-capacity-wh": 551, "max-load-kg": 100 }, { color: ["graphite"] }),
      p("Xiaomi Electric Scooter 4 Pro", "xiaomi", 21999, { "max-speed-kmh": 25, "range-km": 55, "battery-capacity-wh": 474, "max-load-kg": 120 }, { color: ["black"] }),
      p("Segway Ninebot E2 Plus", "segway", 10999, { "max-speed-kmh": 25, "range-km": 25, "battery-capacity-wh": 216, "max-load-kg": 100 }, { color: ["gray"] }),
      p("Kugoo Kirin G2 Pro", "kugoo", 26999, { "max-speed-kmh": 45, "range-km": 50, "battery-capacity-wh": 750, "max-load-kg": 130 }, { color: ["black"] }),
      p("Inmotion L9", "inmotion", 39999, { "max-speed-kmh": 30, "range-km": 120, "battery-capacity-wh": 990, "max-load-kg": 120 }, { color: ["black"] }),
      p("Proove Model X-City Pro", "proove", 13999, { "max-speed-kmh": 30, "range-km": 35, "battery-capacity-wh": 374, "max-load-kg": 120 }, { color: ["black", "gray"] }),
    ],
  },

  "power-stations": {
    noun: "Зарядна станція",
    brands: ["Anker", "EcoFlow", "Bluetti", "Jackery", "DJI"],
    schema: "brand:r battery-capacity-wh:r power-w:r connectivity:m",
    products: [
      p("Anker PowerHouse 767", "anker", 64999, { "battery-capacity-wh": 2048, "power-w": 2400, connectivity: ["usb-c", "usb-a"] }),
      p("EcoFlow Delta 2", "ecoflow", 37999, { "battery-capacity-wh": 1024, "power-w": 1800, connectivity: ["usb-c", "usb-a", "wifi", "bluetooth"] }),
      p("EcoFlow River 2 Pro", "ecoflow", 22999, { "battery-capacity-wh": 768, "power-w": 800, connectivity: ["usb-c", "usb-a", "wifi"] }),
      p("Bluetti AC180", "bluetti", 33999, { "battery-capacity-wh": 1152, "power-w": 1800, connectivity: ["usb-c", "usb-a", "wifi", "bluetooth"] }),
      p("Jackery Explorer 1000 Plus", "jackery", 44999, { "battery-capacity-wh": 1264, "power-w": 2000, connectivity: ["usb-c", "usb-a", "wifi"] }),
      p("DJI Power 1000", "dji", 34999, { "battery-capacity-wh": 1024, "power-w": 2200, connectivity: ["usb-c", "usb-a"] }),
    ],
  },
};
