--
-- PostgreSQL database dump
--

\restrict GrfjX1eBo0oDf6JrEKLxBmJKQH3VDb9LgkDhXTj9zGsuSfGQVkdD4gpKPBK8nrd

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

INSERT INTO public."ProductTypes" ("Id", "Code", "Description") VALUES (0, 'GadgetsAndSmartphones', 'Smartphones, tablets, smartwatches, and e-readers');
INSERT INTO public."ProductTypes" ("Id", "Code", "Description") VALUES (1, 'ComputersAndHardware', 'Laptops, desktop PCs, components, networking, and external storage');
INSERT INTO public."ProductTypes" ("Id", "Code", "Description") VALUES (2, 'PeripheralsAndAudio', 'Keyboards, mice, webcams, mics, headphones, speakers, and monitors');
INSERT INTO public."ProductTypes" ("Id", "Code", "Description") VALUES (3, 'TVAndVideo', 'TVs, drones, action cams, photo/video gear, and surveillance');
INSERT INTO public."ProductTypes" ("Id", "Code", "Description") VALUES (4, 'Gaming', 'Consoles, VR, and gaming hardware');
INSERT INTO public."ProductTypes" ("Id", "Code", "Description") VALUES (5, 'AccessoriesAndPower', 'Powerbanks, chargers, cables, protective cases, and power stations');
INSERT INTO public."ProductTypes" ("Id", "Code", "Description") VALUES (6, 'SmartHomeAndLighting', 'Smart home hubs, sensors, and smart lighting');
INSERT INTO public."ProductTypes" ("Id", "Code", "Description") VALUES (7, 'MajorAppliances', 'Large home appliances like fridges and washing machines');
INSERT INTO public."ProductTypes" ("Id", "Code", "Description") VALUES (8, 'SmallKitchenAppliances', 'Coffee makers, microwaves, kettles, air fryers, and cooking gear');
INSERT INTO public."ProductTypes" ("Id", "Code", "Description") VALUES (9, 'HomeCleaning', 'Vacuums, robot vacuums, air purifiers, and climate control');
INSERT INTO public."ProductTypes" ("Id", "Code", "Description") VALUES (10, 'PersonalCare', 'Shavers, hair dryers, and electric toothbrushes');
INSERT INTO public."ProductTypes" ("Id", "Code", "Description") VALUES (11, 'Transport', 'Electric scooters and personal mobility devices');


--
-- Data for Name: Categories; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (30, 0, 'e-readers');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (1, 0, 'smartphones');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (3, 0, 'tablets');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (4, 0, 'smartwatches');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (24, 1, 'routers');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (25, 1, 'external-drives');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (2, 1, 'laptops');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (9, 1, 'desktop-pcs');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (11, 1, 'processors');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (12, 1, 'graphics-cards');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (13, 1, 'motherboards');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (14, 1, 'ram-memory');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (15, 1, 'ssd-drives');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (16, 1, 'hdd-drives');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (17, 1, 'power-supplies');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (18, 1, 'pc-cases');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (19, 1, 'cooling-systems');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (22, 2, 'webcams');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (23, 2, 'microphones');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (5, 2, 'headphones');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (6, 2, 'wireless-speakers');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (10, 2, 'monitors');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (20, 2, 'keyboards');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (21, 2, 'mice-pads');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (31, 3, 'drones');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (32, 3, 'action-cameras');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (33, 3, 'dslr-cameras');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (36, 3, 'security-cameras');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (7, 3, 'televisions');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (8, 4, 'gaming-consoles');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (26, 5, 'powerbanks');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (27, 5, 'chargers-cables');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (28, 5, 'phone-cases');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (29, 5, 'screen-protectors');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (50, 5, 'power-stations');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (34, 6, 'smarthome-hubs');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (35, 6, 'smart-lighting');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (43, 7, 'refrigerators');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (44, 7, 'washing-machines');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (39, 8, 'coffee-makers');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (40, 8, 'microwaves');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (41, 8, 'electric-kettles');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (42, 8, 'air-fryers');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (37, 9, 'robot-vacuums');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (38, 9, 'air-purifiers');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (45, 9, 'vacuum-cleaners');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (46, 10, 'shavers-trimmers');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (47, 10, 'hair-dryers');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (48, 10, 'electric-toothbrushes');
INSERT INTO public."Categories" ("Id", "ProductTypeId", "Code") VALUES (49, 11, 'electric-scooters');


--
-- Data for Name: Regions; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."Regions" ("Id", "Code") VALUES (1, 'KYIV');
INSERT INTO public."Regions" ("Id", "Code") VALUES (2, 'REGION-2');
INSERT INTO public."Regions" ("Id", "Code") VALUES (3, 'REGION-3');
INSERT INTO public."Regions" ("Id", "Code") VALUES (4, 'REGION-4');
INSERT INTO public."Regions" ("Id", "Code") VALUES (5, 'REGION-5');
INSERT INTO public."Regions" ("Id", "Code") VALUES (6, 'REGION-6');
INSERT INTO public."Regions" ("Id", "Code") VALUES (7, 'REGION-7');
INSERT INTO public."Regions" ("Id", "Code") VALUES (8, 'REGION-8');
INSERT INTO public."Regions" ("Id", "Code") VALUES (9, 'REGION-9');
INSERT INTO public."Regions" ("Id", "Code") VALUES (10, 'REGION-10');
INSERT INTO public."Regions" ("Id", "Code") VALUES (11, 'REGION-11');
INSERT INTO public."Regions" ("Id", "Code") VALUES (12, 'REGION-12');
INSERT INTO public."Regions" ("Id", "Code") VALUES (13, 'REGION-13');
INSERT INTO public."Regions" ("Id", "Code") VALUES (14, 'REGION-14');
INSERT INTO public."Regions" ("Id", "Code") VALUES (15, 'REGION-15');
INSERT INTO public."Regions" ("Id", "Code") VALUES (16, 'REGION-16');
INSERT INTO public."Regions" ("Id", "Code") VALUES (17, 'REGION-17');
INSERT INTO public."Regions" ("Id", "Code") VALUES (18, 'REGION-18');
INSERT INTO public."Regions" ("Id", "Code") VALUES (19, 'REGION-19');
INSERT INTO public."Regions" ("Id", "Code") VALUES (20, 'REGION-20');


--
-- Data for Name: Cities; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (1, 1, '1');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (2, 1, '2');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (3, 2, '3');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (4, 3, '4');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (5, 3, '5');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (6, 3, '6');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (7, 4, '7');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (8, 4, '8');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (9, 5, '9');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (10, 5, '10');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (11, 6, '11');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (12, 7, '12');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (13, 7, '13');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (14, 8, '14');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (15, 8, '15');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (16, 9, '16');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (17, 10, '17');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (18, 10, '18');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (19, 11, '19');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (20, 11, '20');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (21, 12, '21');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (22, 13, '22');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (23, 14, '23');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (24, 15, '24');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (25, 15, '25');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (26, 16, '26');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (27, 17, '27');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (28, 18, '28');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (29, 19, '29');
INSERT INTO public."Cities" ("Id", "RegionId", "Code") VALUES (30, 20, '30');


--
-- Data for Name: Conditions; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."Conditions" ("Id", "Code") VALUES (0, 'new');
INSERT INTO public."Conditions" ("Id", "Code") VALUES (1, 'used_like_new');
INSERT INTO public."Conditions" ("Id", "Code") VALUES (2, 'used_good');
INSERT INTO public."Conditions" ("Id", "Code") VALUES (3, 'used_fair');
INSERT INTO public."Conditions" ("Id", "Code") VALUES (4, 'for_parts');


--
-- Data for Name: Currencies; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."Currencies" ("Id", "Name", "Code", "Symbol") VALUES (0, 'US Dollar', 'USD', '$');
INSERT INTO public."Currencies" ("Id", "Name", "Code", "Symbol") VALUES (1, 'Ukrainian Hryvnia', 'UAH', '₴');


--
-- Data for Name: Statuses; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."Statuses" ("Id", "Code") VALUES (0, 'ACTIVE');
INSERT INTO public."Statuses" ("Id", "Code") VALUES (1, 'ARCHIVED');
INSERT INTO public."Statuses" ("Id", "Code") VALUES (2, 'SOLD');


--
-- Data for Name: Users; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") VALUES (49, NULL, 'user-0', 'AQAAAAIAAYagAAAAEIUHqhl9TqXqLcDiFRHzRJ+hd4Erm2nKGyWgqvyWMsdNeEa59hj33CHyAASu68l54w==', '+4915565130632', NULL, NULL, NULL, '2026-09-17 12:10:41.115455+00', NULL, NULL, 'krip1123', false, true, 'Active');
INSERT INTO public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") VALUES (18, NULL, 'user-18', 'AQAAAAIAAYagAAAAEKxDXnlw2uQa8gtEPumjnLCAEz+K6A/PU87YY7g+YXP7NKkYKGbeKyhvPxaxO8GqNg==', '+380996669123', NULL, NULL, NULL, '2026-07-15 12:43:33.274065+00', NULL, NULL, 'user-18', false, false, 'Active');
INSERT INTO public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") VALUES (21, NULL, 'user-21', 'AQAAAAIAAYagAAAAELnWJPn4QbLBKRWRiNy3XfORV3U3CdCt2FadUwVnkaYjsGfKkq6Hc/jjOiJRSHEhCQ==', '+380996769123', NULL, NULL, NULL, '2026-07-15 12:47:18.098657+00', NULL, NULL, 'user-21', false, false, 'Active');
INSERT INTO public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") VALUES (22, NULL, 'user-22', 'AQAAAAIAAYagAAAAEGkb28Jqsv6zUXzoJ9sePquYAOF/BNe46pa6FW903iZno8Rr0/kbA0NzCcEyVwijew==', '+380996769523', NULL, NULL, NULL, '2026-07-15 12:47:28.142212+00', NULL, NULL, 'user-22', false, false, 'Active');
INSERT INTO public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") VALUES (51, NULL, 'user-0', 'AQAAAAIAAYagAAAAEI+05kSmpKvGGB8VKsffoNMtztu49xN0lD1RpcAkUfEPGz0eE3BEiTQ6ZC1Tih/uXg==', '+380967870204', NULL, NULL, NULL, '2026-09-17 12:48:53.974745+00', NULL, NULL, 'krip1111', false, true, 'Active');
INSERT INTO public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") VALUES (64, NULL, 'egorstarostenko888', 'AQAAAAIAAYagAAAAEJ9Z8MJ6mD2f1kZig7pWAOjtL8WrSUoolPBpw1hn6pgVs/jndOumpnucpN+5VLa/Ig==', NULL, 'egorstarostenko888@gmail.com', NULL, NULL, '2026-09-20 20:49:13.940059+00', NULL, NULL, 'egorstarostenko34', false, false, 'PendingVerification');
INSERT INTO public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") VALUES (16, NULL, 'nikitakolesnik.w', 'AQAAAAIAAYagAAAAEBr951YvJ4vxg96TcvlONBOpfEPTQbH2fd1o555q/pZZ6wSwLTwOA3wnKOb8Q6MHnA==', NULL, 'nikitakolesnik1.w@gmail.com', NULL, NULL, '2026-07-15 12:33:11.259602+00', NULL, NULL, 'user-16', false, false, 'Active');
INSERT INTO public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") VALUES (55, NULL, 'user-55', 'AQAAAAIAAYagAAAAEOjv2fW1rDV2lVzR+vZfxAnYG0RiqfkrpPwKeKm+LM4PH50NPEtxd+hJ/1dPLc3V2A==', '+380990408725', NULL, NULL, NULL, '2026-09-18 20:04:48.96597+00', NULL, NULL, 'krip1141566', false, true, 'Active');
INSERT INTO public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") VALUES (57, NULL, 'egorstarostenko555', 'AQAAAAIAAYagAAAAEMfzuDtWoga0g/Pk2kAGkJknSh10tKyVIF43KJ+lX8iSvQ5OpNEkn0bL0I1r17q4ww==', NULL, 'egorstarostenko555@gmail.com', NULL, NULL, '2026-09-19 13:32:00.666178+00', NULL, NULL, 'piska', false, false, 'PendingVerification');
INSERT INTO public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") VALUES (28, NULL, 'nikitakoliesnk5', 'AQAAAAIAAYagAAAAEIdLmBfE0ZCeck4BzZAoSsDjoPpabbOfoiOH3Uf204fLj33T2BCDz7f3J2Gzmh0GRg==', NULL, 'nikitakoliesnk4@gmail.com', NULL, NULL, '2026-07-15 12:57:39.422005+00', NULL, NULL, 'user-28', false, false, 'Active');
INSERT INTO public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") VALUES (61, NULL, 'user-61', 'AQAAAAIAAYagAAAAEIlMGHLny5zka5JatxNXcNYupj1sM9oQPs/5yzb00XADQBXCeDdMoujaVHzI794yDA==', '+380963147037', NULL, NULL, '2026-09-19 17:22:51.175331+00', '2026-09-19 17:21:16.03804+00', NULL, NULL, 'riirar', false, true, 'Active');
INSERT INTO public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") VALUES (9, NULL, 'user-9', 'AQAAAAIAAYagAAAAEHEiexHSw3V+FxBYMwseljfZW73dReGzqnKQqJcZwdVkS+VJz4YGIolMVDs7Oa1gMA==', '09629208781', NULL, NULL, '2026-09-13 09:54:14.081061+00', '2026-06-28 07:50:24.747221+00', NULL, NULL, 'user-9', false, false, 'Active');
INSERT INTO public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") VALUES (62, NULL, 'starostenko.yehor_kp62', '', NULL, 'starostenko.yehor_kp62@edu.kpi.ua', NULL, '2026-09-20 10:35:36.243225+00', '2026-09-19 19:24:39.681329+00', NULL, NULL, 'КП-62 Старостенко Єгор Євгенійович', false, false, 'Active');
INSERT INTO public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") VALUES (56, NULL, 'kolesniknikita867', 'AQAAAAIAAYagAAAAENCXPFmS797iimcuxFLFVw/MIRVYtasCY6khKNTDqz/WFp0xb/1rg5EHHiWKwvRKgQ==', NULL, 'kolesniknikita867@gmail.com', NULL, NULL, '2026-09-19 13:21:06.17524+00', NULL, NULL, 'kr1p', false, false, 'PendingVerification');
INSERT INTO public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") VALUES (58, NULL, 'egorstarostenko365', 'AQAAAAIAAYagAAAAEP+z+6jsfFgBmR6ClPBag160tmHF91qVaz3S8r4EXu4nemlvxAk0whk4pKI/9fW3yQ==', NULL, 'egorstarostenko365@gmail.com', NULL, NULL, '2026-09-19 14:22:29.707379+00', NULL, NULL, 'piska228', false, false, 'PendingVerification');
INSERT INTO public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") VALUES (3, 'Starostenko', 'Maga', 'AQAAAAIAAYagAAAAEFh+GdTEIk9aP/vIbBmzR49o8RmUi7ImIN2Ka8TU4jWRcxRlbIeOR0DpdMnGLis27A==', '+38099586912', 'polypoker235@gmail.com', NULL, '2026-08-20 11:32:23.449747+00', '2026-06-17 21:15:50.794642+00', 0, NULL, 'user-3', false, false, 'Active');
INSERT INTO public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") VALUES (63, NULL, 'egorstarostenko999', 'AQAAAAIAAYagAAAAEP3z3FH+9Q4Q52Ce/fSd2u9IzteK7MM5XKAR5HJWsqKGMHpXEnBCVLNQGT5TqNoukw==', NULL, 'egorstarostenko999@gmail.com', NULL, NULL, '2026-09-20 11:48:09.765425+00', NULL, NULL, 'egorstarostenko345', false, false, 'PendingVerification');
INSERT INTO public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") VALUES (71, NULL, 'user-71', 'AQAAAAIAAYagAAAAEA7mLWRTF0CEmjZiNp9hvAsITirgy/9bbVIPsB8kBYV/IenBd8fE8FcF4gLLX/dU3g==', '+380962920878', NULL, NULL, NULL, '2026-09-22 11:31:52.067052+00', NULL, NULL, 'Bobaka', false, true, 'Active');
INSERT INTO public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") VALUES (73, NULL, 'egorstarostenko333', 'AQAAAAIAAYagAAAAEKR6NNk0oZUmcmjoiWEqxP/yVKnoPwyvUJNmSDPnhJXF7M9GsGfIu8vn6vS6hMZovw==', NULL, 'egorstarostenko333@gmail.com', NULL, NULL, '2026-10-04 09:56:40.466114+00', NULL, NULL, 'erggg', false, false, 'PendingVerification');
INSERT INTO public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") VALUES (65, NULL, 'marketly.demo', '', NULL, 'marketly.demo@gmail.com', NULL, '2026-10-01 14:39:41.731725+00', '2026-09-20 20:50:33.241603+00', NULL, NULL, 'Nikita Kolesnik', false, false, 'Active');
INSERT INTO public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") VALUES (68, NULL, 'nikitakoliesnik5', 'AQAAAAIAAYagAAAAEPohd+gOKzCF3JPApfuRtJZu0VIb/27ExSplShU5CWfnOk2f2MTk/a7bdCw9D6BqMw==', NULL, 'nikitakoliesnik5@gmail.com', NULL, '2026-09-29 14:34:24.998559+00', '2026-09-21 19:43:37.916083+00', NULL, NULL, '11krip', true, false, 'Active');
INSERT INTO public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") VALUES (66, NULL, 'nikitakolesnik.w', '', NULL, 'nikitakolesnik.w@gmail.com', NULL, '2026-10-03 13:03:34.776419+00', '2026-09-21 10:41:50.258392+00', NULL, NULL, 'Нікіта Колєснік', false, false, 'Active');
INSERT INTO public."Users" ("Id", "LastName", "FirstName", "PasswordHash", "Phone", "Email", "CityId", "LastSeenAt", "RegisteredAt", "AvatarId", "Bio", "UserName", "EmailConfirmed", "PhoneConfirmed", "Status") VALUES (7, 'Старостенко', 'Єгор', 'AQAAAAIAAYagAAAAEKmOGBNJ0cugMCceJpzSmkJW7L5ur+gMZmSWU6kE6oKOMSAkhdHNwyJboEifQXiplA==', '+38099586911', 'egorstarostenko345@gmail.com', 1, '2026-10-06 11:15:06.446458+00', '2026-06-23 19:51:39.779765+00', NULL, 'Hello, my name is Ehor, I am Ukrainian programmer', 'user-7', false, false, 'Active');


--
-- Data for Name: Advertisements; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (256, 'Вебкамера Logitech C920 HD Pro Full HD 1080p', 'Легендарна вебкамера для стрімінгу, навчання та відеодзвінків із двома стереомікрофонами.', 88, 1, 21, 9, NULL, NULL, '2026-10-04 21:22:20.623595+00', '{"Brand": "Logitech", "Model": "C920 HD Pro"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (257, 'Вебкамера Razer Kiyo Pro Full HD 60FPS', 'Професійна вебкамера з адаптивним датчиком освітленості та підтримкою HDR.', 84, 1, 21, 9, NULL, NULL, '2026-10-04 21:22:20.623595+00', '{"Brand": "Razer", "Model": "Kiyo Pro"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (258, 'USB-мікрофон HyperX QuadCast S RGB', 'Конденсаторний ігровий мікрофон із керованим RGB-підсвічуванням та вбудованим поп-фільтром.', 95, 1, 22, 9, NULL, NULL, '2026-10-04 21:22:20.623595+00', '{"Brand": "HyperX", "Model": "QuadCast S"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (259, 'Студійний динамічний мікрофон Shure SM7B', 'Еталонний студійний мікрофон для подкастів, озвучки та професійного вокалу.', 91, 1, 22, 9, NULL, NULL, '2026-10-04 21:22:20.623595+00', '{"Brand": "Shure", "Model": "SM7B"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (260, 'Маршрутизатор ASUS RT-AX88U Pro Wi-Fi 6', 'Потужний двопандіапазонний ігровий роутер із підтримкою Wi-Fi 6 та портами 2.5G.', 87, 1, 23, 9, NULL, NULL, '2026-10-04 21:22:20.623595+00', '{"Brand": "ASUS", "Model": "RT-AX88U Pro"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (261, 'Маршрутизатор TP-Link Archer AX55 Wi-Fi 6', 'Гігабітний роутер для дому із підтримкою OneMesh та захистом HomeShield.', 90, 1, 23, 9, NULL, NULL, '2026-10-04 21:22:20.623595+00', '{"Brand": "TP-Link", "Model": "Archer AX55"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (262, 'Зовнішній SSD накопичувач Samsung T7 Shield 1TB Black', 'Захищений від падінь, пилу та вологи портативний SSD зі швидкістю до 1050 МБ/с.', 92, 1, 24, 9, NULL, NULL, '2026-10-04 21:22:20.623595+00', '{"Brand": "Samsung", "Model": "T7 Shield", "Storage": 1024}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (263, 'Портативний SSD SanDisk Extreme Portable 2TB V2', 'Компактний швидкісний накопичувач із карабінним кріпленням для подорожей.', 86, 1, 24, 9, NULL, NULL, '2026-10-04 21:22:20.623595+00', '{"Brand": "SanDisk", "Model": "Extreme Portable V2", "Storage": 2048}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (264, 'Павербанк Anker 737 PowerBank 24000mAh 140W', 'Ультрапотужна батарея з дисплеєм, здатна швидкісно заряджати ноутбуки та смартфони.', 96, 1, 25, 9, NULL, NULL, '2026-10-04 21:22:20.623595+00', '{"Brand": "Anker", "Model": "737 PowerBank"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (265, 'Павербанк Baseus Blade Digital Display 100W 20000mAh', 'Ультратонкий зовнішній акумулятор для MacBook та Windows-ноутбуків.', 89, 1, 25, 9, NULL, NULL, '2026-10-04 21:22:20.623595+00', '{"Brand": "Baseus", "Model": "Blade 100W"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (266, 'Зарядний пристрій UGREEN Nexode 100W GaN 4-Ports', 'Універсальний мережевий адаптер із технологією GaN для одночасної зарядки 4 пристроїв.', 91, 1, 26, 9, NULL, NULL, '2026-10-04 21:22:20.623595+00', '{"Brand": "UGREEN", "Model": "Nexode 100W"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (267, 'Оригінальний компактний адаптер Apple 35W Dual USB-C', 'Фірмовий зарядний блок з двома портами Type-C для одночасної зарядки iPhone та Apple Watch.', 85, 1, 26, 9, NULL, NULL, '2026-10-04 21:22:20.623595+00', '{"Brand": "Apple", "Model": "35W Dual USB-C"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (268, 'Захисний чохол Spigen Ultra Hybrid MagFit для iPhone 15 Pro', 'Прозорий ударостійкий чохол із вбудованим магнітним кільцем для підтримки MagSafe.', 88, 1, 27, 9, NULL, NULL, '2026-10-04 21:22:20.623595+00', '{"Brand": "Spigen", "Model": "Ultra Hybrid"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (269, 'Преміум чохол Pitaka MagEZ Case 4 для Samsung Galaxy S24 Ultra', 'Ультратонкий чохол із 100% арамідного волокна (Кевлар) з підтримкою магнітної зарядки.', 83, 1, 27, 9, NULL, NULL, '2026-10-04 21:22:20.623595+00', '{"Brand": "Pitaka", "Model": "MagEZ Case 4"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (270, 'Захисне скло ESR Armorite для iPhone 15 Pro (Комплект 2 шт)', 'Загартоване скло з рамкою для швидкої установки, що витримує навантаження до 50 кг.', 89, 1, 28, 9, NULL, NULL, '2026-10-04 21:22:20.623595+00', '{"Brand": "ESR", "Model": "Armorite"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (271, 'Захисне скло Spigen Glas.tR EZ Fit для Samsung S24 Ultra', 'Олеофобне загартоване скло зі спеціальним установочним трафаретом у комплекті.', 87, 1, 28, 9, NULL, NULL, '2026-10-04 21:22:20.623595+00', '{"Brand": "Spigen", "Model": "Glas.tR EZ Fit"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (272, 'Електронна книга Amazon Kindle Paperwhite 11th Gen 16GB', 'Електронна рідери з 6.8-дюймовим екраном E-Ink Carta, регулюванням колірної температури та захистом IPX8.', 94, 1, 30, 9, NULL, NULL, '2026-10-04 21:22:20.623595+00', '{"Brand": "Amazon", "Model": "Kindle Paperwhite 11", "Storage": 16, "ScreenSize": 6.8}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (280, 'Об''єктив Sigma 24-70mm f/2.8 DG DN Art для Sony E-mount', 'Професійний світлосильний стандартний зум-об''єктив із високою деталізацією по всій площі кадру.', 91, 1, 33, 9, NULL, NULL, '2026-10-05 08:08:08.686322+00', '{"Brand": "Sigma", "Model": "24-70mm f/2.8 DG DN Art"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (281, 'Портретний об''єктив Sony FE 85mm f/1.8 (SEL85F18)', 'Компактний і легкий портретний фікс-об''єктив із м''яким розмиттям фону (боке).', 87, 1, 33, 9, NULL, NULL, '2026-10-05 08:08:08.686322+00', '{"Brand": "Sony", "Model": "FE 85mm f/1.8"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (282, '3-осьовий стабілізатор DJI RS 3 Combo', 'Професійний стедікам для дзеркальних та бездзеркальних камер із корисним навантаженням до 3 кг.', 88, 1, 34, 9, NULL, NULL, '2026-10-05 08:08:08.686322+00', '{"Brand": "DJI", "Model": "RS 3 Combo"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (283, 'Стедікам для смартфона Zhiyun Smooth 5S Combo Gray', 'Професійний 3-осьовий стабілізатор для мобільної зйомки з вбудованим підсвічуванням.', 82, 1, 34, 9, NULL, NULL, '2026-10-05 08:08:08.686322+00', '{"Brand": "Zhiyun", "Model": "Smooth 5S"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (284, 'Студійне LED світло Godox SL60W 5600K', 'Потужне постійне світло 60 Вт із байонетом Bowens для фотосесій та відеозйомки.', 86, 1, 35, 9, NULL, NULL, '2026-10-05 08:08:08.686322+00', '{"Brand": "Godox", "Model": "SL60W"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (285, 'Студійне джерело світла Aputure Amaran 100x S Bi-Color', 'Професійний світлодіодний прожектор із регульованою колірною температурою 2700K-6500K.', 90, 1, 35, 9, NULL, NULL, '2026-10-05 08:08:08.686322+00', '{"Brand": "Aputure", "Model": "Amaran 100x S"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (276, 'Екшн-камера GoPro HERO12 Black', 'Флагманська екшн-камера з відео 5.3K60, стабілізацією HyperSmooth 6.0 та підтримкою HDR.', 93, 1, 32, 9, NULL, NULL, '2026-10-05 08:08:08.686322+00', '{"Brand": "GoPro", "Model": "HERO12 Black"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (300, 'Розумний конвектор Xiaomi Smart Space Heater S (KRDNQ03YD)', 'Безшумний електричний обігрівач потужністю 2200 Вт із захистом від вологи IPX4 та керуванням із Mi Home.', 94, 1, 43, 9, NULL, NULL, '2026-10-05 10:14:31.550596+00', '{"Brand": "Xiaomi", "Model": "Smart Space Heater S"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (301, 'Електричний конвектор Cooper&Hunter CH-2000 M', 'Надійний механічний обігрівач для приміщень площею до 25 кв.м із цілісним суцільним нагрівальним елементом.', 86, 1, 43, 9, NULL, NULL, '2026-10-05 10:14:31.550596+00', '{"Brand": "Cooper&Hunter", "Model": "CH-2000 M"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (302, 'Інверторний кондиціонер Midea Mission II MB-09N8D6-I/MB-09N8D6-O', 'Енергоефективна спліт-система з Wi-Fi модулем, роботою на обігрів до -25°C та вишуканим дизайном.', 88, 1, 44, 9, NULL, NULL, '2026-10-05 10:14:31.550596+00', '{"Brand": "Midea", "Model": "Mission II"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (303, 'Спліт-система Cooper&Hunter CH-S12FTXF-NG (Vital)', 'Популярний інверторний кондиціонер із генератором іонів Cold Plasma та преміальною енергозберігаючою комплектацією.', 93, 1, 44, 9, NULL, NULL, '2026-10-05 10:14:31.550596+00', '{"Brand": "Cooper&Hunter", "Model": "Vital Inverter"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (304, 'Очищувач повітря Xiaomi Smart Air Purifier 4 Pro', 'Високоефективний очищувач для великих кімнат до 60 кв.м із потрійною фільтрацією та OLED-дисплеєм.', 96, 1, 45, 9, NULL, NULL, '2026-10-05 10:14:31.550596+00', '{"Brand": "Xiaomi", "Model": "Smart Air Purifier 4 Pro"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (305, 'Кліматичний комплекс Boneco HYBRID H300', 'Універсальний прилад «2-в-1»: мийка повітря із природним зволоженням та фільтрацією HEPA.', 87, 1, 45, 9, NULL, NULL, '2026-10-05 10:14:31.550596+00', '{"Brand": "Boneco", "Model": "H300 Hybrid"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (306, 'Парогенератор Philips PerfectCare Series 8000 PSG8040/60', 'Потужна прасувальна система з автоматичною подачею пари OptimalTEMP без ризику пропалити тканину.', 90, 1, 46, 9, NULL, NULL, '2026-10-05 10:14:31.550596+00', '{"Brand": "Philips", "Model": "PerfectCare Series 8000"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (307, 'Парова праска Tefal Ultimate Pure FV9845E0 3200W', 'Потужна праска з унікальним фільтром Micro-Calc для запобігання появі плям від накипу.', 88, 1, 46, 9, NULL, NULL, '2026-10-05 10:14:31.550596+00', '{"Brand": "Tefal", "Model": "Ultimate Pure"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (312, 'Вбудована посудомийна машина Bosch Serie 4 SMV4EVX14E 60cm', 'Повнорозмірна вбудована посудомийка з третьою корзиною VarioDrawer та автоматичним відкриванням дверцят.', 93, 1, 49, 9, NULL, NULL, '2026-10-05 10:14:31.550596+00', '{"Brand": "Bosch", "Model": "Serie 4 SMV4EVX14E"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (313, 'Окремостояча посудомийна машина Electrolux 300 MaxiFlex 45cm', 'Вузька посудомийка з розбризкувачем SatelliteClean та технологією сушіння AirDry.', 87, 1, 49, 9, NULL, NULL, '2026-10-05 10:14:31.550596+00', '{"Brand": "Electrolux", "Model": "300 MaxiFlex"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (314, 'Електрична духовка Samsung Bespoke NV7B44205AK Dual Cook', 'Вбудована духова шафа з технологією подвійного приготування Dual Cook та очищенням каталітичною панеллю.', 94, 1, 50, 9, NULL, NULL, '2026-10-05 10:14:31.550596+00', '{"Brand": "Samsung", "Model": "Bespoke Dual Cook"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (315, 'Вбудована духова шафа Gorenje Simplicity BO76SYB', 'Елегантна чорна духовка зі склепінчастою формою камери HomeMade Plus та функцією очищення парою AquaClean.', 88, 1, 50, 9, NULL, NULL, '2026-10-05 10:14:31.550596+00', '{"Brand": "Gorenje", "Model": "Simplicity BO76SYB"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (273, 'Електронна книга PocketBook 634 Verse Pro 16GB', 'Компактний рідер з екранним аудіовиходом Bluetooth, підтримкою всіх форматів без конвертації.', 86, 1, 30, 9, NULL, NULL, '2026-10-04 21:22:20.623595+00', '{"Brand": "PocketBook", "Model": "Verse Pro 634", "Storage": 16, "ScreenSize": 6.0}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (274, 'Квадрокоптер DJI Mini 4 Pro Fly More Combo (DJI RC 2)', 'Легкий дрон вагою до 249г із всеспрямованим виявленням перешкод та зйомкою 4K/60fps HDR.', 97, 1, 31, 9, NULL, NULL, '2026-10-04 21:22:20.623595+00', '{"Brand": "DJI", "Model": "Mini 4 Pro"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (275, 'Квадрокоптер DJI Air 3 Fly More Combo', 'Дрон із двома основними камерами (ширококутна та 3x телефото) і часом польоту до 46 хвилин.', 90, 1, 31, 9, NULL, NULL, '2026-10-04 21:22:20.623595+00', '{"Brand": "DJI", "Model": "Air 3"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (277, 'Панорамна екшн-камера Insta360 X3', '360-градусна камера зі зйомкою 5.7K, сенсорним екраном 2.29" та водонепроникністю до 10м.', 89, 1, 32, 9, NULL, NULL, '2026-10-05 08:08:08.686322+00', '{"Brand": "Insta360", "Model": "X3"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (278, 'Бездзеркальний фотоапарат Sony Alpha a7 IV Body', 'Повнокадрова камера з матрицею 33 Мп, фокусуванням за очима реального часу та відео 4K 60p.', 94, 1, 33, 9, NULL, NULL, '2026-10-05 08:08:08.686322+00', '{"Brand": "Sony", "Model": "Alpha a7 IV"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (279, 'Бездзеркальна камера Canon EOS R6 Mark II Body', 'Універсальна повнокадрова камера зі швидкістю серійної зйомки до 40 к/с та 4K 60p без кропу.', 90, 1, 33, 9, NULL, NULL, '2026-10-05 08:08:08.686322+00', '{"Brand": "Canon", "Model": "EOS R6 Mark II"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (286, 'Мультипіч Xiaomi Smart Air Fryer Pro 4L White', 'Розумний аерогриль із прозорим віконцем, OLED-дисплеєм та дистанційним керуванням зі смартфона.', 95, 1, 42, 9, NULL, NULL, '2026-10-05 08:08:08.686322+00', '{"Brand": "Xiaomi", "Model": "Smart Air Fryer Pro 4L"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (287, 'Мультипіч-аерогриль Tefal Easy Fry & Grill XXL EY801D15', 'Універсальна мультипіч об''ємом 6.5 л із чавунною решіткою для гриля та чашею із розділювачем.', 88, 1, 42, 9, NULL, NULL, '2026-10-05 08:08:08.686322+00', '{"Brand": "Tefal", "Model": "Easy Fry & Grill XXL"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (288, 'Автоматична кавомашина DeLonghi Magnifica S ECAM 22.110.B', 'Класична кавомашина зі сталевим кавомолом, вибором міцності та ручним капучинатором.', 96, 1, 39, 9, NULL, NULL, '2026-10-05 08:08:08.686322+00', '{"Brand": "DeLonghi", "Model": "Magnifica S"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (289, 'Кавомашина Philips Series 3300 LatteGo EP3347/90', 'Сучасна кавомашина з автоматичною системою подачі молока LatteGo та кольоровим сенсорним екраном.', 92, 1, 39, 9, NULL, NULL, '2026-10-05 08:08:08.686322+00', '{"Brand": "Philips", "Model": "Series 3300 LatteGo"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (290, 'Робот-пилосос Roborock S8 Black', 'Флагманський робот для сухого та вологого прибирання із силою всмоктування 6000 Па та подвійними щітками.', 94, 1, 37, 9, NULL, NULL, '2026-10-05 08:08:08.686322+00', '{"Brand": "Roborock", "Model": "S8"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (291, 'Робот-пилосос Dreame Bot L10s Ultra зі станцією самоочищення', 'Повністю автономний робот із сушінням ганчірок гарячим повітрям та автоматичним доливом води.', 91, 1, 37, 9, NULL, NULL, '2026-10-05 08:08:08.686322+00', '{"Brand": "Dreame", "Model": "Bot L10s Ultra"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (292, 'Акумуляторний пилосос Dyson V15 Detect Absolute', 'Потужний бездротовий пилосос із лазерним підсвічуванням мікроскопічного пилу та лазерним датчиком.', 97, 1, 45, 9, NULL, NULL, '2026-10-05 08:08:08.686322+00', '{"Brand": "Dyson", "Model": "V15 Detect Absolute"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (196, 'Apple iPhone 15 Pro 128GB Natural Titanium', 'Флагманський смартфон Apple у титановому корпусі з чипом A17 Pro.', 95, 1, 1, 9, NULL, NULL, '2026-10-04 18:38:31.979792+00', '{"RAM": 8, "Brand": "Apple", "Has5G": true, "Model": "iPhone 15 Pro", "Storage": 128, "Processor": "A17 Pro", "ScreenSize": 6.1, "RefreshRate": 120}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (197, 'Samsung Galaxy S24 Ultra 12/512GB', 'Преміальний смартфон із підтримкою S Pen, штучним інтелектом Galaxy AI та камерою 200 Мп.', 92, 1, 1, 9, NULL, NULL, '2026-10-04 18:38:31.979792+00', '{"RAM": 12, "Brand": "Samsung", "Has5G": true, "Model": "Galaxy S24 Ultra", "Storage": 512, "Processor": "Snapdragon 8 Gen 3", "ScreenSize": 6.8, "RefreshRate": 120}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (198, 'Apple MacBook Pro 14 M3 16/512GB', 'Потужний ноутбук для розробки, обробки відео та професійних задач із дисплеєм Liquid Retina XDR.', 88, 1, 2, 9, NULL, NULL, '2026-10-04 18:38:31.979792+00', '{"RAM": 16, "Brand": "Apple", "Model": "MacBook Pro 14", "Storage": 512, "Processor": "Apple M3", "ScreenSize": 14.2, "RefreshRate": 120}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (199, 'ASUS ROG Zephyrus G16 OLED', 'Тонкий ігровий ноутбук із дисплеєм OLED 240 Гц, процесором Intel Core Ultra 9 та відеокартою RTX 4070.', 85, 1, 2, 9, NULL, NULL, '2026-10-04 18:38:31.979792+00', '{"RAM": 32, "Brand": "ASUS", "Model": "ROG Zephyrus G16", "Storage": 1024, "Processor": "Intel Core Ultra 9", "ScreenSize": 16.0, "RefreshRate": 240}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (200, 'Apple iPad Air 11 M2 128GB Wi-Fi', 'Легкий та потужний планшет із чипом M2 для роботи, малювання та розваг.', 80, 1, 3, 9, NULL, NULL, '2026-10-04 18:38:31.979792+00', '{"RAM": 8, "Brand": "Apple", "Model": "iPad Air 11", "Storage": 128, "Processor": "Apple M2", "ScreenSize": 11.0}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (201, 'Xiaomi Pad 6 8/256GB', 'Продуктивний планшет із екраном 144 Гц та металевим корпусом для щоденного використання.', 78, 1, 3, 9, NULL, NULL, '2026-10-04 18:38:31.979792+00', '{"RAM": 8, "Brand": "Xiaomi", "Model": "Pad 6", "Storage": 256, "Processor": "Snapdragon 870", "ScreenSize": 11.0, "RefreshRate": 144}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (202, 'Apple Watch Series 9 GPS 45mm', 'Смартгодинник із чипом S9, яскравішим екраном Retina та підтримкою жесту Double Tap.', 89, 1, 4, 9, NULL, NULL, '2026-10-04 18:38:31.979792+00', '{"Brand": "Apple", "Model": "Series 9", "ScreenSize": 1.9}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (203, 'Samsung Galaxy Watch 6 Classic 47mm', 'Смартгодинник із поворотним безелем, аналізом складу тіла та відстеженням фаз сну.', 81, 1, 4, 9, NULL, NULL, '2026-10-04 18:38:31.979792+00', '{"Brand": "Samsung", "Model": "Galaxy Watch 6 Classic", "ScreenSize": 1.5}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (204, 'Apple AirPods Pro 2 (USB-C)', 'Бездротові навушники з чудовим шумозаглушенням, адаптивним аудіо та кейсом USB-C.', 94, 1, 5, 9, NULL, NULL, '2026-10-04 18:38:31.979792+00', '{"Brand": "Apple", "Model": "AirPods Pro 2"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (205, 'Sony WH-1000XM5 Black', 'Повнорозмірні бездротові навушники з провідним активним шумозаглушенням ANC.', 90, 1, 5, 9, NULL, NULL, '2026-10-04 18:38:31.979792+00', '{"Brand": "Sony", "Model": "WH-1000XM5"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (206, 'JBL Charge 5 Black', 'Захищена портативна акустична система з потужним басом та функцією зарядки гаджетів.', 87, 1, 6, 9, NULL, NULL, '2026-10-04 18:38:31.979792+00', '{"Brand": "JBL", "Model": "Charge 5"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (207, 'Marshall Emberton II Black and Brass', 'Портативна колонка у стилі ретро з об''ємним звучанням 360° та 30+ годинами автономності.', 83, 1, 6, 9, NULL, NULL, '2026-10-04 18:38:31.979792+00', '{"Brand": "Marshall", "Model": "Emberton II"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (208, 'LG OLED 55 C3 Smart TV 4K', 'Телевізор OLED з контрастним зображенням, підтримкою 120 Гц, HDMI 2.1 та Dolby Atmos.', 86, 1, 7, 9, NULL, NULL, '2026-10-04 18:38:31.979792+00', '{"Brand": "LG", "Model": "OLED55C3", "ScreenSize": 55.0, "RefreshRate": 120}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (209, 'Samsung 55 QLED 4K Smart TV', '4K телевізор з квантовими точками, стильним тонким корпусом AirSlim та Smart TV.', 82, 1, 7, 9, NULL, NULL, '2026-10-04 18:38:31.979792+00', '{"Brand": "Samsung", "Model": "QE55Q60C", "ScreenSize": 55.0, "RefreshRate": 60}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (210, 'Sony PlayStation 5 Slim Disc Edition', 'Компактна ігрова консоль з дисководом, накопичувачем на 1 ТБ та геймпадом DualSense.', 96, 1, 8, 9, NULL, NULL, '2026-10-04 18:38:31.979792+00', '{"Brand": "Sony", "Model": "PlayStation 5 Slim", "Storage": 1024}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (211, 'Nintendo Switch OLED Model White', 'Портативна консоль із яскравим 7-дюймовим OLED-екраном для ігор удома та в дорозі.', 84, 1, 8, 9, NULL, NULL, '2026-10-04 18:38:31.979792+00', '{"Brand": "Nintendo", "Model": "Switch OLED", "Storage": 64, "ScreenSize": 7.0}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (212, 'ПК ARTLINE Gaming X77 (i7-13700F / RTX 4070)', 'Потужний системний блок для комфортного 2K геймінгу, відеомонтажу та 3D-моделювання.', 79, 1, 9, 9, NULL, NULL, '2026-10-04 18:38:31.979792+00', '{"RAM": 32, "Brand": "ARTLINE", "Model": "Gaming X77", "Storage": 1024, "Processor": "Intel Core i7-13700F"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (213, 'Ігровий ПК COBRA Advanced (Ryzen 5 5600 / RTX 3060)', 'Оптимальна збірка для сучасних ігор у Full HD з ефективною системою охолодження.', 81, 1, 9, 9, NULL, NULL, '2026-10-04 18:38:31.979792+00', '{"RAM": 16, "Brand": "COBRA", "Model": "Advanced", "Storage": 500, "Processor": "AMD Ryzen 5 5600"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (214, 'Монітор 27 ASUS ROG Swift OLED 240Hz', 'Ігровий QHD OLED монітор із відгуком 0.03 мс та високою частотою оновлення 240 Гц.', 87, 1, 10, 9, NULL, NULL, '2026-10-04 18:38:31.979792+00', '{"Brand": "ASUS", "Model": "ROG Swift PG27AQDM", "ScreenSize": 27.0, "RefreshRate": 240}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (215, 'Вигнутий монітор 27 Samsung Odyssey G5 165Hz', 'Вигнутий ігровий WQHD монітор з ефектом занурення 1000R та підтримкою AMD FreeSync.', 84, 1, 10, 9, NULL, NULL, '2026-10-04 18:38:31.979792+00', '{"Brand": "Samsung", "Model": "Odyssey G5", "ScreenSize": 27.0, "RefreshRate": 165}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (216, 'Процесор Intel Core i7-14700K s-1700 Box', '20-ядерний процесор Intel 14-го покоління для потужних ігрових та робочих станцій.', 89, 1, 11, 9, NULL, NULL, '2026-10-04 20:40:26.725736+00', '{"Brand": "Intel", "Model": "Core i7-14700K", "Processor": "i7-14700K"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (217, 'Процесор AMD Ryzen 7 7800X3D s-AM5', 'Найкращий ігровий процесор з технологією 3D V-Cache для максимального FPS в іграх.', 96, 1, 11, 9, NULL, NULL, '2026-10-04 20:40:26.725736+00', '{"Brand": "AMD", "Model": "Ryzen 7 7800X3D", "Processor": "Ryzen 7 7800X3D"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (218, 'Відеокарта ASUS ROG Strix GeForce RTX 4070 Ti SUPER 16GB', 'Потужна відеокарта з 16 ГБ відеопам’яті, просунутим охолодженням та підсвічуванням Aura Sync.', 91, 1, 12, 9, NULL, NULL, '2026-10-04 20:40:26.725736+00', '{"RAM": 16, "Brand": "ASUS", "Model": "ROG Strix RTX 4070 Ti Super"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (219, 'Відеокарта Gigabyte GeForce RTX 4060 Eagle OC 8GB', 'Оптимальне рішення для геймінгу в роздільній здатності Full HD з підтримкою DLSS 3.', 87, 1, 12, 9, NULL, NULL, '2026-10-04 20:40:26.725736+00', '{"RAM": 8, "Brand": "Gigabyte", "Model": "RTX 4060 Eagle OC"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (220, 'Материнська плата MSI MAG Z790 TOMAHAWK WIFI', 'Надійна ігрова материнська плата під сокет LGA1700 із підтримкою DDR5 та Wi-Fi 6E.', 83, 1, 13, 9, NULL, NULL, '2026-10-04 20:40:26.725736+00', '{"Brand": "MSI", "Model": "MAG Z790 TOMAHAWK"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (221, 'Материнська плата ASUS TUF GAMING B650-PLUS WIFI', 'Материнська плата для процесорів AMD Ryzen 7000/8000/9000 із потужним VRM.', 85, 1, 13, 9, NULL, NULL, '2026-10-04 20:40:26.725736+00', '{"Brand": "ASUS", "Model": "TUF GAMING B650-PLUS"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (222, 'Комплект оперативної пам’яті Kingston Fury DDR5 32GB (2x16GB) 6000MHz', 'Високошвидкісний комплект комплектів пам’яті DDR5 з підтримкою EXPO та XMP 3.0.', 88, 1, 14, 9, NULL, NULL, '2026-10-04 20:40:26.725736+00', '{"RAM": 32, "Brand": "Kingston", "Model": "Fury Beast", "RefreshRate": 6000}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (223, 'ОЗП Corsair Vengeance RGB DDR5 32GB (2x16GB) 6400MHz', 'Оперативна пам’ять преміум-класу з яскравим динамічним RGB-підсвічуванням.', 84, 1, 14, 9, NULL, NULL, '2026-10-04 20:40:26.725736+00', '{"RAM": 32, "Brand": "Corsair", "Model": "Vengeance RGB", "RefreshRate": 6400}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (224, 'SSD накопичувач Samsung 990 PRO NVMe M.2 2TB', 'Флагманський NVMe SSD накопичувач зі швидкістю читання до 7450 МБ/с.', 93, 1, 15, 9, NULL, NULL, '2026-10-04 20:40:26.725736+00', '{"Brand": "Samsung", "Model": "990 PRO", "Storage": 2048}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (225, 'SSD накопичувач Kingston NV2 1TB NVMe M.2', 'Надійний і доступний SSD для прискорення завантаження системи та ігор.', 90, 1, 15, 9, NULL, NULL, '2026-10-04 20:40:26.725736+00', '{"Brand": "Kingston", "Model": "NV2", "Storage": 1024}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (226, 'Жорсткий диск Western Digital Red Pro 8TB 3.5 SATA III', 'Спеціалізований HDD диск для цілодобової роботи в сітьових NAS-накопичувачах та серверах.', 76, 1, 16, 9, NULL, NULL, '2026-10-04 20:40:26.725736+00', '{"Brand": "Western Digital", "Model": "Red Pro", "Storage": 8192}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (227, 'Жорсткий диск Seagate Barracuda 2TB 7200rpm 3.5', 'Класичний накопичувач для зберігання фото, відео та бекапів на настільному ПК.', 81, 1, 16, 9, NULL, NULL, '2026-10-04 20:40:26.725736+00', '{"Brand": "Seagate", "Model": "Barracuda", "Storage": 2048}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (228, 'Блок живлення be quiet! Dark Power 13 1000W 80 Plus Titanium', 'Безшумний та надефективний блок живлення із сертифікатом 80 Plus Titanium та підтримкою ATX 3.0.', 86, 1, 17, 9, NULL, NULL, '2026-10-04 20:40:26.725736+00', '{"Brand": "be quiet!", "Model": "Dark Power 13"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (229, 'Блок живлення Chieftec Proton 750W 80 Plus Bronze', 'Надійний модульний блок живлення для ігрових комп’ютерів середнього рівня.', 82, 1, 17, 9, NULL, NULL, '2026-10-04 20:40:26.725736+00', '{"Brand": "Chieftec", "Model": "Proton 750W"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (230, 'Корпус для ПК NZXT H9 Flow White', 'Двокамерний корпус із панорамним загартованим склом та чудовою вентиляцією.', 91, 1, 18, 9, NULL, NULL, '2026-10-04 20:40:26.725736+00', '{"Brand": "NZXT", "Model": "H9 Flow"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (231, 'Корпус для ПК Deepcool CC560 ARGB Black', 'Місткий корпус формата Mid-Tower із 4 встановленими вентиляторами з ARGB підсвічуванням.', 87, 1, 18, 9, NULL, NULL, '2026-10-04 20:40:26.725736+00', '{"Brand": "Deepcool", "Model": "CC560"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (232, 'Система рідинного охолодження ARCTIC Liquid Freezer III 360 Black', 'Потужна трисекційна СРО для охолодження топових процесорів Intel та AMD.', 94, 1, 19, 9, NULL, NULL, '2026-10-04 20:40:26.725736+00', '{"Brand": "ARCTIC", "Model": "Liquid Freezer III 360"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (233, 'Процесорний кулер Deepcool AK620 Zero Dark', 'Двобаштовий бавовняний кулер з 6 тепловими трубками та двома 120мм вентиляторами.', 89, 1, 19, 9, NULL, NULL, '2026-10-04 20:40:26.725736+00', '{"Brand": "Deepcool", "Model": "AK620"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (234, 'Механічна клавіатура Logitech G PRO X TKL Lightspeed', 'Бездротова кіберспортивна клавіатура без цифрового блоку із перемикачами GX Red Linear.', 91, 1, 20, 9, NULL, NULL, '2026-10-04 20:40:26.725736+00', '{"Brand": "Logitech", "Model": "G PRO X TKL"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (235, 'Механічна клавіатура Keychron K2 V2 Hot-Swap RGB', 'Компактна 75% бездротова механічна клавіатура для Mac та Windows з можливістю гарячої заміни свічів.', 86, 1, 20, 9, NULL, NULL, '2026-10-04 20:40:26.725736+00', '{"Brand": "Keychron", "Model": "K2 V2"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (293, 'Акумуляторний пилосос Bosch Unlimited 7 BBS711W', 'Зручний бездротовий пилосос із гнучкою трубою Flex Tube та змінними акумуляторами Power for ALL.', 85, 1, 45, 9, NULL, NULL, '2026-10-05 08:08:08.686322+00', '{"Brand": "Bosch", "Model": "Unlimited 7"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (294, 'Фен для волосся Dyson Supersonic HD07 Nickel/Copper', 'Інноваційний фен із інтелектуальним контролем температури для захисту естетичного блиску волосся.', 98, 1, 47, 9, NULL, NULL, '2026-10-05 08:08:08.686322+00', '{"Brand": "Dyson", "Model": "Supersonic HD07"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (295, 'Професійний фен BaByliss PRO Veneziano HQ BAB6960IE', 'Потужний італійський професійний фен 2200 Вт із функцією іонізації та високою швидкістю потоку.', 87, 1, 47, 9, NULL, NULL, '2026-10-05 08:08:08.686322+00', '{"Brand": "BaByliss", "Model": "PRO Veneziano HQ"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (296, 'Електробритва Braun Series 9 Pro+ 9517s', 'Преміальна сіткова електробритва із плаваючою голівкою ProHead та тримером ProTrimmer.', 92, 1, 46, 9, NULL, NULL, '2026-10-05 10:14:31.550596+00', '{"Brand": "Braun", "Model": "Series 9 Pro+"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (297, 'Універсальний тример Philips OneBlade Pro 360 QP6551/15', 'Гібридний стайлер для підрівнювання, створення чітких контурів та гоління будь-якої довжини волосся.', 95, 1, 46, 9, NULL, NULL, '2026-10-05 10:14:31.550596+00', '{"Brand": "Philips", "Model": "OneBlade Pro 360"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (298, 'Електрична зубна щітка Oral-B iO Series 9 Black Onyx', 'Розумна щітка з магнітною технологією iO, кольоровим дисплеєм та 7 режимами чищення.', 91, 1, 48, 9, NULL, NULL, '2026-10-05 10:14:31.550596+00', '{"Brand": "Oral-B", "Model": "iO Series 9"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (299, 'Звукова зубна щітка Philips Sonicare DiamondClean 9000 HX9911/88', 'Звукова щітка з датчиком тиску, склянкою для зарядки та відстеженням через додаток.', 89, 1, 48, 9, NULL, NULL, '2026-10-05 10:14:31.550596+00', '{"Brand": "Philips", "Model": "Sonicare DiamondClean 9000"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (308, 'Пральна машина Bosch Serie 6 WAN28263UA 8kg', 'Надійна пральна машина з безшумним інверторним двигуном EcoSilence Drive та функцією SpeedPerfect.', 92, 1, 44, 9, NULL, NULL, '2026-10-05 10:14:31.550596+00', '{"Brand": "Bosch", "Model": "Serie 6"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (309, 'Вузька пральна машина LG AI DD F2V5HS0W 7kg', 'Пральна машина із системою інтелектуального визначення типів тканини AI DD та функцією пари Steam.', 94, 1, 44, 9, NULL, NULL, '2026-10-05 10:14:31.550596+00', '{"Brand": "LG", "Model": "F2V5HS0W"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (310, 'Двокамерний холодильник Samsung RL38T600FSA/UA 385L', 'Холодильник із технологією SpaceMax (більше корисного об''єму) та системою охолодження All-Around Cooling.', 91, 1, 43, 9, NULL, NULL, '2026-10-05 10:14:31.550596+00', '{"Brand": "Samsung", "Model": "RL38T600FSA"}', 1, 1);
INSERT INTO public."Advertisements" ("Id", "Title", "Description", "Popularity", "CurrencyId", "CategoryId", "SellerId", "BuyerId", "SoldAt", "CreatedAt", "Attributes", "ConditionId", "StatusId") VALUES (311, 'Холодильник Liebherr BluPerformance CNef 4335', 'Преміальний німецький холодильник з окремими контурами охолодження DuoCooling та NoFrost.', 89, 1, 43, 9, NULL, NULL, '2026-10-05 10:14:31.550596+00', '{"Brand": "Liebherr", "Model": "CNef 4335"}', 1, 1);


--
-- Data for Name: AdvertisementVariants; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (25, 196, 42999.00, 45999.00, 10, '{"Color": "Natural Titanium"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (26, 196, 42999.00, 45999.00, 5, '{"Color": "Black Titanium"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (27, 197, 53999.00, 57999.00, 8, '{"Color": "Titanium Gray"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (28, 197, 53999.00, 57999.00, 4, '{"Color": "Titanium Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (29, 198, 74999.00, 79999.00, 6, '{"Color": "Space Grey"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (30, 198, 74999.00, 79999.00, 3, '{"Color": "Silver"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (31, 199, 92999.00, 98999.00, 4, '{"Color": "Eclipse Gray"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (32, 200, 28999.00, 30999.00, 12, '{"Color": "Space Gray"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (33, 200, 28999.00, 30999.00, 7, '{"Color": "Blue"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (34, 201, 13999.00, 15499.00, 15, '{"Color": "Gravity Gray"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (35, 202, 18999.00, 20499.00, 10, '{"Color": "Midnight"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (36, 202, 18999.00, 20499.00, 6, '{"Color": "Starlight"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (37, 203, 14499.00, 16299.00, 8, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (38, 203, 14499.00, 16299.00, 5, '{"Color": "Silver"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (39, 204, 9999.00, 10999.00, 20, '{"Color": "White"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (40, 205, 14999.00, 16999.00, 9, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (41, 205, 14999.00, 16999.00, 4, '{"Color": "Silver"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (42, 206, 6499.00, 7199.00, 14, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (43, 206, 6499.00, 7199.00, 8, '{"Color": "Red"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (44, 207, 7299.00, 7999.00, 7, '{"Color": "Black and Brass"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (45, 208, 56999.00, 62999.00, 3, '{"Color": "Dark Silver"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (46, 209, 28999.00, 31999.00, 6, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (47, 210, 22999.00, 24999.00, 11, '{"Color": "White"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (48, 211, 13499.00, 14999.00, 9, '{"Color": "White"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (49, 212, 68999.00, 73999.00, 4, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (50, 213, 29999.00, 32999.00, 7, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (51, 214, 41999.00, 45999.00, 3, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (52, 215, 10999.00, 12499.00, 12, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (53, 216, 18499.00, 19999.00, 8, '{"Color": "Box"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (54, 217, 18999.00, 20499.00, 5, '{"Color": "Tray"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (55, 218, 45999.00, 48999.00, 4, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (56, 219, 14999.00, 16299.00, 12, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (57, 220, 12899.00, 13999.00, 6, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (58, 221, 8999.00, 9799.00, 9, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (59, 222, 5499.00, 5999.00, 15, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (60, 223, 6299.00, 6899.00, 8, '{"Color": "White"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (61, 224, 7999.00, 8699.00, 10, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (62, 225, 2699.00, 2999.00, 25, '{"Color": "Blue"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (63, 226, 9499.00, 10299.00, 5, '{"Color": "Silver"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (64, 227, 2499.00, 2799.00, 14, '{"Color": "Silver"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (65, 228, 12999.00, 13999.00, 4, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (66, 229, 3299.00, 3699.00, 11, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (67, 230, 7899.00, 8499.00, 6, '{"Color": "White"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (68, 231, 2499.00, 2799.00, 18, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (69, 232, 4899.00, 5399.00, 7, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (70, 233, 2899.00, 3199.00, 12, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (71, 234, 8499.00, 9299.00, 6, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (72, 234, 8499.00, 9299.00, 3, '{"Color": "White"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (73, 235, 4199.00, 4599.00, 9, '{"Color": "Grey"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (95, 256, 3299.00, 3699.00, 10, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (96, 257, 5899.00, 6499.00, 6, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (97, 258, 6299.00, 6899.00, 12, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (98, 258, 6299.00, 6899.00, 4, '{"Color": "White"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (99, 259, 17999.00, 19499.00, 5, '{"Color": "Dark Grey"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (100, 260, 11499.00, 12499.00, 7, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (101, 261, 2999.00, 3399.00, 18, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (102, 262, 4299.00, 4699.00, 14, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (103, 262, 4299.00, 4699.00, 8, '{"Color": "Blue"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (104, 263, 7899.00, 8499.00, 6, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (105, 264, 5499.00, 5999.00, 11, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (106, 265, 2899.00, 3299.00, 15, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (107, 266, 2299.00, 2599.00, 20, '{"Color": "Space Grey"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (108, 267, 2499.00, 2799.00, 9, '{"Color": "White"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (109, 268, 999.00, 1199.00, 25, '{"Color": "Clear"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (110, 269, 2899.00, 3199.00, 7, '{"Color": "Black/Grey"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (111, 270, 699.00, 849.00, 30, '{"Color": "Transparent"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (112, 271, 899.00, 999.00, 20, '{"Color": "Transparent"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (113, 272, 6499.00, 6999.00, 10, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (114, 272, 6499.00, 6999.00, 5, '{"Color": "Denim"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (115, 273, 7199.00, 7799.00, 8, '{"Color": "Red"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (116, 274, 46999.00, 49999.00, 4, '{"Color": "Gray"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (117, 275, 58999.00, 62999.00, 3, '{"Color": "Gray"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (118, 276, 16999.00, 18499.00, 8, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (119, 277, 18999.00, 20499.00, 5, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (120, 278, 98999.00, 104999.00, 3, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (121, 279, 94999.00, 99999.00, 4, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (122, 280, 44999.00, 47999.00, 6, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (123, 281, 22999.00, 24999.00, 9, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (124, 282, 22499.00, 24199.00, 5, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (125, 283, 8299.00, 8999.00, 8, '{"Color": "Gray"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (126, 284, 5199.00, 5699.00, 11, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (127, 285, 10899.00, 11899.00, 4, '{"Color": "White"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (128, 286, 3499.00, 3899.00, 16, '{"Color": "White"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (129, 287, 6999.00, 7699.00, 7, '{"Color": "Stainless Steel"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (130, 288, 13499.00, 14999.00, 12, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (131, 289, 21999.00, 23999.00, 6, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (132, 290, 21999.00, 23999.00, 8, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (133, 290, 21999.00, 23999.00, 5, '{"Color": "White"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (134, 291, 32999.00, 35999.00, 4, '{"Color": "White"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (135, 292, 28999.00, 30999.00, 9, '{"Color": "Yellow/Nickel"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (136, 293, 12499.00, 13799.00, 7, '{"Color": "White"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (137, 294, 17999.00, 19499.00, 15, '{"Color": "Nickel/Copper"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (138, 294, 17999.00, 19499.00, 8, '{"Color": "Fuchsia"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (139, 295, 2799.00, 3199.00, 12, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (140, 296, 14999.00, 16499.00, 7, '{"Color": "Silver"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (141, 297, 3299.00, 3699.00, 15, '{"Color": "Black/Green"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (142, 298, 11999.00, 12999.00, 8, '{"Color": "Black Onyx"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (143, 299, 8499.00, 9299.00, 10, '{"Color": "Pink"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (144, 299, 8499.00, 9299.00, 5, '{"Color": "Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (145, 300, 3299.00, 3699.00, 18, '{"Color": "White"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (146, 301, 2199.00, 2499.00, 12, '{"Color": "White"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (147, 302, 18999.00, 20999.00, 6, '{"Color": "White"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (148, 303, 21499.00, 23499.00, 8, '{"Color": "White"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (149, 304, 8299.00, 8999.00, 11, '{"Color": "White"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (150, 305, 14999.00, 16299.00, 4, '{"Color": "White"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (151, 306, 15999.00, 17499.00, 5, '{"Color": "Dark Blue"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (152, 307, 4599.00, 4999.00, 14, '{"Color": "Black/Rose Gold"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (153, 308, 19999.00, 21999.00, 7, '{"Color": "White"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (154, 309, 17499.00, 18999.00, 9, '{"Color": "White"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (155, 310, 23999.00, 25999.00, 6, '{"Color": "Silver"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (156, 311, 34999.00, 37999.00, 3, '{"Color": "Inox"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (157, 312, 21999.00, 23999.00, 5, '{"Color": "Fully Integrated"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (158, 313, 15499.00, 16999.00, 8, '{"Color": "Silver"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (159, 314, 20999.00, 22999.00, 6, '{"Color": "Clean Black"}');
INSERT INTO public."AdvertisementVariants" ("Id", "AdvertisementId", "Price", "PreviousPrice", "Stock", "Attributes") VALUES (160, 315, 14299.00, 15799.00, 10, '{"Color": "Black"}');


--
-- Data for Name: AttributeDictionaries; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."AttributeDictionaries" ("Id", "Code") VALUES (5, 'colors');


--
-- Data for Name: AttributeDefinitions; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (9, 1, 'Brand', 'String', true, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (10, 1, 'Model', 'String', true, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (11, 1, 'Processor', 'String', false, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (12, 1, 'RAM', 'Integer', false, 1, 64, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (13, 1, 'Storage', 'Integer', false, 1, 2048, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (14, 1, 'ScreenSize', 'Decimal', false, 4, 20, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (15, 1, 'RefreshRate', 'Integer', false, 60, 240, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (16, 1, 'Has5G', 'Boolean', false, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (18, 2, 'Brand', 'String', true, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (19, 2, 'Model', 'String', true, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (20, 2, 'Processor', 'String', false, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (21, 2, 'RAM', 'Integer', false, 1, 128, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (22, 2, 'Storage', 'Integer', false, 1, 4096, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (23, 2, 'ScreenSize', 'Decimal', false, 10, 20, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (24, 2, 'RefreshRate', 'Integer', false, 60, 360, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (26, 3, 'Brand', 'String', true, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (27, 3, 'Model', 'String', true, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (28, 3, 'Processor', 'String', false, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (29, 3, 'RAM', 'Integer', false, 1, 32, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (30, 3, 'Storage', 'Integer', false, 1, 2048, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (31, 3, 'ScreenSize', 'Decimal', false, 7, 15, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (32, 3, 'RefreshRate', 'Integer', false, 60, 144, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (34, 4, 'Brand', 'String', true, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (35, 4, 'Model', 'String', true, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (36, 4, 'ScreenSize', 'Decimal', false, 1, 3, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (38, 5, 'Brand', 'String', true, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (39, 5, 'Model', 'String', true, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (41, 6, 'Brand', 'String', true, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (42, 6, 'Model', 'String', true, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (44, 7, 'Brand', 'String', true, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (45, 7, 'Model', 'String', true, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (46, 7, 'ScreenSize', 'Decimal', false, 24, 100, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (47, 7, 'RefreshRate', 'Integer', false, 50, 240, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (49, 8, 'Brand', 'String', true, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (50, 8, 'Model', 'String', true, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (51, 8, 'Storage', 'Integer', false, 1, 2048, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (52, 8, 'ScreenSize', 'Decimal', false, 5, 10, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (54, 9, 'Brand', 'String', true, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (55, 9, 'Model', 'String', true, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (56, 9, 'Processor', 'String', false, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (57, 9, 'RAM', 'Integer', false, 1, 256, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (58, 9, 'Storage', 'Integer', false, 1, 8192, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (60, 10, 'Brand', 'String', true, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (61, 10, 'Model', 'String', true, 0, 0, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (62, 10, 'ScreenSize', 'Decimal', false, 15, 60, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (63, 10, 'RefreshRate', 'Integer', false, 60, 360, false, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (17, 1, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (25, 2, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (33, 3, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (37, 4, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (40, 5, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (43, 6, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (48, 7, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (53, 8, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (59, 9, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (64, 10, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (65, 11, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (66, 12, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (67, 13, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (68, 14, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (69, 15, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (70, 16, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (71, 17, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (72, 18, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (73, 19, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (74, 20, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (75, 21, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (76, 22, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (77, 23, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (78, 24, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (79, 25, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (80, 26, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (81, 27, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (82, 28, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (83, 29, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (84, 30, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (85, 31, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (86, 32, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (87, 33, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (88, 34, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (89, 35, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (90, 36, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (91, 37, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (92, 38, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (93, 39, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (94, 40, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (95, 41, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (96, 42, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (97, 43, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (98, 44, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (99, 45, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (100, 46, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (101, 47, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (102, 48, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (103, 49, 'Color', 'String', false, NULL, NULL, true, false, NULL);
INSERT INTO public."AttributeDefinitions" ("Id", "CategoryId", "Key", "Type", "Required", "Min", "Max", "IsVariant", "Multiple", "DictionaryId") VALUES (104, 50, 'Color', 'String', false, NULL, NULL, true, false, NULL);


--
-- Data for Name: AttributeDefinitionTranslations; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- Data for Name: AttributeDictionaryValues; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."AttributeDictionaryValues" ("Id", "DictionaryId", "Code", "Value") VALUES (5, 5, 'natural-titanium', 'Natural Titanium');
INSERT INTO public."AttributeDictionaryValues" ("Id", "DictionaryId", "Code", "Value") VALUES (6, 5, 'black-titanium', 'Black Titanium');
INSERT INTO public."AttributeDictionaryValues" ("Id", "DictionaryId", "Code", "Value") VALUES (7, 5, 'titanium-gray', 'Titanium Gray');
INSERT INTO public."AttributeDictionaryValues" ("Id", "DictionaryId", "Code", "Value") VALUES (8, 5, 'titanium-black', 'Titanium Black');
INSERT INTO public."AttributeDictionaryValues" ("Id", "DictionaryId", "Code", "Value") VALUES (9, 5, 'space-grey', 'Space Grey');
INSERT INTO public."AttributeDictionaryValues" ("Id", "DictionaryId", "Code", "Value") VALUES (10, 5, 'silver', 'Silver');
INSERT INTO public."AttributeDictionaryValues" ("Id", "DictionaryId", "Code", "Value") VALUES (11, 5, 'eclipse-gray', 'Eclipse Gray');
INSERT INTO public."AttributeDictionaryValues" ("Id", "DictionaryId", "Code", "Value") VALUES (12, 5, 'space-gray', 'Space Gray');
INSERT INTO public."AttributeDictionaryValues" ("Id", "DictionaryId", "Code", "Value") VALUES (13, 5, 'blue', 'Blue');
INSERT INTO public."AttributeDictionaryValues" ("Id", "DictionaryId", "Code", "Value") VALUES (14, 5, 'gravity-gray', 'Gravity Gray');
INSERT INTO public."AttributeDictionaryValues" ("Id", "DictionaryId", "Code", "Value") VALUES (15, 5, 'midnight', 'Midnight');
INSERT INTO public."AttributeDictionaryValues" ("Id", "DictionaryId", "Code", "Value") VALUES (16, 5, 'starlight', 'Starlight');
INSERT INTO public."AttributeDictionaryValues" ("Id", "DictionaryId", "Code", "Value") VALUES (17, 5, 'black', 'Black');
INSERT INTO public."AttributeDictionaryValues" ("Id", "DictionaryId", "Code", "Value") VALUES (18, 5, 'white', 'White');
INSERT INTO public."AttributeDictionaryValues" ("Id", "DictionaryId", "Code", "Value") VALUES (19, 5, 'red', 'Red');
INSERT INTO public."AttributeDictionaryValues" ("Id", "DictionaryId", "Code", "Value") VALUES (20, 5, 'black-and-brass', 'Black and Brass');
INSERT INTO public."AttributeDictionaryValues" ("Id", "DictionaryId", "Code", "Value") VALUES (21, 5, 'dark-silver', 'Dark Silver');


--
-- Data for Name: AttributeDictionaryValueTranslations; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- Data for Name: Avatars; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."Avatars" ("Id", "UserId", "Url", "PublicId") VALUES (15, 3, 'https://res.cloudinary.com/dn3ne3ci6/image/upload/v1787341505/ShagOx/pzjeynelqmrvgfbm6lwz.jpg', 'ShagOx/pzjeynelqmrvgfbm6lwz');
INSERT INTO public."Avatars" ("Id", "UserId", "Url", "PublicId") VALUES (26, 7, 'https://res.cloudinary.com/dn3ne3ci6/image/upload/v1790707075/ShagOx/kjm8vrcd14hqfbhfevdh.jpg', 'ShagOx/kjm8vrcd14hqfbhfevdh');


--
-- Data for Name: BasketAttributes; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- Data for Name: Baskets; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."Baskets" ("Id", "UserId") VALUES (5, 7);


--
-- Data for Name: BasketItems; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."BasketItems" ("Id", "BasketId", "AdvertisementVariantId", "Quantity") VALUES (66, 5, 100, 1);
INSERT INTO public."BasketItems" ("Id", "BasketId", "AdvertisementVariantId", "Quantity") VALUES (67, 5, 99, 1);
INSERT INTO public."BasketItems" ("Id", "BasketId", "AdvertisementVariantId", "Quantity") VALUES (68, 5, 105, 1);
INSERT INTO public."BasketItems" ("Id", "BasketId", "AdvertisementVariantId", "Quantity") VALUES (69, 5, 96, 1);
INSERT INTO public."BasketItems" ("Id", "BasketId", "AdvertisementVariantId", "Quantity") VALUES (70, 5, 104, 1);
INSERT INTO public."BasketItems" ("Id", "BasketId", "AdvertisementVariantId", "Quantity") VALUES (71, 5, 106, 1);
INSERT INTO public."BasketItems" ("Id", "BasketId", "AdvertisementVariantId", "Quantity") VALUES (73, 5, 102, 1);
INSERT INTO public."BasketItems" ("Id", "BasketId", "AdvertisementVariantId", "Quantity") VALUES (74, 5, 95, 1);
INSERT INTO public."BasketItems" ("Id", "BasketId", "AdvertisementVariantId", "Quantity") VALUES (75, 5, 101, 1);
INSERT INTO public."BasketItems" ("Id", "BasketId", "AdvertisementVariantId", "Quantity") VALUES (72, 5, 97, 2);
INSERT INTO public."BasketItems" ("Id", "BasketId", "AdvertisementVariantId", "Quantity") VALUES (76, 5, 109, 1);
INSERT INTO public."BasketItems" ("Id", "BasketId", "AdvertisementVariantId", "Quantity") VALUES (77, 5, 111, 1);


--
-- Data for Name: CategoryTranslations; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (72, 'Смартфони', 1, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (73, 'Ноутбуки', 2, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (74, 'Планшети', 3, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (75, 'Смартгодинники', 4, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (76, 'Навушники', 5, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (77, 'Бездротові колонки', 6, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (78, 'Телевізори', 7, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (79, 'Ігрові приставки', 8, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (80, 'Настільні ПК', 9, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (81, 'Монітори', 10, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (82, 'Процесори', 11, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (83, 'Відеокарти', 12, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (84, 'Материнські плати', 13, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (85, 'Оперативна пам''ять', 14, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (86, 'SSD накопичувачі', 15, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (87, 'Жорсткі диски (HDD)', 16, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (88, 'Блоки живлення', 17, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (89, 'Корпуси для ПК', 18, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (90, 'Системи охолодження', 19, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (91, 'Клавіатури', 20, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (92, 'Мишки та килимки', 21, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (93, 'Вебкамери', 22, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (94, 'Мікрофони', 23, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (95, 'Мережеві маршрутизатори', 24, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (96, 'Зовнішні накопичувачі', 25, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (97, 'Павербанки', 26, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (98, 'Зарядні пристрої та кабелі', 27, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (99, 'Чохли для смартфонів', 28, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (100, 'Захисне скло та плівки', 29, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (101, 'Електронні книги', 30, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (102, 'Дрони та квадрокоптери', 31, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (103, 'Екшн-камери', 32, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (104, 'Дзеркальні та бездзеркальні камери', 33, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (105, 'Хаби розумного дому', 34, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (106, 'Розумне освітлення', 35, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (107, 'Камери відеоспостереження', 36, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (108, 'Роботи-пилососи', 37, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (109, 'Очищувачі повітря', 38, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (110, 'Кавоварки та кавомашини', 39, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (111, 'Мікрохвильові печі', 40, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (112, 'Електрочайники', 41, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (113, 'Мультипечі та аерогрилі', 42, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (114, 'Холодильники', 43, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (115, 'Пральні машини', 44, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (116, 'Пилососи', 45, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (117, 'Електробритви та тримери', 46, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (118, 'Фени для волосся', 47, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (119, 'Електричні зубні щітки', 48, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (120, 'Електросамокати', 49, 'uk');
INSERT INTO public."CategoryTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (121, 'Зарядні станції', 50, 'uk');


--
-- Data for Name: CityTranslations; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- Data for Name: ConditionTranslations; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."ConditionTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (1, 'Новий', 0, 'uk');
INSERT INTO public."ConditionTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (2, 'Вживаний (як новий)', 1, 'uk');
INSERT INTO public."ConditionTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (3, 'Вживаний (добрий стан)', 2, 'uk');
INSERT INTO public."ConditionTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (4, 'Вживаний (задовільний стан)', 3, 'uk');
INSERT INTO public."ConditionTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (5, 'На запчастини / несправний', 4, 'uk');


--
-- Data for Name: Favorites; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."Favorites" ("Id", "UserId", "AdvertisementId") VALUES (110, 7, 260);
INSERT INTO public."Favorites" ("Id", "UserId", "AdvertisementId") VALUES (111, 7, 265);
INSERT INTO public."Favorites" ("Id", "UserId", "AdvertisementId") VALUES (112, 7, 259);
INSERT INTO public."Favorites" ("Id", "UserId", "AdvertisementId") VALUES (115, 7, 256);
INSERT INTO public."Favorites" ("Id", "UserId", "AdvertisementId") VALUES (117, 7, 258);
INSERT INTO public."Favorites" ("Id", "UserId", "AdvertisementId") VALUES (118, 7, 257);


--
-- Data for Name: Images; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (216, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144410/364827001_wlz7hh.webp', 1, 196, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (217, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144410/364826989_lphotg.webp', 2, 196, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (218, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144418/364827007_okiaqh.webp', 3, 196, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (219, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144594/475767125_ixx475.webp', 1, 197, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (220, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144603/475767126_pro9o3.webp', 2, 197, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (221, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144605/475767127_lzpk2z.webp', 3, 197, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (222, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144704/655339080_erwi1u.webp', 1, 198, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (223, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144720/655339086_tbop03.webp', 2, 198, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (224, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144723/655339092_pcsqoj.webp', 3, 198, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (225, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144807/668569970_i6mkhq.webp', 1, 199, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (226, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144811/668569971_zwirjg.webp', 2, 199, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (227, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144813/668569972_c26jsb.webp', 3, 199, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (228, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144884/526087492_iombb1.webp', 1, 200, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (229, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144888/664850304_mezn7v.webp', 2, 200, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (230, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144889/526087494_imwl9e.webp', 3, 200, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (231, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144976/558718757_wzicsi.webp', 1, 201, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (232, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144980/558718766_nrklby.webp', 2, 201, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (233, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791144992/558718773_ngevrw.webp', 3, 201, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (234, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145067/530592347_dbyps4.webp', 1, 202, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (235, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145073/530592352_f0q8zt.webp', 2, 202, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (236, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145187/568716128_qi25po.webp', 1, 203, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (237, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145191/568716137_srq9t2.webp', 2, 203, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (238, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145193/568716146_sm52e1.webp', 3, 203, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (239, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145333/594443650_ak74lf.webp', 1, 204, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (242, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145339/594443651_fehz5o.webp', 2, 204, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (243, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145341/594443652_tpvy17.webp', 3, 204, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (244, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145445/496254834_pd8lkn.webp', 1, 205, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (245, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145462/496254836_ur4ltw.webp', 2, 205, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (246, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145463/496254835_hnkkcx.webp', 3, 205, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (247, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145508/385555052_mdhg6p.webp', 1, 206, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (248, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145524/385555060_yg33sf.webp', 2, 206, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (249, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145526/385555070_kvhfsd.webp', 3, 206, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (250, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145585/437537053_ht3fzb.webp', 1, 207, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (251, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145586/437537057_zpdmvg.webp', 2, 207, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (252, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145588/437537060_dws5xs.webp', 3, 207, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (253, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145661/668791078_ubbbac.webp', 1, 208, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (254, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145673/647475190_ucrftr.webp', 2, 208, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (255, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145675/647475191_ceikbc.webp', 3, 208, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (256, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145724/683053701_f43kqc.webp', 1, 209, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (257, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145728/545471050_ufisbk.webp', 2, 209, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (258, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145730/545471056_mn0mw8.webp', 3, 209, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (259, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145808/520314728_hiphys.webp', 1, 210, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (260, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145825/520314738_qiyljw.webp', 2, 210, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (261, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145836/520314734_kgnu5e.webp', 3, 210, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (262, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145889/231872455_txl2m3.webp', 1, 211, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (263, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145891/231872459_hzwsdf.webp', 2, 211, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (264, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145892/231872462_nnxnxx.webp', 3, 211, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (265, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145982/575205556_f72w3e.webp', 1, 212, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (266, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145992/575281377_kgknjb.webp', 2, 212, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (267, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791145994/575207927_xkfn0g.webp', 3, 212, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (268, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146049/620425198_swfp1h.webp', 1, 213, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (269, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146068/620417564_e2smg3.webp', 2, 213, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (270, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146070/589237643_hcpvas.webp', 3, 213, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (271, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146125/675992750_irpcfo.webp', 1, 214, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (272, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146133/674241713_dggzky.webp', 2, 214, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (273, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146135/674241714_glyg9m.webp', 3, 214, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (274, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146224/629962972_navqi9.webp', 1, 215, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (275, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146238/629963007_avghwz.webp', 2, 215, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (276, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146250/629962994_s9sm2f.webp', 3, 215, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (277, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146461/366100886_juexp9.webp', 1, 216, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (278, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146472/291331343_pfruyy.webp', 2, 216, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (279, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146486/366100886_mirzmt.webp', 3, 216, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (280, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146528/510976566_f0tmq0.webp', 1, 217, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (281, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146596/505784216_exva5a.webp', 1, 218, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (282, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146618/505784217_khmxee.webp', 2, 218, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (283, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146620/505784218_g7xl2z.webp', 3, 218, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (284, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146665/583841741_rfvdll.webp', 1, 219, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (285, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146668/583841752_fvokee.webp', 2, 219, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (286, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146670/583841765_h4s7az.webp', 3, 219, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (287, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146733/328674528_agzig4.webp', 1, 220, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (288, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146743/328674583_p604jz.webp', 2, 220, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (290, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146754/328674631_cc9xuf.webp', 3, 220, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (291, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146845/502800245_uaspzj.webp', 1, 221, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (292, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146845/502800245_uaspzj.webp', 2, 221, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (293, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146879/502800247_oi4paz.webp', 3, 221, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (294, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146919/290600870_zqkb01.webp', 1, 222, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (295, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146928/290600871_bsjlfn.webp', 2, 222, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (296, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146939/290600872_ypayhn.webp', 3, 222, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (297, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146995/554615140_adcthz.webp', 1, 223, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (298, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791146998/554615155_aj7uts.webp', 2, 223, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (299, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147008/554615161_qzk85l.webp', 3, 223, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (300, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147048/380104127_k126uc.webp', 1, 224, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (301, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147051/380104133_yv8c4a.webp', 2, 224, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (302, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147073/380104143_lpjwdx.webp', 3, 224, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (303, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147122/458852748_ltl3ee.webp', 1, 225, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (304, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147132/458852749_f3vxfr.webp', 2, 225, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (305, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147144/458852751_li9olo.webp', 3, 225, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (306, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147189/12241784_ilflwe.webp', 1, 227, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (307, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147192/567295109_bqifke.webp', 2, 227, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (308, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147218/567295110_ddmqtw.webp', 3, 227, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (309, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147245/654896273_xy1tsr.webp', 1, 228, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (310, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147248/654896277_rvk9ge.webp', 2, 228, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (311, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147269/654896282_md4vgx.webp', 3, 228, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (312, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147319/11274834_gopw0b.webp', 1, 229, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (313, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147322/11274851_pavxdm.webp', 2, 229, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (314, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147325/11274865_fpanry.webp', 3, 229, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (315, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147385/622563066_gt81vt.webp
', 1, 230, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (316, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147449/409729778_amgxlw.webp', 1, 231, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (317, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147451/409729779_nsvc1w.webp', 2, 231, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (318, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147453/409729589_z2aliw.webp', 3, 231, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (319, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147510/562264048_jgpyc8.webp', 1, 232, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (320, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147513/562264055_lobuqb.webp', 2, 232, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (321, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147514/562264063_xpetpv.webp', 3, 232, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (322, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147571/428354106_lt4oq6.webp', 1, 233, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (323, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147573/428354107_dipvbc.webp', 2, 233, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (324, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147574/428354108_yc0l3m.webp', 3, 233, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (325, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147651/361655136_kotiti.webp', 1, 234, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (326, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147658/361655138_pwc2ss.webp', 2, 234, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (327, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147678/361655139_lsyfvf.webp', 3, 234, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (328, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147729/646978316_wxgxqp.webp', 1, 235, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (329, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147732/646978317_y41bp5.webp', 2, 235, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (330, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791147734/646978318_kvyi60.webp', 3, 235, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (332, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149022/263194700_myiro7.webp', 1, 256, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (333, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149025/263194701_kg2xif.webp', 2, 256, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (334, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149034/263194702_h0fsgo.webp', 3, 256, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (335, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149082/487803370_snsogo.webp', 1, 257, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (336, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149085/487803382_nr9bvx.webp', 2, 257, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (337, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149086/487803394_xp8r7a.webp', 3, 257, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (338, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149169/494892667_balkaq.webp', 1, 258, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (339, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149171/494892668_vfs6wx.webp', 2, 258, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (340, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149181/494892669_hbvj9g.webp', 3, 258, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (341, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149230/570800964_er9gg5.webp', 1, 259, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (342, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149233/570800966_ijj1lz.webp', 2, 259, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (343, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149234/570800967_ulnkuc.webp', 3, 259, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (344, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149301/574987280_hd3dcn.webp', 1, 260, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (345, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149304/568433682_xds4rn.webp', 2, 260, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (346, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149306/568433683_z8s7sy.webp', 3, 260, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (347, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149374/227189617_unqx9t.webp', 1, 261, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (348, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149377/227189616_sdtb3r.webp', 2, 261, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (349, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149378/227189615_xxtbqr.webp', 3, 261, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (350, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149433/8932640_sacel7.webp', 1, 262, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (351, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149435/26954598_tnei1e.webp', 2, 262, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (352, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149436/26954606_drbp1d.webp', 3, 262, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (353, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149514/192152371_u3op0x.webp', 1, 263, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (354, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149518/192152386_ttuhb6.webp', 2, 263, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (355, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149520/192152399_luqftn.webp', 3, 263, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (356, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149613/425530423_uda6rw.webp', 1, 264, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (357, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149616/425530424_ke9dvy.webp', 2, 264, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (358, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149627/425530425_l4ndyp.webp', 3, 264, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (359, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149673/294910181_irflez.webp', 1, 265, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (360, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149677/294910182_tvlq67.webp', 2, 265, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (361, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149678/294910183_gaoxkj.webp', 3, 265, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (362, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149726/474801847_ivs5tr.webp', 1, 266, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (363, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149844/632056287_dmluid.webp', 1, 267, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (364, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149885/653056722_lshkrt.webp', 1, 268, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (365, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149921/632393017_tqplfm.webp', 1, 269, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (366, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149925/632393024_pbv2lv.webp', 2, 269, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (367, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149926/632393030_fiu3yd.webp', 3, 269, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (368, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791149996/503511231_hzviki.webp', 1, 270, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (369, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150000/503510162_ofi5un.webp', 2, 270, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (370, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150016/503510158_q4el37.webp', 3, 270, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (371, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150078/676802712_il3uax.webp', 1, 271, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (372, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150081/676802708_bkorbf.webp', 2, 271, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (373, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150084/676802709_xt2nm3.webp', 3, 271, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (374, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150158/485705984_tfyyi1.webp', 1, 272, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (375, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150160/494572302_ltyo89.webp', 2, 272, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (376, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150174/494572281_b5blah.webp', 3, 272, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (377, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150237/370432191_txvmhz.webp', 1, 273, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (378, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150239/370432192_yxhlvj.webp', 2, 273, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (379, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150240/370432193_dlps9d.webp', 3, 273, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (380, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150416/dji-mini-4-pro-fly-more-combo-dji-rc-2-cp-ma-00000735-01_a94v8j.webp', 1, 274, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (381, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150436/o1oazeoojtcadjpb09wt75he2nrvmfcl_supllv.webp', 2, 274, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (382, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150446/ahn1p8i2ui1lydf2b1iwcwwfqeaxziqb_begzna.webp', 3, 274, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (383, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150507/372330916_m5v11e.webp', 1, 275, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (384, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150518/51ynlb7lwvl_i2cxpt.webp', 2, 275, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (385, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791150520/dji-air-3-drone-fly-more-combo-with-rc-n2-cp-ma-00000692-01-2_xz6xh2.webp', 3, 275, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (386, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791187727/674221389_vucfxd.webp', 1, 276, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (387, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791187757/529960864_rol5zs.webp', 1, 277, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (388, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791187760/529960875_vhuagb.webp', 2, 277, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (389, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791187761/529960521_wn3l2g.webp', 3, 277, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (390, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791187809/651863404_vo0f6b.webp', 1, 278, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (391, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193303/525600963_xuvrdv.webp', 2, 278, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (392, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193310/525600982_zd8xrc.webp', 3, 278, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (393, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193364/304428962_kvd810.webp', 1, 279, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (394, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193367/304428963_iqd3ic.webp', 2, 279, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (395, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193368/304427987_byg7an.webp', 3, 279, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (396, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193429/595086769_zcwasd.webp', 1, 280, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (397, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193431/595086773_unoamq.webp', 2, 280, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (398, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193435/595086791_pfv9rc.webp', 3, 280, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (399, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193492/651411318_pyycug.webp', 1, 281, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (400, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193496/651411320_zdyj9v.webp', 2, 281, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (401, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193498/651411321_yzo1t2.webp', 3, 281, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (402, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193552/528508640_kemhby.webp', 1, 282, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (403, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193555/528508652_zr9saf.webp', 2, 282, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (404, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193556/528508664_dcffui.webp', 3, 282, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (405, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193611/574225276_zsnix2.webp', 1, 283, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (406, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193615/661671378_illflr.webp', 2, 283, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (407, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193617/574225289_iorl6s.webp', 3, 283, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (408, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193671/677023226_hmeten.webp', 1, 284, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (409, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193687/677023227_rvx3yc.webp', 2, 284, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (410, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193689/677023229_z8ulqj.webp', 3, 284, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (411, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193856/679986143_d09kwl.webp', 1, 285, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (412, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193863/679986146_i72uh7.webp', 2, 285, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (413, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193873/679986149_pibac5.webp', 3, 285, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (414, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193961/389880232_po86no.webp', 1, 286, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (415, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193963/389880233_hmdldh.webp', 2, 286, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (416, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791193974/389880234_qd2don.webp', 3, 286, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (417, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194040/458009734_toqcaz.webp', 1, 287, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (418, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194045/471558300_suqwr8.webp', 2, 287, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (419, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194047/471558302_uhta9i.webp', 3, 287, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (420, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194117/443615066_dsndob.webp', 1, 288, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (421, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194120/287693142_muc73n.webp', 2, 288, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (422, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194121/348228132_chugei.webp', 3, 288, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (423, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194173/357605939_bp42uj.webp', 1, 289, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (424, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194177/357720078_tjcnpj.webp', 2, 289, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (425, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194179/361907807_lpd8kf.webp', 3, 289, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (426, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194569/442061521_umkas8.webp', 1, 290, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (427, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194572/442061524_urpb9u.webp', 2, 290, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (428, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194573/442061538_igc1wi.webp', 3, 290, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (429, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194628/673379507_ai6chk.webp', 1, 291, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (430, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194631/606413715_r65hd4.webp', 2, 291, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (431, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194693/653866874_vx0mdp.webp', 1, 292, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (432, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194697/653866876_urg4of.webp', 2, 292, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (433, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194698/653866878_tkj0i0.webp', 3, 292, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (435, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194758/603244431_ebkrac.webp', 1, 293, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (437, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194770/603244441_nhvfvp.webp', 3, 293, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (436, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194762/603244449_j7qj6l.webp', 2, 293, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (438, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194872/579834822_xxfqlk.webp', 1, 294, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (439, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194875/579834823_mpfdlx.webp', 2, 294, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (440, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194876/579834824_fpqbxq.webp', 3, 294, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (441, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194938/32054513_cmpur5.webp', 1, 295, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (442, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194952/32054359_eginef.webp', 2, 295, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (443, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791194954/167223477_kqwjmt.webp', 3, 295, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (444, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195330/594186817_wyk63f.webp', 1, 296, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (445, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195331/585911693_d2akvv.webp', 2, 296, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (446, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195333/585911418_pjtkj2.webp', 3, 296, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (447, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195383/485797275_zsec1n.webp', 1, 297, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (448, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195385/485792170_awnncv.webp', 2, 297, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (449, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195407/485797277_jpfsuw.webp', 3, 297, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (450, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195466/580710374_jodk5n.webp', 1, 298, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (451, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195468/679464357_un7i97.webp', 2, 298, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (452, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195582/503249904_s9n2y6.webp', 1, 299, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (453, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195593/422478145_oifrns.webp', 2, 299, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (454, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195595/422478147_df4r7g.webp', 3, 299, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (455, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195651/297467401_iameu1.webp', 1, 300, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (456, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195653/297467408_itcesl.webp', 2, 300, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (457, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195655/297467421_hpxwuw.webp', 3, 300, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (458, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195725/10641437_dldkss.webp', 1, 301, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (459, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195898/381633771_p7f5eq.webp', 1, 302, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (460, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195905/658688378_hevv31.webp', 2, 302, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (461, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195906/658688381_zuxlck.webp', 3, 302, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (462, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195969/547914564_teavvd.webp', 1, 303, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (463, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195973/501924125_evcbeu.webp', 2, 303, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (464, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791195974/501924131_kn1gpq.webp', 3, 303, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (465, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791196029/359194038_pommy8.webp', 1, 304, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (466, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791196032/359194039_oqmqea.webp', 2, 304, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (467, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791196033/359194040_sxpvwr.webp', 3, 304, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (468, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791196090/441783274_vvgkwy.webp', 1, 305, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (469, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791196093/441783275_fmiuza.webp', 2, 305, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (470, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791196094/441783276_kzvobd.webp', 3, 305, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (471, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203433/170581204_m5rvo4.webp', 1, 306, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (472, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203436/320924120_aqw0c6.webp', 2, 306, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (473, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203437/320924121_dgtyh0.webp', 3, 306, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (474, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203511/14435434_kbmysu.webp', 1, 307, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (475, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203514/460513956_xly7ky.webp', 2, 307, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (476, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203515/460513968_chqxkz.webp', 3, 307, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (477, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203573/592188907_wkagpp.webp', 1, 308, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (478, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203576/592188908_rcozqw.webp', 2, 308, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (479, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203580/592188909_bvuas8.webp', 3, 308, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (480, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203644/638229380_arxdeq.webp', 1, 309, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (481, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203648/638229381_i4k1yv.webp', 2, 309, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (482, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203649/638229382_zdybmx.webp', 3, 309, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (483, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203704/359825406_yrpjs1.webp', 1, 310, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (484, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203707/359825407_moqd5j.webp', 2, 310, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (485, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203708/359825408_uv4tjr.webp', 3, 310, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (486, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203770/499467120_npjebj.webp', 1, 311, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (487, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203774/499467129_f4usfy.webp', 2, 311, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (488, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203776/499467124_unjviv.webp', 3, 311, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (489, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203831/662648683_hb2bkr.webp', 1, 312, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (490, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203837/668061234_hpku99.webp', 2, 312, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (491, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203839/293144336_no33iu.webp', 3, 312, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (492, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203911/365505733_d2nmn7.webp', 1, 313, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (493, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203915/365505735_j3l5qm.webp', 2, 313, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (494, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203932/365505734_m6wrri.webp', 3, 313, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (495, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203973/416441423_csk2ou.webp', 1, 314, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (496, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203974/649226310_wd1nxo.webp', 2, 314, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (497, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791203976/416441430_moldfi.webp', 3, 314, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (498, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791204167/594962272_lfknnz.webp', 1, 315, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (499, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791204169/594962278_dnuowv.webp', 2, 315, '');
INSERT INTO public."Images" ("Id", "Url", "Order", "AdvertisementId", "PublicId") VALUES (500, 'https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791204170/594962281_qcxkg3.webp', 3, 315, '');


--
-- Data for Name: ProductTypeTranslations; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."ProductTypeTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (1, 'Ґаджети та смартфони', 0, 'uk');
INSERT INTO public."ProductTypeTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (2, 'Комп''ютери та комплектуючі', 1, 'uk');
INSERT INTO public."ProductTypeTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (3, 'Периферія та аудіо', 2, 'uk');
INSERT INTO public."ProductTypeTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (4, 'ТВ та відео', 3, 'uk');
INSERT INTO public."ProductTypeTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (5, 'Геймінг', 4, 'uk');
INSERT INTO public."ProductTypeTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (6, 'Аксесуари та живлення', 5, 'uk');
INSERT INTO public."ProductTypeTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (7, 'Розумний Дім та освітлення', 6, 'uk');
INSERT INTO public."ProductTypeTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (8, 'Велика побутова техніка', 7, 'uk');
INSERT INTO public."ProductTypeTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (9, 'Мала кухонна техніка', 8, 'uk');
INSERT INTO public."ProductTypeTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (10, 'Прибирання та догляд за домом', 9, 'uk');
INSERT INTO public."ProductTypeTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (11, 'Особиста гігієна та догляд', 10, 'uk');
INSERT INTO public."ProductTypeTranslations" ("Id", "Name", "TranslatableId", "Language") VALUES (12, 'Персональний транспорт', 11, 'uk');


--
-- Data for Name: RegionTranslations; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."RegionTranslations" ("Id", "TranslatableId", "Language", "Name") VALUES (1, 1, 'uk', 'Київ');
INSERT INTO public."RegionTranslations" ("Id", "TranslatableId", "Language", "Name") VALUES (2, 1, 'ru', 'Киев');
INSERT INTO public."RegionTranslations" ("Id", "TranslatableId", "Language", "Name") VALUES (3, 1, 'en', 'Kyiv');
INSERT INTO public."RegionTranslations" ("Id", "TranslatableId", "Language", "Name") VALUES (4, 1, 'de', 'Kyjiw');


--
-- Data for Name: Roles; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."Roles" ("Id", "Name", "Description") VALUES (0, 'User', 'Regular registered user with standard permissions.
Can manage personal profile, create and edit own content, and use basic application features.');
INSERT INTO public."Roles" ("Id", "Name", "Description") VALUES (1, 'Admin', 'Administrator role with full access to system management, including users, roles, and core data.');


--
-- Data for Name: StatusTranslations; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."StatusTranslations" ("Id", "TranslatableId", "Language", "Name", "Description") VALUES (1, 0, 'uk', 'Активний', 'Це оголошення зараз активне!');
INSERT INTO public."StatusTranslations" ("Id", "TranslatableId", "Language", "Name", "Description") VALUES (2, 0, 'ru', 'Активный', 'Это объявление сейчас активно!');
INSERT INTO public."StatusTranslations" ("Id", "TranslatableId", "Language", "Name", "Description") VALUES (3, 0, 'en', 'Active', 'This advertisement is currently active!');
INSERT INTO public."StatusTranslations" ("Id", "TranslatableId", "Language", "Name", "Description") VALUES (4, 0, 'de', 'Aktiv', 'Diese Anzeige ist derzeit aktiv!');


--
-- Data for Name: UserRoles; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."UserRoles" ("Id", "UserId", "RoleId") VALUES (0, 3, 1);
INSERT INTO public."UserRoles" ("Id", "UserId", "RoleId") VALUES (0, 7, 1);
INSERT INTO public."UserRoles" ("Id", "UserId", "RoleId") VALUES (0, 9, 1);
INSERT INTO public."UserRoles" ("Id", "UserId", "RoleId") VALUES (0, 16, 1);
INSERT INTO public."UserRoles" ("Id", "UserId", "RoleId") VALUES (0, 18, 1);
INSERT INTO public."UserRoles" ("Id", "UserId", "RoleId") VALUES (0, 21, 1);
INSERT INTO public."UserRoles" ("Id", "UserId", "RoleId") VALUES (0, 22, 1);
INSERT INTO public."UserRoles" ("Id", "UserId", "RoleId") VALUES (0, 28, 0);
INSERT INTO public."UserRoles" ("Id", "UserId", "RoleId") VALUES (0, 49, 0);
INSERT INTO public."UserRoles" ("Id", "UserId", "RoleId") VALUES (0, 51, 0);
INSERT INTO public."UserRoles" ("Id", "UserId", "RoleId") VALUES (0, 55, 0);
INSERT INTO public."UserRoles" ("Id", "UserId", "RoleId") VALUES (0, 56, 0);
INSERT INTO public."UserRoles" ("Id", "UserId", "RoleId") VALUES (0, 57, 0);
INSERT INTO public."UserRoles" ("Id", "UserId", "RoleId") VALUES (0, 58, 0);
INSERT INTO public."UserRoles" ("Id", "UserId", "RoleId") VALUES (0, 61, 0);
INSERT INTO public."UserRoles" ("Id", "UserId", "RoleId") VALUES (0, 62, 0);
INSERT INTO public."UserRoles" ("Id", "UserId", "RoleId") VALUES (0, 63, 0);
INSERT INTO public."UserRoles" ("Id", "UserId", "RoleId") VALUES (0, 64, 0);
INSERT INTO public."UserRoles" ("Id", "UserId", "RoleId") VALUES (0, 65, 0);
INSERT INTO public."UserRoles" ("Id", "UserId", "RoleId") VALUES (0, 66, 0);
INSERT INTO public."UserRoles" ("Id", "UserId", "RoleId") VALUES (0, 68, 0);
INSERT INTO public."UserRoles" ("Id", "UserId", "RoleId") VALUES (0, 71, 0);
INSERT INTO public."UserRoles" ("Id", "UserId", "RoleId") VALUES (0, 73, 0);


--
-- Data for Name: VerificationCodes; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260607194859_InitialCreate', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260608182251_AddUserRoleModule', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260608190539_ConfigureUserRoleCompositeKey', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260609185810_AddCategoryAttributeSchema', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260609193727_AddConditionCurrencyImageSchema', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260609202103_AddAdvertisementSchema', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260609205204_FixConfiguration', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260617203936_ChoreUserConfiguration', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260623190720_ChangePropertiesToJsonbDictionary', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260624202649_AddCodeAndSymbolToCurrency', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260701005342_AddConditionToAdvertisement', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260701010257_AddComparerToAdvertisement', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260707163117_AddFavorite', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260708204241_AddFavorite', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260709000023_AddPublickIdToImage', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260801140333_AddProductType', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260801142636_AddProductTypeToCategory', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260808094746_AddStatusTable', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260808101026_AddStatus', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260808101315_AddStatusToAdvertisement', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260813135131_AddStatusTranslation', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260820115304_AddAvatar', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260903170850_AddRegionTranslation', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260903172248_UpdateRegion', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260905172933_AddCityTranslation', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260906124953_AddConditionTranslation', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260907115349_AddAttributeDefinitionTranslations', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260909100240_AddProductTypeTranslation', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260909163917_AddCategoryTranslation', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260910100554_AddStockToAdvertisement', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260910160323_AddBasketAttribute', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260910163246_ChangeBasketAttribute', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260910170026_ChangeForeignKeyBasketAttribute', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260910203535_ChangeTypeAdvertisementProperyStock', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260911091640_AddBasketansBasketItems', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260912124746_UpdateConfigurationBasketItems', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260913144907_AddUserNameAndBioIntoUser', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260913151542_AddVerificationCode', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260913204832_AddConfirmedsIntoUser', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260914081254_AddInvalidatedAtIntoVerifyCode', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260914091909_AddStatusIntoUserAndPurposeIntoVerifyCode', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260914092442_ChangeTypeStatus', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260915110647_AddPendingValueIntoVerifyCode', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260926155828_AddAttributeVariant', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260926161631_AddAttributeDictionaryAndAttributeDictionaryValue', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260926163724_AddAttributesToDbSet', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260926163906_RanameAttributeDictionary', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260927192428_AddAdvertisementVariantIntoDbSet', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260928114243_AddAttributeDictionaryValueTranslation', '10.0.8');
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20260928183925_ChangeAdvertisementInBasketItem', '10.0.8');


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

\unrestrict GrfjX1eBo0oDf6JrEKLxBmJKQH3VDb9LgkDhXTj9zGsuSfGQVkdD4gpKPBK8nrd

