--
-- PostgreSQL database dump
--

\restrict B3pQ5Y9eMROtJoTXL0KNENq4PYZJOKCM9K9OQJ4g35n2dbfR8cgLCzKl9Zo398h

-- Dumped from database version 17.6
-- Dumped by pg_dump version 17.11 (Debian 17.11-1.pgdg13+2)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Data for Name: ProductTypes; Type: TABLE DATA; Schema: public; Owner: -
--

SET SESSION AUTHORIZATION DEFAULT;

ALTER TABLE public."ProductTypes" DISABLE TRIGGER ALL;

COPY public."ProductTypes" ("Id", "Code", "Description") FROM stdin;
0	GadgetsAndSmartphones	Smartphones, tablets, smartwatches, and e-readers
1	ComputersAndHardware	Laptops, desktop PCs, components, networking, and external storage
2	PeripheralsAndAudio	Keyboards, mice, webcams, mics, headphones, speakers, and monitors
3	TVAndVideo	TVs, drones, action cams, photo/video gear, and surveillance
4	Gaming	Consoles, VR, and gaming hardware
5	AccessoriesAndPower	Powerbanks, chargers, cables, protective cases, and power stations
6	SmartHomeAndLighting	Smart home hubs, sensors, and smart lighting
7	MajorAppliances	Large home appliances like fridges and washing machines
8	SmallKitchenAppliances	Coffee makers, microwaves, kettles, air fryers, and cooking gear
9	HomeCleaning	Vacuums, robot vacuums, air purifiers, and climate control
10	PersonalCare	Shavers, hair dryers, and electric toothbrushes
11	Transport	Electric scooters and personal mobility devices
\.


ALTER TABLE public."ProductTypes" ENABLE TRIGGER ALL;

--
-- Data for Name: Categories; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."Categories" DISABLE TRIGGER ALL;

COPY public."Categories" ("Id", "ProductTypeId", "Code") FROM stdin;
30	0	e-readers
1	0	smartphones
3	0	tablets
4	0	smartwatches
24	1	routers
25	1	external-drives
2	1	laptops
9	1	desktop-pcs
11	1	processors
12	1	graphics-cards
13	1	motherboards
14	1	ram-memory
15	1	ssd-drives
16	1	hdd-drives
17	1	power-supplies
18	1	pc-cases
19	1	cooling-systems
22	2	webcams
23	2	microphones
5	2	headphones
6	2	wireless-speakers
10	2	monitors
20	2	keyboards
21	2	mice-pads
31	3	drones
32	3	action-cameras
33	3	dslr-cameras
36	3	security-cameras
7	3	televisions
8	4	gaming-consoles
26	5	powerbanks
27	5	chargers-cables
28	5	phone-cases
29	5	screen-protectors
50	5	power-stations
34	6	smarthome-hubs
35	6	smart-lighting
43	7	refrigerators
44	7	washing-machines
39	8	coffee-makers
40	8	microwaves
41	8	electric-kettles
42	8	air-fryers
37	9	robot-vacuums
38	9	air-purifiers
45	9	vacuum-cleaners
46	10	shavers-trimmers
47	10	hair-dryers
48	10	electric-toothbrushes
49	11	electric-scooters
\.


ALTER TABLE public."Categories" ENABLE TRIGGER ALL;

--
-- Data for Name: Regions; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."Regions" DISABLE TRIGGER ALL;

COPY public."Regions" ("Id", "Code") FROM stdin;
1	KYIV
2	REGION-2
3	REGION-3
4	REGION-4
5	REGION-5
6	REGION-6
7	REGION-7
8	REGION-8
9	REGION-9
10	REGION-10
11	REGION-11
12	REGION-12
13	REGION-13
14	REGION-14
15	REGION-15
16	REGION-16
17	REGION-17
18	REGION-18
19	REGION-19
20	REGION-20
\.


ALTER TABLE public."Regions" ENABLE TRIGGER ALL;

--
-- Data for Name: Cities; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."Cities" DISABLE TRIGGER ALL;

COPY public."Cities" ("Id", "RegionId", "Code") FROM stdin;
1	1	1
2	1	2
3	2	3
4	3	4
5	3	5
6	3	6
7	4	7
8	4	8
9	5	9
10	5	10
11	6	11
12	7	12
13	7	13
14	8	14
15	8	15
16	9	16
17	10	17
18	10	18
19	11	19
20	11	20
21	12	21
22	13	22
23	14	23
24	15	24
25	15	25
26	16	26
27	17	27
28	18	28
29	19	29
30	20	30
\.


ALTER TABLE public."Cities" ENABLE TRIGGER ALL;

--
-- Data for Name: Conditions; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."Conditions" DISABLE TRIGGER ALL;

COPY public."Conditions" ("Id", "Code") FROM stdin;
0	new
1	used_like_new
2	used_good
3	used_fair
4	for_parts
\.


ALTER TABLE public."Conditions" ENABLE TRIGGER ALL;

--
-- Data for Name: Currencies; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."Currencies" DISABLE TRIGGER ALL;

COPY public."Currencies" ("Id", "Name", "Code", "Symbol") FROM stdin;
0	US Dollar	USD	$
1	Ukrainian Hryvnia	UAH	₴
\.


ALTER TABLE public."Currencies" ENABLE TRIGGER ALL;

--
-- Data for Name: Statuses; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."Statuses" DISABLE TRIGGER ALL;

COPY public."Statuses" ("Id", "Code") FROM stdin;
0	ACTIVE
1	ARCHIVED
2	SOLD
\.


ALTER TABLE public."Statuses" ENABLE TRIGGER ALL;

--
-- Data for Name: Users; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."Users" DISABLE TRIGGER ALL;

COPY public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") FROM stdin;
49	\N	user-0	AQAAAAIAAYagAAAAEIUHqhl9TqXqLcDiFRHzRJ+hd4Erm2nKGyWgqvyWMsdNeEa59hj33CHyAASu68l54w==	+4915565130632	\N	\N	\N	2026-09-17 12:10:41.115455+00	\N	\N	krip1123	f	t	Active
18	\N	user-18	AQAAAAIAAYagAAAAEKxDXnlw2uQa8gtEPumjnLCAEz+K6A/PU87YY7g+YXP7NKkYKGbeKyhvPxaxO8GqNg==	+380996669123	\N	\N	\N	2026-07-15 12:43:33.274065+00	\N	\N	user-18	f	f	Active
21	\N	user-21	AQAAAAIAAYagAAAAELnWJPn4QbLBKRWRiNy3XfORV3U3CdCt2FadUwVnkaYjsGfKkq6Hc/jjOiJRSHEhCQ==	+380996769123	\N	\N	\N	2026-07-15 12:47:18.098657+00	\N	\N	user-21	f	f	Active
22	\N	user-22	AQAAAAIAAYagAAAAEGkb28Jqsv6zUXzoJ9sePquYAOF/BNe46pa6FW903iZno8Rr0/kbA0NzCcEyVwijew==	+380996769523	\N	\N	\N	2026-07-15 12:47:28.142212+00	\N	\N	user-22	f	f	Active
51	\N	user-0	AQAAAAIAAYagAAAAEI+05kSmpKvGGB8VKsffoNMtztu49xN0lD1RpcAkUfEPGz0eE3BEiTQ6ZC1Tih/uXg==	+380967870204	\N	\N	\N	2026-09-17 12:48:53.974745+00	\N	\N	krip1111	f	t	Active
64	\N	egorstarostenko888	AQAAAAIAAYagAAAAEJ9Z8MJ6mD2f1kZig7pWAOjtL8WrSUoolPBpw1hn6pgVs/jndOumpnucpN+5VLa/Ig==	\N	egorstarostenko888@gmail.com	\N	\N	2026-09-20 20:49:13.940059+00	\N	\N	egorstarostenko34	f	f	PendingVerification
16	\N	nikitakolesnik.w	AQAAAAIAAYagAAAAEBr951YvJ4vxg96TcvlONBOpfEPTQbH2fd1o555q/pZZ6wSwLTwOA3wnKOb8Q6MHnA==	\N	nikitakolesnik1.w@gmail.com	\N	\N	2026-07-15 12:33:11.259602+00	\N	\N	user-16	f	f	Active
55	\N	user-55	AQAAAAIAAYagAAAAEOjv2fW1rDV2lVzR+vZfxAnYG0RiqfkrpPwKeKm+LM4PH50NPEtxd+hJ/1dPLc3V2A==	+380990408725	\N	\N	\N	2026-09-18 20:04:48.96597+00	\N	\N	krip1141566	f	t	Active
57	\N	egorstarostenko555	AQAAAAIAAYagAAAAEMfzuDtWoga0g/Pk2kAGkJknSh10tKyVIF43KJ+lX8iSvQ5OpNEkn0bL0I1r17q4ww==	\N	egorstarostenko555@gmail.com	\N	\N	2026-09-19 13:32:00.666178+00	\N	\N	piska	f	f	PendingVerification
28	\N	nikitakoliesnk5	AQAAAAIAAYagAAAAEIdLmBfE0ZCeck4BzZAoSsDjoPpabbOfoiOH3Uf204fLj33T2BCDz7f3J2Gzmh0GRg==	\N	nikitakoliesnk4@gmail.com	\N	\N	2026-07-15 12:57:39.422005+00	\N	\N	user-28	f	f	Active
61	\N	user-61	AQAAAAIAAYagAAAAEIlMGHLny5zka5JatxNXcNYupj1sM9oQPs/5yzb00XADQBXCeDdMoujaVHzI794yDA==	+380963147037	\N	\N	2026-09-19 17:22:51.175331+00	2026-09-19 17:21:16.03804+00	\N	\N	riirar	f	t	Active
9	\N	user-9	AQAAAAIAAYagAAAAEHEiexHSw3V+FxBYMwseljfZW73dReGzqnKQqJcZwdVkS+VJz4YGIolMVDs7Oa1gMA==	09629208781	\N	\N	2026-09-13 09:54:14.081061+00	2026-06-28 07:50:24.747221+00	\N	\N	user-9	f	f	Active
62	\N	starostenko.yehor_kp62		\N	starostenko.yehor_kp62@edu.kpi.ua	\N	2026-09-20 10:35:36.243225+00	2026-09-19 19:24:39.681329+00	\N	\N	КП-62 Старостенко Єгор Євгенійович	f	f	Active
56	\N	kolesniknikita867	AQAAAAIAAYagAAAAENCXPFmS797iimcuxFLFVw/MIRVYtasCY6khKNTDqz/WFp0xb/1rg5EHHiWKwvRKgQ==	\N	kolesniknikita867@gmail.com	\N	\N	2026-09-19 13:21:06.17524+00	\N	\N	kr1p	f	f	PendingVerification
58	\N	egorstarostenko365	AQAAAAIAAYagAAAAEP+z+6jsfFgBmR6ClPBag160tmHF91qVaz3S8r4EXu4nemlvxAk0whk4pKI/9fW3yQ==	\N	egorstarostenko365@gmail.com	\N	\N	2026-09-19 14:22:29.707379+00	\N	\N	piska228	f	f	PendingVerification
3	Starostenko	Maga	AQAAAAIAAYagAAAAEFh+GdTEIk9aP/vIbBmzR49o8RmUi7ImIN2Ka8TU4jWRcxRlbIeOR0DpdMnGLis27A==	+38099586912	polypoker235@gmail.com	\N	2026-08-20 11:32:23.449747+00	2026-06-17 21:15:50.794642+00	0	\N	user-3	f	f	Active
63	\N	egorstarostenko999	AQAAAAIAAYagAAAAEP3z3FH+9Q4Q52Ce/fSd2u9IzteK7MM5XKAR5HJWsqKGMHpXEnBCVLNQGT5TqNoukw==	\N	egorstarostenko999@gmail.com	\N	\N	2026-09-20 11:48:09.765425+00	\N	\N	egorstarostenko345	f	f	PendingVerification
71	\N	user-71	AQAAAAIAAYagAAAAEA7mLWRTF0CEmjZiNp9hvAsITirgy/9bbVIPsB8kBYV/IenBd8fE8FcF4gLLX/dU3g==	+380962920878	\N	\N	\N	2026-09-22 11:31:52.067052+00	\N	\N	Bobaka	f	t	Active
73	\N	egorstarostenko333	AQAAAAIAAYagAAAAEKR6NNk0oZUmcmjoiWEqxP/yVKnoPwyvUJNmSDPnhJXF7M9GsGfIu8vn6vS6hMZovw==	\N	egorstarostenko333@gmail.com	\N	\N	2026-10-04 09:56:40.466114+00	\N	\N	erggg	f	f	PendingVerification
65	\N	marketly.demo		\N	marketly.demo@gmail.com	\N	2026-10-01 14:39:41.731725+00	2026-09-20 20:50:33.241603+00	\N	\N	Nikita Kolesnik	f	f	Active
68	\N	nikitakoliesnik5	AQAAAAIAAYagAAAAEPohd+gOKzCF3JPApfuRtJZu0VIb/27ExSplShU5CWfnOk2f2MTk/a7bdCw9D6BqMw==	\N	nikitakoliesnik5@gmail.com	\N	2026-09-29 14:34:24.998559+00	2026-09-21 19:43:37.916083+00	\N	\N	11krip	t	f	Active
66	\N	nikitakolesnik.w		\N	nikitakolesnik.w@gmail.com	\N	2026-10-03 13:03:34.776419+00	2026-09-21 10:41:50.258392+00	\N	\N	Нікіта Колєснік	f	f	Active
7	Старостенко	Єгор	AQAAAAIAAYagAAAAEKmOGBNJ0cugMCceJpzSmkJW7L5ur+gMZmSWU6kE6oKOMSAkhdHNwyJboEifQXiplA==	+38099586911	egorstarostenko345@gmail.com	1	2026-10-06 11:15:06.446458+00	2026-06-23 19:51:39.779765+00	\N	Hello, my name is Ehor, I am Ukrainian programmer	user-7	f	f	Active
\.


ALTER TABLE public."Users" ENABLE TRIGGER ALL;

--
-- Data for Name: Advertisements; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."Advertisements" DISABLE TRIGGER ALL;

COPY public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") FROM stdin;
256	Вебкамера Logitech C920 HD Pro Full HD 1080p	Легендарна вебкамера для стрімінгу, навчання та відеодзвінків із двома стереомікрофонами.	88	1	21	9	\N	\N	2026-10-04 21:22:20.623595+00	{"Brand": "Logitech", "Model": "C920 HD Pro"}	1	1
257	Вебкамера Razer Kiyo Pro Full HD 60FPS	Професійна вебкамера з адаптивним датчиком освітленості та підтримкою HDR.	84	1	21	9	\N	\N	2026-10-04 21:22:20.623595+00	{"Brand": "Razer", "Model": "Kiyo Pro"}	1	1
258	USB-мікрофон HyperX QuadCast S RGB	Конденсаторний ігровий мікрофон із керованим RGB-підсвічуванням та вбудованим поп-фільтром.	95	1	22	9	\N	\N	2026-10-04 21:22:20.623595+00	{"Brand": "HyperX", "Model": "QuadCast S"}	1	1
259	Студійний динамічний мікрофон Shure SM7B	Еталонний студійний мікрофон для подкастів, озвучки та професійного вокалу.	91	1	22	9	\N	\N	2026-10-04 21:22:20.623595+00	{"Brand": "Shure", "Model": "SM7B"}	1	1
260	Маршрутизатор ASUS RT-AX88U Pro Wi-Fi 6	Потужний двопандіапазонний ігровий роутер із підтримкою Wi-Fi 6 та портами 2.5G.	87	1	23	9	\N	\N	2026-10-04 21:22:20.623595+00	{"Brand": "ASUS", "Model": "RT-AX88U Pro"}	1	1
261	Маршрутизатор TP-Link Archer AX55 Wi-Fi 6	Гігабітний роутер для дому із підтримкою OneMesh та захистом HomeShield.	90	1	23	9	\N	\N	2026-10-04 21:22:20.623595+00	{"Brand": "TP-Link", "Model": "Archer AX55"}	1	1
262	Зовнішній SSD накопичувач Samsung T7 Shield 1TB Black	Захищений від падінь, пилу та вологи портативний SSD зі швидкістю до 1050 МБ/с.	92	1	24	9	\N	\N	2026-10-04 21:22:20.623595+00	{"Brand": "Samsung", "Model": "T7 Shield", "Storage": 1024}	1	1
263	Портативний SSD SanDisk Extreme Portable 2TB V2	Компактний швидкісний накопичувач із карабінним кріпленням для подорожей.	86	1	24	9	\N	\N	2026-10-04 21:22:20.623595+00	{"Brand": "SanDisk", "Model": "Extreme Portable V2", "Storage": 2048}	1	1
264	Павербанк Anker 737 PowerBank 24000mAh 140W	Ультрапотужна батарея з дисплеєм, здатна швидкісно заряджати ноутбуки та смартфони.	96	1	25	9	\N	\N	2026-10-04 21:22:20.623595+00	{"Brand": "Anker", "Model": "737 PowerBank"}	1	1
265	Павербанк Baseus Blade Digital Display 100W 20000mAh	Ультратонкий зовнішній акумулятор для MacBook та Windows-ноутбуків.	89	1	25	9	\N	\N	2026-10-04 21:22:20.623595+00	{"Brand": "Baseus", "Model": "Blade 100W"}	1	1
266	Зарядний пристрій UGREEN Nexode 100W GaN 4-Ports	Універсальний мережевий адаптер із технологією GaN для одночасної зарядки 4 пристроїв.	91	1	26	9	\N	\N	2026-10-04 21:22:20.623595+00	{"Brand": "UGREEN", "Model": "Nexode 100W"}	1	1
267	Оригінальний компактний адаптер Apple 35W Dual USB-C	Фірмовий зарядний блок з двома портами Type-C для одночасної зарядки iPhone та Apple Watch.	85	1	26	9	\N	\N	2026-10-04 21:22:20.623595+00	{"Brand": "Apple", "Model": "35W Dual USB-C"}	1	1
268	Захисний чохол Spigen Ultra Hybrid MagFit для iPhone 15 Pro	Прозорий ударостійкий чохол із вбудованим магнітним кільцем для підтримки MagSafe.	88	1	27	9	\N	\N	2026-10-04 21:22:20.623595+00	{"Brand": "Spigen", "Model": "Ultra Hybrid"}	1	1
269	Преміум чохол Pitaka MagEZ Case 4 для Samsung Galaxy S24 Ultra	Ультратонкий чохол із 100% арамідного волокна (Кевлар) з підтримкою магнітної зарядки.	83	1	27	9	\N	\N	2026-10-04 21:22:20.623595+00	{"Brand": "Pitaka", "Model": "MagEZ Case 4"}	1	1
270	Захисне скло ESR Armorite для iPhone 15 Pro (Комплект 2 шт)	Загартоване скло з рамкою для швидкої установки, що витримує навантаження до 50 кг.	89	1	28	9	\N	\N	2026-10-04 21:22:20.623595+00	{"Brand": "ESR", "Model": "Armorite"}	1	1
271	Захисне скло Spigen Glas.tR EZ Fit для Samsung S24 Ultra	Олеофобне загартоване скло зі спеціальним установочним трафаретом у комплекті.	87	1	28	9	\N	\N	2026-10-04 21:22:20.623595+00	{"Brand": "Spigen", "Model": "Glas.tR EZ Fit"}	1	1
272	Електронна книга Amazon Kindle Paperwhite 11th Gen 16GB	Електронна рідери з 6.8-дюймовим екраном E-Ink Carta, регулюванням колірної температури та захистом IPX8.	94	1	30	9	\N	\N	2026-10-04 21:22:20.623595+00	{"Brand": "Amazon", "Model": "Kindle Paperwhite 11", "Storage": 16, "ScreenSize": 6.8}	1	1
280	Об'єктив Sigma 24-70mm f/2.8 DG DN Art для Sony E-mount	Професійний світлосильний стандартний зум-об'єктив із високою деталізацією по всій площі кадру.	91	1	33	9	\N	\N	2026-10-05 08:08:08.686322+00	{"Brand": "Sigma", "Model": "24-70mm f/2.8 DG DN Art"}	1	1
281	Портретний об'єктив Sony FE 85mm f/1.8 (SEL85F18)	Компактний і легкий портретний фікс-об'єктив із м'яким розмиттям фону (боке).	87	1	33	9	\N	\N	2026-10-05 08:08:08.686322+00	{"Brand": "Sony", "Model": "FE 85mm f/1.8"}	1	1
282	3-осьовий стабілізатор DJI RS 3 Combo	Професійний стедікам для дзеркальних та бездзеркальних камер із корисним навантаженням до 3 кг.	88	1	34	9	\N	\N	2026-10-05 08:08:08.686322+00	{"Brand": "DJI", "Model": "RS 3 Combo"}	1	1
283	Стедікам для смартфона Zhiyun Smooth 5S Combo Gray	Професійний 3-осьовий стабілізатор для мобільної зйомки з вбудованим підсвічуванням.	82	1	34	9	\N	\N	2026-10-05 08:08:08.686322+00	{"Brand": "Zhiyun", "Model": "Smooth 5S"}	1	1
284	Студійне LED світло Godox SL60W 5600K	Потужне постійне світло 60 Вт із байонетом Bowens для фотосесій та відеозйомки.	86	1	35	9	\N	\N	2026-10-05 08:08:08.686322+00	{"Brand": "Godox", "Model": "SL60W"}	1	1
285	Студійне джерело світла Aputure Amaran 100x S Bi-Color	Професійний світлодіодний прожектор із регульованою колірною температурою 2700K-6500K.	90	1	35	9	\N	\N	2026-10-05 08:08:08.686322+00	{"Brand": "Aputure", "Model": "Amaran 100x S"}	1	1
276	Екшн-камера GoPro HERO12 Black	Флагманська екшн-камера з відео 5.3K60, стабілізацією HyperSmooth 6.0 та підтримкою HDR.	93	1	32	9	\N	\N	2026-10-05 08:08:08.686322+00	{"Brand": "GoPro", "Model": "HERO12 Black"}	1	1
300	Розумний конвектор Xiaomi Smart Space Heater S (KRDNQ03YD)	Безшумний електричний обігрівач потужністю 2200 Вт із захистом від вологи IPX4 та керуванням із Mi Home.	94	1	43	9	\N	\N	2026-10-05 10:14:31.550596+00	{"Brand": "Xiaomi", "Model": "Smart Space Heater S"}	1	1
301	Електричний конвектор Cooper&Hunter CH-2000 M	Надійний механічний обігрівач для приміщень площею до 25 кв.м із цілісним суцільним нагрівальним елементом.	86	1	43	9	\N	\N	2026-10-05 10:14:31.550596+00	{"Brand": "Cooper&Hunter", "Model": "CH-2000 M"}	1	1
302	Інверторний кондиціонер Midea Mission II MB-09N8D6-I/MB-09N8D6-O	Енергоефективна спліт-система з Wi-Fi модулем, роботою на обігрів до -25°C та вишуканим дизайном.	88	1	44	9	\N	\N	2026-10-05 10:14:31.550596+00	{"Brand": "Midea", "Model": "Mission II"}	1	1
303	Спліт-система Cooper&Hunter CH-S12FTXF-NG (Vital)	Популярний інверторний кондиціонер із генератором іонів Cold Plasma та преміальною енергозберігаючою комплектацією.	93	1	44	9	\N	\N	2026-10-05 10:14:31.550596+00	{"Brand": "Cooper&Hunter", "Model": "Vital Inverter"}	1	1
304	Очищувач повітря Xiaomi Smart Air Purifier 4 Pro	Високоефективний очищувач для великих кімнат до 60 кв.м із потрійною фільтрацією та OLED-дисплеєм.	96	1	45	9	\N	\N	2026-10-05 10:14:31.550596+00	{"Brand": "Xiaomi", "Model": "Smart Air Purifier 4 Pro"}	1	1
305	Кліматичний комплекс Boneco HYBRID H300	Універсальний прилад «2-в-1»: мийка повітря із природним зволоженням та фільтрацією HEPA.	87	1	45	9	\N	\N	2026-10-05 10:14:31.550596+00	{"Brand": "Boneco", "Model": "H300 Hybrid"}	1	1
306	Парогенератор Philips PerfectCare Series 8000 PSG8040/60	Потужна прасувальна система з автоматичною подачею пари OptimalTEMP без ризику пропалити тканину.	90	1	46	9	\N	\N	2026-10-05 10:14:31.550596+00	{"Brand": "Philips", "Model": "PerfectCare Series 8000"}	1	1
307	Парова праска Tefal Ultimate Pure FV9845E0 3200W	Потужна праска з унікальним фільтром Micro-Calc для запобігання появі плям від накипу.	88	1	46	9	\N	\N	2026-10-05 10:14:31.550596+00	{"Brand": "Tefal", "Model": "Ultimate Pure"}	1	1
312	Вбудована посудомийна машина Bosch Serie 4 SMV4EVX14E 60cm	Повнорозмірна вбудована посудомийка з третьою корзиною VarioDrawer та автоматичним відкриванням дверцят.	93	1	49	9	\N	\N	2026-10-05 10:14:31.550596+00	{"Brand": "Bosch", "Model": "Serie 4 SMV4EVX14E"}	1	1
313	Окремостояча посудомийна машина Electrolux 300 MaxiFlex 45cm	Вузька посудомийка з розбризкувачем SatelliteClean та технологією сушіння AirDry.	87	1	49	9	\N	\N	2026-10-05 10:14:31.550596+00	{"Brand": "Electrolux", "Model": "300 MaxiFlex"}	1	1
314	Електрична духовка Samsung Bespoke NV7B44205AK Dual Cook	Вбудована духова шафа з технологією подвійного приготування Dual Cook та очищенням каталітичною панеллю.	94	1	50	9	\N	\N	2026-10-05 10:14:31.550596+00	{"Brand": "Samsung", "Model": "Bespoke Dual Cook"}	1	1
315	Вбудована духова шафа Gorenje Simplicity BO76SYB	Елегантна чорна духовка зі склепінчастою формою камери HomeMade Plus та функцією очищення парою AquaClean.	88	1	50	9	\N	\N	2026-10-05 10:14:31.550596+00	{"Brand": "Gorenje", "Model": "Simplicity BO76SYB"}	1	1
273	Електронна книга PocketBook 634 Verse Pro 16GB	Компактний рідер з екранним аудіовиходом Bluetooth, підтримкою всіх форматів без конвертації.	86	1	30	9	\N	\N	2026-10-04 21:22:20.623595+00	{"Brand": "PocketBook", "Model": "Verse Pro 634", "Storage": 16, "ScreenSize": 6.0}	1	1
274	Квадрокоптер DJI Mini 4 Pro Fly More Combo (DJI RC 2)	Легкий дрон вагою до 249г із всеспрямованим виявленням перешкод та зйомкою 4K/60fps HDR.	97	1	31	9	\N	\N	2026-10-04 21:22:20.623595+00	{"Brand": "DJI", "Model": "Mini 4 Pro"}	1	1
275	Квадрокоптер DJI Air 3 Fly More Combo	Дрон із двома основними камерами (ширококутна та 3x телефото) і часом польоту до 46 хвилин.	90	1	31	9	\N	\N	2026-10-04 21:22:20.623595+00	{"Brand": "DJI", "Model": "Air 3"}	1	1
277	Панорамна екшн-камера Insta360 X3	360-градусна камера зі зйомкою 5.7K, сенсорним екраном 2.29" та водонепроникністю до 10м.	89	1	32	9	\N	\N	2026-10-05 08:08:08.686322+00	{"Brand": "Insta360", "Model": "X3"}	1	1
278	Бездзеркальний фотоапарат Sony Alpha a7 IV Body	Повнокадрова камера з матрицею 33 Мп, фокусуванням за очима реального часу та відео 4K 60p.	94	1	33	9	\N	\N	2026-10-05 08:08:08.686322+00	{"Brand": "Sony", "Model": "Alpha a7 IV"}	1	1
279	Бездзеркальна камера Canon EOS R6 Mark II Body	Універсальна повнокадрова камера зі швидкістю серійної зйомки до 40 к/с та 4K 60p без кропу.	90	1	33	9	\N	\N	2026-10-05 08:08:08.686322+00	{"Brand": "Canon", "Model": "EOS R6 Mark II"}	1	1
286	Мультипіч Xiaomi Smart Air Fryer Pro 4L White	Розумний аерогриль із прозорим віконцем, OLED-дисплеєм та дистанційним керуванням зі смартфона.	95	1	42	9	\N	\N	2026-10-05 08:08:08.686322+00	{"Brand": "Xiaomi", "Model": "Smart Air Fryer Pro 4L"}	1	1
287	Мультипіч-аерогриль Tefal Easy Fry & Grill XXL EY801D15	Універсальна мультипіч об'ємом 6.5 л із чавунною решіткою для гриля та чашею із розділювачем.	88	1	42	9	\N	\N	2026-10-05 08:08:08.686322+00	{"Brand": "Tefal", "Model": "Easy Fry & Grill XXL"}	1	1
288	Автоматична кавомашина DeLonghi Magnifica S ECAM 22.110.B	Класична кавомашина зі сталевим кавомолом, вибором міцності та ручним капучинатором.	96	1	39	9	\N	\N	2026-10-05 08:08:08.686322+00	{"Brand": "DeLonghi", "Model": "Magnifica S"}	1	1
289	Кавомашина Philips Series 3300 LatteGo EP3347/90	Сучасна кавомашина з автоматичною системою подачі молока LatteGo та кольоровим сенсорним екраном.	92	1	39	9	\N	\N	2026-10-05 08:08:08.686322+00	{"Brand": "Philips", "Model": "Series 3300 LatteGo"}	1	1
290	Робот-пилосос Roborock S8 Black	Флагманський робот для сухого та вологого прибирання із силою всмоктування 6000 Па та подвійними щітками.	94	1	37	9	\N	\N	2026-10-05 08:08:08.686322+00	{"Brand": "Roborock", "Model": "S8"}	1	1
291	Робот-пилосос Dreame Bot L10s Ultra зі станцією самоочищення	Повністю автономний робот із сушінням ганчірок гарячим повітрям та автоматичним доливом води.	91	1	37	9	\N	\N	2026-10-05 08:08:08.686322+00	{"Brand": "Dreame", "Model": "Bot L10s Ultra"}	1	1
292	Акумуляторний пилосос Dyson V15 Detect Absolute	Потужний бездротовий пилосос із лазерним підсвічуванням мікроскопічного пилу та лазерним датчиком.	97	1	45	9	\N	\N	2026-10-05 08:08:08.686322+00	{"Brand": "Dyson", "Model": "V15 Detect Absolute"}	1	1
196	Apple iPhone 15 Pro 128GB Natural Titanium	Флагманський смартфон Apple у титановому корпусі з чипом A17 Pro.	95	1	1	9	\N	\N	2026-10-04 18:38:31.979792+00	{"RAM": 8, "Brand": "Apple", "Has5G": true, "Model": "iPhone 15 Pro", "Storage": 128, "Processor": "A17 Pro", "ScreenSize": 6.1, "RefreshRate": 120}	1	1
197	Samsung Galaxy S24 Ultra 12/512GB	Преміальний смартфон із підтримкою S Pen, штучним інтелектом Galaxy AI та камерою 200 Мп.	92	1	1	9	\N	\N	2026-10-04 18:38:31.979792+00	{"RAM": 12, "Brand": "Samsung", "Has5G": true, "Model": "Galaxy S24 Ultra", "Storage": 512, "Processor": "Snapdragon 8 Gen 3", "ScreenSize": 6.8, "RefreshRate": 120}	1	1
198	Apple MacBook Pro 14 M3 16/512GB	Потужний ноутбук для розробки, обробки відео та професійних задач із дисплеєм Liquid Retina XDR.	88	1	2	9	\N	\N	2026-10-04 18:38:31.979792+00	{"RAM": 16, "Brand": "Apple", "Model": "MacBook Pro 14", "Storage": 512, "Processor": "Apple M3", "ScreenSize": 14.2, "RefreshRate": 120}	1	1
199	ASUS ROG Zephyrus G16 OLED	Тонкий ігровий ноутбук із дисплеєм OLED 240 Гц, процесором Intel Core Ultra 9 та відеокартою RTX 4070.	85	1	2	9	\N	\N	2026-10-04 18:38:31.979792+00	{"RAM": 32, "Brand": "ASUS", "Model": "ROG Zephyrus G16", "Storage": 1024, "Processor": "Intel Core Ultra 9", "ScreenSize": 16.0, "RefreshRate": 240}	1	1
200	Apple iPad Air 11 M2 128GB Wi-Fi	Легкий та потужний планшет із чипом M2 для роботи, малювання та розваг.	80	1	3	9	\N	\N	2026-10-04 18:38:31.979792+00	{"RAM": 8, "Brand": "Apple", "Model": "iPad Air 11", "Storage": 128, "Processor": "Apple M2", "ScreenSize": 11.0}	1	1
201	Xiaomi Pad 6 8/256GB	Продуктивний планшет із екраном 144 Гц та металевим корпусом для щоденного використання.	78	1	3	9	\N	\N	2026-10-04 18:38:31.979792+00	{"RAM": 8, "Brand": "Xiaomi", "Model": "Pad 6", "Storage": 256, "Processor": "Snapdragon 870", "ScreenSize": 11.0, "RefreshRate": 144}	1	1
202	Apple Watch Series 9 GPS 45mm	Смартгодинник із чипом S9, яскравішим екраном Retina та підтримкою жесту Double Tap.	89	1	4	9	\N	\N	2026-10-04 18:38:31.979792+00	{"Brand": "Apple", "Model": "Series 9", "ScreenSize": 1.9}	1	1
203	Samsung Galaxy Watch 6 Classic 47mm	Смартгодинник із поворотним безелем, аналізом складу тіла та відстеженням фаз сну.	81	1	4	9	\N	\N	2026-10-04 18:38:31.979792+00	{"Brand": "Samsung", "Model": "Galaxy Watch 6 Classic", "ScreenSize": 1.5}	1	1
204	Apple AirPods Pro 2 (USB-C)	Бездротові навушники з чудовим шумозаглушенням, адаптивним аудіо та кейсом USB-C.	94	1	5	9	\N	\N	2026-10-04 18:38:31.979792+00	{"Brand": "Apple", "Model": "AirPods Pro 2"}	1	1
205	Sony WH-1000XM5 Black	Повнорозмірні бездротові навушники з провідним активним шумозаглушенням ANC.	90	1	5	9	\N	\N	2026-10-04 18:38:31.979792+00	{"Brand": "Sony", "Model": "WH-1000XM5"}	1	1
206	JBL Charge 5 Black	Захищена портативна акустична система з потужним басом та функцією зарядки гаджетів.	87	1	6	9	\N	\N	2026-10-04 18:38:31.979792+00	{"Brand": "JBL", "Model": "Charge 5"}	1	1
207	Marshall Emberton II Black and Brass	Портативна колонка у стилі ретро з об'ємним звучанням 360° та 30+ годинами автономності.	83	1	6	9	\N	\N	2026-10-04 18:38:31.979792+00	{"Brand": "Marshall", "Model": "Emberton II"}	1	1
208	LG OLED 55 C3 Smart TV 4K	Телевізор OLED з контрастним зображенням, підтримкою 120 Гц, HDMI 2.1 та Dolby Atmos.	86	1	7	9	\N	\N	2026-10-04 18:38:31.979792+00	{"Brand": "LG", "Model": "OLED55C3", "ScreenSize": 55.0, "RefreshRate": 120}	1	1
209	Samsung 55 QLED 4K Smart TV	4K телевізор з квантовими точками, стильним тонким корпусом AirSlim та Smart TV.	82	1	7	9	\N	\N	2026-10-04 18:38:31.979792+00	{"Brand": "Samsung", "Model": "QE55Q60C", "ScreenSize": 55.0, "RefreshRate": 60}	1	1
210	Sony PlayStation 5 Slim Disc Edition	Компактна ігрова консоль з дисководом, накопичувачем на 1 ТБ та геймпадом DualSense.	96	1	8	9	\N	\N	2026-10-04 18:38:31.979792+00	{"Brand": "Sony", "Model": "PlayStation 5 Slim", "Storage": 1024}	1	1
211	Nintendo Switch OLED Model White	Портативна консоль із яскравим 7-дюймовим OLED-екраном для ігор удома та в дорозі.	84	1	8	9	\N	\N	2026-10-04 18:38:31.979792+00	{"Brand": "Nintendo", "Model": "Switch OLED", "Storage": 64, "ScreenSize": 7.0}	1	1
212	ПК ARTLINE Gaming X77 (i7-13700F / RTX 4070)	Потужний системний блок для комфортного 2K геймінгу, відеомонтажу та 3D-моделювання.	79	1	9	9	\N	\N	2026-10-04 18:38:31.979792+00	{"RAM": 32, "Brand": "ARTLINE", "Model": "Gaming X77", "Storage": 1024, "Processor": "Intel Core i7-13700F"}	1	1
213	Ігровий ПК COBRA Advanced (Ryzen 5 5600 / RTX 3060)	Оптимальна збірка для сучасних ігор у Full HD з ефективною системою охолодження.	81	1	9	9	\N	\N	2026-10-04 18:38:31.979792+00	{"RAM": 16, "Brand": "COBRA", "Model": "Advanced", "Storage": 500, "Processor": "AMD Ryzen 5 5600"}	1	1
214	Монітор 27 ASUS ROG Swift OLED 240Hz	Ігровий QHD OLED монітор із відгуком 0.03 мс та високою частотою оновлення 240 Гц.	87	1	10	9	\N	\N	2026-10-04 18:38:31.979792+00	{"Brand": "ASUS", "Model": "ROG Swift PG27AQDM", "ScreenSize": 27.0, "RefreshRate": 240}	1	1
215	Вигнутий монітор 27 Samsung Odyssey G5 165Hz	Вигнутий ігровий WQHD монітор з ефектом занурення 1000R та підтримкою AMD FreeSync.	84	1	10	9	\N	\N	2026-10-04 18:38:31.979792+00	{"Brand": "Samsung", "Model": "Odyssey G5", "ScreenSize": 27.0, "RefreshRate": 165}	1	1
216	Процесор Intel Core i7-14700K s-1700 Box	20-ядерний процесор Intel 14-го покоління для потужних ігрових та робочих станцій.	89	1	11	9	\N	\N	2026-10-04 20:40:26.725736+00	{"Brand": "Intel", "Model": "Core i7-14700K", "Processor": "i7-14700K"}	1	1
217	Процесор AMD Ryzen 7 7800X3D s-AM5	Найкращий ігровий процесор з технологією 3D V-Cache для максимального FPS в іграх.	96	1	11	9	\N	\N	2026-10-04 20:40:26.725736+00	{"Brand": "AMD", "Model": "Ryzen 7 7800X3D", "Processor": "Ryzen 7 7800X3D"}	1	1
218	Відеокарта ASUS ROG Strix GeForce RTX 4070 Ti SUPER 16GB	Потужна відеокарта з 16 ГБ відеопам’яті, просунутим охолодженням та підсвічуванням Aura Sync.	91	1	12	9	\N	\N	2026-10-04 20:40:26.725736+00	{"RAM": 16, "Brand": "ASUS", "Model": "ROG Strix RTX 4070 Ti Super"}	1	1
219	Відеокарта Gigabyte GeForce RTX 4060 Eagle OC 8GB	Оптимальне рішення для геймінгу в роздільній здатності Full HD з підтримкою DLSS 3.	87	1	12	9	\N	\N	2026-10-04 20:40:26.725736+00	{"RAM": 8, "Brand": "Gigabyte", "Model": "RTX 4060 Eagle OC"}	1	1
220	Материнська плата MSI MAG Z790 TOMAHAWK WIFI	Надійна ігрова материнська плата під сокет LGA1700 із підтримкою DDR5 та Wi-Fi 6E.	83	1	13	9	\N	\N	2026-10-04 20:40:26.725736+00	{"Brand": "MSI", "Model": "MAG Z790 TOMAHAWK"}	1	1
221	Материнська плата ASUS TUF GAMING B650-PLUS WIFI	Материнська плата для процесорів AMD Ryzen 7000/8000/9000 із потужним VRM.	85	1	13	9	\N	\N	2026-10-04 20:40:26.725736+00	{"Brand": "ASUS", "Model": "TUF GAMING B650-PLUS"}	1	1
222	Комплект оперативної пам’яті Kingston Fury DDR5 32GB (2x16GB) 6000MHz	Високошвидкісний комплект комплектів пам’яті DDR5 з підтримкою EXPO та XMP 3.0.	88	1	14	9	\N	\N	2026-10-04 20:40:26.725736+00	{"RAM": 32, "Brand": "Kingston", "Model": "Fury Beast", "RefreshRate": 6000}	1	1
223	ОЗП Corsair Vengeance RGB DDR5 32GB (2x16GB) 6400MHz	Оперативна пам’ять преміум-класу з яскравим динамічним RGB-підсвічуванням.	84	1	14	9	\N	\N	2026-10-04 20:40:26.725736+00	{"RAM": 32, "Brand": "Corsair", "Model": "Vengeance RGB", "RefreshRate": 6400}	1	1
224	SSD накопичувач Samsung 990 PRO NVMe M.2 2TB	Флагманський NVMe SSD накопичувач зі швидкістю читання до 7450 МБ/с.	93	1	15	9	\N	\N	2026-10-04 20:40:26.725736+00	{"Brand": "Samsung", "Model": "990 PRO", "Storage": 2048}	1	1
225	SSD накопичувач Kingston NV2 1TB NVMe M.2	Надійний і доступний SSD для прискорення завантаження системи та ігор.	90	1	15	9	\N	\N	2026-10-04 20:40:26.725736+00	{"Brand": "Kingston", "Model": "NV2", "Storage": 1024}	1	1
226	Жорсткий диск Western Digital Red Pro 8TB 3.5 SATA III	Спеціалізований HDD диск для цілодобової роботи в сітьових NAS-накопичувачах та серверах.	76	1	16	9	\N	\N	2026-10-04 20:40:26.725736+00	{"Brand": "Western Digital", "Model": "Red Pro", "Storage": 8192}	1	1
227	Жорсткий диск Seagate Barracuda 2TB 7200rpm 3.5	Класичний накопичувач для зберігання фото, відео та бекапів на настільному ПК.	81	1	16	9	\N	\N	2026-10-04 20:40:26.725736+00	{"Brand": "Seagate", "Model": "Barracuda", "Storage": 2048}	1	1
228	Блок живлення be quiet! Dark Power 13 1000W 80 Plus Titanium	Безшумний та надефективний блок живлення із сертифікатом 80 Plus Titanium та підтримкою ATX 3.0.	86	1	17	9	\N	\N	2026-10-04 20:40:26.725736+00	{"Brand": "be quiet!", "Model": "Dark Power 13"}	1	1
229	Блок живлення Chieftec Proton 750W 80 Plus Bronze	Надійний модульний блок живлення для ігрових комп’ютерів середнього рівня.	82	1	17	9	\N	\N	2026-10-04 20:40:26.725736+00	{"Brand": "Chieftec", "Model": "Proton 750W"}	1	1
230	Корпус для ПК NZXT H9 Flow White	Двокамерний корпус із панорамним загартованим склом та чудовою вентиляцією.	91	1	18	9	\N	\N	2026-10-04 20:40:26.725736+00	{"Brand": "NZXT", "Model": "H9 Flow"}	1	1
231	Корпус для ПК Deepcool CC560 ARGB Black	Місткий корпус формата Mid-Tower із 4 встановленими вентиляторами з ARGB підсвічуванням.	87	1	18	9	\N	\N	2026-10-04 20:40:26.725736+00	{"Brand": "Deepcool", "Model": "CC560"}	1	1
232	Система рідинного охолодження ARCTIC Liquid Freezer III 360 Black	Потужна трисекційна СРО для охолодження топових процесорів Intel та AMD.	94	1	19	9	\N	\N	2026-10-04 20:40:26.725736+00	{"Brand": "ARCTIC", "Model": "Liquid Freezer III 360"}	1	1
233	Процесорний кулер Deepcool AK620 Zero Dark	Двобаштовий бавовняний кулер з 6 тепловими трубками та двома 120мм вентиляторами.	89	1	19	9	\N	\N	2026-10-04 20:40:26.725736+00	{"Brand": "Deepcool", "Model": "AK620"}	1	1
234	Механічна клавіатура Logitech G PRO X TKL Lightspeed	Бездротова кіберспортивна клавіатура без цифрового блоку із перемикачами GX Red Linear.	91	1	20	9	\N	\N	2026-10-04 20:40:26.725736+00	{"Brand": "Logitech", "Model": "G PRO X TKL"}	1	1
235	Механічна клавіатура Keychron K2 V2 Hot-Swap RGB	Компактна 75% бездротова механічна клавіатура для Mac та Windows з можливістю гарячої заміни свічів.	86	1	20	9	\N	\N	2026-10-04 20:40:26.725736+00	{"Brand": "Keychron", "Model": "K2 V2"}	1	1
293	Акумуляторний пилосос Bosch Unlimited 7 BBS711W	Зручний бездротовий пилосос із гнучкою трубою Flex Tube та змінними акумуляторами Power for ALL.	85	1	45	9	\N	\N	2026-10-05 08:08:08.686322+00	{"Brand": "Bosch", "Model": "Unlimited 7"}	1	1
294	Фен для волосся Dyson Supersonic HD07 Nickel/Copper	Інноваційний фен із інтелектуальним контролем температури для захисту естетичного блиску волосся.	98	1	47	9	\N	\N	2026-10-05 08:08:08.686322+00	{"Brand": "Dyson", "Model": "Supersonic HD07"}	1	1
295	Професійний фен BaByliss PRO Veneziano HQ BAB6960IE	Потужний італійський професійний фен 2200 Вт із функцією іонізації та високою швидкістю потоку.	87	1	47	9	\N	\N	2026-10-05 08:08:08.686322+00	{"Brand": "BaByliss", "Model": "PRO Veneziano HQ"}	1	1
296	Електробритва Braun Series 9 Pro+ 9517s	Преміальна сіткова електробритва із плаваючою голівкою ProHead та тримером ProTrimmer.	92	1	46	9	\N	\N	2026-10-05 10:14:31.550596+00	{"Brand": "Braun", "Model": "Series 9 Pro+"}	1	1
297	Універсальний тример Philips OneBlade Pro 360 QP6551/15	Гібридний стайлер для підрівнювання, створення чітких контурів та гоління будь-якої довжини волосся.	95	1	46	9	\N	\N	2026-10-05 10:14:31.550596+00	{"Brand": "Philips", "Model": "OneBlade Pro 360"}	1	1
298	Електрична зубна щітка Oral-B iO Series 9 Black Onyx	Розумна щітка з магнітною технологією iO, кольоровим дисплеєм та 7 режимами чищення.	91	1	48	9	\N	\N	2026-10-05 10:14:31.550596+00	{"Brand": "Oral-B", "Model": "iO Series 9"}	1	1
299	Звукова зубна щітка Philips Sonicare DiamondClean 9000 HX9911/88	Звукова щітка з датчиком тиску, склянкою для зарядки та відстеженням через додаток.	89	1	48	9	\N	\N	2026-10-05 10:14:31.550596+00	{"Brand": "Philips", "Model": "Sonicare DiamondClean 9000"}	1	1
308	Пральна машина Bosch Serie 6 WAN28263UA 8kg	Надійна пральна машина з безшумним інверторним двигуном EcoSilence Drive та функцією SpeedPerfect.	92	1	44	9	\N	\N	2026-10-05 10:14:31.550596+00	{"Brand": "Bosch", "Model": "Serie 6"}	1	1
309	Вузька пральна машина LG AI DD F2V5HS0W 7kg	Пральна машина із системою інтелектуального визначення типів тканини AI DD та функцією пари Steam.	94	1	44	9	\N	\N	2026-10-05 10:14:31.550596+00	{"Brand": "LG", "Model": "F2V5HS0W"}	1	1
310	Двокамерний холодильник Samsung RL38T600FSA/UA 385L	Холодильник із технологією SpaceMax (більше корисного об'єму) та системою охолодження All-Around Cooling.	91	1	43	9	\N	\N	2026-10-05 10:14:31.550596+00	{"Brand": "Samsung", "Model": "RL38T600FSA"}	1	1
311	Холодильник Liebherr BluPerformance CNef 4335	Преміальний німецький холодильник з окремими контурами охолодження DuoCooling та NoFrost.	89	1	43	9	\N	\N	2026-10-05 10:14:31.550596+00	{"Brand": "Liebherr", "Model": "CNef 4335"}	1	1
\.


ALTER TABLE public."Advertisements" ENABLE TRIGGER ALL;

--
-- Data for Name: AdvertisementVariants; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."AdvertisementVariants" DISABLE TRIGGER ALL;

COPY public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") FROM stdin;
25	196	42999.00	45999.00	10	{"Color": "Natural Titanium"}
26	196	42999.00	45999.00	5	{"Color": "Black Titanium"}
27	197	53999.00	57999.00	8	{"Color": "Titanium Gray"}
28	197	53999.00	57999.00	4	{"Color": "Titanium Black"}
29	198	74999.00	79999.00	6	{"Color": "Space Grey"}
30	198	74999.00	79999.00	3	{"Color": "Silver"}
31	199	92999.00	98999.00	4	{"Color": "Eclipse Gray"}
32	200	28999.00	30999.00	12	{"Color": "Space Gray"}
33	200	28999.00	30999.00	7	{"Color": "Blue"}
34	201	13999.00	15499.00	15	{"Color": "Gravity Gray"}
35	202	18999.00	20499.00	10	{"Color": "Midnight"}
36	202	18999.00	20499.00	6	{"Color": "Starlight"}
37	203	14499.00	16299.00	8	{"Color": "Black"}
38	203	14499.00	16299.00	5	{"Color": "Silver"}
39	204	9999.00	10999.00	20	{"Color": "White"}
40	205	14999.00	16999.00	9	{"Color": "Black"}
41	205	14999.00	16999.00	4	{"Color": "Silver"}
42	206	6499.00	7199.00	14	{"Color": "Black"}
43	206	6499.00	7199.00	8	{"Color": "Red"}
44	207	7299.00	7999.00	7	{"Color": "Black and Brass"}
45	208	56999.00	62999.00	3	{"Color": "Dark Silver"}
46	209	28999.00	31999.00	6	{"Color": "Black"}
47	210	22999.00	24999.00	11	{"Color": "White"}
48	211	13499.00	14999.00	9	{"Color": "White"}
49	212	68999.00	73999.00	4	{"Color": "Black"}
50	213	29999.00	32999.00	7	{"Color": "Black"}
51	214	41999.00	45999.00	3	{"Color": "Black"}
52	215	10999.00	12499.00	12	{"Color": "Black"}
53	216	18499.00	19999.00	8	{"Color": "Box"}
54	217	18999.00	20499.00	5	{"Color": "Tray"}
55	218	45999.00	48999.00	4	{"Color": "Black"}
56	219	14999.00	16299.00	12	{"Color": "Black"}
57	220	12899.00	13999.00	6	{"Color": "Black"}
58	221	8999.00	9799.00	9	{"Color": "Black"}
59	222	5499.00	5999.00	15	{"Color": "Black"}
60	223	6299.00	6899.00	8	{"Color": "White"}
61	224	7999.00	8699.00	10	{"Color": "Black"}
62	225	2699.00	2999.00	25	{"Color": "Blue"}
63	226	9499.00	10299.00	5	{"Color": "Silver"}
64	227	2499.00	2799.00	14	{"Color": "Silver"}
65	228	12999.00	13999.00	4	{"Color": "Black"}
66	229	3299.00	3699.00	11	{"Color": "Black"}
67	230	7899.00	8499.00	6	{"Color": "White"}
68	231	2499.00	2799.00	18	{"Color": "Black"}
69	232	4899.00	5399.00	7	{"Color": "Black"}
70	233	2899.00	3199.00	12	{"Color": "Black"}
71	234	8499.00	9299.00	6	{"Color": "Black"}
72	234	8499.00	9299.00	3	{"Color": "White"}
73	235	4199.00	4599.00	9	{"Color": "Grey"}
95	256	3299.00	3699.00	10	{"Color": "Black"}
96	257	5899.00	6499.00	6	{"Color": "Black"}
97	258	6299.00	6899.00	12	{"Color": "Black"}
98	258	6299.00	6899.00	4	{"Color": "White"}
99	259	17999.00	19499.00	5	{"Color": "Dark Grey"}
100	260	11499.00	12499.00	7	{"Color": "Black"}
101	261	2999.00	3399.00	18	{"Color": "Black"}
102	262	4299.00	4699.00	14	{"Color": "Black"}
103	262	4299.00	4699.00	8	{"Color": "Blue"}
104	263	7899.00	8499.00	6	{"Color": "Black"}
105	264	5499.00	5999.00	11	{"Color": "Black"}
106	265	2899.00	3299.00	15	{"Color": "Black"}
107	266	2299.00	2599.00	20	{"Color": "Space Grey"}
108	267	2499.00	2799.00	9	{"Color": "White"}
109	268	999.00	1199.00	25	{"Color": "Clear"}
110	269	2899.00	3199.00	7	{"Color": "Black/Grey"}
111	270	699.00	849.00	30	{"Color": "Transparent"}
112	271	899.00	999.00	20	{"Color": "Transparent"}
113	272	6499.00	6999.00	10	{"Color": "Black"}
114	272	6499.00	6999.00	5	{"Color": "Denim"}
115	273	7199.00	7799.00	8	{"Color": "Red"}
116	274	46999.00	49999.00	4	{"Color": "Gray"}
117	275	58999.00	62999.00	3	{"Color": "Gray"}
118	276	16999.00	18499.00	8	{"Color": "Black"}
119	277	18999.00	20499.00	5	{"Color": "Black"}
120	278	98999.00	104999.00	3	{"Color": "Black"}
121	279	94999.00	99999.00	4	{"Color": "Black"}
122	280	44999.00	47999.00	6	{"Color": "Black"}
123	281	22999.00	24999.00	9	{"Color": "Black"}
124	282	22499.00	24199.00	5	{"Color": "Black"}
125	283	8299.00	8999.00	8	{"Color": "Gray"}
126	284	5199.00	5699.00	11	{"Color": "Black"}
127	285	10899.00	11899.00	4	{"Color": "White"}
128	286	3499.00	3899.00	16	{"Color": "White"}
129	287	6999.00	7699.00	7	{"Color": "Stainless Steel"}
130	288	13499.00	14999.00	12	{"Color": "Black"}
131	289	21999.00	23999.00	6	{"Color": "Black"}
132	290	21999.00	23999.00	8	{"Color": "Black"}
133	290	21999.00	23999.00	5	{"Color": "White"}
134	291	32999.00	35999.00	4	{"Color": "White"}
135	292	28999.00	30999.00	9	{"Color": "Yellow/Nickel"}
136	293	12499.00	13799.00	7	{"Color": "White"}
137	294	17999.00	19499.00	15	{"Color": "Nickel/Copper"}
138	294	17999.00	19499.00	8	{"Color": "Fuchsia"}
139	295	2799.00	3199.00	12	{"Color": "Black"}
140	296	14999.00	16499.00	7	{"Color": "Silver"}
141	297	3299.00	3699.00	15	{"Color": "Black/Green"}
142	298	11999.00	12999.00	8	{"Color": "Black Onyx"}
143	299	8499.00	9299.00	10	{"Color": "Pink"}
144	299	8499.00	9299.00	5	{"Color": "Black"}
145	300	3299.00	3699.00	18	{"Color": "White"}
146	301	2199.00	2499.00	12	{"Color": "White"}
147	302	18999.00	20999.00	6	{"Color": "White"}
148	303	21499.00	23499.00	8	{"Color": "White"}
149	304	8299.00	8999.00	11	{"Color": "White"}
150	305	14999.00	16299.00	4	{"Color": "White"}
151	306	15999.00	17499.00	5	{"Color": "Dark Blue"}
152	307	4599.00	4999.00	14	{"Color": "Black/Rose Gold"}
153	308	19999.00	21999.00	7	{"Color": "White"}
154	309	17499.00	18999.00	9	{"Color": "White"}
155	310	23999.00	25999.00	6	{"Color": "Silver"}
156	311	34999.00	37999.00	3	{"Color": "Inox"}
157	312	21999.00	23999.00	5	{"Color": "Fully Integrated"}
158	313	15499.00	16999.00	8	{"Color": "Silver"}
159	314	20999.00	22999.00	6	{"Color": "Clean Black"}
160	315	14299.00	15799.00	10	{"Color": "Black"}
\.


ALTER TABLE public."AdvertisementVariants" ENABLE TRIGGER ALL;

--
-- Data for Name: AttributeDictionaries; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."AttributeDictionaries" DISABLE TRIGGER ALL;

COPY public."AttributeDictionaries" ("Id", "Code") FROM stdin;
5	colors
\.


ALTER TABLE public."AttributeDictionaries" ENABLE TRIGGER ALL;

--
-- Data for Name: AttributeDefinitions; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."AttributeDefinitions" DISABLE TRIGGER ALL;

COPY public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") FROM stdin;
9	1	Brand	String	t	0	0	f	f	\N
10	1	Model	String	t	0	0	f	f	\N
11	1	Processor	String	f	0	0	f	f	\N
12	1	RAM	Integer	f	1	64	f	f	\N
13	1	Storage	Integer	f	1	2048	f	f	\N
14	1	ScreenSize	Decimal	f	4	20	f	f	\N
15	1	RefreshRate	Integer	f	60	240	f	f	\N
16	1	Has5G	Boolean	f	0	0	f	f	\N
18	2	Brand	String	t	0	0	f	f	\N
19	2	Model	String	t	0	0	f	f	\N
20	2	Processor	String	f	0	0	f	f	\N
21	2	RAM	Integer	f	1	128	f	f	\N
22	2	Storage	Integer	f	1	4096	f	f	\N
23	2	ScreenSize	Decimal	f	10	20	f	f	\N
24	2	RefreshRate	Integer	f	60	360	f	f	\N
26	3	Brand	String	t	0	0	f	f	\N
27	3	Model	String	t	0	0	f	f	\N
28	3	Processor	String	f	0	0	f	f	\N
29	3	RAM	Integer	f	1	32	f	f	\N
30	3	Storage	Integer	f	1	2048	f	f	\N
31	3	ScreenSize	Decimal	f	7	15	f	f	\N
32	3	RefreshRate	Integer	f	60	144	f	f	\N
34	4	Brand	String	t	0	0	f	f	\N
35	4	Model	String	t	0	0	f	f	\N
36	4	ScreenSize	Decimal	f	1	3	f	f	\N
38	5	Brand	String	t	0	0	f	f	\N
39	5	Model	String	t	0	0	f	f	\N
41	6	Brand	String	t	0	0	f	f	\N
42	6	Model	String	t	0	0	f	f	\N
44	7	Brand	String	t	0	0	f	f	\N
45	7	Model	String	t	0	0	f	f	\N
46	7	ScreenSize	Decimal	f	24	100	f	f	\N
47	7	RefreshRate	Integer	f	50	240	f	f	\N
49	8	Brand	String	t	0	0	f	f	\N
50	8	Model	String	t	0	0	f	f	\N
51	8	Storage	Integer	f	1	2048	f	f	\N
52	8	ScreenSize	Decimal	f	5	10	f	f	\N
54	9	Brand	String	t	0	0	f	f	\N
55	9	Model	String	t	0	0	f	f	\N
56	9	Processor	String	f	0	0	f	f	\N
57	9	RAM	Integer	f	1	256	f	f	\N
58	9	Storage	Integer	f	1	8192	f	f	\N
60	10	Brand	String	t	0	0	f	f	\N
61	10	Model	String	t	0	0	f	f	\N
62	10	ScreenSize	Decimal	f	15	60	f	f	\N
63	10	RefreshRate	Integer	f	60	360	f	f	\N
17	1	Color	String	f	\N	\N	t	f	\N
25	2	Color	String	f	\N	\N	t	f	\N
33	3	Color	String	f	\N	\N	t	f	\N
37	4	Color	String	f	\N	\N	t	f	\N
40	5	Color	String	f	\N	\N	t	f	\N
43	6	Color	String	f	\N	\N	t	f	\N
48	7	Color	String	f	\N	\N	t	f	\N
53	8	Color	String	f	\N	\N	t	f	\N
59	9	Color	String	f	\N	\N	t	f	\N
64	10	Color	String	f	\N	\N	t	f	\N
65	11	Color	String	f	\N	\N	t	f	\N
66	12	Color	String	f	\N	\N	t	f	\N
67	13	Color	String	f	\N	\N	t	f	\N
68	14	Color	String	f	\N	\N	t	f	\N
69	15	Color	String	f	\N	\N	t	f	\N
70	16	Color	String	f	\N	\N	t	f	\N
71	17	Color	String	f	\N	\N	t	f	\N
72	18	Color	String	f	\N	\N	t	f	\N
73	19	Color	String	f	\N	\N	t	f	\N
74	20	Color	String	f	\N	\N	t	f	\N
75	21	Color	String	f	\N	\N	t	f	\N
76	22	Color	String	f	\N	\N	t	f	\N
77	23	Color	String	f	\N	\N	t	f	\N
78	24	Color	String	f	\N	\N	t	f	\N
79	25	Color	String	f	\N	\N	t	f	\N
80	26	Color	String	f	\N	\N	t	f	\N
81	27	Color	String	f	\N	\N	t	f	\N
82	28	Color	String	f	\N	\N	t	f	\N
83	29	Color	String	f	\N	\N	t	f	\N
84	30	Color	String	f	\N	\N	t	f	\N
85	31	Color	String	f	\N	\N	t	f	\N
86	32	Color	String	f	\N	\N	t	f	\N
87	33	Color	String	f	\N	\N	t	f	\N
88	34	Color	String	f	\N	\N	t	f	\N
89	35	Color	String	f	\N	\N	t	f	\N
90	36	Color	String	f	\N	\N	t	f	\N
91	37	Color	String	f	\N	\N	t	f	\N
92	38	Color	String	f	\N	\N	t	f	\N
93	39	Color	String	f	\N	\N	t	f	\N
94	40	Color	String	f	\N	\N	t	f	\N
95	41	Color	String	f	\N	\N	t	f	\N
96	42	Color	String	f	\N	\N	t	f	\N
97	43	Color	String	f	\N	\N	t	f	\N
98	44	Color	String	f	\N	\N	t	f	\N
99	45	Color	String	f	\N	\N	t	f	\N
100	46	Color	String	f	\N	\N	t	f	\N
101	47	Color	String	f	\N	\N	t	f	\N
102	48	Color	String	f	\N	\N	t	f	\N
103	49	Color	String	f	\N	\N	t	f	\N
104	50	Color	String	f	\N	\N	t	f	\N
\.


ALTER TABLE public."AttributeDefinitions" ENABLE TRIGGER ALL;

--
-- Data for Name: AttributeDefinitionTranslations; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."AttributeDefinitionTranslations" DISABLE TRIGGER ALL;

COPY public."AttributeDefinitionTranslations" ("Id", "Name", "TranslatableId", "Language") FROM stdin;
\.


ALTER TABLE public."AttributeDefinitionTranslations" ENABLE TRIGGER ALL;

--
-- Data for Name: AttributeDictionaryValues; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."AttributeDictionaryValues" DISABLE TRIGGER ALL;

COPY public."AttributeDictionaryValues" ("Id", "DictionaryId", "Code", "Value") FROM stdin;
5	5	natural-titanium	Natural Titanium
6	5	black-titanium	Black Titanium
7	5	titanium-gray	Titanium Gray
8	5	titanium-black	Titanium Black
9	5	space-grey	Space Grey
10	5	silver	Silver
11	5	eclipse-gray	Eclipse Gray
12	5	space-gray	Space Gray
13	5	blue	Blue
14	5	gravity-gray	Gravity Gray
15	5	midnight	Midnight
16	5	starlight	Starlight
17	5	black	Black
18	5	white	White
19	5	red	Red
20	5	black-and-brass	Black and Brass
21	5	dark-silver	Dark Silver
\.


ALTER TABLE public."AttributeDictionaryValues" ENABLE TRIGGER ALL;

--
-- Data for Name: AttributeDictionaryValueTranslations; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."AttributeDictionaryValueTranslations" DISABLE TRIGGER ALL;

COPY public."AttributeDictionaryValueTranslations" ("Id", "Name", "TranslatableId", "Language") FROM stdin;
\.


ALTER TABLE public."AttributeDictionaryValueTranslations" ENABLE TRIGGER ALL;

--
-- Data for Name: Avatars; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."Avatars" DISABLE TRIGGER ALL;

COPY public."Avatars" ("Id", "UserId", "Url", "PublicId") FROM stdin;
15	3	https://res.cloudinary.com/dn3ne3ci6/image/upload/v1787341505/ShagOx/pzjeynelqmrvgfbm6lwz.jpg	ShagOx/pzjeynelqmrvgfbm6lwz
26	7	https://res.cloudinary.com/dn3ne3ci6/image/upload/v1790707075/ShagOx/kjm8vrcd14hqfbhfevdh.jpg	ShagOx/kjm8vrcd14hqfbhfevdh
\.


ALTER TABLE public."Avatars" ENABLE TRIGGER ALL;

--
-- Data for Name: BasketAttributes; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."BasketAttributes" DISABLE TRIGGER ALL;

COPY public."BasketAttributes" ("Id", "AttributeDefinitionId", "Order") FROM stdin;
\.


ALTER TABLE public."BasketAttributes" ENABLE TRIGGER ALL;

--
-- Data for Name: Baskets; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."Baskets" DISABLE TRIGGER ALL;

COPY public."Baskets" ("Id", "UserId") FROM stdin;
5	7
\.


ALTER TABLE public."Baskets" ENABLE TRIGGER ALL;

--
-- Data for Name: BasketItems; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."BasketItems" DISABLE TRIGGER ALL;

COPY public."BasketItems" ("Id", "BasketId", "AdvertisementVariantId", "Quantity") FROM stdin;
66	5	100	1
67	5	99	1
68	5	105	1
69	5	96	1
70	5	104	1
71	5	106	1
73	5	102	1
74	5	95	1
75	5	101	1
72	5	97	2
76	5	109	1
77	5	111	1
\.


ALTER TABLE public."BasketItems" ENABLE TRIGGER ALL;

--
-- Data for Name: CategoryTranslations; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."CategoryTranslations" DISABLE TRIGGER ALL;

COPY public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") FROM stdin;
72	Смартфони	1	uk
73	Ноутбуки	2	uk
74	Планшети	3	uk
75	Смартгодинники	4	uk
76	Навушники	5	uk
77	Бездротові колонки	6	uk
78	Телевізори	7	uk
79	Ігрові приставки	8	uk
80	Настільні ПК	9	uk
81	Монітори	10	uk
82	Процесори	11	uk
83	Відеокарти	12	uk
84	Материнські плати	13	uk
85	Оперативна пам'ять	14	uk
86	SSD накопичувачі	15	uk
87	Жорсткі диски (HDD)	16	uk
88	Блоки живлення	17	uk
89	Корпуси для ПК	18	uk
90	Системи охолодження	19	uk
91	Клавіатури	20	uk
92	Мишки та килимки	21	uk
93	Вебкамери	22	uk
94	Мікрофони	23	uk
95	Мережеві маршрутизатори	24	uk
96	Зовнішні накопичувачі	25	uk
97	Павербанки	26	uk
98	Зарядні пристрої та кабелі	27	uk
99	Чохли для смартфонів	28	uk
100	Захисне скло та плівки	29	uk
101	Електронні книги	30	uk
102	Дрони та квадрокоптери	31	uk
103	Екшн-камери	32	uk
104	Дзеркальні та бездзеркальні камери	33	uk
105	Хаби розумного дому	34	uk
106	Розумне освітлення	35	uk
107	Камери відеоспостереження	36	uk
108	Роботи-пилососи	37	uk
109	Очищувачі повітря	38	uk
110	Кавоварки та кавомашини	39	uk
111	Мікрохвильові печі	40	uk
112	Електрочайники	41	uk
113	Мультипечі та аерогрилі	42	uk
114	Холодильники	43	uk
115	Пральні машини	44	uk
116	Пилососи	45	uk
117	Електробритви та тримери	46	uk
118	Фени для волосся	47	uk
119	Електричні зубні щітки	48	uk
120	Електросамокати	49	uk
121	Зарядні станції	50	uk
\.


ALTER TABLE public."CategoryTranslations" ENABLE TRIGGER ALL;

--
-- Data for Name: CityTranslations; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."CityTranslations" DISABLE TRIGGER ALL;

COPY public."CityTranslations" ("Id", "TranslatableId", "Name", "Language") FROM stdin;
\.


ALTER TABLE public."CityTranslations" ENABLE TRIGGER ALL;

--
-- Data for Name: ConditionTranslations; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."ConditionTranslations" DISABLE TRIGGER ALL;

COPY public."ConditionTranslations" ("Id", "Name", "TranslatableId", "Language") FROM stdin;
1	Новий	0	uk
2	Вживаний (як новий)	1	uk
3	Вживаний (добрий стан)	2	uk
4	Вживаний (задовільний стан)	3	uk
5	На запчастини / несправний	4	uk
\.


ALTER TABLE public."ConditionTranslations" ENABLE TRIGGER ALL;

--
-- Data for Name: Favorites; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."Favorites" DISABLE TRIGGER ALL;

COPY public."Favorites" ("Id", "UserId", "AdvertisementId") FROM stdin;
110	7	260
111	7	265
112	7	259
115	7	256
117	7	258
118	7	257
\.


ALTER TABLE public."Favorites" ENABLE TRIGGER ALL;

--
-- Data for Name: Images; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."Images" DISABLE TRIGGER ALL;

COPY public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") FROM stdin;
216	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144410/364827001_wlz7hh.webp	1	196	
217	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144410/364826989_lphotg.webp	2	196	
218	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144418/364827007_okiaqh.webp	3	196	
219	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144594/475767125_ixx475.webp	1	197	
220	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144603/475767126_pro9o3.webp	2	197	
221	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144605/475767127_lzpk2z.webp	3	197	
222	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144704/655339080_erwi1u.webp	1	198	
223	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144720/655339086_tbop03.webp	2	198	
224	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144723/655339092_pcsqoj.webp	3	198	
225	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144807/668569970_i6mkhq.webp	1	199	
226	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144811/668569971_zwirjg.webp	2	199	
227	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144813/668569972_c26jsb.webp	3	199	
228	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144884/526087492_iombb1.webp	1	200	
229	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144888/664850304_mezn7v.webp	2	200	
230	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144889/526087494_imwl9e.webp	3	200	
231	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144976/558718757_wzicsi.webp	1	201	
232	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144980/558718766_nrklby.webp	2	201	
233	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144992/558718773_ngevrw.webp	3	201	
234	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145067/530592347_dbyps4.webp	1	202	
235	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145073/530592352_f0q8zt.webp	2	202	
236	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145187/568716128_qi25po.webp	1	203	
237	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145191/568716137_srq9t2.webp	2	203	
238	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145193/568716146_sm52e1.webp	3	203	
239	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145333/594443650_ak74lf.webp	1	204	
242	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145339/594443651_fehz5o.webp	2	204	
243	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145341/594443652_tpvy17.webp	3	204	
244	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145445/496254834_pd8lkn.webp	1	205	
245	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145462/496254836_ur4ltw.webp	2	205	
246	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145463/496254835_hnkkcx.webp	3	205	
247	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145508/385555052_mdhg6p.webp	1	206	
248	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145524/385555060_yg33sf.webp	2	206	
249	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145526/385555070_kvhfsd.webp	3	206	
250	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145585/437537053_ht3fzb.webp	1	207	
251	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145586/437537057_zpdmvg.webp	2	207	
252	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145588/437537060_dws5xs.webp	3	207	
253	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145661/668791078_ubbbac.webp	1	208	
254	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145673/647475190_ucrftr.webp	2	208	
255	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145675/647475191_ceikbc.webp	3	208	
256	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145724/683053701_f43kqc.webp	1	209	
257	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145728/545471050_ufisbk.webp	2	209	
258	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145730/545471056_mn0mw8.webp	3	209	
259	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145808/520314728_hiphys.webp	1	210	
260	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145825/520314738_qiyljw.webp	2	210	
261	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145836/520314734_kgnu5e.webp	3	210	
262	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145889/231872455_txl2m3.webp	1	211	
263	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145891/231872459_hzwsdf.webp	2	211	
264	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145892/231872462_nnxnxx.webp	3	211	
265	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145982/575205556_f72w3e.webp	1	212	
266	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145992/575281377_kgknjb.webp	2	212	
267	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145994/575207927_xkfn0g.webp	3	212	
268	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146049/620425198_swfp1h.webp	1	213	
269	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146068/620417564_e2smg3.webp	2	213	
270	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146070/589237643_hcpvas.webp	3	213	
271	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146125/675992750_irpcfo.webp	1	214	
272	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146133/674241713_dggzky.webp	2	214	
273	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146135/674241714_glyg9m.webp	3	214	
274	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146224/629962972_navqi9.webp	1	215	
275	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146238/629963007_avghwz.webp	2	215	
276	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146250/629962994_s9sm2f.webp	3	215	
277	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146461/366100886_juexp9.webp	1	216	
278	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146472/291331343_pfruyy.webp	2	216	
279	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146486/366100886_mirzmt.webp	3	216	
280	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146528/510976566_f0tmq0.webp	1	217	
281	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146596/505784216_exva5a.webp	1	218	
282	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146618/505784217_khmxee.webp	2	218	
283	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146620/505784218_g7xl2z.webp	3	218	
284	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146665/583841741_rfvdll.webp	1	219	
285	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146668/583841752_fvokee.webp	2	219	
286	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146670/583841765_h4s7az.webp	3	219	
287	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146733/328674528_agzig4.webp	1	220	
288	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146743/328674583_p604jz.webp	2	220	
290	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146754/328674631_cc9xuf.webp	3	220	
291	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146845/502800245_uaspzj.webp	1	221	
292	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146845/502800245_uaspzj.webp	2	221	
293	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146879/502800247_oi4paz.webp	3	221	
294	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146919/290600870_zqkb01.webp	1	222	
295	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146928/290600871_bsjlfn.webp	2	222	
296	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146939/290600872_ypayhn.webp	3	222	
297	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146995/554615140_adcthz.webp	1	223	
298	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146998/554615155_aj7uts.webp	2	223	
299	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147008/554615161_qzk85l.webp	3	223	
300	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147048/380104127_k126uc.webp	1	224	
301	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147051/380104133_yv8c4a.webp	2	224	
302	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147073/380104143_lpjwdx.webp	3	224	
303	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147122/458852748_ltl3ee.webp	1	225	
304	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147132/458852749_f3vxfr.webp	2	225	
305	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147144/458852751_li9olo.webp	3	225	
306	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147189/12241784_ilflwe.webp	1	227	
307	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147192/567295109_bqifke.webp	2	227	
308	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147218/567295110_ddmqtw.webp	3	227	
309	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147245/654896273_xy1tsr.webp	1	228	
310	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147248/654896277_rvk9ge.webp	2	228	
311	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147269/654896282_md4vgx.webp	3	228	
312	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147319/11274834_gopw0b.webp	1	229	
313	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147322/11274851_pavxdm.webp	2	229	
314	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147325/11274865_fpanry.webp	3	229	
315	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147385/622563066_gt81vt.webp\n	1	230	
316	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147449/409729778_amgxlw.webp	1	231	
317	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147451/409729779_nsvc1w.webp	2	231	
318	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147453/409729589_z2aliw.webp	3	231	
319	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147510/562264048_jgpyc8.webp	1	232	
320	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147513/562264055_lobuqb.webp	2	232	
321	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147514/562264063_xpetpv.webp	3	232	
322	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147571/428354106_lt4oq6.webp	1	233	
323	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147573/428354107_dipvbc.webp	2	233	
324	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147574/428354108_yc0l3m.webp	3	233	
325	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147651/361655136_kotiti.webp	1	234	
326	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147658/361655138_pwc2ss.webp	2	234	
327	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147678/361655139_lsyfvf.webp	3	234	
328	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147729/646978316_wxgxqp.webp	1	235	
329	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147732/646978317_y41bp5.webp	2	235	
330	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147734/646978318_kvyi60.webp	3	235	
332	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149022/263194700_myiro7.webp	1	256	
333	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149025/263194701_kg2xif.webp	2	256	
334	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149034/263194702_h0fsgo.webp	3	256	
335	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149082/487803370_snsogo.webp	1	257	
336	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149085/487803382_nr9bvx.webp	2	257	
337	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149086/487803394_xp8r7a.webp	3	257	
338	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149169/494892667_balkaq.webp	1	258	
339	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149171/494892668_vfs6wx.webp	2	258	
340	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149181/494892669_hbvj9g.webp	3	258	
341	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149230/570800964_er9gg5.webp	1	259	
342	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149233/570800966_ijj1lz.webp	2	259	
343	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149234/570800967_ulnkuc.webp	3	259	
344	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149301/574987280_hd3dcn.webp	1	260	
345	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149304/568433682_xds4rn.webp	2	260	
346	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149306/568433683_z8s7sy.webp	3	260	
347	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149374/227189617_unqx9t.webp	1	261	
348	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149377/227189616_sdtb3r.webp	2	261	
349	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149378/227189615_xxtbqr.webp	3	261	
350	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149433/8932640_sacel7.webp	1	262	
351	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149435/26954598_tnei1e.webp	2	262	
352	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149436/26954606_drbp1d.webp	3	262	
353	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149514/192152371_u3op0x.webp	1	263	
354	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149518/192152386_ttuhb6.webp	2	263	
355	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149520/192152399_luqftn.webp	3	263	
356	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149613/425530423_uda6rw.webp	1	264	
357	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149616/425530424_ke9dvy.webp	2	264	
358	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149627/425530425_l4ndyp.webp	3	264	
359	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149673/294910181_irflez.webp	1	265	
360	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149677/294910182_tvlq67.webp	2	265	
361	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149678/294910183_gaoxkj.webp	3	265	
362	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149726/474801847_ivs5tr.webp	1	266	
363	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149844/632056287_dmluid.webp	1	267	
364	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149885/653056722_lshkrt.webp	1	268	
365	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149921/632393017_tqplfm.webp	1	269	
366	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149925/632393024_pbv2lv.webp	2	269	
367	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149926/632393030_fiu3yd.webp	3	269	
368	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149996/503511231_hzviki.webp	1	270	
369	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150000/503510162_ofi5un.webp	2	270	
370	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150016/503510158_q4el37.webp	3	270	
371	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150078/676802712_il3uax.webp	1	271	
372	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150081/676802708_bkorbf.webp	2	271	
373	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150084/676802709_xt2nm3.webp	3	271	
374	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150158/485705984_tfyyi1.webp	1	272	
375	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150160/494572302_ltyo89.webp	2	272	
376	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150174/494572281_b5blah.webp	3	272	
377	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150237/370432191_txvmhz.webp	1	273	
378	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150239/370432192_yxhlvj.webp	2	273	
379	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150240/370432193_dlps9d.webp	3	273	
380	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150416/dji-mini-4-pro-fly-more-combo-dji-rc-2-cp-ma-00000735-01_a94v8j.webp	1	274	
381	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150436/o1oazeoojtcadjpb09wt75he2nrvmfcl_supllv.webp	2	274	
382	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150446/ahn1p8i2ui1lydf2b1iwcwwfqeaxziqb_begzna.webp	3	274	
383	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150507/372330916_m5v11e.webp	1	275	
384	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150518/51ynlb7lwvl_i2cxpt.webp	2	275	
385	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150520/dji-air-3-drone-fly-more-combo-with-rc-n2-cp-ma-00000692-01-2_xz6xh2.webp	3	275	
386	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791187727/674221389_vucfxd.webp	1	276	
387	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791187757/529960864_rol5zs.webp	1	277	
388	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791187760/529960875_vhuagb.webp	2	277	
389	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791187761/529960521_wn3l2g.webp	3	277	
390	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791187809/651863404_vo0f6b.webp	1	278	
391	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193303/525600963_xuvrdv.webp	2	278	
392	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193310/525600982_zd8xrc.webp	3	278	
393	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193364/304428962_kvd810.webp	1	279	
394	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193367/304428963_iqd3ic.webp	2	279	
395	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193368/304427987_byg7an.webp	3	279	
396	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193429/595086769_zcwasd.webp	1	280	
397	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193431/595086773_unoamq.webp	2	280	
398	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193435/595086791_pfv9rc.webp	3	280	
399	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193492/651411318_pyycug.webp	1	281	
400	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193496/651411320_zdyj9v.webp	2	281	
401	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193498/651411321_yzo1t2.webp	3	281	
402	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193552/528508640_kemhby.webp	1	282	
403	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193555/528508652_zr9saf.webp	2	282	
404	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193556/528508664_dcffui.webp	3	282	
405	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193611/574225276_zsnix2.webp	1	283	
406	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193615/661671378_illflr.webp	2	283	
407	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193617/574225289_iorl6s.webp	3	283	
408	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193671/677023226_hmeten.webp	1	284	
409	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193687/677023227_rvx3yc.webp	2	284	
410	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193689/677023229_z8ulqj.webp	3	284	
411	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193856/679986143_d09kwl.webp	1	285	
412	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193863/679986146_i72uh7.webp	2	285	
413	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193873/679986149_pibac5.webp	3	285	
414	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193961/389880232_po86no.webp	1	286	
415	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193963/389880233_hmdldh.webp	2	286	
416	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193974/389880234_qd2don.webp	3	286	
417	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194040/458009734_toqcaz.webp	1	287	
418	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194045/471558300_suqwr8.webp	2	287	
419	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194047/471558302_uhta9i.webp	3	287	
420	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194117/443615066_dsndob.webp	1	288	
421	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194120/287693142_muc73n.webp	2	288	
422	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194121/348228132_chugei.webp	3	288	
423	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194173/357605939_bp42uj.webp	1	289	
424	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194177/357720078_tjcnpj.webp	2	289	
425	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194179/361907807_lpd8kf.webp	3	289	
426	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194569/442061521_umkas8.webp	1	290	
427	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194572/442061524_urpb9u.webp	2	290	
428	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194573/442061538_igc1wi.webp	3	290	
429	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194628/673379507_ai6chk.webp	1	291	
430	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194631/606413715_r65hd4.webp	2	291	
431	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194693/653866874_vx0mdp.webp	1	292	
432	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194697/653866876_urg4of.webp	2	292	
433	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194698/653866878_tkj0i0.webp	3	292	
435	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194758/603244431_ebkrac.webp	1	293	
437	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194770/603244441_nhvfvp.webp	3	293	
436	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194762/603244449_j7qj6l.webp	2	293	
438	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194872/579834822_xxfqlk.webp	1	294	
439	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194875/579834823_mpfdlx.webp	2	294	
440	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194876/579834824_fpqbxq.webp	3	294	
441	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194938/32054513_cmpur5.webp	1	295	
442	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194952/32054359_eginef.webp	2	295	
443	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194954/167223477_kqwjmt.webp	3	295	
444	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195330/594186817_wyk63f.webp	1	296	
445	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195331/585911693_d2akvv.webp	2	296	
446	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195333/585911418_pjtkj2.webp	3	296	
447	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195383/485797275_zsec1n.webp	1	297	
448	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195385/485792170_awnncv.webp	2	297	
449	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195407/485797277_jpfsuw.webp	3	297	
450	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195466/580710374_jodk5n.webp	1	298	
451	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195468/679464357_un7i97.webp	2	298	
452	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195582/503249904_s9n2y6.webp	1	299	
453	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195593/422478145_oifrns.webp	2	299	
454	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195595/422478147_df4r7g.webp	3	299	
455	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195651/297467401_iameu1.webp	1	300	
456	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195653/297467408_itcesl.webp	2	300	
457	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195655/297467421_hpxwuw.webp	3	300	
458	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195725/10641437_dldkss.webp	1	301	
459	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195898/381633771_p7f5eq.webp	1	302	
460	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195905/658688378_hevv31.webp	2	302	
461	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195906/658688381_zuxlck.webp	3	302	
462	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195969/547914564_teavvd.webp	1	303	
463	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195973/501924125_evcbeu.webp	2	303	
464	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195974/501924131_kn1gpq.webp	3	303	
465	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791196029/359194038_pommy8.webp	1	304	
466	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791196032/359194039_oqmqea.webp	2	304	
467	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791196033/359194040_sxpvwr.webp	3	304	
468	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791196090/441783274_vvgkwy.webp	1	305	
469	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791196093/441783275_fmiuza.webp	2	305	
470	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791196094/441783276_kzvobd.webp	3	305	
471	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203433/170581204_m5rvo4.webp	1	306	
472	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203436/320924120_aqw0c6.webp	2	306	
473	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203437/320924121_dgtyh0.webp	3	306	
474	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203511/14435434_kbmysu.webp	1	307	
475	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203514/460513956_xly7ky.webp	2	307	
476	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203515/460513968_chqxkz.webp	3	307	
477	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203573/592188907_wkagpp.webp	1	308	
478	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203576/592188908_rcozqw.webp	2	308	
479	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203580/592188909_bvuas8.webp	3	308	
480	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203644/638229380_arxdeq.webp	1	309	
481	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203648/638229381_i4k1yv.webp	2	309	
482	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203649/638229382_zdybmx.webp	3	309	
483	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203704/359825406_yrpjs1.webp	1	310	
484	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203707/359825407_moqd5j.webp	2	310	
485	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203708/359825408_uv4tjr.webp	3	310	
486	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203770/499467120_npjebj.webp	1	311	
487	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203774/499467129_f4usfy.webp	2	311	
488	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203776/499467124_unjviv.webp	3	311	
489	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203831/662648683_hb2bkr.webp	1	312	
490	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203837/668061234_hpku99.webp	2	312	
491	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203839/293144336_no33iu.webp	3	312	
492	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203911/365505733_d2nmn7.webp	1	313	
493	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203915/365505735_j3l5qm.webp	2	313	
494	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203932/365505734_m6wrri.webp	3	313	
495	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203973/416441423_csk2ou.webp	1	314	
496	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203974/649226310_wd1nxo.webp	2	314	
497	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203976/416441430_moldfi.webp	3	314	
498	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791204167/594962272_lfknnz.webp	1	315	
499	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791204169/594962278_dnuowv.webp	2	315	
500	https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791204170/594962281_qcxkg3.webp	3	315	
\.


ALTER TABLE public."Images" ENABLE TRIGGER ALL;

--
-- Data for Name: ProductTypeTranslations; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."ProductTypeTranslations" DISABLE TRIGGER ALL;

COPY public."ProductTypeTranslations" ("Id", "Name", "TranslatableId", "Language") FROM stdin;
1	Ґаджети та смартфони	0	uk
2	Комп'ютери та комплектуючі	1	uk
3	Периферія та аудіо	2	uk
4	ТВ та відео	3	uk
5	Геймінг	4	uk
6	Аксесуари та живлення	5	uk
7	Розумний Дім та освітлення	6	uk
8	Велика побутова техніка	7	uk
9	Мала кухонна техніка	8	uk
10	Прибирання та догляд за домом	9	uk
11	Особиста гігієна та догляд	10	uk
12	Персональний транспорт	11	uk
\.


ALTER TABLE public."ProductTypeTranslations" ENABLE TRIGGER ALL;

--
-- Data for Name: RegionTranslations; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."RegionTranslations" DISABLE TRIGGER ALL;

COPY public."RegionTranslations" ("Id", "TranslatableId", "Language", "Name") FROM stdin;
1	1	uk	Київ
2	1	ru	Киев
3	1	en	Kyiv
4	1	de	Kyjiw
\.


ALTER TABLE public."RegionTranslations" ENABLE TRIGGER ALL;

--
-- Data for Name: Roles; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."Roles" DISABLE TRIGGER ALL;

COPY public."Roles" ("Id", "Name", "Description") FROM stdin;
0	User	Regular registered user with standard permissions.\nCan manage personal profile, create and edit own content, and use basic application features.
1	Admin	Administrator role with full access to system management, including users, roles, and core data.
\.


ALTER TABLE public."Roles" ENABLE TRIGGER ALL;

--
-- Data for Name: StatusTranslations; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."StatusTranslations" DISABLE TRIGGER ALL;

COPY public."StatusTranslations" ("Id", "TranslatableId", "Language", "Name", "Description") FROM stdin;
1	0	uk	Активний	Це оголошення зараз активне!
2	0	ru	Активный	Это объявление сейчас активно!
3	0	en	Active	This advertisement is currently active!
4	0	de	Aktiv	Diese Anzeige ist derzeit aktiv!
\.


ALTER TABLE public."StatusTranslations" ENABLE TRIGGER ALL;

--
-- Data for Name: UserRoles; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."UserRoles" DISABLE TRIGGER ALL;

COPY public."UserRoles" ("Id", "UserId", "RoleId") FROM stdin;
0	3	1
0	7	1
0	9	1
0	16	1
0	18	1
0	21	1
0	22	1
0	28	0
0	49	0
0	51	0
0	55	0
0	56	0
0	57	0
0	58	0
0	61	0
0	62	0
0	63	0
0	64	0
0	65	0
0	66	0
0	68	0
0	71	0
0	73	0
\.


ALTER TABLE public."UserRoles" ENABLE TRIGGER ALL;

--
-- Data for Name: VerificationCodes; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."VerificationCodes" DISABLE TRIGGER ALL;

COPY public."VerificationCodes" ("Id", "UserId", "CodeHash", "ExpiresAt", "UsedAt", "Attempts", "CreatedAt", "InvalidatedAt", "Purpose", "PendingValue") FROM stdin;
\.


ALTER TABLE public."VerificationCodes" ENABLE TRIGGER ALL;

--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: -
--

ALTER TABLE public."__EFMigrationsHistory" DISABLE TRIGGER ALL;

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20260607194859_InitialCreate	10.0.8
20260608182251_AddUserRoleModule	10.0.8
20260608190539_ConfigureUserRoleCompositeKey	10.0.8
20260609185810_AddCategoryAttributeSchema	10.0.8
20260609193727_AddConditionCurrencyImageSchema	10.0.8
20260609202103_AddAdvertisementSchema	10.0.8
20260609205204_FixConfiguration	10.0.8
20260617203936_ChoreUserConfiguration	10.0.8
20260623190720_ChangePropertiesToJsonbDictionary	10.0.8
20260624202649_AddCodeAndSymbolToCurrency	10.0.8
20260701005342_AddConditionToAdvertisement	10.0.8
20260701010257_AddComparerToAdvertisement	10.0.8
20260707163117_AddFavorite	10.0.8
20260708204241_AddFavorite	10.0.8
20260709000023_AddPublickIdToImage	10.0.8
20260801140333_AddProductType	10.0.8
20260801142636_AddProductTypeToCategory	10.0.8
20260808094746_AddStatusTable	10.0.8
20260808101026_AddStatus	10.0.8
20260808101315_AddStatusToAdvertisement	10.0.8
20260813135131_AddStatusTranslation	10.0.8
20260820115304_AddAvatar	10.0.8
20260903170850_AddRegionTranslation	10.0.8
20260903172248_UpdateRegion	10.0.8
20260905172933_AddCityTranslation	10.0.8
20260906124953_AddConditionTranslation	10.0.8
20260907115349_AddAttributeDefinitionTranslations	10.0.8
20260909100240_AddProductTypeTranslation	10.0.8
20260909163917_AddCategoryTranslation	10.0.8
20260910100554_AddStockToAdvertisement	10.0.8
20260910160323_AddBasketAttribute	10.0.8
20260910163246_ChangeBasketAttribute	10.0.8
20260910170026_ChangeForeignKeyBasketAttribute	10.0.8
20260910203535_ChangeTypeAdvertisementProperyStock	10.0.8
20260911091640_AddBasketansBasketItems	10.0.8
20260912124746_UpdateConfigurationBasketItems	10.0.8
20260913144907_AddUserNameAndBioIntoUser	10.0.8
20260913151542_AddVerificationCode	10.0.8
20260913204832_AddConfirmedsIntoUser	10.0.8
20260914081254_AddInvalidatedAtIntoVerifyCode	10.0.8
20260914091909_AddStatusIntoUserAndPurposeIntoVerifyCode	10.0.8
20260914092442_ChangeTypeStatus	10.0.8
20260915110647_AddPendingValueIntoVerifyCode	10.0.8
20260926155828_AddAttributeVariant	10.0.8
20260926161631_AddAttributeDictionaryAndAttributeDictionaryValue	10.0.8
20260926163724_AddAttributesToDbSet	10.0.8
20260926163906_RanameAttributeDictionary	10.0.8
20260927192428_AddAdvertisementVariantIntoDbSet	10.0.8
20260928114243_AddAttributeDictionaryValueTranslation	10.0.8
20260928183925_ChangeAdvertisementInBasketItem	10.0.8
\.


ALTER TABLE public."__EFMigrationsHistory" ENABLE TRIGGER ALL;

--
-- Name: AdvertisementVariant_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."AdvertisementVariant_Id_seq"', 160, true);


--
-- Name: Advertisements_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."Advertisements_Id_seq"', 315, true);


--
-- Name: AttributeDefinitionTranslations_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."AttributeDefinitionTranslations_Id_seq"', 4, true);


--
-- Name: AttributeDefinition_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."AttributeDefinition_Id_seq"', 104, true);


--
-- Name: AttributeDictionaryValueTranslations_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."AttributeDictionaryValueTranslations_Id_seq"', 1, false);


--
-- Name: AttributeDictionaryValue_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."AttributeDictionaryValue_Id_seq"', 21, true);


--
-- Name: AttributeDictionary_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."AttributeDictionary_Id_seq"', 5, true);


--
-- Name: Avatars_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."Avatars_Id_seq"', 26, true);


--
-- Name: BasketAttributes_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."BasketAttributes_Id_seq"', 7, true);


--
-- Name: BasketItems_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."BasketItems_Id_seq"', 77, true);


--
-- Name: Baskets_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."Baskets_Id_seq"', 5, true);


--
-- Name: CategoryTranslations_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."CategoryTranslations_Id_seq"', 121, true);


--
-- Name: Category_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."Category_Id_seq"', 57, true);


--
-- Name: Cities_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."Cities_Id_seq"', 30, true);


--
-- Name: CityTranslations_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."CityTranslations_Id_seq"', 1, false);


--
-- Name: ConditionTranslations_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."ConditionTranslations_Id_seq"', 5, true);


--
-- Name: Condition_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."Condition_Id_seq"', 1, false);


--
-- Name: Currency_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."Currency_Id_seq"', 1, false);


--
-- Name: Favorite_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."Favorite_Id_seq"', 118, true);


--
-- Name: Image_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."Image_Id_seq"', 500, true);


--
-- Name: ProductTypeTranslations_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."ProductTypeTranslations_Id_seq"', 12, true);


--
-- Name: ProductTypes_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."ProductTypes_Id_seq"', 49, true);


--
-- Name: RegionTranslations_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."RegionTranslations_Id_seq"', 4, true);


--
-- Name: Regions_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."Regions_Id_seq"', 1, false);


--
-- Name: Roles_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."Roles_Id_seq"', 1, false);


--
-- Name: StatusTranslations_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."StatusTranslations_Id_seq"', 4, true);


--
-- Name: Statuses_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."Statuses_Id_seq"', 1, false);


--
-- Name: Users_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."Users_Id_seq"', 75, true);


--
-- Name: VerificationCodes_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."VerificationCodes_Id_seq"', 138, true);


--
-- PostgreSQL database dump complete
--

\unrestrict B3pQ5Y9eMROtJoTXL0KNENq4PYZJOKCM9K9OQJ4g35n2dbfR8cgLCzKl9Zo398h

