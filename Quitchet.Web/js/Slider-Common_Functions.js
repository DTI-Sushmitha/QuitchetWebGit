//Home Price Sliders Functionality & methods - Start
//True VAlues For Homes 
var trueValues = [0, 10000, 20000, 30000, 40000, 50000, 60000, 70000, 80000, 90000, 100000,
                     110000, 120000, 130000, 140000, 150000, 160000, 170000, 180000, 190000, 200000,
                     210000, 220000, 230000, 240000, 250000, 260000, 270000, 280000, 290000, 300000,
                     310000, 320000, 330000, 340000, 350000, 360000, 370000, 380000, 390000, 400000,
                     410000, 420000, 430000, 440000, 450000, 460000, 470000, 480000, 490000, 500000,
                     510000, 520000, 530000, 540000, 550000, 560000, 570000, 580000, 590000, 600000,
                     610000, 620000, 630000, 640000, 650000, 660000, 670000, 680000, 690000, 700000,
                     710000, 720000, 730000, 740000, 750000, 760000, 770000, 780000, 790000, 800000,
                     810000, 820000, 830000, 840000, 850000, 860000, 870000, 880000, 890000, 900000,
                     910000, 920000, 930000, 940000, 950000, 960000, 970000, 980000, 990000, 1000000,
                     1100000, 1200000, 1300000, 1400000, 1500000, 1600000, 1700000, 1800000, 1900000, 2000000,
                     2100000, 2200000, 2300000, 2400000, 2500000, 2600000, 2700000, 2800000, 2900000, 3000000,
                     3100000, 3200000, 3300000, 3400000, 3500000, 3600000, 3700000, 3800000, 3900000, 4000000,
                     4100000, 4200000, 4300000, 4400000, 4500000, 4600000, 4700000, 4800000, 4900000, 5000000,
                     5100000, 5200000, 5300000, 5400000, 5500000, 5600000, 5700000, 5800000, 5900000, 6000000,
                     6100000, 6200000, 6300000, 6400000, 6500000, 6600000, 6700000, 6800000, 6900000, 7000000,
                     7100000, 7200000, 7300000, 7400000, 7500000, 7600000, 7700000, 7800000, 7900000, 8000000,
                     8100000, 8200000, 8300000, 8400000, 8500000, 8600000, 8700000, 8800000, 8900000, 9000000,
                     9100000, 9200000, 9300000, 9400000, 9500000, 9600000, 9700000, 9800000, 9900000, 10000000
];
// Key/Index Values for Homes
var values = [0, 0.5, 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6, 6.5, 7, 7.5, 8, 8.5, 9, 9.5, 10,
    10.5, 11, 11.5, 12, 12.5, 13, 13.5, 14, 14.5, 15, 15.5, 16, 16.5, 17, 17.5, 18, 18.5, 19, 19.5, 20,
    20.5, 21, 21.5, 22, 22.5, 23, 23.5, 24, 24.5, 25, 25.5, 26, 26.5, 27, 27.5, 28, 28.5, 29, 29.5, 30,
    30.5, 31, 31.5, 32, 32.5, 33, 33.5, 34, 34.5, 35, 35.5, 36, 36.5, 37, 37.5, 38, 38.5, 39, 39.5, 40,
    40.5, 41, 41.5, 42, 42.5, 43, 43.5, 44, 44.5, 45, 45.5, 46, 46.5, 47, 47.5, 48, 48.5, 49, 49.5, 50,
    50.5, 51, 51.5, 52, 52.5, 53, 53.5, 54, 54.5, 55, 55.5, 56, 56.5, 57, 57.5, 58, 58.5, 59, 59.5, 60,
    60.5, 61, 61.5, 62, 62.5, 63, 63.5, 64, 64.5, 65, 65.5, 66, 66.5, 67, 67.5, 68, 68.5, 69, 69.5, 70,
    70.5, 71, 71.5, 72, 72.5, 73, 73.5, 74, 74.5, 75, 75.5, 76, 76.5, 77, 77.5, 78, 78.5, 79, 79.5, 80,
    80.5, 81, 81.5, 82, 82.5, 83, 83.5, 84, 84.5, 85, 85.5, 86, 86.5, 87, 87.5, 88, 88.5, 89, 89.5, 90,
    91, 92, 93, 94, 95, 96, 97, 98, 99, 100];

var valuessssss = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26,
    27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52,
    53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78,
    79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103,
    104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124,
    125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 145,
    146, 147, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160, 161, 162, 163, 164, 165, 166,
    167, 168, 169, 170, 171, 172, 173, 174, 175, 176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187,
    188, 189, 190]
//Funtion to get rEal home price value
function getRealValue(sliderValue) {
    for (var i = 0; i < values.length; i++) {
        if (values[i] >= sliderValue) {
            return trueValues[i];
        }
    }
    return 0;
}

//---------------To Get Dupe/Index Values
function getIndexValue(sliderValue) {
    for (var i = 0; i < trueValues.length; i++) {
        if (trueValues[i] == sliderValue) {
            return values[i];
        }
    }
    return 0;
}

//To Get Formatted Price for Homes/ Lots
function getFormattedPrice(priceval) {
    var price;
    if (priceval.toString().length == 5) {
        price = '$' + priceval.toString().substring(0, 2) + 'K';
    }
    if (priceval.toString().length == 6) {
        price = '$' + priceval.toString().substring(0, 3) + 'K';
    }
    if (priceval.toString().length == 7) {
        var milion = priceval.toString().substring(0, 2);
        var splitmilion = milion.toString().substring(0, 1) + '.' + milion.toString().substring(1, 2);
        price = '$' + splitmilion + 'M';
    }

    return price;
}
//Home Sliders Functionality & methods - End

//Rental Price Sliders Functionality & methods - Start
var Rental_Price_Truevalues = [0, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800,
    850, 900, 950, 1000, 1050, 1100, 1150, 1200, 1250, 1300, 1350, 1400, 1450, 1500, 1550, 1600,
    1650, 1700, 1750, 1800, 1850, 1900, 1950, 2000, 2100, 2200, 2300, 2400, 2500, 2600, 2700,
   2800, 2900, 3000, 3100, 3200, 3300, 3400, 3500, 3600, 3700, 3800, 3900, 4000,
   4100, 4200, 4300, 4400, 4500, 4600, 4700, 4800, 4900, 5000, 5250, 5500, 5750,
   6000, 6250, 6500, 6750, 7000, 7250, 7500, 8000, 8500, 9000, 9500, 10000,
   10500, 11000, 11500, 12000, 12500, 13000, 13500, 14000, 14500, 15000, 16000, 17000, 18000, 19000, 20000,
   21000, 22000, 23000, 24000, 25000,
   30000, 35000, 40000, 50000,
   60000, 70000, 80000,80001];
var Rental_Price_Indexvalues = [0,1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16,
    17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32,
    33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47,
    48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60,
    61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73,
    74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85,
    86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100,
    101, 102, 103, 104,105,
    106,107, 108, 109,
    110, 111,112,113];
//----To Get Real Values and Index of REntal Price
function getRentalPriceRealValue(sliderValue) {
    for (var i = 0; i < Rental_Price_Indexvalues.length; i++) {
        if (Rental_Price_Indexvalues[i] >= sliderValue) {
            return Rental_Price_Truevalues[i];
        }
    }
    return 0;
}
function getRentalPriceIndexValue(sliderValue) {
    for (var i = 0; i < Rental_Price_Truevalues.length; i++) {
        if (Rental_Price_Truevalues[i] == sliderValue) {
            return Rental_Price_Indexvalues[i];
        }
    }
    return 0;
}
// To get formatted values for Rental Price
function getFormattedPrice_Rental(priceval) {
    var price;
    if (priceval.toString().length == 3) {
        price = '$' + priceval + '/mo';
    }
    else if (priceval.toString().length == 4) {
        if (priceval == "1000" || priceval == "2000" || priceval == "3000" || priceval == "4000" || priceval == "5000" || priceval == "6000" || priceval == "7000" || priceval == "8000" || priceval == "9000") {
            price = priceval.toString().substring(0, 1);
            price = "$" + price + ".0k/mo";
        }
        else
            price = '$' + priceval / 1000 + 'k/mo';
        //endd = '$' + endd.toString().substring(0, 2) + 'K/mo';
    }
    else {
        if (priceval == "10000" || priceval == "20000" || priceval == "30000" || priceval == "40000" || priceval == "50000" || priceval == "60000" || priceval == "70000" || priceval == "80000") {
            price = priceval.toString().substring(0, 1);
            price = "$" + price + "0.0k/mo";
        }
        else
            price = '$' + priceval / 1000 + 'k/mo';
    }
    return price;
}
//Rental Sliders Functionality & methods - End

//Sqft slider Functionality - Start
var sqft_True_values = [];
sqft_True_values[0] = 0;
var sqft_Index_values = [];
sqft_Index_values[0] = 0;
for (var i = 1; i <= 70; i++) {
    sqft_True_values[i] = sqft_True_values[i - 1] + 100;
    sqft_Index_values[i] = i;
}
//----To Get Real Values and Index of REntal Price
function getSqftRealValue(sliderValue) {
    for (var i = 0; i < sqft_Index_values.length; i++) {
        if (sqft_Index_values[i] >= sliderValue) {
            return sqft_True_values[i];
        }
    }
    return 0;
}
function getSqftIndexValue(sliderValue) {
    for (var i = 0; i < sqft_True_values.length; i++) {
        if (sqft_True_values[i] == sliderValue) {
            return sqft_Index_values[i];
        }
    }
    return 0;
}
//Lot Acreage Slider Functionality - End
//Sqft slider Functionality - End

//Lot Arcreage Slider Functionality - Start
var LotAcreage_True_Values = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 15, 20, 25, 30, 35, 40, 45, 50];
var LotAcreage_Index_Values = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18];
//----To Get Real Values and Index of REntal Price
function getLotAcrgRealValue(sliderValue) {
    for (var i = 0; i < LotAcreage_Index_Values.length; i++) {
        if (LotAcreage_Index_Values[i] >= sliderValue) {
            return LotAcreage_True_Values[i];
        }
    }
    return 0;
}
function getLotAcrgIndexValue(sliderValue) {
    for (var i = 0; i < LotAcreage_True_Values.length; i++) {
        if (LotAcreage_True_Values[i] == sliderValue) {
            return LotAcreage_Index_Values[i];
        }
    }
    return 0;
}
//Lot Acreage Slider Functionality - End

