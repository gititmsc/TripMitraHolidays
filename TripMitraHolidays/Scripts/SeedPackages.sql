-- ============================================================
-- TripMitra Holidays — 50 Test Package Records
-- Run this against: TripMitraHolidays database
-- ============================================================

SET NOCOUNT ON;

-- ──────────────────────────────────────────────────────────────
-- 1. PACKAGES (50 records)
-- ──────────────────────────────────────────────────────────────
INSERT INTO [dbo].[Packages]
    (PackageName, Slug, ShortDescription, Description, PackagePrice, DiscountPrice,
     DurationDays, DurationNights, Destination, Country, StartingCity,
     PackageType, TourCategory, HotelRating, MealType, Transportation,
     IsFlightIncluded, IsVisaIncluded, IsFeatured, IsPopular, IsActive,
     DisplayOrder, MetaTitle, MetaKeywords, MetaDescription, CreatedDate)
VALUES

-- 1
('Goa Beach Paradise', 'goa-beach-paradise',
 'Sun, sand and sea — 5 days of pure Goa bliss with beach shacks, water sports and nightlife.',
 '<p>Discover the golden beaches, vibrant shacks, and pulsating nightlife of Goa. This package covers North &amp; South Goa with guided tours.</p>',
 15000, 12999, 5, 4, 'Goa', 'India', 'Mumbai',
 'Domestic', 'Beach', '3 Star', 'Breakfast Only', 'Flight',
 1, 0, 1, 1, 1, 1,
 'Goa Beach Holiday Package 5 Days', 'goa tour, goa beach package, goa holiday',
 'Best 5-day Goa beach package from Mumbai with hotel and breakfast.', GETUTCDATE()),

-- 2
('Kerala Backwaters Escape', 'kerala-backwaters-escape',
 'Cruise through emerald backwaters, misty hill stations and pristine beaches of Gods Own Country.',
 '<p>Kerala offers an unmatched blend of backwaters, Ayurveda, hill stations and beaches. Includes houseboat stay in Alleppey.</p>',
 22000, 18999, 6, 5, 'Kerala', 'India', 'Bengaluru',
 'Domestic', 'Cultural', '4 Star', 'Half Board', 'Flight',
 1, 0, 1, 1, 1, 2,
 'Kerala Tour Package 6 Days | Backwaters & Hills', 'kerala tour, backwaters, houseboat, kerala holiday',
 'Explore Kerala backwaters and hill stations in 6 days with houseboat experience.', GETUTCDATE()),

-- 3
('Rajasthan Royal Heritage', 'rajasthan-royal-heritage',
 'An epic journey through the palaces, forts and deserts of the Land of Kings.',
 '<p>Explore Jaipur, Jodhpur, Udaipur and Jaisalmer — the jewels of Rajasthan. Includes camel safari and desert camping.</p>',
 28000, 24500, 8, 7, 'Rajasthan', 'India', 'Delhi',
 'Domestic', 'Historical', '4 Star', 'Breakfast Only', 'Private Car',
 0, 0, 1, 1, 1, 3,
 'Rajasthan Heritage Tour 8 Days | Forts & Palaces', 'rajasthan tour, jaipur, jodhpur, udaipur, desert safari',
 'Complete 8-day Rajasthan tour covering Jaipur, Jodhpur, Udaipur and Jaisalmer.', GETUTCDATE()),

-- 4
('Manali Snow Adventure', 'manali-snow-adventure',
 'Experience the magic of snow-capped peaks, river rafting and Rohtang Pass in Manali.',
 '<p>Manali is the perfect destination for adventure seekers. This package includes Solang Valley, Rohtang Pass and river rafting.</p>',
 14000, 11999, 5, 4, 'Manali', 'India', 'Delhi',
 'Domestic', 'Mountains', '3 Star', 'Breakfast Only', 'Bus',
 0, 0, 0, 1, 1, 4,
 'Manali Adventure Tour Package 5 Days', 'manali tour, rohtang pass, manali snow, adventure manali',
 '5-day Manali adventure package with Rohtang Pass and river rafting.', GETUTCDATE()),

-- 5
('Andaman Island Explorer', 'andaman-island-explorer',
 'Discover pristine coral reefs, turquoise waters and secluded beaches of the Andaman Islands.',
 '<p>The Andaman &amp; Nicobar Islands offer some of the most stunning beaches and marine life in Asia. Includes Radhanagar Beach, Havelock and Neil Island.</p>',
 35000, 30000, 6, 5, 'Port Blair', 'India', 'Chennai',
 'Domestic', 'Beach', '4 Star', 'Half Board', 'Flight',
 1, 0, 1, 1, 1, 5,
 'Andaman Tour Package 6 Days | Beach & Snorkelling', 'andaman tour, andaman package, havelock island, port blair',
 'Explore the beautiful Andaman Islands with beach hopping, snorkelling and scuba diving.', GETUTCDATE()),

-- 6
('Darjeeling & Sikkim Charm', 'darjeeling-sikkim-charm',
 'Tea gardens, Buddhist monasteries and views of Kanchenjunga in this scenic Northeast package.',
 '<p>A perfect combination of the colonial charm of Darjeeling and the serene beauty of Sikkim. Includes Tiger Hill sunrise, Rumtek Monastery and Tsomgo Lake.</p>',
 20000, NULL, 7, 6, 'Darjeeling', 'India', 'Kolkata',
 'Domestic', 'Mountains', '3 Star', 'Full Board', 'Private Car',
 0, 0, 0, 1, 1, 6,
 'Darjeeling Sikkim Tour 7 Days', 'darjeeling tour, sikkim tour, northeast india, kanchenjunga',
 '7-day Darjeeling and Sikkim tour with tea garden visit and monastery tours.', GETUTCDATE()),

-- 7
('Ladakh Himalayan Odyssey', 'ladakh-himalayan-odyssey',
 'A legendary road trip through the world-highest passes, monasteries and azure lakes of Ladakh.',
 '<p>Ladakh is the crown jewel of India for adventure travellers. This package covers Leh, Pangong Lake, Nubra Valley and the Khardung La pass.</p>',
 38000, 34000, 9, 8, 'Leh', 'India', 'Delhi',
 'Domestic', 'Adventure', '3 Star', 'Full Board', 'Flight',
 1, 0, 1, 1, 1, 7,
 'Ladakh Tour Package 9 Days | Pangong Lake & Nubra', 'ladakh tour, leh ladakh, pangong lake, nubra valley',
 'Experience the magical Ladakh with this 9-day package covering Pangong, Nubra and Leh monasteries.', GETUTCDATE()),

-- 8
('Shimla Manali Honeymoon', 'shimla-manali-honeymoon',
 'A dreamy honeymoon getaway through the scenic hill stations of Himachal Pradesh.',
 '<p>Perfect for newlyweds, this package covers the colonial town of Shimla and the adventure paradise of Manali with snowfields and apple orchards.</p>',
 18000, 15999, 6, 5, 'Shimla', 'India', 'Delhi',
 'Honeymoon', 'Mountains', '4 Star', 'Breakfast Only', 'Private Car',
 0, 0, 0, 1, 1, 8,
 'Shimla Manali Honeymoon Package 6 Days', 'shimla manali honeymoon, hills honeymoon package, couple tour himachal',
 'Romantic 6-day Shimla-Manali honeymoon package with luxury stay and sightseeing.', GETUTCDATE()),

-- 9
('Coorg Coffee Country', 'coorg-coffee-country',
 'Wander through misty coffee estates, waterfalls and spice plantations in the Scotland of India.',
 '<p>Coorg (Kodagu) is Karnatakas most scenic hill station. This package includes plantation walks, Abbey Falls, Raja''s Seat and local Kodava culture experience.</p>',
 12000, NULL, 4, 3, 'Coorg', 'India', 'Bengaluru',
 'Domestic', 'Cultural', '3 Star', 'Breakfast Only', 'Private Car',
 0, 0, 0, 0, 1, 9,
 'Coorg Tour Package 4 Days | Coffee Estate Stay', 'coorg tour, coorg package, kodagu, coffee estate coorg',
 '4-day Coorg tour with coffee plantation stay, waterfall visits and spice garden tours.', GETUTCDATE()),

-- 10
('Varanasi Spiritual Journey', 'varanasi-spiritual-journey',
 'Experience the eternal city of Varanasi with Ganga Aarti, morning boat rides and sacred ghats.',
 '<p>Varanasi, one of the world''s oldest living cities, offers a profound spiritual experience. Includes sunrise boat ride on the Ganges, Dashashwamedh Ghat Aarti and Sarnath excursion.</p>',
 9500, 7999, 4, 3, 'Varanasi', 'India', 'Delhi',
 'Pilgrimage', 'Religious', '3 Star', 'Breakfast Only', 'Flight',
 1, 0, 0, 0, 1, 10,
 'Varanasi Pilgrimage Tour 4 Days | Ganga Aarti', 'varanasi tour, kashi tour, ganga aarti, spiritual tour india',
 '4-day Varanasi spiritual tour with Ganga Aarti, boat ride and Sarnath visit.', GETUTCDATE()),

-- 11
('Thailand Beach & Culture', 'thailand-beach-culture',
 'Bangkok temples, Pattaya beaches and Phuket sunsets — the best of Thailand in one package.',
 '<p>Thailand is Southeast Asia''s most loved destination. This package covers Bangkok, Pattaya and Phuket with island hopping and street food tours.</p>',
 45000, 39000, 7, 6, 'Bangkok', 'Thailand', 'Mumbai',
 'International', 'Beach', '4 Star', 'Breakfast Only', 'Flight',
 1, 1, 1, 1, 1, 11,
 'Thailand Tour Package 7 Days | Bangkok Pattaya Phuket', 'thailand tour, bangkok tour, phuket package, thailand holiday',
 'Best 7-day Thailand tour covering Bangkok, Pattaya and Phuket with visa assistance.', GETUTCDATE()),

-- 12
('Bali Island Paradise', 'bali-island-paradise',
 'Rice terraces, Hindu temples, volcanic peaks and beach clubs — Bali is pure magic.',
 '<p>Bali is the Island of Gods. This package covers Ubud culture, Tanah Lot temple, Uluwatu sunset and Seminyak beach clubs.</p>',
 55000, 48000, 7, 6, 'Bali', 'Indonesia', 'Delhi',
 'International', 'Beach', '4 Star', 'Breakfast Only', 'Flight',
 1, 1, 1, 1, 1, 12,
 'Bali Tour Package 7 Days | Ubud & Seminyak', 'bali tour, bali package, indonesia holiday, ubud bali',
 'Experience magical Bali with this 7-day package covering temples, rice terraces and beaches.', GETUTCDATE()),

-- 13
('Dubai Gold & Glamour', 'dubai-gold-glamour',
 'Burj Khalifa, desert safaris, luxury malls and stunning skylines — Dubai delivers everything.',
 '<p>Dubai is where the future meets luxury. This package includes Burj Khalifa visit, Dubai Museum, desert safari with BBQ dinner and a dhow cruise.</p>',
 60000, 52000, 5, 4, 'Dubai', 'UAE', 'Mumbai',
 'International', 'Luxury', '5 Star', 'Breakfast Only', 'Flight',
 1, 1, 1, 1, 1, 13,
 'Dubai Tour Package 5 Days | Burj Khalifa & Desert Safari', 'dubai tour, dubai package, burj khalifa, desert safari dubai',
 '5-day Dubai luxury tour with Burj Khalifa, desert safari and dhow cruise experience.', GETUTCDATE()),

-- 14
('Singapore City Experience', 'singapore-city-experience',
 'Marina Bay Sands, Sentosa, Gardens by the Bay — Singapore is a compact world of wonders.',
 '<p>Singapore is Asia''s most cosmopolitan city. This package includes Universal Studios, Gardens by the Bay, Sentosa Island and Night Safari.</p>',
 65000, NULL, 6, 5, 'Singapore', 'Singapore', 'Chennai',
 'International', 'Cultural', '4 Star', 'Breakfast Only', 'Flight',
 1, 1, 1, 1, 1, 14,
 'Singapore Tour Package 6 Days | Sentosa & Universal Studios', 'singapore tour, singapore package, universal studios, sentosa island',
 '6-day Singapore family tour with Universal Studios, Sentosa and Night Safari included.', GETUTCDATE()),

-- 15
('Maldives Luxury Escape', 'maldives-luxury-escape',
 'Overwater bungalows, crystal-clear lagoons and vibrant coral reefs in the Maldives.',
 '<p>The Maldives is the ultimate luxury escape. This package includes overwater villa stay, snorkelling with manta rays and a romantic sunset cruise.</p>',
 120000, 99000, 5, 4, 'Male', 'Maldives', 'Delhi',
 'International', 'Beach', '5 Star', 'All Inclusive', 'Flight',
 1, 1, 1, 1, 1, 15,
 'Maldives Honeymoon Package 5 Days | Overwater Villa', 'maldives tour, maldives package, overwater villa, maldives honeymoon',
 'Luxury 5-day Maldives honeymoon package with overwater bungalow and all-inclusive meals.', GETUTCDATE()),

-- 16
('Sri Lanka Heritage Tour', 'sri-lanka-heritage-tour',
 'Ancient ruins, elephant sanctuaries, colonial hill towns and whale watching in Sri Lanka.',
 '<p>Sri Lanka packs a remarkable variety into a small island — from Sigiriya Rock to tea country and Mirissa whale watching.</p>',
 42000, 36000, 7, 6, 'Colombo', 'Sri Lanka', 'Chennai',
 'International', 'Historical', '4 Star', 'Breakfast Only', 'Flight',
 1, 1, 0, 1, 1, 16,
 'Sri Lanka Tour Package 7 Days | Sigiriya & Tea Trails', 'sri lanka tour, colombo package, sigiriya, kandy, whale watching sri lanka',
 '7-day Sri Lanka cultural tour covering Colombo, Kandy, Sigiriya and Mirissa beach.', GETUTCDATE()),

-- 17
('Nepal Himalaya Trekking', 'nepal-himalaya-trekking',
 'Kathmandu temples, Pokhara lakeside and the majestic Annapurna range in Nepal.',
 '<p>Nepal is the trekkers paradise. This package covers Kathmandu Durbar Square, Boudhanath Stupa, Pokhara and the Sarangkot sunrise over Annapurna.</p>',
 30000, 26000, 6, 5, 'Kathmandu', 'Nepal', 'Delhi',
 'International', 'Mountains', '3 Star', 'Full Board', 'Flight',
 1, 0, 0, 1, 1, 17,
 'Nepal Tour Package 6 Days | Kathmandu & Pokhara', 'nepal tour, kathmandu package, pokhara trek, annapurna nepal',
 '6-day Nepal tour covering Kathmandu and Pokhara with Annapurna base view.', GETUTCDATE()),

-- 18
('Bhutan Happiness Kingdom', 'bhutan-happiness-kingdom',
 'Tiger''s Nest Monastery, pristine forests and the world''s happiest culture await in Bhutan.',
 '<p>Bhutan is the last Himalayan kingdom and the world''s only carbon-negative country. Highlights include Paro Taktsang (Tiger''s Nest), Punakha Dzong and Thimphu.</p>',
 55000, NULL, 6, 5, 'Paro', 'Bhutan', 'Delhi',
 'International', 'Cultural', '4 Star', 'Full Board', 'Flight',
 1, 1, 1, 0, 1, 18,
 'Bhutan Tour Package 6 Days | Tiger''s Nest Monastery', 'bhutan tour, bhutan package, tigers nest, paro, thimphu',
 '6-day Bhutan cultural tour with Tiger''s Nest trek, Punakha Dzong and local culture experience.', GETUTCDATE()),

-- 19
('Rishikesh Yoga Adventure', 'rishikesh-yoga-adventure',
 'River rafting, bungee jumping, yoga retreats and the Ganga Aarti in the Yoga Capital of the World.',
 '<p>Rishikesh combines spiritual wellness with adventure. This package includes white-water rafting on the Ganges, yoga sessions, beach camping and the evening Ganga Aarti.</p>',
 8500, 7000, 3, 2, 'Rishikesh', 'India', 'Delhi',
 'Adventure', 'Religious', '3 Star', 'Full Board', 'Bus',
 0, 0, 0, 1, 1, 19,
 'Rishikesh Adventure & Yoga Package 3 Days', 'rishikesh tour, river rafting rishikesh, yoga retreat, ganga aarti',
 '3-day Rishikesh adventure package with river rafting, yoga sessions and Ganga Aarti.', GETUTCDATE()),

-- 20
('Ooty Nilgiris Queen', 'ooty-nilgiris-queen',
 'Botanical gardens, Nilgiri toy train, tea estates and misty valleys in the Queen of Hill Stations.',
 '<p>Ooty (Udhagamandalam) is the most famous hill station in South India. This package includes a ride on the iconic Nilgiri Mountain Railway and a visit to the famous rose garden.</p>',
 11000, 9500, 4, 3, 'Ooty', 'India', 'Coimbatore',
 'Domestic', 'Mountains', '3 Star', 'Breakfast Only', 'Private Car',
 0, 0, 0, 0, 1, 20,
 'Ooty Tour Package 4 Days | Nilgiri Mountain Railway', 'ooty tour, ooty package, nilgiri toy train, hill station ooty',
 '4-day Ooty tour with Nilgiri Mountain Railway ride, botanical garden and tea estate visit.', GETUTCDATE()),

-- 21
('Munnar Tea Valley', 'munnar-tea-valley',
 'Rolling tea gardens, misty peaks and rare Neelakurinji flowers in Munnar''s high ranges.',
 '<p>Munnar is Kerala''s most visited hill station, famous for its vast tea plantations and cool climate. Package includes Eravikulam National Park and Mattupetty Dam.</p>',
 13500, NULL, 4, 3, 'Munnar', 'India', 'Kochi',
 'Domestic', 'Mountains', '3 Star', 'Breakfast Only', 'Private Car',
 0, 0, 0, 1, 1, 21,
 'Munnar Tour Package 4 Days | Tea Gardens Kerala', 'munnar tour, munnar package, tea garden munnar, kerala hills',
 '4-day Munnar hill station tour with tea garden visits and Eravikulam National Park.', GETUTCDATE()),

-- 22
('Agra Taj Mahal Day Trip', 'agra-taj-mahal-day-trip',
 'Marvel at the world''s greatest monument of love — the Taj Mahal — plus Agra Fort and Fatehpur Sikri.',
 '<p>A comfortable 2-day trip to Agra covering the iconic Taj Mahal at sunrise, the majestic Agra Fort and the ghost city of Fatehpur Sikri.</p>',
 7500, 6200, 2, 1, 'Agra', 'India', 'Delhi',
 'Domestic', 'Historical', '3 Star', 'Breakfast Only', 'Private Car',
 0, 0, 0, 0, 1, 22,
 'Agra Tour 2 Days | Taj Mahal Sunrise Visit', 'agra tour, taj mahal tour, agra fort, taj mahal sunrise',
 '2-day Agra tour with Taj Mahal sunrise visit, Agra Fort and Fatehpur Sikri.', GETUTCDATE()),

-- 23
('Spiti Valley Expedition', 'spiti-valley-expedition',
 'Remote monasteries, barren moonscapes and high-altitude villages in the Middle Land — Spiti.',
 '<p>Spiti Valley is one of India''s most remote and spectacular destinations. This expedition covers Key Monastery, Kaza, Dhankar and the world''s highest petrol station at Kunzum Pass.</p>',
 32000, 28000, 8, 7, 'Spiti', 'India', 'Chandigarh',
 'Adventure', 'Mountains', '3 Star', 'Full Board', 'Private Car',
 0, 0, 0, 0, 1, 23,
 'Spiti Valley Tour Package 8 Days | Monasteries & Mountains', 'spiti valley tour, kaza, key monastery, kunzum pass, himachal adventure',
 '8-day Spiti Valley expedition covering Key Monastery, Kaza and high-altitude villages.', GETUTCDATE()),

-- 24
('Tirupati Balaji Darshan', 'tirupati-balaji-darshan',
 'Seek blessings at one of the world''s most visited religious sites — Sri Venkateswara Temple.',
 '<p>Tirupati Balaji is the richest temple in the world and one of the most sacred pilgrimage centres for Hindus. This package includes seeghra darshan and Chandragiri Fort visit.</p>',
 9000, 7500, 3, 2, 'Tirupati', 'India', 'Chennai',
 'Pilgrimage', 'Religious', '3 Star', 'Full Board', 'Flight',
 1, 0, 0, 0, 1, 24,
 'Tirupati Darshan Package 3 Days | Balaji Temple', 'tirupati tour, balaji darshan, tirupati package, venkateswara temple',
 '3-day Tirupati pilgrimage package with Balaji darshan, Chandragiri Fort and hotel stay.', GETUTCDATE()),

-- 25
('Vietnam Heritage & Beach', 'vietnam-heritage-beach',
 'Ha Long Bay cruises, Hoi An lanterns, Halong Bay and the pristine beaches of Da Nang.',
 '<p>Vietnam offers extraordinary diversity — from the imperial city of Hue to the World Heritage site of Ha Long Bay and the beachfront of Da Nang.</p>',
 48000, 42000, 8, 7, 'Hanoi', 'Vietnam', 'Mumbai',
 'International', 'Cultural', '4 Star', 'Breakfast Only', 'Flight',
 1, 1, 0, 1, 1, 25,
 'Vietnam Tour Package 8 Days | Ha Long Bay & Hoi An', 'vietnam tour, ha long bay, hoi an, da nang, vietnam package',
 '8-day Vietnam tour covering Hanoi, Ha Long Bay, Hoi An and Da Nang beach.', GETUTCDATE()),

-- 26
('Ayodhya Spiritual Tour', 'ayodhya-spiritual-tour',
 'Visit the sacred birthplace of Lord Ram — the divine city of Ayodhya after its grand renovation.',
 '<p>Ayodhya is one of the Sapta Puri — seven sacred cities in Hinduism. This package covers the Ram Mandir, Kanak Bhawan, Hanuman Garhi and a Saryu River evening aarti.</p>',
 8000, NULL, 3, 2, 'Ayodhya', 'India', 'Lucknow',
 'Pilgrimage', 'Religious', '3 Star', 'Breakfast Only', 'Flight',
 1, 0, 0, 0, 1, 26,
 'Ayodhya Tour Package 3 Days | Ram Mandir Darshan', 'ayodhya tour, ram mandir, ayodhya package, ram janmabhoomi',
 '3-day Ayodhya pilgrimage with Ram Mandir darshan, Hanuman Garhi and Saryu aarti.', GETUTCDATE()),

-- 27
('Kaziranga Wildlife Safari', 'kaziranga-wildlife-safari',
 'Spot one-horned rhinos, elephants and tigers in the UNESCO-listed Kaziranga National Park.',
 '<p>Kaziranga is home to the world''s largest population of one-horned rhinos. This package includes elephant safari at dawn, jeep safari and a visit to Majuli island.</p>',
 24000, 20000, 5, 4, 'Kaziranga', 'India', 'Guwahati',
 'Domestic', 'Wildlife', '3 Star', 'Full Board', 'Flight',
 1, 0, 0, 0, 1, 27,
 'Kaziranga Safari Package 5 Days | Rhino & Tiger', 'kaziranga tour, one horned rhino, assam wildlife, kaziranga safari',
 '5-day Kaziranga National Park safari with elephant and jeep safari, one-horned rhino sighting.', GETUTCDATE()),

-- 28
('Pondicherry French Riviera', 'pondicherry-french-riviera',
 'French boulevards, Auroville ashrams, colonial villas and unspoilt beaches in Pondicherry.',
 '<p>Pondicherry (Puducherry) is a unique blend of French colonial heritage and Tamil culture. Package covers the French Quarter, Auroville, Paradise Beach and local cuisine.</p>',
 10500, 8999, 4, 3, 'Pondicherry', 'India', 'Chennai',
 'Domestic', 'Cultural', '3 Star', 'Breakfast Only', 'Bus',
 0, 0, 0, 0, 1, 28,
 'Pondicherry Tour Package 4 Days | French Quarter & Auroville', 'pondicherry tour, auroville, french riviera india, pondy package',
 '4-day Pondicherry tour with Auroville visit, French Quarter walk and Paradise Beach.', GETUTCDATE()),

-- 29
('Jim Corbett Tiger Safari', 'jim-corbett-tiger-safari',
 'India''s oldest national park and best chance of spotting the Royal Bengal Tiger in the wild.',
 '<p>Jim Corbett National Park is India''s oldest wildlife reserve and a Project Tiger reserve. This package includes two jeep safaris, elephant ride and bird watching.</p>',
 16000, 13500, 3, 2, 'Jim Corbett', 'India', 'Delhi',
 'Domestic', 'Wildlife', '4 Star', 'Full Board', 'Private Car',
 0, 0, 0, 1, 1, 29,
 'Jim Corbett Safari Package 3 Days | Tiger Sighting', 'jim corbett tour, tiger safari, corbett national park, wildlife india',
 '3-day Jim Corbett safari package with two game drives and chances of tiger sighting.', GETUTCDATE()),

-- 30
('Amritsar Golden Temple', 'amritsar-golden-temple',
 'The spiritual home of the Sikh faith — the magnificent Golden Temple and Wagah Border ceremony.',
 '<p>Amritsar is Punjab''s cultural and spiritual capital. This package includes the Golden Temple langar experience, Wagah Border Beating Retreat ceremony and Jallianwala Bagh.',
 8500, NULL, 3, 2, 'Amritsar', 'India', 'Delhi',
 'Pilgrimage', 'Religious', '3 Star', 'Full Board', 'Flight',
 1, 0, 0, 1, 1, 30,
 'Amritsar Tour Package 3 Days | Golden Temple & Wagah', 'amritsar tour, golden temple, wagah border, punjab tour',
 '3-day Amritsar tour with Golden Temple darshan, Langar experience and Wagah Border ceremony.', GETUTCDATE()),

-- 31
('Jaisalmer Desert Safari', 'jaisalmer-desert-safari',
 'Golden sand dunes, camel safaris, stargazing and traditional folk music in the Desert City.',
 '<p>Jaisalmer, the Golden City, rises like a mirage from the Thar Desert. This package includes Jaisalmer Fort, Patwon Ki Haveli, Sam Sand Dunes overnight camp and camel safari.</p>',
 15000, 12500, 4, 3, 'Jaisalmer', 'India', 'Jaipur',
 'Domestic', 'Historical', '3 Star', 'Full Board', 'Private Car',
 0, 0, 0, 0, 1, 31,
 'Jaisalmer Tour Package 4 Days | Desert Camp & Camel Safari', 'jaisalmer tour, desert safari, camel safari, sam sand dunes, thar desert',
 '4-day Jaisalmer package with camel safari, desert camping and Jaisalmer Fort.', GETUTCDATE()),

-- 32
('Kolkata Heritage Walk', 'kolkata-heritage-walk',
 'Colonial architecture, Durga Puja pandals, street food and the cultural soul of East India.',
 '<p>Kolkata is India''s cultural capital. This package covers Victoria Memorial, Howrah Bridge, Dakshineswar Temple, the famous College Street and Kumartuli artisan village.</p>',
 9000, 7500, 3, 2, 'Kolkata', 'India', 'Delhi',
 'Domestic', 'Cultural', '3 Star', 'Breakfast Only', 'Flight',
 1, 0, 0, 0, 1, 32,
 'Kolkata Heritage Tour 3 Days | Victoria Memorial & Howrah', 'kolkata tour, victoria memorial, howrah bridge, west bengal tour',
 '3-day Kolkata heritage tour covering Victoria Memorial, Howrah Bridge and street food trails.', GETUTCDATE()),

-- 33
('Ranthambore Tiger Reserve', 'ranthambore-tiger-reserve',
 'Ancient ruins meet wildlife — spot tigers around the dramatic 10th-century Ranthambore Fort.',
 '<p>Ranthambore is one of the best places in India to see tigers in the wild. Safaris go through the dramatic landscape around the 10th-century Ranthambore Fort.</p>',
 18000, 15000, 4, 3, 'Ranthambore', 'India', 'Jaipur',
 'Domestic', 'Wildlife', '4 Star', 'Half Board', 'Private Car',
 0, 0, 0, 1, 1, 33,
 'Ranthambore Tiger Safari 4 Days | Wildlife & Fort', 'ranthambore tour, tiger safari ranthambore, rajasthan wildlife, corbett vs ranthambore',
 '4-day Ranthambore National Park tiger safari with Ranthambore Fort excursion.', GETUTCDATE()),

-- 34
('Lakshadweep Coral Island', 'lakshadweep-coral-island',
 'India''s tropical paradise — pristine coral lagoons, glass-bottomed boat rides and water sports.',
 '<p>Lakshadweep is India''s smallest Union Territory and least visited paradise. This exclusive package includes Agatti Island, kayaking and snorkelling over living coral reefs.</p>',
 65000, 55000, 5, 4, 'Agatti', 'India', 'Kochi',
 'Domestic', 'Beach', '4 Star', 'All Inclusive', 'Flight',
 1, 0, 1, 0, 1, 34,
 'Lakshadweep Tour Package 5 Days | Coral Reefs & Lagoons', 'lakshadweep tour, agatti island, lakshadweep package, coral reef india',
 '5-day Lakshadweep exclusive tour with coral reef snorkelling and kayaking on lagoons.', GETUTCDATE()),

-- 35
('Mysore Royal Heritage', 'mysore-royal-heritage',
 'The Palace of Mysore, Chamundi Hills, sandalwood markets and the Brindavan Gardens.',
 '<p>Mysore (Mysuru) is Karnataka''s most regal city. This package includes the illuminated Mysore Palace, Chamundi Hills, Somnathpur Temple and Srirangapatna island fortress.</p>',
 10000, 8499, 3, 2, 'Mysore', 'India', 'Bengaluru',
 'Domestic', 'Historical', '3 Star', 'Breakfast Only', 'Bus',
 0, 0, 0, 0, 1, 35,
 'Mysore Tour Package 3 Days | Mysore Palace & Chamundi', 'mysore tour, mysore palace, chamundi hills, karnataka tour, dasara mysore',
 '3-day Mysore heritage tour covering the illuminated Mysore Palace, Chamundi Hills and Brindavan Gardens.', GETUTCDATE()),

-- 36
('Uttarakhand Char Dham', 'uttarakhand-char-dham',
 'The supreme Hindu pilgrimage — Yamunotri, Gangotri, Kedarnath and Badrinath in the Himalayas.',
 '<p>Char Dham Yatra is the most sacred pilgrimage for Hindus. This 14-day journey covers all four Himalayan dhams with comfortable stays and dedicated puja arrangements.</p>',
 35000, 30000, 14, 13, 'Haridwar', 'India', 'Delhi',
 'Pilgrimage', 'Religious', '3 Star', 'Full Board', 'Private Car',
 0, 0, 0, 1, 1, 36,
 'Char Dham Yatra Package 14 Days | Complete Pilgrimage', 'char dham yatra, kedarnath, badrinath, gangotri, yamunotri',
 'Complete 14-day Char Dham Yatra package covering all four Himalayan pilgrimage sites.', GETUTCDATE()),

-- 37
('Meghalaya Living Roots', 'meghalaya-living-roots',
 'Living root bridges, cleanest village in Asia, waterfalls and the Scotland of the East.',
 '<p>Meghalaya (Abode of Clouds) is one of India''s most scenic Northeast states. This package covers Shillong, Cherrapunji living root bridges, Dawki river and Mawlynnong village.</p>',
 22000, 18000, 5, 4, 'Shillong', 'India', 'Guwahati',
 'Domestic', 'Cultural', '3 Star', 'Full Board', 'Flight',
 1, 0, 0, 0, 1, 37,
 'Meghalaya Tour 5 Days | Living Root Bridges & Dawki', 'meghalaya tour, shillong, cherrapunji, dawki, living root bridge, northeast india',
 '5-day Meghalaya tour with living root bridges, Dawki river and Mawlynnong Asia''s cleanest village.', GETUTCDATE()),

-- 38
('Puri Jagannath Rath Yatra', 'puri-jagannath-rath-yatra',
 'The sacred Jagannath Temple, golden beach of Puri and the Sun Temple of Konark in Odisha.',
 '<p>Puri is one of the four sacred dhams of Hinduism. This package includes Jagannath Temple darshan, Konark Sun Temple and Puri Beach.</p>',
 11000, NULL, 4, 3, 'Puri', 'India', 'Bhubaneswar',
 'Pilgrimage', 'Religious', '3 Star', 'Breakfast Only', 'Flight',
 1, 0, 0, 0, 1, 38,
 'Puri Tour Package 4 Days | Jagannath Temple & Konark', 'puri tour, jagannath temple, konark sun temple, odisha pilgrimage',
 '4-day Puri pilgrimage with Jagannath Temple darshan, Konark Sun Temple and Chilika Lake.', GETUTCDATE()),

-- 39
('Udaipur Lake City Romance', 'udaipur-lake-city-romance',
 'Floating palaces, serene lakes and romantic sunsets in the Venice of the East.',
 '<p>Udaipur, the City of Lakes, is Rajasthan''s most romantic city. This package includes a boat ride on Lake Pichola, City Palace, Jagdish Temple and Saheliyon ki Bari.</p>',
 16000, 13500, 4, 3, 'Udaipur', 'India', 'Jaipur',
 'Honeymoon', 'Historical', '4 Star', 'Breakfast Only', 'Private Car',
 0, 0, 1, 1, 1, 39,
 'Udaipur Honeymoon Package 4 Days | Lake Pichola & City Palace', 'udaipur tour, lake pichola, city palace, udaipur honeymoon, rajasthan romance',
 '4-day Udaipur romantic package with Lake Pichola boat ride, City Palace and heritage hotel stay.', GETUTCDATE()),

-- 40
('Mahabaleshwar Strawberry Hills', 'mahabaleshwar-strawberry-hills',
 'Strawberry farms, misty valleys, Arthur''s Seat viewpoint and Pratapgad Fort in Maharashtra.',
 '<p>Mahabaleshwar is Maharashtra''s premier hill station, famous for strawberries and panoramic views. Package includes Panchgani, Wenna Lake and Venna River boating.</p>',
 10000, 8500, 3, 2, 'Mahabaleshwar', 'India', 'Mumbai',
 'Domestic', 'Mountains', '3 Star', 'Breakfast Only', 'Bus',
 0, 0, 0, 0, 1, 40,
 'Mahabaleshwar Tour Package 3 Days | Strawberry Farm & Hills', 'mahabaleshwar tour, panchgani, strawberry farm, maharashtra hill station',
 '3-day Mahabaleshwar tour with strawberry farm, Arthur Seat viewpoint and Venna Lake boating.', GETUTCDATE()),

-- 41
('Hampi Ancient Ruins', 'hampi-ancient-ruins',
 'The ruins of the Vijayanagara Empire — boulder-strewn landscapes and magnificent stone temples.',
 '<p>Hampi is a UNESCO World Heritage Site and one of India''s most extraordinary archaeological sites. This package covers Virupaksha Temple, Vittala Temple and Matanga Hill sunset.</p>',
 11500, NULL, 3, 2, 'Hampi', 'India', 'Bengaluru',
 'Domestic', 'Historical', '3 Star', 'Breakfast Only', 'Bus',
 0, 0, 0, 0, 1, 41,
 'Hampi Tour Package 3 Days | Vijayanagara Ruins & Temples', 'hampi tour, vijayanagara, vittala temple, karnataka heritage, hampi ruins',
 '3-day Hampi heritage tour covering the Vijayanagara ruins, Vittala Temple and Matanga Hill.', GETUTCDATE()),

-- 42
('Kashmir Valley Heaven', 'kashmir-valley-heaven',
 'Dal Lake shikaras, Mughal gardens, snow-capped peaks and saffron fields in Paradise on Earth.',
 '<p>Kashmir is rightly called Paradise on Earth. This package covers Dal Lake houseboat stay, Gulmarg gondola ride, Pahalgam meadows and the Mughal gardens of Shalimar.</p>',
 32000, 27000, 7, 6, 'Srinagar', 'India', 'Delhi',
 'Domestic', 'Mountains', '4 Star', 'Full Board', 'Flight',
 1, 0, 1, 1, 1, 42,
 'Kashmir Tour Package 7 Days | Dal Lake & Gulmarg', 'kashmir tour, dal lake, gulmarg gondola, pahalgam, srinagar package',
 '7-day Kashmir valley tour with Dal Lake houseboat, Gulmarg gondola and Pahalgam meadows.', GETUTCDATE()),

-- 43
('Nainital Kumaon Hills', 'nainital-kumaon-hills',
 'Emerald lakes, colonial hill towns and Himalayan views in the beautiful Kumaon region.',
 '<p>Nainital is Uttarakhand''s most popular lake resort. This package covers Naini Lake boating, Naina Devi Temple, Snow View Point and Corbett Tiger Reserve extension.</p>',
 12000, 9999, 4, 3, 'Nainital', 'India', 'Delhi',
 'Domestic', 'Mountains', '3 Star', 'Breakfast Only', 'Bus',
 0, 0, 0, 0, 1, 43,
 'Nainital Tour Package 4 Days | Lake & Himalayan Views', 'nainital tour, naini lake, kumaon hills, uttarakhand tour, hill station',
 '4-day Nainital package with Naini Lake boating, Snow View Point and Naina Devi Temple.', GETUTCDATE()),

-- 44
('Gokarna Beach Retreat', 'gokarna-beach-retreat',
 'Unspoilt beaches, a magnificent shoreline and an ancient Shiva temple in coastal Karnataka.',
 '<p>Gokarna is a holy temple town that also happens to have some of India''s most beautiful and secluded beaches — Om Beach, Kudle Beach and Half Moon Beach.</p>',
 9000, 7500, 3, 2, 'Gokarna', 'India', 'Goa',
 'Domestic', 'Beach', '3 Star', 'Breakfast Only', 'Bus',
 0, 0, 0, 0, 1, 44,
 'Gokarna Beach Tour 3 Days | Om Beach & Mahabaleshwar Temple', 'gokarna tour, gokarna beach, om beach, kudle beach, karnataka beach',
 '3-day Gokarna beach retreat covering Om Beach, Kudle Beach and the ancient Mahabaleshwar Temple.', GETUTCDATE()),

-- 45
('Andhra Pilgrimage Circuit', 'andhra-pilgrimage-circuit',
 'Tirupati, Srisailam and Ahobilam — the three most sacred shrines of Andhra Pradesh.',
 '<p>This pilgrimage package covers the most important temples of Andhra Pradesh, including the famed Tirupati Venkateswara, the Shaiva shrine of Srisailam and the Narasimha shrine at Ahobilam.</p>',
 14000, 11500, 5, 4, 'Tirupati', 'India', 'Hyderabad',
 'Pilgrimage', 'Religious', '3 Star', 'Full Board', 'Private Car',
 0, 0, 0, 0, 1, 45,
 'Andhra Pilgrimage Package 5 Days | Tirupati & Srisailam', 'andhra temple tour, tirupati srisailam, andhra pilgrimage, south india temples',
 '5-day Andhra Pradesh pilgrimage covering Tirupati, Srisailam and Ahobilam shrines.', GETUTCDATE()),

-- 46
('Khajuraho Erotic Temples', 'khajuraho-erotic-temples',
 'The UNESCO-listed medieval temples of Khajuraho with their extraordinary erotic sculptures.',
 '<p>Khajuraho''s temples are a UNESCO World Heritage Site and a masterpiece of medieval Indian architecture. This package also covers Orchha Fort and Panna Tiger Reserve.</p>',
 14000, NULL, 4, 3, 'Khajuraho', 'India', 'Delhi',
 'Domestic', 'Historical', '3 Star', 'Breakfast Only', 'Flight',
 1, 0, 0, 0, 1, 46,
 'Khajuraho Tour Package 4 Days | UNESCO Temple Sculptures', 'khajuraho tour, khajuraho temples, orchha, madhya pradesh tour, unesco india',
 '4-day Khajuraho tour covering UNESCO temples, Orchha Fort and Panna Tiger Reserve.', GETUTCDATE()),

-- 47
('Sundarbans Mangrove Safari', 'sundarbans-mangrove-safari',
 'Cruise through the world''s largest mangrove forest in search of Royal Bengal Tigers.',
 '<p>The Sundarbans is a UNESCO World Heritage Site and the world''s largest mangrove delta. This package includes boat safaris through the tidal rivers, bird watching and tiger tracking.</p>',
 18000, 15000, 4, 3, 'Sundarbans', 'India', 'Kolkata',
 'Domestic', 'Wildlife', '3 Star', 'Full Board', 'Private Car',
 0, 0, 0, 0, 1, 47,
 'Sundarbans Tour Package 4 Days | Mangrove Safari', 'sundarbans tour, mangrove safari, royal bengal tiger, west bengal wildlife',
 '4-day Sundarbans mangrove safari with boat cruise, bird watching and Royal Bengal Tiger tracking.', GETUTCDATE()),

-- 48
('Haridwar Rishikesh Pilgrimage', 'haridwar-rishikesh-pilgrimage',
 'The Gateway to the Gods and Yoga Capital of the World — combined in one divine package.',
 '<p>Haridwar and Rishikesh together offer the ultimate spiritual experience on the banks of the Ganges. Package includes Har Ki Pauri aarti, Triveni Ghat and Ram Jhula bridge.</p>',
 8000, 6500, 3, 2, 'Haridwar', 'India', 'Delhi',
 'Pilgrimage', 'Religious', '3 Star', 'Breakfast Only', 'Bus',
 0, 0, 0, 0, 1, 48,
 'Haridwar Rishikesh Tour 3 Days | Har Ki Pauri & Ganga Aarti', 'haridwar tour, rishikesh tour, ganga aarti, har ki pauri, pilgrimage uttarakhand',
 '3-day Haridwar-Rishikesh tour with Ganga Aarti, Ram Jhula and yoga session.', GETUTCDATE()),

-- 49
('Cambodia Angkor Wat', 'cambodia-angkor-wat',
 'The world''s largest religious monument — Angkor Wat — and the temples of Siem Reap.',
 '<p>Angkor Wat is the world''s largest religious structure and a wonder of human achievement. This package includes three full days of temple exploration and a sunset viewing from Phnom Bakheng.</p>',
 52000, 44000, 6, 5, 'Siem Reap', 'Cambodia', 'Delhi',
 'International', 'Historical', '4 Star', 'Breakfast Only', 'Flight',
 1, 1, 0, 0, 1, 49,
 'Cambodia Angkor Wat Tour 6 Days | Siem Reap Temples', 'cambodia tour, angkor wat, siem reap, temple tour cambodia, cambodia package',
 '6-day Cambodia tour with Angkor Wat, Ta Prohm, Bayon and Phnom Bakheng sunset.', GETUTCDATE()),

-- 50
('Varkala Cliff Beach Kerala', 'varkala-cliff-beach-kerala',
 'A bohemian cliffside beach town with yoga retreats, Ayurveda spas and the Arabian Sea.',
 '<p>Varkala is Kerala''s most dramatic beach destination, with its red laterite cliffs rising directly above the Arabian Sea. Package includes Ayurveda treatment, Janardhana Swami Temple and cliff-side dining.</p>',
 13000, 10999, 4, 3, 'Varkala', 'India', 'Thiruvananthapuram',
 'Domestic', 'Beach', '3 Star', 'Breakfast Only', 'Flight',
 1, 0, 0, 0, 1, 50,
 'Varkala Beach Tour 4 Days | Kerala Clifftop & Ayurveda', 'varkala tour, varkala beach, kerala beach, cliff beach, ayurveda kerala',
 '4-day Varkala cliffside beach package with Ayurveda session, Janardhana Temple and beach yoga.', GETUTCDATE());

GO

-- ──────────────────────────────────────────────────────────────
-- 2. ITINERARIES  (for packages 1–10)
-- ──────────────────────────────────────────────────────────────
DECLARE @p1 INT = (SELECT PackageId FROM Packages WHERE Slug = 'goa-beach-paradise');
DECLARE @p2 INT = (SELECT PackageId FROM Packages WHERE Slug = 'kerala-backwaters-escape');
DECLARE @p3 INT = (SELECT PackageId FROM Packages WHERE Slug = 'rajasthan-royal-heritage');
DECLARE @p4 INT = (SELECT PackageId FROM Packages WHERE Slug = 'manali-snow-adventure');
DECLARE @p5 INT = (SELECT PackageId FROM Packages WHERE Slug = 'andaman-island-explorer');

-- Goa (5 days)
INSERT INTO PackageItineraries (PackageId, DayNumber, Title, Description) VALUES
(@p1, 1, 'Arrival & North Goa Beaches', 'Arrive at Goa airport. Check into hotel. Evening visit to Calangute, Baga and Anjuna beaches. Overnight at beachside resort.'),
(@p1, 2, 'North Goa Sightseeing', 'Fort Aguada, Chapora Fort, Vagator Beach and the vibrant Saturday Night Market. Sunset at Anjuna flea market.'),
(@p1, 3, 'Water Sports Day', 'Full day of water sports — jet skiing, banana boat, parasailing and scuba diving at Grand Island.'),
(@p1, 4, 'South Goa Exploration', 'Colva Beach, Palolem Beach and the magnificent Basilica of Bom Jesus and Se Cathedral in Old Goa.'),
(@p1, 5, 'Leisure & Departure', 'Breakfast at hotel. Leisure time for shopping at Panjim market. Transfer to airport for departure.');

-- Kerala (6 days)
INSERT INTO PackageItineraries (PackageId, DayNumber, Title, Description) VALUES
(@p2, 1, 'Arrival in Kochi', 'Arrive at Kochi airport. Check into hotel. Evening visit to Fort Kochi, Chinese Fishing Nets and St. Francis Church.'),
(@p2, 2, 'Kochi to Munnar', 'Morning drive to Munnar (130 km). En route visit Cheeyappara Waterfalls and spice plantations. Arrive Munnar evening.'),
(@p2, 3, 'Munnar Tea Gardens', 'Visit Eravikulam National Park, Top Station, Mattupetty Dam and the TATA Tea Museum. Evening at leisure.'),
(@p2, 4, 'Munnar to Alleppey', 'Drive to Alleppey (165 km). Afternoon houseboat check-in on the famous Kerala backwaters. Cruise through tranquil village life.'),
(@p2, 5, 'Alleppey to Kovalam', 'Morning cruise to Alleppey jetty. Drive to Kovalam beach resort. Afternoon beach leisure and Ayurveda massage session.'),
(@p2, 6, 'Kovalam & Departure', 'Morning walk on Kovalam beach. Visit Padmanabhaswamy Temple in Trivandrum. Transfer to airport for departure.');

-- Rajasthan (8 days)
INSERT INTO PackageItineraries (PackageId, DayNumber, Title, Description) VALUES
(@p3, 1, 'Arrive Delhi – Transfer to Jaipur', 'Arrive Delhi. Drive to Jaipur (280 km). Check into heritage hotel. Evening at Bapu Bazaar for Rajasthani handicrafts.'),
(@p3, 2, 'Jaipur Pink City', 'Amber Fort elephant ride, City Palace, Jantar Mantar and Hawa Mahal. Evening Light & Sound show at Amber Fort.'),
(@p3, 3, 'Jaipur to Jodhpur', 'Drive to Jodhpur (340 km). En route visit Ajmer Sharif Dargah and Pushkar Lake. Arrive Jodhpur evening.'),
(@p3, 4, 'Jodhpur Blue City', 'Mehrangarh Fort, Jaswant Thada, Umaid Bhawan Palace museum and the vibrant Clock Tower market.'),
(@p3, 5, 'Jodhpur to Udaipur', 'Drive to Udaipur (260 km). En route visit Ranakpur Jain Temples. Check into lake-view hotel in Udaipur.'),
(@p3, 6, 'Udaipur Lake City', 'City Palace, boat ride on Lake Pichola, Jagdish Temple and Saheliyon ki Bari. Sunset at Karni Mata Temple.'),
(@p3, 7, 'Udaipur to Jaisalmer', 'Fly or drive to Jaisalmer. Afternoon visit Jaisalmer Fort (a living fort). Overnight in desert camp.'),
(@p3, 8, 'Camel Safari & Departure', 'Sunrise camel safari on Sam Sand Dunes. Patwon Ki Haveli visit. Transfer to airport for departure.');

-- Manali (5 days)
INSERT INTO PackageItineraries (PackageId, DayNumber, Title, Description) VALUES
(@p4, 1, 'Delhi to Manali (Overnight Bus)', 'Depart Delhi on Volvo AC bus. Overnight journey through the scenic Kullu valley.'),
(@p4, 2, 'Arrive Manali – Local Sightseeing', 'Arrive Manali morning. Check into hotel. Visit Hadimba Devi Temple, Vashisht hot springs and Old Manali.'),
(@p4, 3, 'Rohtang Pass / Solang Valley', 'Early morning drive to Rohtang Pass (3978m) for snow activities. Afternoon visit Solang Valley for paragliding.'),
(@p4, 4, 'River Rafting & Leisure', 'Morning river rafting on the Beas River (14 km stretch). Afternoon visit Kullu market and craft centres.'),
(@p4, 5, 'Departure', 'Breakfast. Check out. Board evening bus back to Delhi overnight.');

-- Andaman (6 days)
INSERT INTO PackageItineraries (PackageId, DayNumber, Title, Description) VALUES
(@p5, 1, 'Arrival Port Blair', 'Arrive Port Blair. Transfer to hotel. Afternoon visit Cellular Jail and attend the mesmerising Light & Sound Show.'),
(@p5, 2, 'Ross Island & North Bay', 'Morning ferry to Ross Island (former British headquarters). Afternoon glass-bottomed boat at North Bay Island.'),
(@p5, 3, 'Transfer to Havelock Island', 'Ferry to Havelock Island. Check into beach resort. Evening at Radhanagar Beach (Asia''s Best Beach).'),
(@p5, 4, 'Havelock Snorkelling & Scuba', 'Morning snorkelling at Elephant Beach coral reef. Afternoon optional scuba diving (extra cost). Sunset at Radhanagar.'),
(@p5, 5, 'Neil Island Day Trip', 'Day trip to Neil Island. Visit Natural Bridge, Bharatpur Beach and Laxmanpur Beach.'),
(@p5, 6, 'Return to Port Blair & Departure', 'Return ferry to Port Blair. Shopping at Aberdeen Bazaar for local souvenirs. Transfer to airport.');

GO

-- ──────────────────────────────────────────────────────────────
-- 3. INCLUSIONS (for first 20 packages)
-- ──────────────────────────────────────────────────────────────
INSERT INTO PackageInclusions (PackageId, Item)
SELECT p.PackageId, inc.Item
FROM Packages p
CROSS APPLY (VALUES
    ('Accommodation in listed / similar hotels'),
    ('Daily breakfast as per itinerary'),
    ('All transfers by air-conditioned vehicle'),
    ('Airport / railway station pick-up and drop'),
    ('All applicable hotel taxes')
) AS inc(Item)
WHERE p.Slug IN (
    'goa-beach-paradise','kerala-backwaters-escape','rajasthan-royal-heritage',
    'manali-snow-adventure','andaman-island-explorer','darjeeling-sikkim-charm',
    'ladakh-himalayan-odyssey','shimla-manali-honeymoon','coorg-coffee-country',
    'varanasi-spiritual-journey'
);

INSERT INTO PackageInclusions (PackageId, Item)
SELECT p.PackageId, inc.Item
FROM Packages p
CROSS APPLY (VALUES
    ('Return international airfare (economy class)'),
    ('Hotel accommodation as per itinerary'),
    ('Daily breakfast included'),
    ('Visa processing assistance'),
    ('Travel insurance coverage'),
    ('Airport transfers on arrival and departure')
) AS inc(Item)
WHERE p.Slug IN (
    'thailand-beach-culture','bali-island-paradise','dubai-gold-glamour',
    'singapore-city-experience','maldives-luxury-escape','sri-lanka-heritage-tour',
    'nepal-himalaya-trekking','bhutan-happiness-kingdom','vietnam-heritage-beach',
    'cambodia-angkor-wat'
);

-- ──────────────────────────────────────────────────────────────
-- 4. EXCLUSIONS (for first 20 packages)
-- ──────────────────────────────────────────────────────────────
INSERT INTO PackageExclusions (PackageId, Item)
SELECT p.PackageId, exc.Item
FROM Packages p
CROSS APPLY (VALUES
    ('Airfare / train fare unless mentioned'),
    ('Lunch and dinner unless specified'),
    ('Personal expenses — laundry, tips, telephone'),
    ('Entry fees to monuments and national parks'),
    ('Adventure activity charges (rafting, paragliding etc.)')
) AS exc(Item)
WHERE p.Slug IN (
    'goa-beach-paradise','kerala-backwaters-escape','rajasthan-royal-heritage',
    'manali-snow-adventure','andaman-island-explorer','darjeeling-sikkim-charm',
    'ladakh-himalayan-odyssey','shimla-manali-honeymoon','coorg-coffee-country',
    'varanasi-spiritual-journey'
);

INSERT INTO PackageExclusions (PackageId, Item)
SELECT p.PackageId, exc.Item
FROM Packages p
CROSS APPLY (VALUES
    ('Visa fees (unless explicitly included)'),
    ('Travel insurance (unless included)'),
    ('Personal shopping and expenses'),
    ('Optional excursions and activities'),
    ('Meals not mentioned in the itinerary'),
    ('Porterage and tips')
) AS exc(Item)
WHERE p.Slug IN (
    'thailand-beach-culture','bali-island-paradise','dubai-gold-glamour',
    'singapore-city-experience','maldives-luxury-escape','sri-lanka-heritage-tour',
    'nepal-himalaya-trekking','bhutan-happiness-kingdom','vietnam-heritage-beach',
    'cambodia-angkor-wat'
);

GO

-- ──────────────────────────────────────────────────────────────
-- 5. VERIFY
-- ──────────────────────────────────────────────────────────────
SELECT
    COUNT(*)           AS TotalPackages,
    SUM(CASE WHEN IsFeatured = 1 THEN 1 ELSE 0 END) AS Featured,
    SUM(CASE WHEN IsPopular  = 1 THEN 1 ELSE 0 END) AS Popular,
    SUM(CASE WHEN IsActive   = 1 THEN 1 ELSE 0 END) AS Active
FROM Packages;

SELECT COUNT(*) AS TotalItineraries  FROM PackageItineraries;
SELECT COUNT(*) AS TotalInclusions   FROM PackageInclusions;
SELECT COUNT(*) AS TotalExclusions   FROM PackageExclusions;
