using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Enums;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Persistance.DatabaseContext.SeedData
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<WriteDbContext.WriteDbContext>();
                int result = 0;
                var cities = new List<City>
                    {
                        new City {Id = 1,  Label = "تهران",Value ="تهران" ,IsActive = true, ProvinceId = 1 },
                        new City {Id = 2,  Label = "پردیس",Value ="پردیس",IsActive = true, ProvinceId = 1 },
                        new City {Id = 3, Label = "ری",Value ="ری", ProvinceId = 1 ,IsActive = true,},
                        new City {Id = 4,  Label = "رباط کریم",Value ="رباط کریم", ProvinceId = 1 ,IsActive = true,},
                        new City {Id = 5, Label = "شهریار",Value ="شهریار", ProvinceId = 1 , IsActive = true},
                        new City {Id = 6, Label = "ورامین",Value ="ورامین", ProvinceId = 1 , IsActive = true},
                        new City {Id = 7,  Label = "بهارستان",Value ="بهارستان", ProvinceId = 1 , IsActive = true},
                        new City {Id = 8,  Label = "شریف آباد", Value ="شریف آباد",ProvinceId = 1 , IsActive = true},
                        new City {Id = 9, Label = "چهاردانگه",Value ="چهاردانگه", ProvinceId = 1 , IsActive = true},
                        new City {Id = 10, Label = "شهرقدس",Value ="شهرقدس", ProvinceId = 1 , IsActive = true},
                        new City {Id = 11, Label = "بومهن", Value ="بومهن",ProvinceId = 1 , IsActive = true},
                        new City {Id = 12, Label = "اسلام شهر",Value ="اسلام شهر",ProvinceId = 1 , IsActive = true},
                        new City {Id = 13, Label = "پاکدشت",Value ="پاکدشت", ProvinceId = 1 , IsActive = true},
                        new City {Id = 14, Label = "قرچک",Value ="قرچک", ProvinceId = 1 , IsActive = true},
                        new City {Id = 15, Label = "کهریزک",Value ="کهریزک", ProvinceId = 1 , IsActive = true},
                        new City {Id = 16, Label = "رودهن",Value ="رودهن", ProvinceId = 1 , IsActive = true},
                        new City {Id = 17, Label = "حسن آباد",Value ="حسن آباد", ProvinceId = 1 , IsActive = true},
                        new City {Id = 18, Label = "پرند",Value ="پرند", ProvinceId = 1 ,IsActive = true,},
                        new City {Id = 19, Label = "باقر شهر",Value ="باقر شهر", ProvinceId = 1 ,IsActive = true,},
                        new City {Id = 20, Label = "آبسرد",Value ="آبسرد", ProvinceId = 1 ,IsActive = true,},
                        new City {Id = 21, Label = "فیروزکوه",Value ="فیروزکوه", ProvinceId = 1 ,IsActive = true,},
                        new City {Id = 22, Label = "ملارد",Value ="ملارد", ProvinceId = 1 ,IsActive = true,},
                        new City {Id = 23, Label = "صفا دشت",Value ="صفا دشت", ProvinceId = 1 ,IsActive = true,},
                        new City {Id=24,  Label = "دماوند",Value ="دماوند", ProvinceId = 1 ,IsActive = true,},
                        new City {Id=25, Label = "کمالشهر",Value ="کمالشهر", ProvinceId = 1 ,IsActive = true,},
                        new City {Id=26, Label = "آبعلی",Value ="آبعلی", ProvinceId = 1 ,IsActive = true,},
                        new City {Id=27, Label = "محمد شهر",Value ="محمد شهر", ProvinceId = 1 ,IsActive = true,},
                        new City {Id=28,  Label = "قیامدشت",Value ="قیامدشت", ProvinceId = 1 ,IsActive = true,},
                        new City {Id=29, Label = "جاجرود",Value ="جاجرود", ProvinceId = 1 ,IsActive = true,},
                        new City {Id=30,  Label = "پرندک",Value ="پرندک", ProvinceId = 1 ,IsActive = true,},
                        new City {Id=31, Label = "پیشوا",Value ="پیشوا", ProvinceId = 1 ,IsActive = true,},
                        new City {Id=32, Label = "رودباران قصران",Value ="رودباران قصران", ProvinceId = 1 ,IsActive = true,},
                        new City {Id=33, Label = "لوسانات",Value ="لوسانات", ProvinceId = 1 ,IsActive = true,},
                        new City {Id=34, Label = "شمیرانات",Value ="شمیرانات", ProvinceId = 1 ,IsActive = true,},
                        ////////////////////////////////////////////////////////////
                        new City {Id=35, Label = "رشت", Value= "رشت" , ProvinceId = 2 ,IsActive = true,},
                        new City {Id=36, Label = "بندرانزلی", Value= "بندرانزلی", ProvinceId = 2 ,IsActive = true,},
                        new City {Id=37, Label = "آستارا", Value= "آستارا",ProvinceId = 2 ,IsActive = true,},
                        new City {Id=38, Label = "لاهیجان", Value= "لاهیجان",ProvinceId = 2 ,IsActive = true,},
                        new City {Id=39, Label = "لوشان", Value= "لوشان",ProvinceId = 2 ,IsActive = true,},
                        new City {Id=40, Label = "هشت پر", Value= "هشت پر",ProvinceId = 2 ,IsActive = true,},
                        new City {Id=41, Label = "بندر کیانشهر", Value= "بندر کیانشهر",ProvinceId = 2 ,IsActive = true,},
                        new City {Id=42, Label = "کوچصفهان", Value= "کوچصفهان",ProvinceId = 2 ,IsActive = true,},
                        new City {Id=43, Label = "کلاچای", Value= "کلاچای",ProvinceId = 2 ,IsActive = true,},
                        new City {Id=44, Label = "آستانه اشرفيه", Value= "آستانه اشرفيه",ProvinceId = 2 ,IsActive = true,},
                        new City {Id=45, Label = "رضوان شهر", Value= "رضوان شهر",ProvinceId = 2 ,IsActive = true,},
                        new City {Id=46, Label = "ماسال", Value= "ماسال",ProvinceId = 2 ,IsActive = true,},
                        new City {Id=47, Label = "طوالش", Value= "طوالش",ProvinceId = 2 ,IsActive = true,},
                        new City {Id=48, Label = "رستم آباد",Value= "رستم آباد", ProvinceId = 2 ,IsActive = true,},
                        new City {Id=49, Label = "رودبار", Value= "رودبار",ProvinceId = 2,IsActive = true, },
                        new City {Id=50, Label = "املش", Value= "املش",ProvinceId = 2 ,IsActive = true,},
                        new City {Id=51, Label = "رودسر", Value= "رودسر",ProvinceId = 2 ,IsActive = true,},
                        new City {Id=52, Label = "صومعه سرا", Value= "صومعه سرا",ProvinceId = 2 , IsActive = true},
                        new City {Id=53, Label = "شفت", Value= "شفت",ProvinceId = 2 ,IsActive = true,},
                        new City {Id=54,  Label = "فومن", Value= "فومن",ProvinceId = 2 ,IsActive = true,},
                        new City {Id=55, Label = "سیاهکل", Value= "سیاهکل",ProvinceId = 2 ,IsActive = true,},
                        new City {Id=56, Label = "لنگرود", Value= "لنگرود",ProvinceId = 2 ,IsActive = true,},
                        new City {Id=57, Label = "اسالم", Value= "اسالم",ProvinceId = 2 ,IsActive = true,},
                        new City {Id=58, Label = "چابکسر", Value= "چابکسر",ProvinceId = 2 ,IsActive = true,},
                        new City {Id=59, Label = "تالش", Value= "تالش",ProvinceId = 2 ,IsActive = true,},
                        new City {Id=60, Label = "خشکبیجار", Value= "خشکبیجار",ProvinceId = 2 ,IsActive = true,},
                        new City {Id=61, Label = "منجیل", Value= "منجیل",ProvinceId = 2 ,IsActive = true,},
                        new City {Id=62, Label = "سنگسر", Value= "سنگسر",ProvinceId = 2 ,IsActive = true,},
                        ///////////////////////////////////////////////////////////
                        new City {Id=63, Label = "تبریز",Value= "تبریز", ProvinceId = 3 ,IsActive = true,},
                        new City {Id=64, Label = "ممقان", Value= "ممقان",ProvinceId = 3,IsActive = true, },
                        new City {Id=65, Label = "خسرو شهر", Value= "خسرو شهر",ProvinceId = 3,IsActive = true, },
                        new City {Id=66, Label = "خراجو",Value= "خراجو", ProvinceId = 3,IsActive = true, },
                        new City {Id=67, Label = "اهر", Value= "اهر",ProvinceId = 3,IsActive = true, },
                        new City {Id=68, Label = "ورزقان",Value= "ورزقان", ProvinceId = 3,IsActive = true, },
                        new City {Id=69, Label = "آذرشهر", Value= "آذرشهر",ProvinceId = 3,IsActive = true, },
                        new City {Id=70, Label = "اسکو", Value= "اسکو",ProvinceId = 3,IsActive = true, },
                        new City {Id=71,  Label = "بستان آباد",Value= "بستان آباد", ProvinceId = 3,IsActive = true, },
                        new City {Id=72, Label = "شبستر",Value= "شبستر", ProvinceId = 3,IsActive = true, },
                        new City {Id=73, Label = "خاروانا", Value= "خاروانا",ProvinceId = 3 ,IsActive = true,},
                        new City {Id=74, Label = "سراب",Value= "سراب", ProvinceId = 3,IsActive = true, },
                        new City {Id=75, Label = "هادی شهر",Value= "هادی شهر", ProvinceId = 3,IsActive = true, },
                        new City {Id=76, Label = "کلیبر", Value= "کلیبر",ProvinceId = 3 ,IsActive = true,},
                        new City {Id=77, Label = "بناب",Value= "بناب", ProvinceId = 3,IsActive = true, },
                        new City {Id=78, Label = "عجب شیر", Value= "عجب شیر",ProvinceId = 3 ,IsActive = true,},
                        new City {Id=79, Label = "مراغه",Value= "مراغه", ProvinceId = 3 ,IsActive = true,},
                        new City {Id=80, Label = "ملکان",Value= "ملکان", ProvinceId = 3 ,IsActive = true,},
                        new City {Id=81, Label = "جلفا",Value= "جلفا", ProvinceId = 3 ,IsActive = true,},
                        new City {Id=82, Label = "مرند",Value= "مرند", ProvinceId = 3 ,IsActive = true,},
                        new City {Id=83, Label = "ترکمنچای", Value= "ترکمنچای",ProvinceId = 3 ,IsActive = true,},
                        new City {Id=84, Label = "میانه", Value= "میانه",ProvinceId = 3,IsActive = true, },
                        new City {Id=85,  Label = "هریس",Value= "هریس", ProvinceId = 3 ,IsActive = true,},
                        new City {Id=86, Label = "چاراویماق",Value= "چاراویماق", ProvinceId = 3 ,IsActive = true,},
                        new City {Id=87, Label = "هشترود", Value= "هشترود",ProvinceId = 3,IsActive = true, },
                        new City {Id=88, Label = "قره اغاج", Value= "قره اغاج",ProvinceId = 3,IsActive = true, },
                        new City {Id=89, Label = "خدا آفرین",Value= "خدا آفرین", ProvinceId = 3 ,IsActive = true,},
                        new City {Id=90,  Label = "مهربان",Value= "مهربان", ProvinceId = 3 ,IsActive = true,},
                        ///////////////////////////////////////////////////////////
                        new City {Id=91,  Label = "اهواز", Value= "اهواز",ProvinceId = 4 ,IsActive = true,},
                        new City {Id=92, Label = "بندرماهشهر",Value= "بندرماهشهر", ProvinceId = 4 ,IsActive = true,},
                        new City {Id=93, Label = "بندر امام خمینی",Value= "بندر امام خمینی", ProvinceId = 4,IsActive = true, },
                        new City {Id=94, Label = "امیدیه",Value= "امیدیه", ProvinceId = 4 ,IsActive = true,},
                        new City {Id=95, Label = "آبادان",Value= "آبادان", ProvinceId = 4 ,IsActive = true,},
                        new City {Id=96,  Label = "صیدون", Value= "صیدون",ProvinceId = 4 ,IsActive = true,},
                        new City {Id=97, Label = "سردشت دزفول",Value= "سردشت دزفول", ProvinceId = 4 ,IsActive = true,},
                        new City {Id=98, Label = "اروند کنار", Value= "اروند کنار",ProvinceId = 4,IsActive = true, },
                        new City {Id=99,  Label = "گتوند", Value= "گتوند",ProvinceId = 4 ,IsActive = true,},
                        new City {Id=100, Label = "شوشتر", Value= "شوشتر",ProvinceId = 4,IsActive = true,},
                        new City {Id=101, Label = "لالی", Value= "لالی",ProvinceId = 4,IsActive = true, },
                        new City {Id=102, Label = "مسجد سلیمان", Value= "مسجد سلیمان",ProvinceId = 4,IsActive = true, },
                        new City {Id=103, Label = "شوش", Value= "شوش",ProvinceId = 4,IsActive = true,},
                        new City {Id=104, Label = "لالی",Value= "لالی", ProvinceId = 4 ,IsActive = true,},
                        new City {Id=105, Label = "مسجد سلیمان", Value= "مسجد سلیمان",ProvinceId = 4,IsActive = true, },
                        new City {Id=106, Label = "شوش",Value= "شوش", ProvinceId = 4 , IsActive = true},
                        new City {Id=107, Label = "حمیدیه", Value= "حمیدیه",ProvinceId = 4 ,IsActive = true,},
                        new City {Id=108, Label = "ایذه", Value= "ایذه",ProvinceId = 4 ,IsActive = true,},
                        new City {Id=109, Label = "اندیمشک",Value= "اندیمشک", ProvinceId = 4 ,IsActive = true,},
                        new City {Id=110, Label = "دهدز",Value= "دهدز", ProvinceId = 4,IsActive = true, },
                        new City {Id=111, Label = "باغملک",Value= "باغملک", ProvinceId = 4 ,IsActive = true,},
                        new City {Id=112, Label = "هویزه",Value= "هویزه", ProvinceId = 4 ,IsActive = true,},
                        new City {Id=113, Label = "هندیجان", Value= "هندیجان",ProvinceId = 4,IsActive = true, },
                        new City {Id=114, Label = "بهبهان", Value= "بهبهان",ProvinceId = 4,IsActive = true, },
                        new City {Id=115, Label = "خرمشهر",Value= "خرمشهر", ProvinceId = 4 ,IsActive = true,},
                        new City {Id=116, Label = "دزفول", Value= "دزفول",ProvinceId = 4,IsActive = true, },
                        new City {Id=117, Label = "دشت آزادگان", Value= "دشت آزادگان",ProvinceId = 4,IsActive = true, },
                        new City {Id=118, Label = "رامشیر",Value= "رامشیر", ProvinceId = 4 ,IsActive = true,},
                        new City {Id=119, Label = "رامهرمز",Value= "رامهرمز", ProvinceId = 4 ,IsActive = true,},
                        new City {Id=120, Label = "شادگان", Value= "شادگان",ProvinceId = 4 ,IsActive = true,},
                        new City {Id=121, Label = "اغاجاری",Value= "اغاجاری", ProvinceId = 4,IsActive = true, },
                        new City {Id=122, Label = "بستان", Value= "بستان",ProvinceId = 4,IsActive = true, },
                        new City {Id=123, Label = "سوسنگرد",Value= "سوسنگرد", ProvinceId = 4 },
                        new City {Id=124, Label = "الوان",Value= "الوان", ProvinceId = 4 ,IsActive = true,},
                        new City {Id=125, Label = "شاوور", Value= "شاوور",ProvinceId = 4 ,IsActive = true,},
                        new City {Id=126, Label = "اندیکا(قلعه خواجو)", Value= "اندیکا(قلعه خواجو)",ProvinceId = 4,IsActive = true, },
                        new City {Id=127, Label = "باوی",Value= "باوی", ProvinceId = 4 ,IsActive = true,},
                        ///////////////////////////////////////////////////////////
                        new City {Id=128, Label = "شیراز",Value= "شیراز", ProvinceId = 5 },
                        new City {Id=129, Label = "مرودشت",Value= "مرودشت", ProvinceId = 5 },
                        new City {Id=130, Label = "دشمن زیاری", Value= "دشمن زیاری",ProvinceId = 5 },
                        new City {Id=131, Label = "فراشبند", Value= "فراشبند",ProvinceId = 5 },
                        new City {Id=132, Label = "قیروکارزین",Value= "قیروکارزین", ProvinceId = 5 },
                        new City {Id=133, Label = "فیروزآباد",Value= "فیروزآباد", ProvinceId = 5 },
                        new City {Id=134, Label = "بالاده",Value= "بالاده", ProvinceId = 5 },
                        new City {Id=135, Label = "درودزن",Value= "درودزن", ProvinceId = 5 },
                        new City {Id=136, Label = "شیب کوه",Value= "شیب کوه", ProvinceId = 5 },
                        new City {Id=137, Label = "کازرون",Value= "کازرون", ProvinceId = 5 },
                        new City {Id=138, Label = "فسا",Value= "فسا", ProvinceId = 5 },
                        new City {Id= 139,  Label = "سپیدان", Value= "سپیدان",ProvinceId = 5 },
                        new City {Id= 140, Label = "زرقان",Value= "زرقان", ProvinceId = 5 },
                        new City {Id= 141, Label = "آباده طشک",Value= "آباده طشک", ProvinceId = 5 },
                        new City {Id= 142, Label = "لامرد",Value= "لامرد", ProvinceId = 5 },
                        new City {Id= 143, Label = "لارستان",Value= "لارستان", ProvinceId = 5 },
                        new City {Id= 144, Label = "مهر",Value= "مهر", ProvinceId = 5 },
                        new City {Id= 145, Label = "داراب", Value= "داراب",ProvinceId = 5 },
                        new City {Id= 146, Label = "زرین دشت",Value= "زرین دشت", ProvinceId = 5 },
                        new City {Id= 147, Label = "قائمیه",Value= "قائمیه", ProvinceId = 5 },
                        new City {Id= 148, Label = "جهرم",Value= "جهرم", ProvinceId = 5 },
                        new City {Id= 149, Label = "اقلید", Value= "اقلید",ProvinceId = 5 },
                        new City {Id= 150, Label = "استهبان",Value= "استهبان", ProvinceId = 5 },
                        new City {Id= 151, Label = "ارسنجان",Value= "ارسنجان", ProvinceId = 5 },
                        new City {Id= 152, Label = "ممسنی (نورآباد)",Value= "ممسنی (نورآباد)", ProvinceId = 5 },
                        new City {Id= 153, Label = "نی ریز",Value= "نی ریز", ProvinceId = 5 },
                        new City {Id= 154, Label = "سرچهان",Value= "سرچهان", ProvinceId = 5 },
                        new City {Id= 155, Label = "پاسارگاد",Value= "پاسارگاد",ProvinceId = 5 },
                        new City {Id= 156, Label = "قادرآباد",Value= "قادرآباد", ProvinceId = 5 },
                        new City {Id= 157, Label = "سمیکان",Value= "سمیکان", ProvinceId = 5 },
                        new City {Id= 158, Label = "ایزدخواست",Value= "ایزدخواست", ProvinceId = 5 },
                        new City {Id= 159, Label = "قطب آباد",Value= "قطب آباد", ProvinceId = 5 },
                        new City {Id= 160, Label = "خرم بید", Value= "خرم بید",ProvinceId = 5 },
                        new City {Id= 161, Label = "آباده", Value= "آباده",ProvinceId = 5 },
                        new City {Id= 162, Label = "کامفیروز", Value= "کامفیروز",ProvinceId = 5 },
                        new City {Id= 163, Label = "بیضا",Value= "بیضا", ProvinceId = 5 },
                        new City {Id = 164, Label = "اشکنان",Value= "اشکنان", ProvinceId = 5 },
                        new City {Id = 165, Label = "مهر فارس",Value= "مهر فارس", ProvinceId = 5 },
                        new City {Id = 166, Label = "سعادت شهر",Value= "سعادت شهر", ProvinceId = 5 },
                        new City {Id = 167, Label = "خاوران",Value= "خاوران", ProvinceId = 5 },
                        new City {Id = 168, Label = "صفا شهر",Value= "صفا شهر", ProvinceId = 5 },
                        new City {Id = 169, Label = "خنج",Value= "خنج", ProvinceId = 5 },
                        new City {Id = 170, Label = "رستم",Value= "رستم", ProvinceId = 5 },
                        new City {Id = 171, Label = "زاهد شهر",Value= "زاهد شهر", ProvinceId = 5 },
                        new City {Id = 172, Label = "نودان",Value= "نودان", ProvinceId = 5 },
                        new City {Id = 173, Label = "سروستان", Value= "سروستان",ProvinceId = 5 },
                        new City {Id = 174, Label = "خرامه", Value= "خرامه",ProvinceId = 5 },
                        new City {Id = 175, Label = "کوار", Value= "کوار",ProvinceId = 5 },
                        new City {Id = 176, Label = "لار", Value= "لار",ProvinceId = 5 },
                        new City {Id = 177, Label = "بناب جدید",Value= "بناب جدید", ProvinceId = 5 },
                        new City {Id = 178, Label = "گراش",Value= "گراش", ProvinceId = 5 },
                        ///////////////////////////////////////////////////////////
                        new City {Id=179, Label = "اصفهان",Value= "اصفهان", ProvinceId = 6 },
                        new City {Id=180, Label = "کاشان",Value= "کاشان", ProvinceId = 6 },
                        new City {Id=181,  Label = "شاهین شهر (میمه)", Value= "شاهین شهر (میمه)",ProvinceId = 6 },
                        new City {Id=182, Label = "شهررضا", Value= "شهررضا",ProvinceId = 6 },
                        new City {Id=183, Label = "گلپایگان", Value= "گلپایگان",ProvinceId = 6 },
                        new City {Id=184,  Label = "مبارکه", Value= "مبارکه",ProvinceId = 6 },
                        new City {Id=185,  Label = "نایین",Value= "نایین", ProvinceId = 6 },
                        new City {Id=186,  Label = "نجف آباد",Value= "نجف آباد", ProvinceId = 6 },
                        new City {Id=187,  Label = "نطنز", Value= "نطنز",ProvinceId = 6 },
                        new City {Id=188,  Label = "بهارستان", Value= "بهارستان",ProvinceId = 6 },
                        new City {Id=189,  Label = "بن رود", Value= "بن رود",ProvinceId = 6 },
                        new City {Id = 190, Label = "نوش اباد", Value= "نوش اباد",ProvinceId = 6 },
                        new City {Id = 191, Label = "باغ شاد", Value= "باغ شاد",ProvinceId = 6 },
                        new City {Id = 192, Label = "زواره",Value= "زواره", ProvinceId = 6 },
                        new City {Id = 193, Label = "ورزنه",Value= "ورزنه", ProvinceId = 6 },
                        new City {Id = 194, Label = "حسن آباد جرقویه",Value= "حسن آباد جرقویه", ProvinceId = 6 },
                        new City {Id = 195, Label = "کوهپایه", Value= "کوهپایه",ProvinceId = 6 },
                        new City {Id = 196, Label = "هرند",Value= "هرند", ProvinceId = 6 },
                        new City {Id = 197, Label = "دولت آباد",Value= "دولت آباد", ProvinceId = 6 },
                        new City {Id = 198, Label = "خوراسگان", Value= "خوراسگان",ProvinceId = 6 },
                        new City {Id = 199, Label = "زاینده رود",Value= "زاینده رود", ProvinceId = 6 },
                        new City {Id = 200, Label = "دهاقان",Value= "دهاقان", ProvinceId = 6 },
                        new City {Id = 201, Label = "گز", Value= "گز",ProvinceId = 6 },
                        new City {Id = 202, Label = "برزک",Value= "برزک", ProvinceId = 6 },
                        new City {Id = 203,  Label = "فولاد شهر", Value= "فولاد شهر",ProvinceId = 6 },
                        new City {Id = 204, Label = "پیربکران",Value= "پیربکران", ProvinceId = 6 },
                        new City {Id = 205, Label = "وزوان", Value= "وزوان",ProvinceId = 6 },
                        new City {Id = 206, Label = "بوئین و میاندشت", Value= "بوئین و میاندشت",ProvinceId = 6 },
                        new City {Id = 207, Label = "زرین شهر",Value= "زرین شهر", ProvinceId = 6 },
                        new City {Id = 208, Label = "میمه", Value= "میمه",ProvinceId = 6 },
                        new City {Id = 209, Label = "باغ بهادران",Value= "باغ بهادران", ProvinceId = 6 },
                        new City {Id = 210, Label = "خمینی شهر",Value= "خمینی شهر", ProvinceId = 6 },
                        new City {Id = 211, Label = "اردستان",Value= "اردستان", ProvinceId = 6 },
                        new City {Id = 212, Label = "خوانسار", Value= "خوانسار",ProvinceId = 6 },
                        new City {Id = 213,  Label = "سمیرم", Value= "سمیرم",ProvinceId = 6 },
                        new City {Id = 214, Label = "چادگان", Value= "چادگان",ProvinceId = 6 },
                        new City {Id = 215, Label = "فریدن", Value= "فریدن",ProvinceId = 6 },
                        new City {Id = 216, Label = "فریدون شهر",Value= "فریدون شهر", ProvinceId = 6 },
                        new City {Id = 217, Label = "داران",Value= "داران", ProvinceId = 6 },
                        new City {Id = 218, Label = "فلاورجان", Value= "فلاورجان",ProvinceId = 6 },
                        new City {Id = 219, Label = "اران و بیدگل",Value= "اران و بیدگل", ProvinceId = 6 },
                        new City {Id = 220, Label = "لنجان", Value= "لنجان",ProvinceId = 6 },
                        new City {Id = 221, Label = "خور و بیابانک",Value= "خور و بیابانک", ProvinceId = 6 },
                        new City {Id = 222, Label = "تیران و کرون",Value= "تیران و کرون",ProvinceId = 6 },
                        new City {Id = 223, Label = "مهردشت (علویجه)",Value= "مهردشت (علویجه)", ProvinceId = 6 },
                        new City {Id = 224, Label = "قمصر",Value= "قمصر", ProvinceId = 6 },
                        //////////////////////////////////////////////////////////
                        new City {Id = 225,  Label = "مشهد", Value = "مشهد", ProvinceId = 7 },
                        new City {Id = 226,  Label = "سبزوار", Value = "سبزوار", ProvinceId = 7 },
                        new City {Id = 227, Label = "نیشابور", Value = "نیشابور", ProvinceId = 7 },
                        new City {Id = 228, Label = "c",  Value = "عشق آباد",ProvinceId = 7 },
                        new City {Id = 229, Label = "عشق آباد", Value = "عشق آباد", ProvinceId = 7 },
                        new City {Id = 230, Label = "فیروزه",  Value = "فیروزه",ProvinceId = 7 },
                        new City {Id = 231, Label = "جوین - نقاب", Value = "جوین - نقاب", ProvinceId = 7 },
                        new City {Id = 232, Label = "کاخک",  Value = "کاخک",ProvinceId = 7 },
                        new City {Id = 233, Label = "تخت جلگه", Value = "تخت جلگه", ProvinceId = 7 },
                        new City {Id = 234, Label = "خواف", Value = "خواف", ProvinceId = 7 },
                        new City {Id = 235, Label = "جغتای",  Value = "جغتای",ProvinceId = 7 },
                        new City {Id = 236, Label = "تایباد", Value = "تایباد", ProvinceId = 7 },
                        new City {Id = 237, Label = "تربت جام", Value = "تربت جام", ProvinceId = 7 },
                        new City {Id = 238, Label = "مه ولات - فیض آباد", Value = "مه ولات - فیض آباد", ProvinceId = 7 },
                        new City {Id = 239, Label = "تربت حدریه",  Value = "تربت حدریه",ProvinceId = 7 },
                        new City {Id = 240, Label = "بینالود",  Value = "بینالود",ProvinceId = 7 },
                        new City {Id = 241, Label = "چناران", Value = "چناران", ProvinceId = 7 },
                        new City {Id = 242, Label = "فریمان",  Value = "فریمان",ProvinceId = 7 },
                        new City {Id = 243,  Label = "کلات", Value = "کلات", ProvinceId = 7 },
                        new City {Id = 244, Label = "بردسکن", Value = "بردسکن", ProvinceId = 7 },
                        new City {Id = 245, Label = "خلیل آباد",  Value = "خلیل آباد",ProvinceId = 7 },
                        new City {Id= 246, Label = "کاشمر", Value = "کاشمر", ProvinceId = 7 },
                        new City {Id= 247, Label = "قوچان", Value = "قوچان", ProvinceId = 7 },
                        new City {Id= 248, Label = "بجستان", Value = "بجستان", ProvinceId = 7 },
                        new City {Id= 249, Label = "گناباد",  Value = "گناباد",ProvinceId = 7 },
                        new City {Id= 250, Label = "سرخس", Value = "سرخس", ProvinceId = 7 },
                        new City {Id = 251, Label = "درگز",  Value = "درگز",ProvinceId = 7 },
                        new City {Id = 252, Label = "درود", Value = "درود", ProvinceId = 7 },
                        new City {Id = 253, Label = "سرولایت", Value = "سرولایت", ProvinceId = 7 },
                        new City {Id = 254, Label = "دوغارون", Value = "دوغارون", ProvinceId = 7 },
                        new City {Id = 255, Label = "داورزن", Value = "داورزن", ProvinceId = 7 },
                        new City {Id = 256, Label = "گلبهار",Value = "گلبهار",  ProvinceId = 7 },
                        new City {Id = 257, Label = "دولت آباد زاوه", Value = "دولت آباد زاوه", ProvinceId = 7 },
                        new City {Id = 258, Label = "طرقبه",Value = "طرقبه",  ProvinceId = 7 },
                        new City {Id = 259, Label = "میان جلگه", Value = "میان جلگه", ProvinceId = 7 },
                        new City {Id = 260, Label = "رشتخوار", Value = "رشتخوار", ProvinceId = 7 },
                        ///////////////////////////////////////////////////////////
                        new City {Id = 261, Label = "قزوین", Value = "قزوین", ProvinceId = 8 },
                        new City {Id = 262, Label = "اوج", Value = "اوج", ProvinceId = 8 },
                        new City {Id = 263,  Label = "الوند",Value = "الوند",  ProvinceId = 8 },
                        new City {Id = 264, Label = "دانسفهان",Value = "دانسفهان",  ProvinceId = 8 },
                        new City {Id = 265, Label = "شال", Value = "شال", ProvinceId = 8 },
                        new City {Id = 266, Label = "تاکستان", Value = "تاکستان", ProvinceId = 8 },
                        new City {Id = 267, Label = "ابیک", Value = "ابیک", ProvinceId = 8 },
                        new City {Id = 268, Label = "ضیا آباد",Value = "ضیا آباد",  ProvinceId = 8 },
                        new City {Id = 269, Label = "بوئین زهرا", Value = "بوئین زهرا", ProvinceId = 8 },
                        new City {Id = 270, Label = "طارم سلفی", Value = "طارم سلفی", ProvinceId = 8 },
                        new City {Id = 271, Label = "شهر صنعتی البرز", Value = "شهر صنعتی البرز", ProvinceId = 8 },
                        ///////////////////////////////////////////////////////////
                        new City {Id = 272, Label = "سمنان",Value = "سمنان", ProvinceId = 9 },
                        new City {Id = 273, Label = "شاهرود",Value = "شاهرود", ProvinceId = 9 },
                        new City {Id = 274, Label = "دامغان",Value = "دامغان", ProvinceId = 9 },
                        new City {Id = 275, Label = "بسطام",Value = "بسطام", ProvinceId = 9 },
                        new City {Id = 276, Label = "شهمیرزاد", Value = "شهمیرزاد",ProvinceId = 9 },
                        new City {Id = 277, Label = "گرمسار", Value = "گرمسار",ProvinceId = 9 },
                        new City {Id = 278, Label = "سرخه", Value = "سرخه",ProvinceId = 9 },
                        new City {Id = 279, Label = "ارادان", Value = "ارادان",ProvinceId = 9 },
                        new City {Id = 280, Label = "میامی",Value = "میامی", ProvinceId = 9 },
                        new City {Id = 281, Label = "مهدی شهر",Value = "مهدی شهر", ProvinceId = 9 },
                        new City {Id = 282, Label = "ایوانکی",Value = "ایوانکی", ProvinceId = 9 },
                        new City {Id = 283, Label = "مجن",Value = "مجن", ProvinceId = 9 },
                        ///////////////////////////////////////////////////////////
                        new City {Id = 284, Label = "قم", Value = "قم",ProvinceId = 10 },
                        new City {Id = 285, Label = "سلفچگان", Value = "سلفچگان",ProvinceId = 10 },
                        new City {Id = 286, Label = "جعفریه",Value = "جعفریه", ProvinceId = 10 },
                        new City {Id = 287, Label = "دستجرد خلجستان",Value = "دستجرد خلجستان", ProvinceId = 10 },
                        new City {Id = 288, Label = "کهک", Value = "کهک",ProvinceId = 10 },
                        //////////////////////////////////////////////////////////
                        new City {Id = 289, Label = "اراک",Value = "اراک", ProvinceId = 11 },
                        new City {Id = 290, Label = "سربند",Value = "سربند", ProvinceId = 11 },
                        new City {Id = 291, Label = "شهرک صنعتی نوبران",Value = "شهرک صنعتی نوبران", ProvinceId = 11 },
                        new City {Id = 292, Label = "خنداب",Value = "خنداب", ProvinceId = 11 },
                        new City {Id = 293, Label = "قورچی باشی", Value = "قورچی باشی",ProvinceId = 11 },
                        new City {Id = 294, Label = "غرق آباد",Value = "غرق آباد", ProvinceId = 11 },
                        new City {Id = 295, Label = "کمیجان",Value = "کمیجان", ProvinceId = 11 },
                        new City {Id = 296, Label = "اشتیان",Value = "اشتیان", ProvinceId = 11 },
                        new City {Id = 297, Label = "تفرش",Value = "تفرش", ProvinceId = 11 },
                        new City {Id = 298, Label = "خمین",Value = "خمین", ProvinceId = 11 },
                        new City {Id = 299, Label = "دلیجان",Value = "دلیجان", ProvinceId = 11 },
                        new City {Id = 300,  Label = "زرندیه",Value = "زرندیه", ProvinceId = 11 },
                        new City {Id = 301, Label = "ساوه",Value = "ساوه", ProvinceId = 11 },
                        new City {Id = 302, Label = "شازند",Value = "شازند", ProvinceId = 11 },
                        new City {Id = 303, Label = "مهاجران",Value = "مهاجران", ProvinceId = 11 },
                        new City {Id = 304, Label = "محلات",Value = "محلات", ProvinceId = 11 },
                        ///////////////////////////////////////////////////////////
                        new City {Id = 305, Label = "زنجان", Value = "زنجان",ProvinceId = 12 },
                        new City {Id = 306,  Label = "ابهر", Value = "ابهر",ProvinceId = 12 },
                        new City {Id = 307, Label = "صائین قلعه",Value = "صائین قلعه", ProvinceId = 12 },
                        new City {Id = 308, Label = "قیدار", Value = "قیدار",ProvinceId = 12 },
                        new City {Id = 309, Label = "طارم",Value = "طارم", ProvinceId = 12 },
                        new City {Id = 310, Label = "سلطانیه",Value = "سلطانیه", ProvinceId = 12 },
                        new City {Id = 311,  Label = "ماه نشان", Value = "ماه نشان",ProvinceId = 12 },
                        new City {Id = 312, Label = "ایجرود (زرین آباد)",Value = "ایجرود (زرین آباد)", ProvinceId = 12 },
                        new City {Id = 313, Label = "خدابنده", Value = "خدابنده",ProvinceId = 12 },
                        new City {Id = 314, Label = "خرمدره", Value = "خرمدره",ProvinceId = 12 },
                        new City {Id = 315,  Label = "بزینه رود",Value = "بزینه رود", ProvinceId = 12 },
                        ///////////////////////////////////////////////////////////
                        new City {Id = 316, Label = "ساری", Value = "ساری",ProvinceId = 13 },
                        new City {Id = 317, Label = "آمل", Value = "آمل", ProvinceId = 13 },
                        new City {Id = 318, Label = "بابل", Value = "بابل",ProvinceId = 13 },
                        new City {Id = 319, Label = "بابلسر",Value = "بابلسر", ProvinceId = 13 },
                        new City {Id = 320, Label = "رامسر", Value = "رامسر",ProvinceId = 13 },
                        new City {Id = 321, Label = "چالوس", Value = "چالوس",ProvinceId = 13 },
                        new City {Id = 322, Label = "نوشهر",Value = "نوشهر", ProvinceId = 13 },
                        new City {Id = 323,  Label = "قائم‌شهر",Value = "قائم‌شهر", ProvinceId = 13 },
                        new City {Id = 324,  Label = "محمودآباد",Value = "محمودآباد", ProvinceId = 13 },
                        new City {Id = 325, Label = "بندپی شرقی",Value = "بندپی شرقی", ProvinceId = 13 },
                        new City {Id = 326,  Label = "کلاردشت",Value = "کلاردشت", ProvinceId = 13 },
                        new City {Id = 327,  Label = "هراز",Value = "هراز", ProvinceId = 13 },
                        new City {Id = 328,  Label = "بهشهر", Value = "بهشهر",ProvinceId = 13 },
                        new City {Id = 329, Label = "نکا", Value = "نکا",ProvinceId = 13 },
                        new City {Id = 330,  Label = "تنکابن", Value = "تنکابن",ProvinceId = 13 },
                        new City {Id = 331, Label = "گلوگاه",Value = "گلوگاه", ProvinceId = 13 },
                        new City {Id = 332,  Label = "سوادکوه",Value = "سوادکوه", ProvinceId = 13 },
                        new City {Id = 333,  Label = "نور", Value = "نور",ProvinceId = 13 },
                        new City {Id = 334,Label = "بهنمیر",Value = "بهنمیر", ProvinceId = 13 },
                        new City {Id = 335,  Label = "رینه",Value = "رینه", ProvinceId = 13 },
                        new City {Id = 336,  Label = "بندپی غربی",Value = "بندپی غربی", ProvinceId = 13 },
                        new City {Id = 337, Label = "عباس‌آباد", Value = "عباس‌آباد",ProvinceId = 13 },
                        new City {Id = 338, Label = "فریدون‌کنار", Value = "فریدون‌کنار",ProvinceId = 13 },
                        new City {Id = 339,  Label = "کله‌بست", Value = "کله‌بست",ProvinceId = 13 },
                        new City {Id = 340, Label = "پل‌سفید",Value = "پل‌سفید", ProvinceId = 13 },
                        new City {Id = 341, Label = "زیرآب",Value = "زیرآب", ProvinceId = 13 },
                        new City {Id = 342, Label = "چمستان",Value = "چمستان", ProvinceId = 13 },
                        new City {Id = 343, Label = "امیرکلا", Value = "امیرکلا",ProvinceId = 13 },
                        new City {Id = 344, Label = "میاندورود",Value = "میاندورود", ProvinceId = 13 },
                        new City {Id = 345, Label = "جویبار", Value = "جویبار",ProvinceId = 13 },
                        ///////////////////////////////////////////////////////////
                        new City {Id = 346,  Label = "گرگان",Value = "گرگان", ProvinceId = 14 },
                        new City {Id = 347,  Label = "بندرترکمن", Value = "بندرترکمن",ProvinceId = 14 },
                        new City {Id = 348,  Label = "گنبدکاووس", Value = "گنبدکاووس",ProvinceId = 14 },
                        new City {Id = 349, Label = "گمیشان",Value = "گمیشان", ProvinceId = 14 },
                        new City {Id = 350,  Label = "مراوه‌تپه", Value = "مراوه‌تپه",ProvinceId = 14 },
                        new City {Id =351,  Label = "بندرگز",Value = "بندرگز", ProvinceId = 14 },
                        new City {Id =352,   Label = "کردکوی", Value = "کردکوی",ProvinceId = 14 },
                        new City {Id =353,   Label = "آق‌قلا",Value = "آق‌قلا", ProvinceId = 14 },
                        new City {Id =354,   Label = "گالیکش",Value = "گالیکش", ProvinceId = 14 },
                        new City {Id =355,   Label = "آزادشهر", Value = "آزادشهر",ProvinceId = 14 },
                        new City {Id =356,   Label = "رامیان",Value = "رامیان", ProvinceId = 14 },
                        new City {Id =357,   Label = "مینودشت", Value = "مینودشت",ProvinceId = 14 },
                        new City {Id =358,  Label = "علی‌آباد",Value = "علی‌آباد", ProvinceId = 14 },
                        new City {Id =359, Label = "کلاله",Value = "کلاله", ProvinceId = 14 },
                        ///////////////////////////////////////////////////////////
                        new City {Id = 360,  Label = "اردبیل", Value = "اردبیل",ProvinceId = 15 },
                        new City {Id = 361,  Label = "سرعین", Value = "سرعین",ProvinceId = 15 },
                        new City {Id = 362,  Label = "مشکین‌شهر", Value = "مشکین‌شهر",ProvinceId = 15 },
                        new City {Id = 363, Label = "بیله‌سوار", Value = "بیله‌سوار",ProvinceId = 15 },
                        new City {Id = 364,  Label = "پارس‌آباد",Value = "پارس‌آباد", ProvinceId = 15 },
                        new City {Id = 365,  Label = "گرمی", Value = "گرمی",ProvinceId = 15 },
                        new City {Id = 366, Label = "تازه‌انگوت", Value = "تازه‌انگوت",ProvinceId = 15 },
                        new City {Id = 367,  Label = "ارشق",Value = "ارشق", ProvinceId = 15 },
                        new City {Id = 368,  Label = "کوثر", Value = "کوثر",ProvinceId = 15 },
                        new City {Id = 369,  Label = "هیر", Value = "هیر",ProvinceId = 15 },
                        new City {Id = 370,  Label = "خلخال",Value = "خلخال", ProvinceId = 15 },
                        new City {Id = 371,  Label = "نمین",Value = "نمین",ProvinceId = 15 },
                        new City {Id = 372,  Label = "مغان", Value = "مغان",ProvinceId = 15 },
                        new City {Id = 373,  Label = "اصلاندوز",Value = "اصلاندوز", ProvinceId = 15 },
                        ///////////////////////////////////////////////////////////
                        new City {Id = 374,  Label = "ارومیه",  Value = "ارومیه",ProvinceId = 16 },
                        new City {Id = 375, Label = "سیلوانه",  Value = "سیلوانه",ProvinceId = 16 },
                        new City {Id = 376,  Label = "صومای برادوست",  Value = "صومای برادوست",ProvinceId = 16 },
                        new City {Id = 377,  Label = "نازلو", Value = "نازلو", ProvinceId = 16 },
                        new City {Id = 378,  Label = "گوگ‌تپه",  Value = "گوگ‌تپه",ProvinceId = 16 },
                        new City {Id = 379, Label = "ابواوغلی",  Value = "ابواوغلی",ProvinceId = 16 },
                        new City {Id = 380, Label = "چهاربرج",  Value = "چهاربرج",ProvinceId = 16 },
                        new City {Id = 381,  Label = "سیه‌چشمه", Value = "سیه‌چشمه", ProvinceId = 16 },
                        new City {Id = 382,  Label = "تازه‌شهر", Value = "تازه‌شهر", ProvinceId = 16 },
                        new City {Id = 383, Label = "شوط",  Value = "شوط",ProvinceId = 16 },
                        new City {Id = 384,  Label = "چایپاره",  Value = "چایپاره",ProvinceId = 16 },
                        new City {Id = 385,  Label = "پیرانشهر",  Value = "پیرانشهر",ProvinceId = 16 },
                        new City {Id = 386,  Label = "سلماس", Value = "سلماس", ProvinceId = 16 },
                        new City {Id = 387,  Label = "پلدشت", Value = "پلدشت", ProvinceId = 16 },
                        new City {Id = 388,  Label = "چالدران", Value = "چالدران", ProvinceId = 16 },
                        new City {Id = 389, Label = "ماکو", Value = "ماکو", ProvinceId = 16 },
                        new City {Id = 390, Label = "قره‌ضیاءالدین", Value = "قره‌ضیاءالدین", ProvinceId = 16 },
                        new City {Id = 391,  Label = "بوکان", Value = "بوکان", ProvinceId = 16 },
                        new City {Id = 392, Label = "خوی",  Value = "خوی",ProvinceId = 16 },
                        new City {Id = 393, Label = "سردشت", Value = "سردشت", ProvinceId = 16 },
                        new City {Id = 394, Label = "مهاباد",  Value = "مهاباد",ProvinceId = 16 },
                        new City {Id = 395, Label = "تکاب",  Value = "تکاب",ProvinceId = 16 },
                        new City {Id = 396, Label = "شاهین‌دژ", Value = "شاهین‌دژ", ProvinceId = 16 },
                        new City {Id = 397,  Label = "میاندوآب", Value = "میاندوآب", ProvinceId = 16 },
                        new City {Id = 398,  Label = "اشنویه", Value = "اشنویه", ProvinceId = 16 },
                        new City {Id = 399, Label = "نقده",  Value = "نقده",ProvinceId = 16 },
                        ///////////////////////////////////////////////////////////
                        new City {Id = 400, Label = "همدان", Value = "همدان", ProvinceId = 17 },
                        new City {Id = 401, Label = "قروه",  Value = "قروه",ProvinceId = 17 },
                        new City {Id = 402,  Label = "سامن", Value = "سامن", ProvinceId = 17 },
                        new City {Id = 403, Label = "لالیجان", Value = "لالیجان", ProvinceId = 17 },
                        new City {Id = 404,  Label = "صالح‌آباد", Value = "صالح‌آباد", ProvinceId = 17 },
                        new City {Id = 405, Label = "تویسرکان", Value = "تویسرکان", ProvinceId = 17 },
                        new City {Id = 406, Label = "کبودرآهنگ", Value = "کبودرآهنگ", ProvinceId = 17 },
                        new City {Id = 407, Label = "ملایر",  Value = "ملایر",ProvinceId = 17 },
                        new City {Id = 408, Label = "نهاوند",  Value = "نهاوند",ProvinceId = 17 },
                        new City {Id = 409, Label = "اسدآباد", Value = "اسدآباد", ProvinceId = 17 },
                        new City {Id = 410, Label = "رزن",  Value = "رزن",ProvinceId = 17 },
                        new City {Id = 411, Label = "بهار", Value = "بهار", ProvinceId = 17 },
                        ///////////////////////////////////////////////////////////
                        new City {Id = 412, Label = "سنندج", Value = "سنندج", ProvinceId = 18 },
                        new City {Id = 413, Label = "سقز", Value = "سقز", ProvinceId = 18 },
                        new City {Id = 414, Label = "دیواندره",  Value = "دیواندره",ProvinceId = 18 },
                        new City {Id = 415, Label = "کامیاران", Value = "کامیاران", ProvinceId = 18 },
                        new City {Id = 416, Label = "قروه",  Value = "قروه",ProvinceId = 18 },
                        new City {Id = 417, Label = "دهگلان",  Value = "دهگلان",ProvinceId = 18 },
                        new City {Id = 418, Label = "سروآباد",  Value = "سروآباد",ProvinceId = 18 },
                        new City {Id = 419, Label = "مریوان", Value = "مریوان", ProvinceId = 18 },
                        new City {Id = 420, Label = "بانه",  Value = "بانه",ProvinceId = 18 },
                        new City {Id = 421, Label = "بیجار",  Value = "بیجار",ProvinceId = 18 },
                        ///////////////////////////////////////////////////////////
                        new City {Id = 422, Label = "کرمانشاه", Value = "کرمانشاه", ProvinceId = 19 },
                        new City {Id = 423, Label = "تازه‌آباد", Value = "تازه‌آباد", ProvinceId = 19 },
                        new City {Id = 424, Label = "گلانغرب",Value = "گلانغرب",  ProvinceId = 19 },
                        new City {Id = 425, Label = "کنگاور",Value = "کنگاور",  ProvinceId = 19 },
                        new City {Id = 426, Label = "قصرشیرین",Value = "قصرشیرین",  ProvinceId = 19 },
                        new City {Id = 427, Label = "سنقر", Value = "سنقر", ProvinceId = 19 },
                        new City {Id = 428, Label = "پاوه", Value = "پاوه", ProvinceId = 19 },
                        new City {Id = 429, Label = "جوانرود", Value = "جوانرود", ProvinceId = 19 },
                        new City {Id = 430, Label = "روانسر",Value = "روانسر",  ProvinceId = 19 },
                        new City {Id = 431, Label = "سرپل‌ذهاب",Value = "سرپل‌ذهاب",  ProvinceId = 19 },
                        new City {Id = 432, Label = "صحنه",Value = "صحنه",  ProvinceId = 19 },
                        new City {Id = 433, Label = "اسلام‌آبادغرب",Value = "اسلام‌آبادغرب",  ProvinceId = 19 },
                        new City {Id = 434, Label = "هرسین", Value = "هرسین", ProvinceId = 19 },
                        new City {Id = 435, Label = "ثلاث‌باباجانی",Value = "ثلاث‌باباجانی",  ProvinceId = 19 },
                        new City {Id = 436, Label = "دالاهو", Value = "دالاهو", ProvinceId = 19 },
                        ///////////////////////////////////////////////////////////
                        new City {Id = 437,  Label = "خرم‌آباد", Value = "آباد", ProvinceId = 20 },
                        new City {Id = 438, Label = "بروجرد",Value = "بروجرد",  ProvinceId = 20 },
                        new City {Id = 439, Label = "الشتر", Value = "الشتر", ProvinceId = 20 },
                        new City {Id = 450, Label = "دروه", Value = "دروه", ProvinceId = 20 },
                        new City {Id = 451, Label = "ازنا",Value = "ازنا",  ProvinceId = 20 },
                        new City {Id = 452, Label = "الیگودرز",Value = "الیگودرز",  ProvinceId = 20 },
                        new City {Id = 453, Label = "سلسله",Value = "سلسله",  ProvinceId = 20 },
                        new City {Id = 454, Label = "پلدختر", Value = "پلدختر", ProvinceId = 20 },
                        new City {Id = 455, Label = "دورود",Value = "دورود",  ProvinceId = 20 },
                        new City {Id = 456, Label = "دلفان (نورآباد)", Value = "دلفان (نورآباد)", ProvinceId = 20 },
                        new City {Id = 457, Label = "رومشکان",Value = "رومشکان",  ProvinceId = 20 },
                        new City {Id = 458, Label = "کوهدشت",Value = "کوهدشت",  ProvinceId = 20 },
                        ///////////////////////////////////////////////////////////
                        new City {Id = 459, Label = "بوشهر",Value = "بوشهر",  ProvinceId = 21 },
                        new City {Id = 460, Label = "جم",Value = "جم",  ProvinceId = 21 },
                        new City {Id = 461, Label = "سعدآباد",Value = "سعدآباد",  ProvinceId = 21 },
                        new City {Id = 462, Label = "دلوار", Value = "دلوار", ProvinceId = 21 },
                        new City {Id = 463, Label = "بندردیر",Value = "بندردیر",  ProvinceId = 21 },
                        new City {Id = 464, Label = "اهرم (تنگستان)", Value = "اهرم (تنگستان)", ProvinceId = 21 },
                        new City {Id = 465, Label = "عسلویه", Value = "عسلویه", ProvinceId = 21 },
                        new City {Id = 466, Label = "دشتی خورموج", Value = "دشتی خورموج", ProvinceId = 21 },
                        new City {Id = 467, Label = "دیر (بردخون)", Value = "دیر (بردخون)", ProvinceId = 21 },
                        new City {Id = 468,  Label = "دیلم", Value = "دیلم", ProvinceId = 21 },
                        new City {Id = 469, Label = "بندرگناوه", Value = "بندرگناوه", ProvinceId = 21 },
                        new City {Id = 470, Label = "شبانکاره",Value = "شبانکاره",  ProvinceId = 21 },
                        new City {Id = 471, Label = "دشتستان (برازجان)",Value = "دشتستان (برازجان)",  ProvinceId = 21 },
                        new City {Id = 472, Label = "کنگان",Value = "کنگان",  ProvinceId = 21 },
                        ///////////////////////////////////////////
                        new City {Id = 473, Label = "کرمان",Value = "کرمان", ProvinceId = 22 },
                        new City {Id = 474, Label = "رودبارجنوب",Value = "رودبارجنوب", ProvinceId = 22 },
                        new City {Id = 475, Label = "بافت", Value = "بافت",ProvinceId = 22 },
                        new City {Id = 476, Label = "بردسیر",Value = "بردسیر", ProvinceId = 22 },
                        new City {Id = 477, Label = "بم",Value = "بم", ProvinceId = 22 },
                        new City {Id = 478, Label = "عنبرآباد", Value = "عنبرآباد",ProvinceId = 22 },
                        new City {Id = 479, Label = "جیرفت", Value = "جیرفت",ProvinceId = 22 },
                        new City {Id = 480, Label = "رفسنجان",Value = "رفسنجان", ProvinceId = 22 },
                        new City {Id = 481, Label = "کوهبنان",Value = "کوهبنان", ProvinceId = 22 },
                        new City {Id = 482, Label = "زرند",Value = "زرند", ProvinceId = 22 },
                        new City {Id = 483, Label = "سیرجان",Value = "سیرجان", ProvinceId = 22 },
                        new City {Id = 484, Label = "شهربابک",Value = "شهربابک",ProvinceId = 22 },
                        new City {Id = 485, Label = "قلعه‌گنج",Value = "قلعه‌گنج", ProvinceId = 22 },
                        new City {Id = 486, Label = "راور", Value = "راور",ProvinceId = 22 },
                        new City {Id = 487, Label = "کهنوج",Value = "کهنوج", ProvinceId = 22 },
                        new City {Id = 488, Label = "منوجان", Value = "منوجان",ProvinceId = 22 },
                        new City {Id = 489, Label = "ماهان",Value = "ماهان", ProvinceId = 22 },
                        new City {Id = 490, Label = "فاریاب",Value = "فاریاب", ProvinceId = 22 },
                        new City {Id = 491, Label = "پاریز",Value = "پاریز", ProvinceId = 22 },
                        new City {Id = 492, Label = "نگار",Value = "نگار", ProvinceId = 22 },
                        new City {Id = 493, Label = "رابر",Value = "رابر", ProvinceId = 22 },
                        new City {Id = 494, Label = "نرماشیر",Value = "نرماشیر", ProvinceId = 22 },
                        new City {Id = 495, Label = "فهرج", Value = "فهرج",ProvinceId = 22 },
                        new City {Id = 496, Label = "راین",Value = "راین", ProvinceId = 22 },
                        new City {Id = 497, Label = "شهداد",Value = "شهداد", ProvinceId = 22 },
                        new City {Id = 498, Label = "انار", Value = "انار",ProvinceId = 22 },
                        new City {Id = 499, Label = "کشکوییه", Value = "کشکوییه",ProvinceId = 22 },
                        new City {Id = 500, Label = "ریگان", Value = "ریگان",ProvinceId = 22 },
                        new City {Id = 501, Label = "زنگی‌آباد",Value = "زنگی‌آباد", ProvinceId = 22 },
                        new City {Id = 502, Label = "گلباف",Value = "گلباف", ProvinceId = 22 },
                        ///////////////////////////////////////////////////////////
                        new City {Id =503, Label = "بندرعباس", Value = "بندرعباس",ProvinceId = 23 },
                        new City {Id =504, Label = "کیش",Value = "کیش", ProvinceId = 23 },
                        new City {Id =505, Label = "ابوموسی", Value = "ابوموسی",ProvinceId = 23 },
                        new City {Id =506, Label = "قشم", Value = "قشم",ProvinceId = 23 },
                        new City {Id =507, Label = "حاجی آباد", Value = "حاجی آباد",ProvinceId = 23 },
                        new City {Id =508, Label = "بستک", Value = "بستک",ProvinceId = 23 },
                        new City {Id = 509, Label = "بندرلنگه",Value = "بندرلنگه", ProvinceId = 23 },
                        new City {Id = 510, Label = "جاسک",Value = "جاسک", ProvinceId = 23 },
                        new City {Id = 511, Label = "میناب", Value = "میناب",ProvinceId = 23 },
                        new City {Id = 512, Label = "جناح", Value = "جناح",ProvinceId = 23 },
                        new City {Id = 513, Label = "شهاب",Value = "شهاب", ProvinceId = 23 },
                        new City {Id = 514, Label = "پارسیان", Value = "پارسیان",ProvinceId = 23 },
                        new City {Id = 515, Label = "بیابان سیریک",Value = "بیابان سیریک", ProvinceId = 23 },
                        new City {Id = 516, Label = "بندرخمیر",Value = "بندرخمیر", ProvinceId = 23 },
                        new City {Id = 517, Label = "دهبارز (رودان)",Value = "دهبارز (رودان)", ProvinceId = 23 },
                        new City {Id = 518, Label = "فین", Value = "فین",ProvinceId = 23 },
                        new City {Id = 519, Label = "بشاگرد", Value = "بشاگرد",ProvinceId = 23 },
                        new City {Id = 520, Label = "سعادت آباد",Value = "سعادت آباد", ProvinceId = 23 },
                        ///////////////////////////////////////////////////////////
                        new City {Id = 521, Label = "شهرکرد",Value = "شهرکرد", ProvinceId = 24 },
                        new City {Id = 522, Label = "خانمیرزا",Value = "خانمیرزا", ProvinceId = 24 },
                        new City {Id = 523, Label = "فلارد", Value = "فلارد",ProvinceId = 24 },
                        new City {Id = 524, Label = "کوهرنگ", Value = "کوهرنگ",ProvinceId = 24 },
                        new City {Id = 525, Label = "فارسان",Value = "فارسان", ProvinceId = 24 },
                        new City {Id = 526, Label = "لردگان",Value = "لردگان", ProvinceId = 24 },
                        new City {Id = 527, Label = "اردل", Value = "اردل",ProvinceId = 24 },
                        new City {Id = 528, Label = "بروجن", Value = "بروجن",ProvinceId = 24 },
                        new City {Id = 529, Label = "کیار", Value = "کیار",ProvinceId = 24 },
                        new City {Id = 530, Label = "شلمزار", Value = "شلمزار",ProvinceId = 24 },
                        new City {Id = 531, Label = "سامان", Value = "سامان",ProvinceId = 24 },
                        new City {Id = 532, Label = "فرخ بخش", Value = "فرخ بخش",ProvinceId = 24 },
                        new City {Id = 533, Label = "بلداجی", Value = "بلداجی",ProvinceId = 24 },
                        new City {Id = 534,  Label = "گندمان", Value = "گندمان",ProvinceId = 24 },
                        ///////////////////////////////////////////////////////////
                        new City {Id = 535, Label = "یزد", Value = "یزد",ProvinceId = 25 },
                        new City {Id = 536, Label = "اردکان",Value = "اردکان", ProvinceId = 25 },
                        new City {Id = 537, Label = "میبد", Value = "میبد",ProvinceId = 25 },
                        new City {Id = 538, Label = "بافق",Value = "بافق", ProvinceId = 25 },
                        new City {Id = 539, Label = "لبرکوه",Value = "لبرکوه", ProvinceId = 25 },
                        new City {Id = 540, Label = "تفت", Value = "تفت",ProvinceId = 25 },
                        new City {Id = 541, Label = "مهرریز", Value = "مهرریز",ProvinceId = 25 },
                        new City {Id = 542, Label = "خاتم", Value = "خاتم",ProvinceId = 25 },
                        new City {Id = 543, Label = "اشکذر", Value = "اشکذر",ProvinceId = 25 },
                        new City {Id = 544, Label = "مروست", Value = "مروست",ProvinceId = 25 },
                        new City {Id = 545, Label = "بهاباد",Value = "بهاباد", ProvinceId = 25 },
                        new City {Id = 546, Label = "هرات",Value = "هرات", ProvinceId = 25 },
                        ///////////////////////////////////////////////////////////
                        new City {Id = 547, Label = "زاهدان", Value = "زاهدان",ProvinceId = 26 },
                        new City {Id = 548,  Label = "زابل",Value = "زابل", ProvinceId = 26 },
                        new City {Id = 549, Label = "هیرمند", Value = "هیرمند",ProvinceId = 26 },
                        new City {Id = 550, Label = "راسک", Value = "راسک",ProvinceId = 26 },
                        new City {Id = 551, Label = "سیب وسوران",Value = "سیب وسوران", ProvinceId = 26 },
                        new City {Id = 552, Label = "زهک", Value = "زهک",ProvinceId = 26 },
                        new City {Id = 553, Label = "سرباز", Value = "سرباز",ProvinceId = 26 },
                        new City {Id = 554, Label = "ایرانشهر",Value = "ایرانشهر", ProvinceId = 26 },
                        new City {Id = 555, Label = "چابهار",Value = "چابهار", ProvinceId = 26 },
                        new City {Id = 556, Label = "نیک شهر",Value = "نیک شهر", ProvinceId = 26 },
                        new City {Id = 557, Label = "خاش", Value = "خاش",ProvinceId = 26 },
                        new City {Id = 558, Label = "شهرکی و نارویی", Value = "شهرکی و نارویی",ProvinceId = 26 },
                        new City {Id = 559, Label = "سراوان",Value = "سراوان", ProvinceId = 26 },
                        new City {Id = 560, Label = "دشتیاری",Value = "دشتیاری", ProvinceId = 26 },
                        new City {Id = 561, Label = "بمپور", Value = "بمپور",ProvinceId = 26 },
                        new City {Id = 562, Label = "دلگان",Value = "دلگان", ProvinceId = 26 },
                        new City {Id = 563, Label = "زابلی", Value = "زابلی",ProvinceId = 26 },
                        new City {Id = 564, Label = "پشت آب",Value = "پشت آب", ProvinceId = 26 },
                        new City {Id = 565, Label = "شیب آب",Value = "شیب آب", ProvinceId = 26 },
                        new City {Id = 566, Label = "قصرقند", Value = "قصرقند",ProvinceId = 26 },
                        new City {Id = 567, Label = "لاشار",Value = "لاشار", ProvinceId = 26 },
                        new City {Id = 568, Label = "کنارک", Value = "کنارک", ProvinceId = 26 },
                        ///////////////////////////////////////////////////////////
                        new City {Id = 569, Label = "ایلام", Value = "ایلام", ProvinceId = 27 },
                        new City {Id = 570, Label = "ایوان", Value = "ایوان", ProvinceId = 27 },
                        new City {Id = 571, Label = "آبدانان", Value = "آبدانان", ProvinceId = 27 },
                        new City {Id = 572, Label = "دره شهر", Value = "دره شهر", ProvinceId = 27 },
                        new City {Id = 573, Label = "شیروان و چرداول",Value= "شیروان و چرداول", ProvinceId = 27 },
                        new City {Id = 574, Label = "دهلران",Value= "دهلران", ProvinceId = 27 },
                        new City {Id = 575, Label = "مهران",Value= "مهران", ProvinceId = 27 },
                        new City {Id = 576, Label = "بدره",Value= "بدره", ProvinceId = 27 },
                        new City {Id = 577, Label = "سیروان",Value= "سیروان", ProvinceId = 27 },
                        new City {Id = 578, Label = "موسیان",Value= "موسیان", ProvinceId = 27 },
                        new City {Id = 579, Label = "سرآبله",Value= "سرآبله", ProvinceId = 27 },
                        new City {Id = 580, Label = "زرین آباد",Value= "زرین آباد", ProvinceId = 27 },
                        new City {Id = 581, Label = "هلیلان",Value= "هلیلان", ProvinceId = 27 },
                        new City {Id = 582, Label = "ملکشاهی",Value= "ملکشاهی", ProvinceId = 27 },
                        ///////////////////////////////////////////////////////////
                        new City {Id = 583, Label = "یاسوج",Value= "یاسوج",ProvinceId = 28 },
                        new City {Id = 584, Label = "گچساران",Value= "گچساران", ProvinceId = 28 },
                        new City {Id = 585, Label = "دنا", Value= "دنا", ProvinceId = 28 },
                        new City {Id = 586, Label = "بهمئی", Value= "بهمئی",ProvinceId = 28 },
                        new City {Id = 587, Label = "دهدشت", Value= "دهدشت",ProvinceId = 28 },
                        new City {Id = 588, Label = "سی سخت",Value= "سخت", ProvinceId = 28 },
                        new City {Id = 589, Label = "دوگنبدان",Value= "دوگنبدان", ProvinceId = 28 },
                        new City {Id = 590, Label = "چرام",Value= "چرام", ProvinceId = 28 },
                        new City {Id = 591, Label = "لیکک", Value= "لیکک",ProvinceId = 28 },
                        new City {Id = 592, Label = "باشت", Value= "باشت",ProvinceId = 28 },
                        new City {Id = 593, Label = "مارگون",Value= "مارگون", ProvinceId = 28 },
                        ///////////////////////////////////////////////////////////
                        new City {Id = 594,  Label = "بجنورد",Value= "بجنورد", ProvinceId = 29 },
                        new City {Id = 595,  Label = "اشخانه",Value= "اشخانه", ProvinceId = 29 },
                        new City {Id = 596, Label = "رازوجرگلان", Value= "رازوجرگلان",ProvinceId = 29 },
                        new City {Id = 597,  Label = "گرمه",Value= "گرمه", ProvinceId = 29 },
                        new City {Id = 598, Label = "اسفراین", Value= "اسفراین",ProvinceId = 29 },
                        new City {Id = 599, Label = "جاجرم",Value= "جاجرم", ProvinceId = 29 },
                        new City {Id = 600,  Label = "مانه و سملقان",Value= "مانه و سملقان", ProvinceId = 29 },
                        new City {Id = 601,  Label = "شیروان", Value= "شیروان",ProvinceId = 29 },
                        new City {Id = 602, Label = "فاروج", Value= "فاروج",ProvinceId = 29 },
                        new City {Id = 603, Label = "غلامان",Value= "غلامان", ProvinceId = 29 },
                        ///////////////////////////////////////////////////////////
                        new City {Id = 604, Label = "بیرجند",Value= "بیرجند", ProvinceId = 30 },
                        new City {Id = 605, Label = "طبس", Value= "طبس",ProvinceId = 30 },
                        new City {Id = 606, Label = "فردوس",Value= "فردوس", ProvinceId = 30 },
                        new City {Id = 607,  Label = "حاجی آباد",Value= "آباد", ProvinceId = 30 },
                        new City {Id = 608,  Label = "قائنات",Value= "قائنات", ProvinceId = 30 },
                        new City {Id = 609, Label = "بشرویه", Value= "بشرویه",ProvinceId = 30 },
                        new City {Id = 610, Label = "نهبندان", Value= "نهبندان",ProvinceId = 30 },
                        new City {Id = 611,  Label = "سربیشه",Value= "سربیشه", ProvinceId = 30 },
                        new City {Id = 612,  Label = "زیرکوه",Value= "زیرکوه", ProvinceId = 30 },
                        new City {Id = 613,  Label = "قائن",Value= "قائن", ProvinceId = 30 },
                        new City {Id = 614, Label = "اسدیه",Value= "اسدیه", ProvinceId = 30 },
                        new City {Id = 615,  Label = "درمیان",Value= "درمیان", ProvinceId = 30 },
                        ///////////////////////////////////////////////////////////
                        new City {Id = 616, Label = "کرج",Value= "کرج", ProvinceId = 31 },
                        new City {Id = 617,  Label = "فردیس کرج",Value= "فردیس کرج", ProvinceId = 31 },
                        new City {Id = 618,  Label = "طالقان",Value= "طالقان", ProvinceId = 31 },
                        new City {Id = 619,  Label = "هشتگرد", Value= "هشتگرد",ProvinceId = 31 },
                        new City {Id = 620,  Label = "اشتهارد",Value= "اشتهارد", ProvinceId = 31 },
                        new City {Id = 621,  Label = "گرمدره", Value= "گرمدره",ProvinceId = 31 },
                        new City {Id = 622, Label = "ماهدشت",Value= "ماهدشت", ProvinceId = 31 },
                        new City {Id = 623,  Label = "ساوجبلاغ", Value= "ساوجبلاغ",ProvinceId = 31 },
                        new City {Id = 624,  Label = "نظرآباد",Value= "نظرآباد", ProvinceId = 31 },
                        ///////////////////////////////////////////////////////////
                        new City { Id = 625,  Label = "خاورمیانه", Value= "خاورمیانه",ProvinceId = 32 },
                        new City { Id = 626,  Label = "اروپا", Value= "اروپا",ProvinceId = 32 },
                        new City { Id = 627,  Label = "اسیای شرقی",Value= "رشت", ProvinceId = 32 },
                        new City { Id = 628,  Label = "امریکای شمالی",Value= "امریکای شمالی", ProvinceId = 32 },
                        new City { Id = 629, Label = "امریکای جنوبی",Value= "امریکای جنوبی", ProvinceId = 32 },
                        new City { Id = 630, Label = "استرالیا", Value= "استرالیا",ProvinceId = 32 }
                        ///////////////////////////////////////////////////////////
                    };

                // Ensure the database is created
                var isDbCreated = await context.Database.EnsureCreatedAsync();
                if (isDbCreated)
                {
                    return;
                    //throw new DataBaseExcption($"DataBase is Created the messagae is ${isDbCreated} from EnsureCreated");
                }

                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(new IdentityRole[]
                    {
                        new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                        new IdentityRole() { Name = "User", ConcurrencyStamp = "1", NormalizedName = "User" },
                        new IdentityRole() { Name = "Staff", ConcurrencyStamp = "1", NormalizedName = "Staff" },
                    });
                }
                if (!context.PersonalityTraits.Any())
                {

                    var traits = new List<PersonalityTrait>
                    {
                        new PersonalityTrait { Name = "Extraversion", Description = "تمایل به جستجوی تحریک و لذت بردن از همراهی دیگران.", TraitType = "Interpersonal" },
                        new PersonalityTrait { Name = "Agreeableness", Description = "تمایل به دلسوزی و همکاری با دیگران.", TraitType = "Interpersonal" },
                        new PersonalityTrait { Name = "Conscientiousness", Description = "توانایی سازماندهی، مسئولیت پذیری و هدف گذاری.", TraitType = "Work" },
                        new PersonalityTrait { Name = "Openness", Description = "گرایش به خلاقیت، کنجکاوی و گشودگی به تجربیات جدید.", TraitType = "Cognitive" },
                        new PersonalityTrait { Name = "Neuroticism", Description = "تمایل به تجربه احساسات منفی مانند اضطراب و تحریک پذیری.", TraitType = "Emotional Stability" },
                        new PersonalityTrait { Name = "Resilience", Description = "توانایی بازیابی سریع از مشکلات و سازگاری با تغییرات.", TraitType = "Work" },
                        new PersonalityTrait { Name = "Leadership", Description = "توانایی الهام بخشیدن، تأثیرگذاری و هدایت مؤثر دیگران.", TraitType = "Leadership" },
                        new PersonalityTrait { Name = "Communication", Description = "اثربخشی در بیان افکار و گوش دادن فعال به دیگران.", TraitType = "Interpersonal" },
                        new PersonalityTrait { Name = "Critical Thinking", Description = "توانایی تحلیل و ارزیابی عینی مسائل یا ایده‌ها.", TraitType = "Cognitive" },
                        new PersonalityTrait { Name = "Adaptability", Description = "تمایل و توانایی سازگاری با موقعیت‌ها یا محیط‌های جدید.", TraitType = "Work" }
                    };
                    context.PersonalityTraits.AddRange(traits);
                    await context.SaveChangesAsync();
                }
                if (!context.PersonalityTestItems.Any())
                {
                    var traitsDictionary = context.PersonalityTraits
                                .GroupBy(t => t.Name)
                                .ToDictionary(g => g.Key, g => g.First().Id);
                    // var traitsDictionary = context.PersonalityTraits.ToDictionary(t => t.Name, t => t.Id);
                    foreach (var key in traitsDictionary.Keys)
                    {
                        Console.WriteLine($"Trait Key: {key}");
                    }
                    context.PersonalityTestItems.AddRange(new List<PersonalityTestItem>
                        {
                            // Extraversion (ID = 1)
                            new PersonalityTestItem { Name = "EXT1", ItemText = "از اینکه مرکز توجه باشم لذت می‌برم.", ScoringDirection = "Positive",
                                Description = "سنجش جامعه‌پذیری",
                                 PersonalityTraitId = traitsDictionary["Extraversion"] },
                            new PersonalityTestItem { Name = "EXT2", Description = "تنهایی را ترجیح می‌دهد", ItemText = "از جمع‌های بزرگ دوری می‌کنم.", ScoringDirection = "Negative", PersonalityTraitId = traitsDictionary["Extraversion"] },

                            // Agreeableness (ID = 2)
                            new PersonalityTestItem { Name = "AGR1",Description = "همدلی را می سنجد", ItemText = "من با احساسات دیگران همدردی می‌کنم.", ScoringDirection = "Positive", PersonalityTraitId = traitsDictionary["Agreeableness"]  },
                            new PersonalityTestItem { Name = "AGR2",Description = "پرخاشگری تحت فشار", ItemText = "وقتی ناراحتم به مردم توهین می‌کنم.", ScoringDirection = "Negative", PersonalityTraitId = traitsDictionary["Agreeableness"] },

                            // Conscientiousness (ID = 3)
                            new PersonalityTestItem { Name = "CON1",Description = "قابلیت اطمینان را اندازه گیری می کند", ItemText = "من وظایف را به موقع انجام می‌دهم.", ScoringDirection = "Positive", PersonalityTraitId = traitsDictionary["Conscientiousness"]  },
                            new PersonalityTestItem { Name = "CON2", Description= "بی‌نظمی را اندازه‌گیری می‌کند", ItemText = "من اغلب کارها را ناتمام رها می‌کنم.", ScoringDirection = "Negative", PersonalityTraitId = traitsDictionary["Conscientiousness"]  },

                            // Openness (ID = 4)
                            new PersonalityTestItem { Name = "OPN1",Description = "خلاقیت را می سنجد", ItemText = "من قوه تخیل قوی و روشنی دارم.", ScoringDirection = "Positive", PersonalityTraitId = traitsDictionary["Openness"] },
                            new PersonalityTestItem { Name = "OPN2",Description = "ترجیح بر آشنایی", ItemText = "من روتین را به تنوع ترجیح می‌دهم.", ScoringDirection = "Negative", PersonalityTraitId = traitsDictionary["Openness"] },

                            // Neuroticism (ID = 5)
                            new PersonalityTestItem { Name = "NEU1",Description = "واکنش‌پذیری عاطفی را اندازه‌گیری می‌کند", ItemText = "من به راحتی دچار استرس می‌شوم.", ScoringDirection = "Positive", PersonalityTraitId = traitsDictionary["Neuroticism"] },
                            new PersonalityTestItem { Name = "NEU2",Description = "کنترل عاطفی را اندازه گیری می کند", ItemText = "من تحت فشار آرام می‌مانم.", ScoringDirection = "Negative", PersonalityTraitId = traitsDictionary["Neuroticism"] },

                            // Resilience (ID = 6)
                            new PersonalityTestItem { Name = "RES1",Description = "تاب آوری را اندازه گیری می کند", ItemText = "بعد از شکست‌ها سریع به حالت عادی برمی‌گردم.", ScoringDirection = "Positive", PersonalityTraitId =traitsDictionary["Resilience"]  },
                            new PersonalityTestItem { Name = "RES2", Description = "معیار پایداری",ItemText = "وقتی اوضاع سخت می‌شود، به راحتی تسلیم می‌شوم.", ScoringDirection = "Negative", PersonalityTraitId = traitsDictionary["Resilience"]  },

                            // Leadership (ID = 7)
                            new PersonalityTestItem { Name = "LDR1", Description ="ابتکار رهبری را اندازه گیری می کند", ItemText = "از اینکه مسئولیت موقعیت‌ها را به عهده بگیرم لذت می‌برم.", ScoringDirection = "Positive", PersonalityTraitId = traitsDictionary["Leadership"] },
                            new PersonalityTestItem { Name = "LDR2", Description= "از نقش های رهبری اجتناب می کند", ItemText = "من ترجیح می‌دهم به جای رهبری، دنباله‌رو باشم.", ScoringDirection = "Negative", PersonalityTraitId = traitsDictionary["Leadership"] },

                            // Communication (ID = 8)
                            new PersonalityTestItem { Name = "COM1", Description = "ارتباط کلامی", ItemText = "من به راحتی می‌توانم منظورم را به روشنی بیان کنم.", ScoringDirection = "Positive", PersonalityTraitId = traitsDictionary["Communication"] },
                            new PersonalityTestItem { Name = "COM2", Description = "مشکل در بیان", ItemText = "من برای توضیح افکارم تقلا می‌کنم.", ScoringDirection = "Negative", PersonalityTraitId =traitsDictionary["Communication"] },

                            // Critical Thinking (ID = 9)
                            new PersonalityTestItem { Name = "CRT1",Description = "تفکر تحلیلی", ItemText = "از حل مسائل پیچیده لذت می‌برم.", ScoringDirection = "Positive", PersonalityTraitId = traitsDictionary["Critical Thinking"] },
                            new PersonalityTestItem { Name = "CRT2", Description = "از تجزیه و تحلیل اجتناب می کند", ItemText = "از کارهایی که نیاز به تفکر عمیق دارند، اجتناب می‌کنم.", ScoringDirection = "Negative", PersonalityTraitId = traitsDictionary["Critical Thinking"] },

                            // Adaptability (ID = 10)
                            new PersonalityTestItem { Name = "ADP1", Description = "انعطاف پذیری", ItemText = "من به راحتی با موقعیت‌های جدید سازگار می‌شوم.", ScoringDirection = "Positive", PersonalityTraitId = traitsDictionary["Adaptability"] },
                            new PersonalityTestItem { Name = "ADP2", Description = "مقاومت در برابر تغییر", ItemText = "از تغییرات غیرمنتظره متنفرم.", ScoringDirection = "Negative", PersonalityTraitId = traitsDictionary["Adaptability"] },
                        });
                }
                if (!context.Provinces.Any())
                {
                    // Reset the IDENTITY seed to 1
                    //context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Provinces', RESEED, 0)");
                    //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Provinces ON");

                    var provinces = new List<Province>
                                    {
                                        new Province { Id = 1, Label = "تهران", Value = "تهران" },
                                        new Province { Id = 2, Label = "گیلان", Value = "گیلان" },
                                        new Province { Id = 3, Label = "آذربایجان شرقی", Value = "آذربایجان شرقی" },
                                        new Province { Id = 4, Label = "خوزستان", Value = "خوزستان" },
                                        new Province { Id = 5, Label = "فارس", Value = "فارس" },
                                        new Province { Id = 6, Label = "اصفهان", Value = "اصفهان" },
                                        new Province { Id = 7, Label = "خراسان رضوی", Value = "خراسان رضوی" },
                                        new Province { Id = 8, Label = "قزوین", Value = "قزوین" },
                                        new Province { Id = 9, Label = "سمنان", Value = "سمنان" },
                                        new Province { Id = 10, Label = "قم", Value = "قم" },
                                        new Province { Id = 11, Label = "مرکزی", Value = "مرکزی" },
                                        new Province { Id = 12, Label = "زنجان", Value = "زنجان" },
                                        new Province { Id = 13, Label = "مازندران", Value = "مازندران" },
                                        new Province { Id = 14, Label = "گلستان", Value = "گلستان" },
                                        new Province { Id = 15, Label = "اردبیل", Value = "اردبیل" },
                                        new Province { Id = 16, Label = "آذربایجان غربی", Value = "آذربایجان غربی" },
                                        new Province { Id = 17, Label = "همدان", Value = "همدان" },
                                        new Province { Id = 18, Label = "کردستان", Value = "کردستان" },
                                        new Province { Id = 19, Label = "کرمانشاه", Value = "کرمانشاه" },
                                        new Province { Id = 20, Label = "لرستان", Value = "لرستان" },
                                        new Province { Id = 21, Label = "بوشهر", Value = "بوشهر" },
                                        new Province { Id = 22, Label = "کرمان", Value = "کرمان" },
                                        new Province { Id = 23, Label = "هرمزگان", Value = "هرمزگان" },
                                        new Province { Id = 24, Label = "چهارمحال و بختیاری", Value = "چهارمحال و بختیاری" },
                                        new Province { Id = 25, Label = "یزد", Value = "یزد" },
                                        new Province { Id = 26, Label = "سیستان و بلوچستان", Value = "سیستان و بلوچستان" },
                                        new Province { Id = 27, Label = "ایلام", Value = "ایلام" },
                                        new Province { Id = 28, Label = "کهگلویه و بویراحمد", Value = "کهگلویه و بویراحمد" },
                                        new Province { Id = 29, Label = "خراسان شمالی", Value = "خراسان شمالی" },
                                        new Province { Id = 30, Label = "خراسان جنوبی", Value = "خراسان جنوبی" },
                                        new Province { Id = 31, Label = "البرز", Value = "البرز" },
                                        new Province { Id = 32, Label = "خارج کشور", Value = "خارج کشور" }
                                    };

                    await context.Provinces.AddRangeAsync(provinces.Where(x => x.Label != null).Take(32).ToList());
                    await context.SaveChangesAsync();
                }
                if (!context.Cities.Any())
                {
                    //context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Cities', RESEED, 0)");
                    //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Cities ON");


                    await context.Cities.AddRangeAsync(cities);
                    await context.SaveChangesAsync();


                }

                if (!context.JobCategories.Any())
                {
                    var jobCategories = new List<JobCategory>
                {


                            new JobCategory {  Name = "فروش بازار یابی - سطوح کارشناسی", NameEn = "Sales Marketing - Expert Level", Slug = "sales-marketing-expert", Value = "1", IsActive = true },
                            new JobCategory {  Name = "فروش و بازیابی - فروشنده / بازار یاب و ویزیتور / صندوقدار", NameEn = "Sales and Marketing - Salesperson / Marketer / Cashier", Slug = "sales-marketing-salesperson", Value = "2", IsActive = true },
                            new JobCategory {  Name = "مدیر فروشگاه / مدیر رستوران", NameEn = "Store Manager / Restaurant Manager", Slug = "store-restaurant-manager", Value = "3", IsActive = true },
                            new JobCategory {  Name = "خدمات و پشتیبانی مشتریان", NameEn = "Customer Service and Support", Slug = "customer-service-support", Value = "4", IsActive = true },
                            new JobCategory {  Name = "نماینده علمی / مدرس", NameEn = "Scientific Representative / Instructor", Slug = "scientific-representative-instructor", Value = "5", IsActive = true },
                            new JobCategory {  Name = "مدیریت بیمه", NameEn = "Insurance Management", Slug = "insurance-management", Value = "6", IsActive = true },
                            new JobCategory {  Name = "دیجیتال مارکتینگ و سئو", NameEn = "Digital Marketing and SEO", Slug = "digital-marketing-seo", Value = "7", IsActive = true },
                            new JobCategory {  Name = "ترجمه / تولید محتوا / نویسندگی و ویراستاری", NameEn = "Translation / Content Creation / Writing and Editing", Slug = "translation-content-writing", Value = "8", IsActive = true },
                            new JobCategory {  Name = "توسعه نرم افزار و برنامه نویسی", NameEn = "Software Development and Programming", Slug = "software-development-programming", Value = "9", IsActive = true },
                            new JobCategory {  Name = "تست نرم افزار", NameEn = "Software Testing", Slug = "software-testing", Value = "10", IsActive = true },
                            new JobCategory {  Name = "شبکه /Devops / پشتیبانی سخت افزاری و نرم افزاری", NameEn = "Network / Devops / Hardware and Software Support", Slug = "network-devops-support", Value = "11", IsActive = true },
                            new JobCategory {  Name = "علوم داده / هوش مصنوعی", NameEn = "Data Science / Artificial Intelligence", Slug = "data-science-ai", Value = "12", IsActive = true },
                            new JobCategory {  Name = "طراحی بازی", NameEn = "Game Design", Slug = "game-design", Value = "13", IsActive = true },
                            new JobCategory {  Name = "طراحی لباس / طراحی طلا و جواهر", NameEn = "Fashion Design / Jewelry Design", Slug = "fashion-jewelry-design", Value = "14", IsActive = true },
                            new JobCategory {  Name = "طراحی صنعتی / نقشه شی صنعتی", NameEn = "Industrial Design / Industrial Object Design", Slug = "industrial-design", Value = "15", IsActive = true },
                            new JobCategory {  Name = "عکاسی", NameEn = "Photography", Slug = "photography", Value = "16", IsActive = true },
                            new JobCategory {  Name = "مشاغل حوزه فیلم و سینما", NameEn = "Film and Cinema Professions", Slug = "film-cinema", Value = "17", IsActive = true },
                            new JobCategory {  Name = " طراحی موسیقی و صدا", NameEn = "Music and Sound Design", Slug = "music-sound-design", Value = "18", IsActive = true },
                            new JobCategory {  Name = "(UI/UX) طراحی رابطه و تجربه کاربری ", NameEn = "UI/UX Design", Slug = "ui-ux-design", Value = "19", IsActive = true },
                            new JobCategory {  Name = "مدیر محصول / مالک محصول", NameEn = "Product Manager / Product Owner", Slug = "product-manager-owner", Value = "20", IsActive = true },
                            new JobCategory {  Name = "تحلیل و توسعه  کسب و کار / استراتژی  / برنامه ریزی ", NameEn = "Business Analysis and Development / Strategy / Planning", Slug = "business-analysis-strategy", Value = "21", IsActive = true },
                            new JobCategory {  Name = "خرید / تدارکات", NameEn = "Purchasing / Procurement", Slug = "purchasing-procurement", Value = "22", IsActive = true },
                            new JobCategory {  Name = "مهندس صنایع / مدیریت تولید / مدیریت پروژه / مدیریت عملیات", NameEn = "Industrial Engineering / Production Management / Project Management / Operations Management", Slug = "industrial-engineering-management", Value = "23", IsActive = true },
                            new JobCategory {  Name = "خرید / تدارکات", NameEn = "Purchasing / Procurement", Slug = "purchasing-procurement-2", Value = "24", IsActive = true },
                            new JobCategory {  Name = "بازگانی / تجارت", NameEn = "Commerce / Trade", Slug = "commerce-trade", Value = "25", IsActive = true },
                            new JobCategory {  Name = "لجستیک / حمل و نقل / انبارداری", NameEn = "Logistics / Transportation / Warehousing", Slug = "logistics-transportation", Value = "26", IsActive = true },
                            new JobCategory {  Name = "راننده / مسئول توزیع / پیک موتوری", NameEn = "Driver / Distribution Manager / Courier", Slug = "driver-distribution", Value = "27", IsActive = true },
                            new JobCategory {  Name = "مالی و حسابداری", NameEn = "Finance and Accounting", Slug = "finance-accounting", Value = "28", IsActive = true },
                            new JobCategory {  Name = "معامله گر و تحلیل گر بازارهای مالی ", NameEn = "Financial Markets Trader and Analyst", Slug = "financial-markets-analyst", Value = "29", IsActive = true },
                            new JobCategory {  Name = "تحصیل دار / کارپرداز", NameEn = "Collector / Paymaster", Slug = "collector-paymaster", Value = "30", IsActive = true },
                            new JobCategory {  Name = "مسئول دفتر / کارمند اداری ثبت اطلاعات / تایپیست", NameEn = "Office Manager / Administrative Clerk / Data Entry / Typist", Slug = "office-admin-data-entry", Value = "31", IsActive = true },
                            new JobCategory {  Name = "منابع انسانی", NameEn = "Human Resources", Slug = "human-resources", Value = "32", IsActive = true },
                            new JobCategory {  Name = "مدیر اجرایی", NameEn = "Executive Manager", Slug = "executive-manager", Value = "33", IsActive = true },
                            new JobCategory {  Name = "مدیر عامل / مدیر کارخانه", NameEn = "CEO / Factory Manager", Slug = "ceo-factory-manager", Value = "34", IsActive = true },
                            new JobCategory {  Name = "مهندسی برق", NameEn = "Electrical Engineering", Slug = "electrical-engineering", Value = "35", IsActive = true },
                            new JobCategory {  Name = "مهندسی پزشکی", NameEn = "Medical Engineering", Slug = "medical-engineering", Value = "36", IsActive = true },
                            new JobCategory {  Name = "مهندس مکانیگ / مهندس هوا فضا", NameEn = "Mechanical Engineer / Aerospace Engineer", Slug = "mechanical-aerospace-engineering", Value = "37", IsActive = true },
                            new JobCategory {  Name = "مهندس صنایع غذایی", NameEn = "Food Industry Engineer", Slug = "food-industry-engineering", Value = "38", IsActive = true },
                            new JobCategory {  Name = "مهندس شیمی / مهندس نفت گاز", NameEn = "Chemical Engineer / Oil and Gas Engineer", Slug = "chemical-oil-gas-engineering", Value = "39", IsActive = true },
                            new JobCategory {  Name = "مهندس انرژی / مهندس هسته ای", NameEn = "Energy Engineer / Nuclear Engineer", Slug = "energy-nuclear-engineering", Value = "40", IsActive = true },
                            new JobCategory {  Name = "(HSE) بهداشت ، ایمنی و محیط زیست", NameEn = "Health, Safety and Environment (HSE)", Slug = "health-safety-environment", Value = "41", IsActive = true },
                            new JobCategory {  Name = "مهندس عمران", NameEn = "Civil Engineer", Slug = "civil-engineering", Value = "42", IsActive = true },
                            new JobCategory {  Name = "مهندس معماری و شهرسازی", NameEn = "Architecture and Urban Planning Engineer", Slug = "architecture-urban-planning", Value = "43", IsActive = true },
                            new JobCategory {  Name = "مهندس معدن / زمین شناسی", NameEn = "Mining Engineer / Geology", Slug = "mining-geology", Value = "44", IsActive = true },
                            new JobCategory {  Name = "مهندسی مواد و متالوژی", NameEn = "Materials Engineering and Metallurgy", Slug = "materials-metallurgy", Value = "45", IsActive = true },
                            new JobCategory {  Name = "مهندسی نساجی", NameEn = "Textile Engineering", Slug = "textile-engineering", Value = "46", IsActive = true },
                            new JobCategory {  Name = "مهندسی پلیمر", NameEn = "Polymer Engineering", Slug = "polymer-engineering", Value = "47", IsActive = true },
                            new JobCategory {  Name = "مهندس کشاورزی / علوم دامی", NameEn = "Agricultural Engineer / Animal Science", Slug = "agricultural-animal-science", Value = "48", IsActive = true },
                            new JobCategory {  Name = "زیست شناسی / علوم زیستی / علوم آزمایشگاهی", NameEn = "Biology / Life Sciences / Laboratory Sciences", Slug = "biology-life-laboratory-sciences", Value = "49", IsActive = true },
                            new JobCategory {  Name = "داروسازی/ بیوشیمی /شیمی", NameEn = "Pharmacy / Biochemistry / Chemistry", Slug = "pharmacy-biochemistry-chemistry", Value = "50", IsActive = true },
                            new JobCategory {  Name = "پزشک / دندان پزشک / دامپزشک ", NameEn = "Physician / Dentist / Veterinarian", Slug = "physician-dentist-veterinarian", Value = "51", IsActive = true },
                            new JobCategory {  Name = "پرستار بهیار / تکنسین حوزه سلامت و درمان /دستیاز پزشک", NameEn = "Nurse / Healthcare Technician / Physician Assistant", Slug = "nurse-healthcare-technician", Value = "52", IsActive = true },
                            new JobCategory {  Name = "پرستار سالمند / پرستار کودک", NameEn = "Elderly Caregiver / Child Caregiver", Slug = "elderly-child-caregiver", Value = "53", IsActive = true },
                            new JobCategory {  Name = "روانشناسی / مشاوره / علوم اجتماعی", NameEn = "Psychology / Counseling / Social Sciences", Slug = "psychology-counseling-social", Value = "54", IsActive = true },
                            new JobCategory {  Name = "حقوقی", NameEn = "Legal", Slug = "legal", Value = "55", IsActive = true },
                            new JobCategory {  Name = "روابط عمومی", NameEn = "Public Relations", Slug = "public-relations", Value = "56", IsActive = true },
                            new JobCategory {  Name = "روزنامه نگار / خبرنگار", NameEn = "Journalist / Reporter", Slug = "journalist-reporter", Value = "57", IsActive = true },
                            new JobCategory {  Name = "آموزش / تدریس", NameEn = "Education / Teaching", Slug = "education-teaching", Value = "58", IsActive = true },
                            new JobCategory {  Name = "پژوهش", NameEn = "Research", Slug = "research", Value = "59", IsActive = true },
                            new JobCategory {  Name = "نگهبان", NameEn = "Security Guard", Slug = "security-guard", Value = "60", IsActive = true },
                            new JobCategory {  Name = "کارگر ساده / نیروی خدماتی", NameEn = "General Worker / Service Personnel", Slug = "general-worker-service", Value = "61", IsActive = true },
                            new JobCategory {  Name = "تگنسین فنی / تعمیرکار / کارگر ماهر", NameEn = "Technical Technician / Repairman / Skilled Worker", Slug = "technical-technician-repairman", Value = "62", IsActive = true },
                            new JobCategory {  Name = "... تخصص های ساختمانی /بنا / گچ کار /کاشی کار و ", NameEn = "Construction Specialties / Builder / Plasterer / Tile Worker, etc.", Slug = "construction-specialties", Value = "63", IsActive = true },
                            new JobCategory {  Name = "مبل ساز/رنگ کار چوب/نجار / کابینت کار/MDF کار", NameEn = "Furniture Maker / Wood Painter / Carpenter / Cabinet Maker / MDF Worker", Slug = "furniture-carpenter-cabinet", Value = "64", IsActive = true },
                            new JobCategory {  Name = "آرایشگر", NameEn = "Hairdresser", Slug = "hairdresser", Value = "65", IsActive = true },
                            new JobCategory {  Name = "قناد و شیرنی پزی", NameEn = "Confectioner and Pastry Chef", Slug = "confectioner-pastry", Value = "66", IsActive = true },
                            new JobCategory {  Name = "بافنده فرش /قالی باف", NameEn = "Carpet Weaver", Slug = "carpet-weaver", Value = "67", IsActive = true },
                            new JobCategory {  Name = "نانوا", NameEn = "Baker", Slug = "baker", Value = "68", IsActive = true },
                            new JobCategory {  Name = "قفل و کلید ساز", NameEn = "Locksmith", Slug = "locksmith", Value = "69", IsActive = true },
                            new JobCategory {  Name = "قصاب", NameEn = "Butcher", Slug = "butcher", Value = "70", IsActive = true },
                            new JobCategory {  Name = "کفاش", NameEn = "Shoemaker", Slug = "shoemaker", Value = "71", IsActive = true },
                            new JobCategory {  Name = "خیاط", NameEn = "Tailor", Slug = "tailor", Value = "72", IsActive = true },
                            new JobCategory {  Name = "آشپز", NameEn = "Chef", Slug = "chef", Value = "73", IsActive = true },
                            new JobCategory {  Name = "کافی من /گارسون /باریستا", NameEn = "Waiter / Barista", Slug = "waiter-barista", Value = "74", IsActive = true },
                            new JobCategory {  Name = "راهنمای تور /مهماندار", NameEn = "Tour Guide / Host", Slug = "tour-guide-host", Value = "75", IsActive = true },
                            new JobCategory {  Name = "ورزش/ تربیت بدنی/تغذیه", NameEn = "Sports / Physical Education / Nutrition", Slug = "sports-physical-education-nutrition", Value = "76", IsActive = true },
                            new JobCategory {  Name = "تاریخ /جغرافیا / باستان شناسی", NameEn = "History / Geography / Archaeology", Slug = "history-geography-archaeology", Value = "77", IsActive = true },
                            new JobCategory {  Name = "طراحی گرافیک / طراحی انیمیشن و موشن گرافیک", NameEn = "Graphic Design / Animation and Motion Graphics Design", Slug = "graphic-design-animation", Value = "78", IsActive = true }
   
           // Add other job categories here
                };

                    context.JobCategories.AddRange(jobCategories);
                }

                if (!context.TechnicalOptions.Any())
                {
                    context.TechnicalOptions.AddRange(
                        new TechnicalOption { Label = "تازه کار", Value = "1" },
                        new TechnicalOption { Label = "كارشناس / كارشناس ارشد", Value = "2" },
                        new TechnicalOption { Label = "سرپرست / مدیر میانی", Value = "3" },
                        new TechnicalOption { Label = "مدیر ارشد", Value = "4" }
                    );
                }

                if (!context.MBTIQuestions.Any())
                {
                    context.MBTIQuestions.AddRange(
                           new MBTIQuestion { QuestionText = "آیا از حضور در اجتماعات بزرگ لذت می‌برید؟", Category = "E" },
                           new MBTIQuestion { QuestionText = "آیا ترجیح می‌دهید زمان خود را به تنهایی یا با یک یا دو دوست نزدیک بگذرانید؟", Category = "I" },
                           new MBTIQuestion { QuestionText = "آیا به راحتی با افراد جدید آشنا می‌شوید؟", Category = "E" },
                           new MBTIQuestion { QuestionText = "آیا اغلب به دنبال زمان‌هایی برای تنهایی و تفکر می‌گردید؟", Category = "I" },
                           new MBTIQuestion { QuestionText = "آیا هنگام کار گروهی انرژی بیشتری دارید؟", Category = "E" },
                           new MBTIQuestion { QuestionText = "آیا ترجیح می‌دهید به تنهایی روی پروژه‌های خود کار کنید؟", Category = "I" },
                           new MBTIQuestion { QuestionText = "آیا از صحبت کردن در جمع لذت می‌برید؟", Category = "E" },
                           new MBTIQuestion { QuestionText = "آیا اغلب احساس می‌کنید نیاز به زمان‌های استراحت از جمع دارید؟", Category = "I" },
                           new MBTIQuestion { QuestionText = "آیا بیشتر از برقراری تماس تلفنی ترجیح می‌دهید پیام دهید؟", Category = "I" },
                           new MBTIQuestion { QuestionText = "آیا ترجیح می‌دهید در جمع بزرگ بازی کنید یا بنشینید و تماشا کنید؟", Category = "E" },
                           new MBTIQuestion { QuestionText = "آیا از حضور در مهمانی‌های شلوغ لذت می‌برید؟", Category = "E" },
                           new MBTIQuestion { QuestionText = "آیا از صحبت کردن با افراد جدید اضطراب دارید؟", Category = "I" },
                           new MBTIQuestion { QuestionText = "آیا از انجام فعالیت‌های گروهی انرژی می‌گیرید؟", Category = "E" },
                           new MBTIQuestion { QuestionText = "آیا ترجیح می‌دهید زمان خود را با تفکر و برنامه‌ریزی برای آینده بگذرانید؟", Category = "I" },
                           new MBTIQuestion { QuestionText = "آیا اغلب در ملاقات‌های اجتماعی فعال هستید؟", Category = "E" },
                           new MBTIQuestion { QuestionText = "آیا بعد از حضور در اجتماعات بزرگ احساس خستگی می‌کنید؟", Category = "I" },
                           new MBTIQuestion { QuestionText = "آیا از ایجاد ارتباط با دیگران لذت می‌برید؟", Category = "E" },
                           new MBTIQuestion { QuestionText = "آیا از حضور در مکان‌های آرام و ساکت لذت می‌برید؟", Category = "I" },
                           new MBTIQuestion { QuestionText = "آیا از ملاقات و گفتگو با افراد مختلف انرژی می‌گیرید؟", Category = "E" },
                           new MBTIQuestion { QuestionText = "آیا ترجیح می‌دهید وقت خود را در خانه بگذرانید تا در جمع؟", Category = "I" },
                           new MBTIQuestion { QuestionText = "آیا به جزئیات و حقایق عینی اهمیت می‌دهید؟", Category = "S" },
                           new MBTIQuestion { QuestionText = "آیا اغلب در مورد احتمالات و آینده فکر می‌کنید؟", Category = "N" },
                           new MBTIQuestion { QuestionText = "آیا ترجیح می‌دهید با اطلاعات قابل لمس و مستند کار کنید؟", Category = "S" },
                           new MBTIQuestion { QuestionText = "آیا به دنبال الگوها و معانی عمیق‌تر در امور هستید؟", Category = "N" },
                           new MBTIQuestion { QuestionText = "آیا به جزئیات عملی کارها علاقه‌مند هستید؟", Category = "S" },
                           new MBTIQuestion { QuestionText = "آیا اغلب به ایده‌های خلاقانه و نوآورانه فکر می‌کنید؟", Category = "N" },
                           new MBTIQuestion { QuestionText = "آیا به زمان حال و آنچه که واقعاً اتفاق می‌افتد اهمیت می‌دهید؟", Category = "S" },
                           new MBTIQuestion { QuestionText = "آیا به نظریه‌ها و احتمالات بیشتر از حقایق موجود اهمیت می‌دهید؟", Category = "N" },
                           new MBTIQuestion { QuestionText = "آیا به دنبال جزئیات دقیق و ملموس هستید؟", Category = "S" },
                           new MBTIQuestion { QuestionText = "آیا به دنبال احتمالات و تغییرات بلندمدت هستید؟", Category = "N" },
                           new MBTIQuestion { QuestionText = "آیا به واقعیت‌های موجود و حقایق جاری توجه دارید؟", Category = "S" },
                           new MBTIQuestion { QuestionText = "آیا ترجیح می‌دهید به معانی و الگوهای پنهان توجه کنید؟", Category = "N" },
                           new MBTIQuestion { QuestionText = "آیا به مشاهدات عینی و جزئیات توجه دارید؟", Category = "S" },
                           new MBTIQuestion { QuestionText = "آیا به دنبال ایده‌های جدید و احتمالاً ناملموس هستید؟", Category = "N" },
                           new MBTIQuestion { QuestionText = "آیا ترجیح می‌دهید با اطلاعات واقعی و قابل اعتماد کار کنید؟", Category = "S" },
                           new MBTIQuestion { QuestionText = "آیا به دنبال معانی عمیق‌تر و مفاهیم پنهان هستید؟", Category = "N" },
                           new MBTIQuestion { QuestionText = "آیا از کار با اطلاعات دقیق و واقعی لذت می‌برید؟", Category = "S" },
                           new MBTIQuestion { QuestionText = "آیا به دنبال الگوها و نظریه‌های پنهان در امور هستید؟", Category = "N" },
                           new MBTIQuestion { QuestionText = "آیا به دنبال حقایق و اطلاعات ملموس هستید؟", Category = "S" },
                           new MBTIQuestion { QuestionText = "آیا به دنبال احتمالات و ایده‌های آینده‌نگرانه هستید؟", Category = "N" },
                           new MBTIQuestion { QuestionText = "آیا هنگام تصمیم‌گیری بیشتر به منطق و تحلیل توجه می‌کنید؟", Category = "T" },
                           new MBTIQuestion { QuestionText = "آیا در تصمیم‌گیری‌ها احساسات و ارزش‌های شخصی را در نظر می‌گیرید؟", Category = "F" },
                           new MBTIQuestion { QuestionText = "آیا به قوانین و اصول منطقی پایبند هستید؟", Category = "T" },
                           new MBTIQuestion { QuestionText = "آیا در روابط شخصی به همدلی و تفاهم اهمیت می‌دهید؟", Category = "F" },
                           new MBTIQuestion { QuestionText = "آیا به واقعیت‌های موجود بیشتر از احساسات شخصی توجه می‌کنید؟", Category = "T" },
                           new MBTIQuestion { QuestionText = "آیا در موقعیت‌های اجتماعی به احساسات دیگران حساس هستید؟", Category = "F" },
                           new MBTIQuestion { QuestionText = "آیا ترجیح می‌دهید مسائل را به روش عینی و بی‌طرفانه بررسی کنید؟", Category = "T" },
                           new MBTIQuestion { QuestionText = "آیا در مواجهه با مشکلات به دنبال راه حل‌هایی هستید که همه را راضی کند؟", Category = "F" },
                           new MBTIQuestion { QuestionText = "آیا در تصمیم‌گیری‌ها بیشتر به منطق و استدلال توجه می‌کنید؟", Category = "T" },
                           new MBTIQuestion { QuestionText = "آیا در تصمیم‌گیری‌ها بیشتر به احساسات و همدلی توجه می‌کنید؟", Category = "F" },
                           new MBTIQuestion { QuestionText = "آیا به اصول منطقی و عینی پایبند هستید؟", Category = "T" },
                           new MBTIQuestion { QuestionText = "آیا در روابط شخصی به احساسات و تفاهم اهمیت می‌دهید؟", Category = "F" },
                           new MBTIQuestion { QuestionText = "آیا به واقعیت‌ها و حقایق بیشتر از احساسات توجه می‌کنید؟", Category = "T" },
                           new MBTIQuestion { QuestionText = "آیا در موقعیت‌های اجتماعی به احساسات دیگران توجه دارید؟", Category = "F" },
                           new MBTIQuestion { QuestionText = "آیا ترجیح می‌دهید مسائل را به روش عینی و منطقی بررسی کنید؟", Category = "T" },
                           new MBTIQuestion { QuestionText = "آیا در مواجهه با مشکلات به دنبال راه حل‌های همدلانه هستید؟", Category = "F" },
                           new MBTIQuestion { QuestionText = "آیا در تصمیم‌گیری‌ها بیشتر به منطق و واقعیت توجه می‌کنید؟", Category = "T" },
                           new MBTIQuestion { QuestionText = "آیا در تصمیم‌گیری‌ها بیشتر به احساسات و ارزش‌ها توجه می‌کنید؟", Category = "F" },
                           new MBTIQuestion { QuestionText = "آیا به اصول عینی و منطقی پایبند هستید؟", Category = "T" },
                           new MBTIQuestion { QuestionText = "آیا در روابط شخصی به همدلی و درک متقابل اهمیت می‌دهید؟", Category = "F" },
                           new MBTIQuestion { QuestionText = "آیا دوست دارید برنامه‌ریزی کنید و به برنامه‌ها پایبند باشید؟", Category = "J" },
                           new MBTIQuestion { QuestionText = "آیا ترجیح می‌دهید انعطاف‌پذیر باشید و برنامه‌ها را به راحتی تغییر دهید؟", Category = "P" },
                           new MBTIQuestion { QuestionText = "آیا از داشتن یک برنامه زمانی دقیق و مشخص لذت می‌برید؟", Category = "J" },
                           new MBTIQuestion { QuestionText = "آیا به دنبال فرصت‌هایی برای تجربه‌های جدید و غیرمنتظره هستید؟", Category = "P" },
                           new MBTIQuestion { QuestionText = "آیا ترجیح می‌دهید کارها را بر اساس یک برنامه مشخص انجام دهید؟", Category = "J" },
                           new MBTIQuestion { QuestionText = "آیا از انعطاف‌پذیری و آزادی در انجام کارها لذت می‌برید؟", Category = "P" },
                           new MBTIQuestion { QuestionText = "آیا دوست دارید کارها را به ترتیب و منظم انجام دهید؟", Category = "J" },
                           new MBTIQuestion { QuestionText = "آیا ترجیح می‌دهید کارها را به طور طبیعی و بدون برنامه‌ریزی دقیق انجام دهید؟", Category = "P" },
                           new MBTIQuestion { QuestionText = "آیا دوست دارید برنامه‌ریزی کنید و به آن پایبند بمانید؟", Category = "J" },
                           new MBTIQuestion { QuestionText = "آیا ترجیح می‌دهید برنامه‌ریزی انعطاف‌پذیر و بدون محدودیت داشته باشید؟", Category = "P" },
                           new MBTIQuestion { QuestionText = "آیا از داشتن یک برنامه مشخص و مدون لذت می‌برید؟", Category = "J" },
                           new MBTIQuestion { QuestionText = "آیا به دنبال فرصت‌های جدید و پیش‌بینی نشده هستید؟", Category = "P" },
                           new MBTIQuestion { QuestionText = "آیا ترجیح می‌دهید کارها را بر اساس برنامه انجام دهید؟", Category = "J" },
                           new MBTIQuestion { QuestionText = "آیا از انعطاف‌پذیری و آزادی در برنامه‌ریزی لذت می‌برید؟", Category = "P" },
                           new MBTIQuestion { QuestionText = "آیا دوست دارید کارها را به ترتیب و منظم انجام دهید؟", Category = "J" },
                           new MBTIQuestion { QuestionText = "آیا ترجیح می‌دهید کارها را به طور طبیعی و بدون برنامه‌ریزی دقیق انجام دهید؟", Category = "P" },
                           new MBTIQuestion { QuestionText = "آیا از برنامه‌ریزی دقیق و مدون لذت می‌برید؟", Category = "J" },
                           new MBTIQuestion { QuestionText = "آیا از انعطاف‌پذیری و آزادی در انجام کارها لذت می‌برید؟", Category = "P" },
                           new MBTIQuestion { QuestionText = "آیا دوست دارید کارها را به ترتیب و منظم انجام دهید؟", Category = "J" },
                           new MBTIQuestion { QuestionText = "آیا ترجیح می‌دهید کارها را به طور طبیعی و بدون برنامه‌ریزی دقیق انجام دهید؟", Category = "P" }
                   );
                }
                if (!context.MBTIResults.Any())
                {
                    context.MBTIResults.AddRange(
                           new MBTIResult { Name = "INTJ", Type = "معمار", Description = "متفکران استراتژیک و خیال‌پرداز با برنامه‌ای برای هر چیز.", Result = "متفکران خلاق و استراتژیک با برنامه برای همه چیز." },
                           new MBTIResult { Name = "INTP", Type = "متفکر", Description = "مخترعان نوآور با عطشی بی‌پایان برای دانش.", Result = "مخترعان نوآور با عطش سیری‌ناپذیر برای دانش." },
                           new MBTIResult { Name = "ENTJ", Type = "رهبر", Description = "رهبران جسور، تخیلی و اراده‌ای قوی که همیشه راهی یا می‌سازند یا می‌یابند.", Result = "رهبرانی جسور، خلاق و با اراده که همیشه یا راهی پیدا می‌کنند یا راهی می‌سازند." },
                           new MBTIResult { Name = "ENTP", Type = "مبتکر", Description = "افرادی باهوش و کنجکاو که به دنبال ایده‌های جدید و چالش‌های فکری هستند.", Result = "متفکران باهوش و کنجکاوی که نمی‌توانند در برابر یک چالش فکری مقاومت کنند." },
                           new MBTIResult { Name = "INFJ", Type = "مشاور", Description = "الهام‌بخش و آرمان‌گرا با توانایی قوی در درک دیگران و ایجاد تغییرات مثبت.", Result = "آرمان‌گرایانی آرام و عرفانی، در عین حال بسیار الهام‌بخش و خستگی‌ناپذیر." },
                           new MBTIResult { Name = "INFP", Type = "شفادهنده", Description = "افراد ساکت و شاعرانه با دیدگاه‌های اصیل و قلب‌های مهربان.", Result = "مردمی شاعر، مهربان و نوع‌دوست، همیشه مشتاق کمک به یک هدف خوب." },
                           new MBTIResult { Name = "ENFJ", Type = "مربی", Description = "رهبران کاریزماتیک و الهام‌بخش که قادر به هیجان و تأثیرگذاری بر دیگران هستند.", Result = "رهبران کاریزماتیک و الهام‌بخش، قادر به مسحور کردن شنوندگان خود." },
                           new MBTIResult { Name = "ENFP", Type = "مبارز", Description = "روح‌های خلاق، پرشور و اجتماعی که همیشه دلیل برای لبخند پیدا می‌کنند.", Result = "افرادی پرشور، خلاق و اجتماعی که همیشه دلیلی برای لبخند زدن پیدا می‌کنند." },
                           new MBTIResult { Name = "ISTJ", Type = "بازرس", Description = "افراد عمل‌گرا، واقع‌گرا و مسئولیت‌پذیر که به سنت‌ها و قوانین پایبند هستند.", Result = "افرادی عمل‌گرا و واقع‌بین که در قابل اعتماد بودنشان شکی نیست." },
                           new MBTIResult { Name = "ISFJ", Type = "مدافع", Description = "محافظان ساکت و مهربان که همیشه آماده کمک به دیگران هستند.", Result = "محافظانی بسیار فداکار و خونگرم، همیشه آماده دفاع از عزیزانشان." },
                           new MBTIResult { Name = "ESTJ", Type = "مدیر", Description = "مدیران عملی و عمل‌گرا که به سازمان‌دهی و مدیریت امور علاقه‌مند هستند.", Result = "مدیران عالی، بی‌رقیب در مدیریت چیزها - یا افراد." },
                           new MBTIResult { Name = "ESFJ", Type = "مشوق", Description = "افرادی اجتماعی و محبوب که همیشه به دنبال هماهنگی و همکاری با دیگران هستند.", Result = "افرادی فوق‌العاده دلسوز، اجتماعی و محبوب، همیشه مشتاق کمک کردن." },
                           new MBTIResult { Name = "ISTP", Type = "صنعتگر", Description = "ماجراجویان عملی و آرام که از اکتشاف و استفاده از ابزارها و ماشین‌ها لذت می‌برند.", Result = "آزمایشگران جسور و عملگرا، استادان انواع ابزارها." },
                           new MBTIResult { Name = "ISFP", Type = "هنرمند", Description = "افراد ساکت، حساس و ملایم که به دنبال خلق زیبایی در جهان هستند.", Result = "هنرمندانی انعطاف‌پذیر و جذاب، همیشه آماده‌ی کشف و تجربه‌ی چیزهای جدید." },
                           new MBTIResult { Name = "ESTP", Type = "ترویج‌دهنده", Description = "افراد فعال، پرشور و پرانرژی که به دنبال زندگی در لحظه هستند.", Result = "افرادی باهوش، پرانرژی و بسیار تیزبین که واقعاً از زندگی در شرایط بحرانی لذت می‌برند." },
                           new MBTIResult { Name = "ESFP", Type = "سرگرم‌کننده", Description = "افراد شاد و مشتاق که به دنبال لذت و تفریح در زندگی هستند.", Result = "هنرمندان خودجوش، پرانرژی و مشتاق - زندگی در کنار آنها هرگز کسل کننده نیست." }
                   );
                }
                if (!context.PsychologyTests.Any())
                {
                    var psychologyTests = new List<PsychologyTest>
                    {
                    new PsychologyTest
                    {
                        Name = "تست شغلی هلند", // تست رغبت‌سنجی هالند
                        Description = "علایق شغلی را در 6 تیپ شخصیتی (RIASEC) شناسایی می‌کند.",
                        Type = PsychologyTestType.Holland
                    },

                    new PsychologyTest
                    {
                        Name = "پنج ویژگی شخصیتی بزرگ (NEO-PI-R)", // پنج عامل بزرگ شخصیت
                        Description = "شخصیت را در پنج بُعد اصلی ارزیابی می‌کند (OCEAN).",
                        Type = PsychologyTestType.BigFive
                    },
                    new PsychologyTest
                    {
                        Name = "ارزیابی دیسک", // تست دیسک
                        Description = "بر رفتار در محیط‌های کاری تمرکز دارد: تسلط، نفوذ، ثبات و وظیفه‌شناسی.",
                        Type = PsychologyTestType.DISC
                    },
                    new PsychologyTest
                    {
                        Name = "هوش هیجانی (EQ-i)", // هوش هیجانی
                        Description = "آگاهی عاطفی، کنترل، همدلی و مهارت‌های اجتماعی را ارزیابی می‌کند.",
                        Type = PsychologyTestType.EmotionalIntelligence
                    },
                    new PsychologyTest
                    {
                        Name = "آزمون استعدادهای شناختی", // تست استعداد شناختی
                        Description = "هوش عمومی، منطق، ریاضی و استدلال کلامی را می‌سنجد.",
                        Type = PsychologyTestType.Cognitive
                    },
                    new PsychologyTest
                    {
                        Name = "آزمون قضاوت موقعیتی (SJT)", // تست قضاوت موقعیتی
                        Description = "تصمیم‌گیری در سناریوهای مرتبط با شغل را ارزیابی می‌کند.",
                        Type = PsychologyTestType.SJT
                    }};
                    context.PsychologyTests.AddRange(psychologyTests);
                    await context.SaveChangesAsync();

                }
                if (!context.PsychologyTestQuestions.Any())
                {
                    var answerOptions = new List<AnswerOption>
                    {
                        
    // تست هالند
                        new AnswerOption { Value = 1, Label = "کاملاً مخالفم", PsychologyTestId = 1 },
                        new AnswerOption { Value = 2, Label = "مخالفم", PsychologyTestId = 1 },
                        new AnswerOption { Value = 3, Label = "موافقم", PsychologyTestId = 1 },
                        new AnswerOption { Value = 4, Label = "کاملاً موافقم", PsychologyTestId = 1 },

                        // تست پنج عامل بزرگ شخصیت (Big Five)
                        new AnswerOption { Value = 1, Label = "کاملاً مخالفم", PsychologyTestId = 2 },
                        new AnswerOption { Value = 2, Label = "مخالفم", PsychologyTestId = 2 },
                        new AnswerOption { Value = 3, Label = "موافقم", PsychologyTestId = 2 },
                        new AnswerOption { Value = 4, Label = "کاملاً موافقم", PsychologyTestId = 2 },

                        // تست DISC
                        new AnswerOption { Value = 1, Label = "کاملاً مخالفم", PsychologyTestId = 3 },
                        new AnswerOption { Value = 2, Label = "مخالفم", PsychologyTestId = 3 },
                        new AnswerOption { Value = 3, Label = "موافقم", PsychologyTestId = 3 },
                        new AnswerOption { Value = 4, Label = "کاملاً موافقم", PsychologyTestId = 3 },

                        // تست هوش هیجانی
                        new AnswerOption { Value = 1, Label = "کاملاً مخالفم", PsychologyTestId = 4 },
                        new AnswerOption { Value = 2, Label = "مخالفم", PsychologyTestId = 4 },
                        new AnswerOption { Value = 3, Label = "موافقم", PsychologyTestId = 4 },
                        new AnswerOption { Value = 4, Label = "کاملاً موافقم", PsychologyTestId = 4 },

                        // تست استعداد شناختی (معمولاً درست/نادرست یا چند گزینه‌ای خاص است – فرضی ساده)
                        new AnswerOption { Value = 1, Label = "درست است", PsychologyTestId = 5 },
                        new AnswerOption { Value = 2, Label = "نادرست است", PsychologyTestId = 5 },

                        // تست قضاوت موقعیتی (فرضی: بهترین گزینه، بدترین گزینه، خوب، بد)
                        new AnswerOption { Value = 1, Label = "بسیار نامناسب", PsychologyTestId = 6 },
                        new AnswerOption { Value = 2, Label = "نامناسب", PsychologyTestId = 6 },
                        new AnswerOption { Value = 3, Label = "مناسب", PsychologyTestId = 6 },
                        new AnswerOption { Value = 4, Label = "بسیار مناسب", PsychologyTestId = 6 }
                        //new AnswerOption { Value = 1, Label = "Strongly Disagree" , PsychologyTestId = 1},
                        //new AnswerOption { Value = 2, Label = "Disagree", PsychologyTestId = 1},
                        //new AnswerOption { Value = 3, Label = "Agree",PsychologyTestId = 1 },
                        //new AnswerOption { Value = 4, Label = "Strongly Agree" , PsychologyTestId = 1},
                        //new AnswerOption { Value = 1, Label = "Strongly Disagree" , PsychologyTestId = 2},
                        //new AnswerOption { Value = 2, Label = "Disagree", PsychologyTestId = 2},
                        //new AnswerOption { Value = 3, Label = "Agree",PsychologyTestId = 2},
                        //new AnswerOption { Value = 4, Label = "Strongly Agree" , PsychologyTestId = 2},


                        //new AnswerOption { Value = 1, Label = "Strongly Disagree" , PsychologyTestId = 3},
                        //new AnswerOption { Value = 2, Label = "Disagree", PsychologyTestId = 3},
                        //new AnswerOption { Value = 3, Label = "Agree",PsychologyTestId = 3},
                        //new AnswerOption { Value = 4, Label = "Strongly Agree" , PsychologyTestId = 3},



                        //new AnswerOption { Value = 1, Label = "Strongly Disagree" , PsychologyTestId = 4},
                        //new AnswerOption { Value = 2, Label = "Disagree", PsychologyTestId = 4},
                        //new AnswerOption { Value = 3, Label = "Agree",PsychologyTestId = 4},
                        //new AnswerOption { Value = 4, Label = "Strongly Agree" , PsychologyTestId = 4}

                    };
                    var lst = new List<PsychologyTestQuestion>
                            {
                                // ✅ Holland Career Test (TestId = 1)
                                new PsychologyTestQuestion {  PsychologyTestId = 1, QuestionText = "من دوست دارم روی ماشین کار کنم.", QuestionType = "RatingScale", CorrectAnswer = "R", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion {  PsychologyTestId = 1, QuestionText = "من دوست دارم پازل درست کنم.", QuestionType = "RatingScale", CorrectAnswer = "I", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion {  PsychologyTestId = 1, QuestionText = "من در کار مستقل خوب هستم.", QuestionType = "RatingScale", CorrectAnswer = "I", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = answerOptions },
                                new PsychologyTestQuestion {  PsychologyTestId = 1, QuestionText = "من دوست دارم رهبری یک گروه را بر عهده بگیرم.", QuestionType = "RatingScale", CorrectAnswer = "E", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = answerOptions },
                                new PsychologyTestQuestion {  PsychologyTestId = 1, QuestionText = "من از نوشتن خلاقانه لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "A", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = answerOptions },
                                new PsychologyTestQuestion {  PsychologyTestId = 1, QuestionText = "من ترجیح می‌دهم در فضای باز کار کنم.", QuestionType = "RatingScale", CorrectAnswer = "R", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion {  PsychologyTestId = 1, QuestionText = "من دوست دارم به مردم در مشکلاتشان کمک کنم.", QuestionType = "RatingScale", CorrectAnswer = "S", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion {  PsychologyTestId = 1, QuestionText = "من از سازماندهی چیزهایی مثل فایل‌ها و گزارش‌ها لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = answerOptions },
                                new PsychologyTestQuestion {  PsychologyTestId = 1, QuestionText = "از تلاش برای تأثیرگذاری یا متقاعد کردن مردم لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "E", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion {  PsychologyTestId = 1, QuestionText = "من دوست دارم چیزهایی را با دستانم بسازم.", QuestionType = "RatingScale", CorrectAnswer = "R", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion {  PsychologyTestId = 1, QuestionText = "من از تعمیر وسایل برقی لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "R", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion {  PsychologyTestId = 1, QuestionText = "من کارهای ساختارمند را ترجیح می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion {  PsychologyTestId = 1, QuestionText = "من به خلاقیت در کار اهمیت می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "A", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion {  PsychologyTestId = 1, QuestionText = "از حل کردن مسائل ریاضی لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "I", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion {  PsychologyTestId = 1, QuestionText = "دوست دارم در یادگیری به دیگران کمک کنم.", QuestionType = "RatingScale", CorrectAnswer = "S", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion {  PsychologyTestId = 1, QuestionText = "از کار با ابزار لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "R", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion {  PsychologyTestId = 1, QuestionText = "من شغلی با دستورالعمل‌های واضح را ترجیح می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion {  PsychologyTestId = 1, QuestionText = "من دوست دارم مردم را متقاعد کنم که با من موافق باشند.", QuestionType = "RatingScale", CorrectAnswer = "E", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = answerOptions },
                                new PsychologyTestQuestion {  PsychologyTestId = 1, QuestionText = "من از باغبانی یا محوطه سازی لذت می برم.", QuestionType = "RatingScale", CorrectAnswer = "R", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = answerOptions },
                                new PsychologyTestQuestion {  PsychologyTestId = 1, QuestionText = "من عاشق آزمایش کردن و کشف چیزهای جدید هستم.", QuestionType = "RatingScale", CorrectAnswer = "I", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},

                                // Big Five Personality Test (TestId = 2)
                                new PsychologyTestQuestion { PsychologyTestId = 2, QuestionText = "من به راحتی با دیگران ارتباط برقرار می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "E", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 2, QuestionText = "من احساسات دیگران را به خوبی درک می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "A", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = answerOptions },
                                new PsychologyTestQuestion { PsychologyTestId = 2, QuestionText = "من معمولاً برای رسیدن به اهدافم برنامه ریزی می کنم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = answerOptions },
                                new PsychologyTestQuestion { PsychologyTestId = 2, QuestionText = "من اغلب احساس اضطراب یا نگرانی می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "N", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = answerOptions },
                                new PsychologyTestQuestion { PsychologyTestId = 2, QuestionText = "به تجربه‌های جدید و متفاوت علاقه‌مندم.", QuestionType = "RatingScale", CorrectAnswer = "O", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 2, QuestionText = "در موقعیت‌های اجتماعی فعال و پرانرژی هستم.", QuestionType = "RatingScale", CorrectAnswer = "E", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 2, QuestionText = "من به نیازهای دیگران توجه زیادی می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "A", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 2, QuestionText = "من وظایفم را به موقع انجام می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 2, QuestionText = "من به راحتی دچار استرس می‌شوم.", QuestionType = "RatingScale", CorrectAnswer = "N", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 2, QuestionText = " من از هنر، موسیقی و ادبیات لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "O", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 2, QuestionText = "من اغلب شروع کننده گفتگو با دیگران هستم.", QuestionType = "RatingScale", CorrectAnswer = "E", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 2, QuestionText = "من نسبت به احساسات اطرافیانم حساس هستم.", QuestionType = "RatingScale", CorrectAnswer = "A", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 2, QuestionText = "من آدم مسئولیت پذیری هستم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 2, QuestionText = "من نوسانات خلقی مکرری را تجربه می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "N", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 2, QuestionText = "من پذیرای ایده‌های جدید هستم.", QuestionType = "RatingScale", CorrectAnswer = "O", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 2, QuestionText = "از گذراندن وقت با دیگران لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "E", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 2, QuestionText = "من به نیازهای دیگران توجه دارم.", QuestionType = "RatingScale", CorrectAnswer = "A", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 2, QuestionText = "من همیشه کارهای ناتمام را تمام می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 2, QuestionText = "من به راحتی دچار استرس می‌شوم.", QuestionType = "RatingScale", CorrectAnswer = "N", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 2, QuestionText = "از یادگیری چیزهای جدید لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "O", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},

                                // DISC Personality Test (TestId = 3)
                                new PsychologyTestQuestion { PsychologyTestId = 3, QuestionText = "در موقعیت‌های گروهی مسئولیت را به عهده می‌گیرم.", QuestionType = "RatingScale", CorrectAnswer = "D", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 3, QuestionText = "من از الهام بخشیدن به دیگران با ایده‌هایم لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "I", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 3, QuestionText = "من یک محیط کاری ثابت و قابل پیش‌بینی را ترجیح می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "S", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 3, QuestionText = "من در کارم خیلی به جزئیات اهمیت می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = answerOptions },
                                new PsychologyTestQuestion { PsychologyTestId = 3, QuestionText = "من روی رسیدن سریع به نتایج تمرکز می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "D", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 3, QuestionText = "از معاشرت و شبکه‌سازی لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "I", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 3, QuestionText = "من برای هماهنگی و همکاری تیمی ارزش قائلم.", QuestionType = "RatingScale", CorrectAnswer = "S", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 3, QuestionText = "من قوانین و رویه‌ها را به دقت دنبال می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 3, QuestionText = "من اهل رقابت و هدف هستم.", QuestionType = "RatingScale", CorrectAnswer = "D", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 3, QuestionText = "من مشتاق پروژه‌های جدید هستم.", QuestionType = "RatingScale", CorrectAnswer = "I", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 3, QuestionText = "من یک روال ثابت را ترجیح می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "S", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 3, QuestionText = "من از صحت کارم اطمینان دارم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 3, QuestionText = "من سریع تصمیم می‌گیرم.", QuestionType = "RatingScale", CorrectAnswer = "D", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 3, QuestionText = "از انگیزه دادن به دیگران لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "I", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 3, QuestionText = "من در شرایط سخت صبور هستم.", QuestionType = "RatingScale", CorrectAnswer = "S", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 3, QuestionText = "من کارم را برای یافتن خطاها دو بار بررسی می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 3, QuestionText = "از ریسک کردن برای رسیدن به اهداف لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "D", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 3, QuestionText = "من در محیط‌های گروهی خوش‌بین هستم.", QuestionType = "RatingScale", CorrectAnswer = "I", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 3, QuestionText = "من برای وفاداری در تیم‌ها ارزش قائلم.", QuestionType = "RatingScale", CorrectAnswer = "S", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 3, QuestionText = "من در کارم کیفیت را در اولویت قرار می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},

                                // Emotional Intelligence Test (TestId = 4)
                                new PsychologyTestQuestion { PsychologyTestId = 4, QuestionText = "من از احساساتم به محض وقوع آنها آگاه هستم.", QuestionType = "RatingScale", CorrectAnswer = "SA", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 4, QuestionText = "من می‌توانم تکانه‌هایم را به طور مؤثر کنترل کنم.", QuestionType = "RatingScale", CorrectAnswer = "SR", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 4, QuestionText = "من برای رسیدن به اهدافم انگیزه دارم.", QuestionType = "RatingScale", CorrectAnswer = "M", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 4, QuestionText = "می‌توانم حس کنم دیگران چه احساسی دارند.", QuestionType = "RatingScale", CorrectAnswer = "E", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 4, QuestionText = "من روابط قوی با دیگران برقرار می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "SS", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 4, QuestionText = "من نقاط قوت و ضعف خودم را تشخیص می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "SA", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 4, QuestionText = "من تحت فشار آرام می‌مانم.", QuestionType = "RatingScale", CorrectAnswer = "SR", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 4, QuestionText = "من به کارم علاقه و اشتیاق دارم.", QuestionType = "RatingScale", CorrectAnswer = "M", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 4, QuestionText = "من دیدگاه‌های دیگران را درک می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "E", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 4, QuestionText = "من به طور مؤثر با دیگران ارتباط برقرار می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "SS", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 4, QuestionText = "من به واکنش‌های احساسی‌ام فکر می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "SA", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 4, QuestionText = "من در مواقع اختلاف و درگیری، احساساتم را مدیریت می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "SR", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 4, QuestionText = "من با وجود شکست‌ها انگیزه‌ام را حفظ می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "M", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 4, QuestionText = "من نسبت به دیگران همدلی نشان می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "E", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 4, QuestionText = "من اختلافات را به طور سازنده حل می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "SS", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 4, QuestionText = "من محرک‌های احساسی‌ام را درک می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "SA", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 4, QuestionText = "من رفتارم را با موقعیت‌ها تطبیق می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "SR", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 4, QuestionText = "من با اشتیاق اهداف را دنبال می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "M", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 4, QuestionText = "من به طور فعال به دیگران گوش می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "E", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 4, QuestionText = "من در تیم‌ها به خوبی همکاری می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "SS", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},

                                // Cognitive Ability Test (TestId = 5)
                                new PsychologyTestQuestion { PsychologyTestId = 5, QuestionText = "می‌توانم الگوها را در مسائل پیچیده تشخیص دهم.", QuestionType = "RatingScale", CorrectAnswer = "L", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 5, QuestionText = "من در حل مسائل عددی مهارت دارم.", QuestionType = "RatingScale", CorrectAnswer = "N", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 5, QuestionText = "من دستورالعمل‌های پیچیده‌ی کتبی را می‌فهمم.", QuestionType = "RatingScale", CorrectAnswer = "V", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 5, QuestionText = "من می‌توانم اشیاء را در فضای سه‌بعدی تجسم کنم.", QuestionType = "RatingScale", CorrectAnswer = "triad", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 5, QuestionText = "من به سرعت معماهای استدلال منطقی را حل می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "L", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 5, QuestionText = "من در محاسبات ذهنی مهارت دارم.", QuestionType = "RatingScale", CorrectAnswer = "N", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 5, QuestionText = "من به راحتی می‌توانم مفاهیم انتزاعی را تفسیر کنم.", QuestionType = "RatingScale", CorrectAnswer = "V", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 5, QuestionText = "من می‌توانم اشیاء را به طور ذهنی و با دقت بچرخانم.", QuestionType = "RatingScale", CorrectAnswer = "S", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 5, QuestionText = "من در حل مسائل تحلیلی عالی هستم.", QuestionType = "RatingScale", CorrectAnswer = "L", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 5, QuestionText = "من می‌توانم درصدها را سریع محاسبه کنم.", QuestionType = "RatingScale", CorrectAnswer = "N", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 5, QuestionText = "من در درک قیاس‌های کلامی مهارت دارم.", QuestionType = "RatingScale", CorrectAnswer = "V", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 5, QuestionText = "من می‌توانم روابط فضایی را به خوبی تجسم کنم.", QuestionType = "RatingScale", CorrectAnswer = "S", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 5, QuestionText = "من مسائل پیچیده را به صورت سیستماتیک حل می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "L", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 5, QuestionText = "من در استدلال عددی مهارت دارم.", QuestionType = "RatingScale", CorrectAnswer = "N", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 5, QuestionText = "می‌توانم دستورالعمل‌های شفاهی دقیق را درک کنم.", QuestionType = "RatingScale", CorrectAnswer = "V", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 5, QuestionText = "من در کارهای استدلال فضایی مهارت دارم.", QuestionType = "RatingScale", CorrectAnswer = "S", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 5, QuestionText = "از مسائل منطقی چالش‌برانگیز لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "L", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 5, QuestionText = "من می‌توانم محاسبات ذهنی سریع انجام دهم.", QuestionType = "RatingScale", CorrectAnswer = "N", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 5, QuestionText = "من متون نوشتاری پیچیده را می‌فهمم.", QuestionType = "RatingScale", CorrectAnswer = "V", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 5, QuestionText = "من می‌توانم معماهای فضایی را به طور مؤثر حل کنم.", QuestionType = "RatingScale", CorrectAnswer = "S", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},

                                // Situational Judgment Test (SJT) (TestId = 6)
                                new PsychologyTestQuestion { PsychologyTestId = 6, QuestionText = "وقتی یکی از اعضای تیم، ضرب‌الاجل را از دست می‌دهد، من فوراً و به طور سازنده به آن رسیدگی می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "Leadership", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 6, QuestionText = "هنگام حل اختلافات در محل کار، آرامش خود را حفظ می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "Conflict Resolution", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 6, QuestionText = "من وظایف را به طور مؤثر در مهلت‌های محدود اولویت‌بندی می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "Time Management", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 6, QuestionText = "من برای دستیابی به اهداف تیمی با همکارانم همکاری می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "Teamwork", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 6, QuestionText = "من در موقعیت‌های چالش‌برانگیز، تصمیمات اخلاقی می‌گیرم.", QuestionType = "RatingScale", CorrectAnswer = "Ethics", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 6, QuestionText = "من در طول جلسات تیمی به طور واضح ارتباط برقرار می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "Communication", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 6, QuestionText = "من با تغییرات غیرمنتظره در برنامه‌های پروژه سازگار می‌شوم.", QuestionType = "RatingScale", CorrectAnswer = "Adaptability", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 6, QuestionText = "من به همکاران بازخورد سازنده ارائه می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "Leadership", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 6, QuestionText = "من در طول بحث‌های مربوط به اختلاف نظر، فعالانه گوش می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "Conflict Resolution", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 6, QuestionText = "من زمانم را طوری مدیریت می‌کنم که به مهلت‌های پروژه برسم.", QuestionType = "RatingScale", CorrectAnswer = "Time Management", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 6, QuestionText = "من به طور مؤثر در پروژه‌های تیمی مشارکت می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "Teamwork", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 6, QuestionText = "من در تصمیم‌گیری‌ها به اصول اخلاقی پایبند هستم.", QuestionType = "RatingScale", CorrectAnswer = "Ethics", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 6, QuestionText = "من ایده‌ها را به روشنی برای ذینفعان بیان می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "Communication", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 6, QuestionText = "من به سرعت با اولویت‌های کاری جدید سازگار می‌شوم.", QuestionType = "RatingScale", CorrectAnswer = "Adaptability", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 6, QuestionText = "من در طول چالش‌ها به تیمم انگیزه می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "Leadership", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 6, QuestionText = "من اختلافات را با انصاف حل می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "Conflict Resolution", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 6, QuestionText = "من کارها را برای بهینه‌سازی بهره‌وری اولویت‌بندی می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "Time Management", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 6, QuestionText = "من در محیط‌های مشارکتی خوب کار می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "Teamwork", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 6, QuestionText = "من تصمیماتی می‌گیرم که با ارزش‌های شرکت همسو باشند.", QuestionType = "RatingScale", CorrectAnswer = "Ethics", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                                new PsychologyTestQuestion { PsychologyTestId = 6, QuestionText = "من رویکردم را با چالش‌های جدید تطبیق می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "Adaptability", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow , AnswerOptions = answerOptions},
                            };
                    context.PsychologyTestQuestions.AddRange(lst);
                    var response = await context.SaveChangesAsync();

                }

                if (!context.Companies.Any())
                {
                    context.Companies.AddRange(
                        new Company
                        {
                            //   Id = 1,
                            Name = "Digikala",
                            Logo = "/images/companies/digikala.png",
                            Description = "Largest e-commerce company in Iran",
                            Industry = "E-commerce",
                            Location = "Tehran, Iran",
                            Website = "https://digikala.com",
                            Size = JobFinder.Contracts.Enums.CompanySize.Large,
                            FoundedDate = new DateTime(2007, 1, 1),
                            //CreatedAt = DateTime.Now
                            ContactPhone = "09125274263",
                            Benefits = new List<CompanyBenefit>(),
                            ContactEmail = "placeholder.com",
                            IsActive = true,
                            IsVerified = true,
                            Jobs = new List<Job>(),
                            DateCreated = DateTime.Now,
                            DateModified = DateTime.Now,
                            UserId = "5542BA7C-C896-4500-85F3-2E1E1197122F",
                            CityId = 1,// cities.FirstOrDefault(x => x.Id == 1).Id,
                            ProvinceId = 1,
                            Advertisements = new List<Advertisement>(),
                            LogoUrl = "",
                            Rating = 5,
                            JobCategoryId = null,

                        },
                        new Company
                        {
                            //   Id = 2,
                            Name = "Snapp",
                            Logo = "/images/companies/snapp.png",
                            Description = "Ride-hailing service in Iran",
                            Industry = "Transportation",
                            Location = "Tehran, Iran",
                            Website = "https://snapp.ir",
                            Size = JobFinder.Contracts.Enums.CompanySize.Large,
                            FoundedDate = new DateTime(2014, 1, 1),
                            ContactPhone = "09125274263",
                            Benefits = new List<CompanyBenefit>(),
                            ContactEmail = "placeholder.com",
                            IsActive = true,
                            IsVerified = true,
                            Jobs = new List<Job>(),
                            DateCreated = DateTime.Now,
                            DateModified = DateTime.Now,
                            UserId = "5542BA7C-C896-4500-85F3-2E1E1197122F",
                            //CreatedAt = DateTime.Now
                            CityId = 1,// cities.FirstOrDefault(x => x.Id == 1).Id,
                            ProvinceId = 1,
                            Advertisements = new List<Advertisement>(),
                            LogoUrl = "",
                            Rating = 5,
                            JobCategoryId = null,
                        },
                        new Company
                        {
                            //  Id = 3,
                            Name = "Cafe Bazaar",
                            Logo = "/images/companies/cafebazaar.png",
                            Description = "Android marketplace for Persian-speaking countries",
                            Industry = "Technology",
                            Location = "Tehran, Iran",
                            Website = "https://cafebazaar.ir",
                            Size = JobFinder.Contracts.Enums.CompanySize.Large,
                            FoundedDate = new DateTime(2010, 1, 1),
                            ContactPhone = "09125274263",
                            Benefits = new List<CompanyBenefit>(),
                            ContactEmail = "placeholder.com",
                            IsActive = true,
                            IsVerified = true,
                            Jobs = new List<Job>(),
                            DateCreated = DateTime.Now,
                            DateModified = DateTime.Now,
                            UserId = "5542BA7C-C896-4500-85F3-2E1E1197122F",
                            CityId = 1,// cities.FirstOrDefault(x => x.Id == 1).Id,
                            ProvinceId = 1,
                            Advertisements = new List<Advertisement>(),
                            LogoUrl = "",
                            Rating = 5,
                            JobCategoryId = null,
                            //CreatedAt = DateTime.Now
                        }
                    );
                    result = await context.SaveChangesAsync();

                }
                // context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Cities OFF");
                //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Provinces OFF");

                if (!context.TermsOfServices.Any())
                {
                    var termsOfService = new TermsOfService
                    {
                        Version = "1.0",
                        LastUpdated = "2024-05-15",
                        DateCreated = DateTime.Now,
                        IsActive = true,
                    };
                    await context.TermsOfServices.AddRangeAsync(termsOfService);
                    var id = await context.SaveChangesAsync();
                    var sections = new List<TermsSection>
                    {
                        new TermsSection
                        {
                            Id = 1,
                            TermsOfServiceId =id, // ارجاع به TermsOfService با Id = 1
                            Title = "مقدمه",
                            Content = "به پلتفرم کاریابی ما خوش آمدید. این شرایط سرویس، قوانین و مقررات استفاده از خدمات ما را برای کارجویان و کارفرمایان تعیین می‌کند. با استفاده از پلتفرم، شما موافقت خود را با این شرایط اعلام می‌کنید.",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new TermsSection
                        {
                            Id = 2,
                            TermsOfServiceId = id,
                            Title = "تعهدات کارجویان",
                            Content = "کارجویان متعهد می‌شوند اطلاعات هویتی و رزومه خود را به صورت دقیق و کامل وارد کنند. هرگونه اطلاعات نادرست، ممکن است منجر به حذف حساب کاربری شود. ارسال رزومه برای موقعیت‌های شغلی نامرتبط، مجاز نیست.",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new TermsSection
                        {
                            Id = 3,
                            TermsOfServiceId = id,
                            Title = "تعهدات کارفرمایان",
                            Content = "کارفرمایان متعهد می‌شوند اطلاعات شرکت و فرصت‌های شغلی را به صورت صحیح و شفاف درج کنند. هرگونه آگهی شغلی که شامل تبعیض یا محتوای غیرقانونی باشد، حذف خواهد شد. کارفرمایان باید در زمان مشخص به درخواست‌های ارسالی پاسخ دهند.",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new TermsSection
                        {
                            Id = 4,
                            TermsOfServiceId = id,
                            Title = "حریم خصوصی و حفاظت از داده‌ها",
                            Content = "ما متعهد به حفاظت از اطلاعات شخصی کاربران هستیم. اطلاعات جمع‌آوری شده تنها با رضایت صریح شما و به منظور بهبود خدمات، در اختیار طرفین (کارجو و کارفرما) قرار خواهد گرفت. برای اطلاعات بیشتر، لطفاً سیاست حفظ حریم خصوصی ما را مطالعه کنید.",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new TermsSection
                        {
                            Id = 5,
                            TermsOfServiceId = id,
                            Title = "مالکیت معنوی",
                            Content = "کلیه حقوق مالکیت معنوی پلتفرم، از جمله لوگو، محتوا و طراحی، متعلق به ما است. هرگونه استفاده غیرمجاز از این موارد پیگرد قانونی دارد.",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new TermsSection
                        {
                            Id = 6,
                            TermsOfServiceId = id,
                            Title = "محدودیت مسئولیت",
                            Content = "پلتفرم ما تنها واسطه بین کارجو و کارفرما است. ما هیچگونه مسئولیتی در قبال صحت اطلاعات درج شده توسط کاربران یا نتایج مصاحبه‌ها و استخدام‌ها نداریم. مسئولیت نهایی بر عهده طرفین است.",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new TermsSection
                        {
                             Id = 7,
                            TermsOfServiceId = id,
                            Title = "حل اختلافات",
                            Content = "در صورت بروز هرگونه اختلاف، ابتدا تلاش می‌شود از طریق مذاکره و میانجی‌گری حل و فصل شود. در غیر این صورت، حل اختلاف از طریق مراجع قانونی صالح پیگیری خواهد شد.",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        }
                    };

                    //termsOfService.Sections = sections;

                    await context.TermsSections.AddRangeAsync(sections);
                    var ids = await context.SaveChangesAsync();
                }
            }
        }




        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<WriteDbContext.WriteDbContext>();

                    if (_context.Database.IsSqlServer())
                    {
                        var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
                        if (pendingMigrations.Any())
                        {
                            // Apply migrations only if schema doesn't match
                            var appliedMigrations = await _context.Database.GetAppliedMigrationsAsync();
                            if (!appliedMigrations.Any())
                            {
                                // If no migrations are applied but tables exist, mark initial migration as applied
                                //await _context.Database.ExecuteSqlRawAsync(
                                //    "INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) VALUES ('20250428173255_InitialCreate', '8.0.0')");
                            }
                            else
                            {
                                await _context.Database.MigrateAsync();
                            }
                        }
                        else
                        {
                            Console.WriteLine("No pending migrations.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "An error occurred while initializing the database");
                throw;
            }
        }

        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            try
            {
                await TrySeedAsync(serviceProvider);
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, "An error occurred while seeding the database");
                throw;
            }
        }

        private static async Task TrySeedAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<WriteDbContext.WriteDbContext>();

                // Seed PricingCategories if none exist
                if (!_context.PricingCategories.Any())
                {
                    _context.PricingCategories.AddRange(GetPreconfiguredPricingCategories());
                }

                // Seed Features if none exist
                if (!_context.Features.Any())
                {
                    _context.Features.AddRange(GetPreconfiguredFeatures());
                }

                // Seed MenuItems if none exist
                if (!_context.MenuItems.Any())
                {
                    _context.MenuItems.AddRange(GetPreconfiguredMenuItems());
                }
                if (!_context.JobCategories.Any())
                {
                    // _context.JobCategories.AddRange(GetPreconfiguredJobCategory());
                }
                await _context.SaveChangesAsync();
            }

        }

        private static IEnumerable<PricingCategory> GetPreconfiguredPricingCategories()
        {
            return new List<PricingCategory>
            {
                // English categories
                new PricingCategory
                {
                    Name = "Standard Job Ads",
                    Description = "Basic job postings",
                    IconName = "work",
                    Language = "en",
                    Plans = new List<PricingPlan>
                    {
                        new PricingPlan
                        {
                            Title = "1 Standard Job Ad",
                            Subtitle = "Basic package",
                            Price = 395000,
                            Currency = "Toman",
                            Duration = 30,
                            Name = "1 Standard Job Ad",
                            DurationUnit = "days",
                            JobCount = 1,
                            Features = new List<PricingFeature>
                            {
                                new PricingFeature { Description = "Post job for 30 days" ,IconName = "work"},

                                new PricingFeature { Description = "Access to applicant tracking system" ,IconName = "work"},
                                new PricingFeature { Description = "Basic resume search" ,IconName = "work"},
                                new PricingFeature { Description = "Email notifications" ,IconName = "work"}
                            },
                            ButtonText = "Register and Buy",
                            Type = "standard"
                        },
                        new PricingPlan
                        {
                            Title = "5 Standard Job Ads",
                            Name = "5 Standard Job Ads",
                            Subtitle = "Medium package",
                            Price = 1875000,
                            Currency = "Toman",
                            Duration = 30,
                            DurationUnit = "days",
                            JobCount = 5,
                            Features = new List<PricingFeature>
                            {
                                new PricingFeature { Description = "Post jobs for 30 days",IconName = "work" },
                                new PricingFeature { Description = "Access to applicant tracking system" ,IconName = "work"},
                                new PricingFeature { Description = "Advanced resume search" ,IconName = "work"},
                                new PricingFeature { Description = "Email notifications" ,IconName = "work"},
                                new PricingFeature { Description = "Basic analytics" ,IconName = "work"}
                            },
                            ButtonText = "Register and Buy",
                            Type = "standard"
                        },
                        new PricingPlan
                        {
                            Title = "10 Standard Job Ads",
                            Name = "10 Standard Job Ads",
                            Subtitle = "Large package",
                            Price = 3495000,
                            Currency = "Toman",
                            Duration = 30,
                            DurationUnit = "days",
                            JobCount = 10,
                            Features = new List<PricingFeature>
                            {
                                new PricingFeature { Description = "Post jobs for 30 days",IconName = "work" },
                                new PricingFeature { Description = "Access to applicant tracking system" ,IconName = "work"},
                                new PricingFeature { Description = "Advanced resume search",IconName = "work" },
                                new PricingFeature { Description = "Email notifications",IconName = "work" },
                                new PricingFeature { Description = "Advanced analytics" ,IconName = "work"},
                                new PricingFeature { Description = "Priority support" ,IconName = "work"}
                            },
                            ButtonText = "Register and Buy",
                            Type = "standard"
                        }
                    }
                },
                new PricingCategory
                {
                    Name = "Featured Job Ads",
                    Description = "Highlighted job postings",
                    IconName = "star",
                    Language = "en",
                    Plans = new List<PricingPlan>
                    {
                        new PricingPlan
                        {
                            Title = "1 Featured Job Ad",
                            Name = "1 Featured Job Ad",
                            Subtitle = "Basic featured package",
                            Price = 595000,
                            Currency = "Toman",
                            Duration = 30,
                            DurationUnit = "days",
                            JobCount = 1,
                            Features = new List<PricingFeature>
                            {
                                new PricingFeature { Description = "Post job for 30 days" ,IconName = "star"},
                                new PricingFeature { Description = "Highlighted in search results",IconName = "star" },
                                new PricingFeature { Description = "Access to applicant tracking system" ,IconName = "star"},
                                new PricingFeature { Description = "Advanced resume search" ,IconName = "star"},
                                new PricingFeature { Description = "Email notifications" ,IconName = "star"}
                            },
                            ButtonText = "Register and Buy",
                            Type = "featured"
                        },
                        new PricingPlan
                        {
                            Title = "5 Featured Job Ads",
                            Name = "5 Featured Job Ads",
                            Subtitle = "Medium featured package",
                            Price = 2750000,
                            Currency = "Toman",
                            Duration = 30,
                            DurationUnit = "days",
                            JobCount = 5,
                            Features = new List<PricingFeature>
                            {
                                new PricingFeature { Description = "Post jobs for 30 days" ,IconName = "star"},
                                new PricingFeature { Description = "Highlighted in search results" ,IconName = "star"},
                                new PricingFeature { Description = "Access to applicant tracking system" ,IconName = "star"},
                                new PricingFeature { Description = "Advanced resume search" ,IconName = "star"},
                                new PricingFeature { Description = "Email notifications",IconName = "star" },
                                new PricingFeature { Description = "Basic analytics",IconName = "star" }
                            },
                            ButtonText = "Register and Buy",
                            Type = "featured"
                        },
                        new PricingPlan
                        {
                            Title = "10 Featured Job Ads",
                            Name = "10 Featured Job Ads",
                            Subtitle = "Large featured package",
                            Price = 4995000,
                            Currency = "Toman",
                            Duration = 30,
                            DurationUnit = "days",
                            JobCount = 10,
                            Features = new List<PricingFeature>
                            {
                                new PricingFeature { Description = "Post jobs for 30 days",IconName = "star" },
                                new PricingFeature { Description = "Highlighted in search results",IconName = "star" },
                                new PricingFeature { Description = "Access to applicant tracking system",IconName = "star" },
                                new PricingFeature { Description = "Advanced resume search" ,IconName = "star"},
                                new PricingFeature { Description = "Email notifications" ,IconName = "star"},
                                new PricingFeature { Description = "Basic analytics" ,IconName = "star"},
                                new PricingFeature { Description = "Priority support" ,IconName = "star"}
                            },
                            ButtonText = "Register and Buy",
                            Type = "featured"
                        }
                    }
                },
                new PricingCategory
                {
                    Name = "Special Job Ads",
                    Description = "Premium job postings",
                    IconName = "workspace_premium",
                    Language = "en",
                    Plans = new List<PricingPlan>
                    {
                        new PricingPlan
                        {
                            Title = "1 Special Job Ad",
                            Name = "1 Special Job Ad",
                            Subtitle = "Basic premium package",
                            Price = 795000,
                            Currency = "Toman",
                            Duration = 30,
                            DurationUnit = "days",
                            JobCount = 1,
                            Features = new List<PricingFeature>
                            {
                                new PricingFeature { Description = "Post job for 30 days" ,IconName = "workspace_premium"},
                                new PricingFeature { Description = "Top placement in search results" ,IconName = "workspace_premium"},
                                new PricingFeature { Description = "Featured on homepage",IconName = "workspace_premium" },
                                new PricingFeature { Description = "Access to applicant tracking system",IconName = "workspace_premium" },
                                new PricingFeature { Description = "Advanced resume search" ,IconName = "workspace_premium"},
                                new PricingFeature { Description = "Email notifications",IconName = "workspace_premium" }
                            },
                            ButtonText = "Register and Buy",
                            Type = "special"
                        },
                        new PricingPlan
                        {
                            Title = "5 Special Job Ads",
                            Name = "5 Special Job Ads",
                            Subtitle = "Medium premium package",
                            Price = 3750000,
                            Currency = "Toman",
                            Duration = 30,
                            DurationUnit = "days",
                            JobCount = 5,
                            Features = new List<PricingFeature>
                            {
                                new PricingFeature { Description = "Post jobs for 30 days" ,IconName = "workspace_premium"},
                                new PricingFeature { Description = "Top placement in search results",IconName = "workspace_premium" },
                                new PricingFeature { Description = "Featured on homepage" ,IconName = "workspace_premium"},
                                new PricingFeature { Description = "Access to applicant tracking system",IconName = "workspace_premium" },
                                new PricingFeature { Description = "Advanced resume search",IconName = "workspace_premium" },
                                new PricingFeature { Description = "Email notifications" ,IconName = "workspace_premium"},
                                new PricingFeature { Description = "Advanced analytics" ,IconName = "workspace_premium"}
                            },
                            ButtonText = "Register and Buy",
                            Type = "special"
                        },
                        new PricingPlan
                        {
                            Title = "10 Special Job Ads",
                            Name = "10 Special Job Ads",
                            Subtitle = "Large premium package",
                            Price = 6995000,
                            Currency = "Toman",
                            Duration = 30,
                            DurationUnit = "days",
                            JobCount = 10,
                            Features = new List<PricingFeature>
                            {
                                new PricingFeature { Description = "Post jobs for 30 days" ,IconName = "workspace_premium"},
                                new PricingFeature { Description = "Top placement in search results" ,IconName = "workspace_premium"},
                                new PricingFeature { Description = "Featured on homepage" ,IconName = "workspace_premium"},
                                new PricingFeature { Description = "Access to applicant tracking system" ,IconName = "workspace_premium"},
                                new PricingFeature { Description = "Advanced resume search",IconName = "workspace_premium" },
                                new PricingFeature { Description = "Email notifications",IconName = "workspace_premium" },
                                new PricingFeature { Description = "Advanced analytics",IconName = "workspace_premium" },
                                new PricingFeature { Description = "Priority support",IconName = "workspace_premium" },
                                new PricingFeature { Description = "Dedicated account manager",IconName = "workspace_premium" }
                            },
                            ButtonText = "Register and Buy",
                            Type = "special",
                            IsPopular = true
                        }
                    }
                },
                // Farsi categories
                new PricingCategory
                {
                    Name = "آگهی‌های شغلی استاندارد",
                    Description = "آگهی‌های شغلی پایه",
                    IconName = "work",
                    Language = "fa",
                    Plans = new List<PricingPlan>
                    {
                        new PricingPlan
                        {
                            Title = "1 آگهی شغلی استاندارد",
                            Name = "1 آگهی شغلی استاندارد",
                            Subtitle = "پکیج پایه",
                            Price = 395000,
                            Currency = "تومان",
                            Duration = 30,
                            DurationUnit = "روز",
                            JobCount = 1,
                            Features = new List<PricingFeature>
                            {
                                new PricingFeature { Description = "انتشار آگهی به مدت 30 روز",IconName = "work" },
                                new PricingFeature { Description = "دسترسی به سیستم پیگیری متقاضیان" ,IconName = "work"},
                                new PricingFeature { Description = "جستجوی پایه رزومه",IconName = "work" },
                                new PricingFeature { Description = "اعلان‌های ایمیل",IconName = "work" }
                            },
                            ButtonText = "ثبت نام و خرید",
                            Type = "standard"
                        },
                        new PricingPlan
                        {
                            Title = "5 آگهی شغلی استاندارد",
                            Name = "5 آگهی شغلی استاندارد",
                            Subtitle = "پکیج متوسط",
                            Price = 1875000,
                            Currency = "تومان",
                            Duration = 30,
                            DurationUnit = "روز",
                            JobCount = 5,
                            Features = new List<PricingFeature>
                            {
                                new PricingFeature { Description = "انتشار آگهی به مدت 30 روز" ,IconName = "work"},
                                new PricingFeature { Description = "دسترسی به سیستم پیگیری متقاضیان" ,IconName = "work"},
                                new PricingFeature { Description = "جستجوی پیشرفته رزومه",IconName = "work" },
                                new PricingFeature { Description = "اعلان‌های ایمیل" ,IconName = "work"},
                                new PricingFeature { Description = "تحلیل‌های پایه",IconName = "work" }
                            },
                            ButtonText = "ثبت نام و خرید",
                            Type = "standard"
                        },
                        new PricingPlan
                        {
                            Title = "10 آگهی شغلی استاندارد",
                            Name = "10 آگهی شغلی استاندارد",
                            Subtitle = "پکیج بزرگ",
                            Price = 3495000,
                            Currency = "تومان",
                            Duration = 30,
                            DurationUnit = "روز",
                            JobCount = 10,
                            Features = new List<PricingFeature>
                            {
                                new PricingFeature { Description = "انتشار آگهی به مدت 30 روز",IconName = "work" },
                                new PricingFeature { Description = "دسترسی به سیستم پیگیری متقاضیان" ,IconName = "work"},
                                new PricingFeature { Description = "جستجوی پیشرفته رزومه",IconName = "work" },
                                new PricingFeature { Description = "اعلان‌های ایمیل",IconName = "work" },
                                new PricingFeature { Description = "تحلیل‌های پیشرفته",IconName = "work" },
                                new PricingFeature { Description = "پشتیبانی اولویت‌دار",IconName = "work" }
                            },
                            ButtonText = "ثبت نام و خرید",
                            Type = "standard"
                        }
                    }
                },
                new PricingCategory
                {
                    Name = "آگهی‌های شغلی ویژه",
                    Description = "آگهی‌های شغلی برجسته",
                    IconName = "star",
                    Language = "fa",
                    Plans = new List<PricingPlan>
                    {
                        new PricingPlan
                        {
                            Title = "1 آگهی شغلی ویژه",
                            Name = "1 آگهی شغلی ویژه",
                            Subtitle = "پکیج ویژه پایه",
                            Price = 595000,
                            Currency = "تومان",
                            Duration = 30,
                            DurationUnit = "روز",
                            JobCount = 1,
                            Features = new List<PricingFeature>
                            {
                                new PricingFeature { Description = "انتشار آگهی به مدت 30 روز",    IconName = "star" },
                                new PricingFeature { Description = "برجسته در نتایج جستجو",    IconName = "star" },
                                new PricingFeature { Description = "دسترسی به سیستم پیگیری متقاضیان",    IconName = "star" },
                                new PricingFeature { Description = "جستجوی پیشرفته رزومه",    IconName = "star" },
                                new PricingFeature { Description = "اعلان‌های ایمیل",    IconName = "star" }
                            },
                            ButtonText = "ثبت نام و خرید",
                            Type = "featured"
                        },
                        new PricingPlan
                        {
                            Title = "5 آگهی شغلی ویژه",
                            Name = "5 آگهی شغلی ویژه",
                            Subtitle = "پکیج ویژه متوسط",
                            Price = 2750000,
                            Currency = "تومان",
                            Duration = 30,
                            DurationUnit = "روز",
                            JobCount = 5,
                            Features = new List<PricingFeature>
                            {
                                new PricingFeature { Description = "انتشار آگهی به مدت 30 روز",    IconName = "star" },
                                new PricingFeature { Description = "برجسته در نتایج جستجو" ,    IconName = "star"},
                                new PricingFeature { Description = "دسترسی به سیستم پیگیری متقاضیان",    IconName = "star" },
                                new PricingFeature { Description = "جستجوی پیشرفته رزومه" ,    IconName = "star"},
                                new PricingFeature { Description = "اعلان‌های ایمیل",    IconName = "star" },
                                new PricingFeature { Description = "تحلیل‌های پایه" ,    IconName = "star"}
                            },
                            ButtonText = "ثبت نام و خرید",
                            Type = "featured"
                        },
                        new PricingPlan
                        {
                            Title = "10 آگهی شغلی ویژه",
                            Name = "10 آگهی شغلی ویژه",
                            Subtitle = "پکیج ویژه بزرگ",
                            Price = 4995000,
                            Currency = "تومان",
                            Duration = 30,
                            DurationUnit = "روز",
                            JobCount = 10,
                            Features = new List<PricingFeature>
                            {
                                new PricingFeature { Description = "انتشار آگهی به مدت 30 روز",    IconName = "star" },
                                new PricingFeature { Description = "برجسته در نتایج جستجو",    IconName = "star" },
                                new PricingFeature { Description = "دسترسی به سیستم پیگیری متقاضیان",    IconName = "star" },
                                new PricingFeature { Description = "جستجوی پیشرفته رزومه",    IconName = "star" },
                                new PricingFeature { Description = "اعلان‌های ایمیل",    IconName = "star" },
                                new PricingFeature { Description = "تحلیل‌های پایه",    IconName = "star" },
                                new PricingFeature { Description = "پشتیبانی اولویت‌دار",    IconName = "star" }
                            },
                            ButtonText = "ثبت نام و خرید",
                            Type = "featured"
                        }
                    }
                },
                new PricingCategory
                {
                    Name = "آگهی‌های شغلی خاص",
                    Description = "آگهی‌های شغلی ویژه",
                    IconName = "workspace_premium",
                    Language = "fa",
                    Plans = new List<PricingPlan>
                    {
                        new PricingPlan
                        {
                            Title = "1 آگهی شغلی خاص",
                            Name = "1 آگهی شغلی خاص",
                            Subtitle = "پکیج ویژه پایه",
                            Price = 795000,
                            Currency = "تومان",
                            Duration = 30,
                            DurationUnit = "روز",
                            JobCount = 1,
                            Features = new List<PricingFeature>
                            {
                                new PricingFeature { Description = "انتشار آگهی به مدت 30 روز",IconName = "workspace_premium" },
                                new PricingFeature { Description = "جایگاه برتر در نتایج جستجو",IconName = "workspace_premium" },
                                new PricingFeature { Description = "نمایش در صفحه اصلی" ,IconName = "workspace_premium"},
                                new PricingFeature { Description = "دسترسی به سیستم پیگیری متقاضیان" ,IconName = "workspace_premium"},
                                new PricingFeature { Description = "جستجوی پیشرفته رزومه",IconName = "workspace_premium" },
                                new PricingFeature { Description = "اعلان‌های ایمیل" ,IconName = "workspace_premium"}
                            },
                            ButtonText = "ثبت نام و خرید",
                            Type = "special"
                        },
                        new PricingPlan
                        {
                            Title = "5 آگهی شغلی خاص",
                            Name = "5 آگهی شغلی خاص",
                            Subtitle = "پکیج ویژه متوسط",
                            Price = 3750000,
                            Currency = "تومان",
                            Duration = 30,
                            DurationUnit = "روز",
                            JobCount = 5,
                            Features = new List<PricingFeature>
                            {
                                new PricingFeature { Description = "انتشار آگهی به مدت 30 روز",IconName = "workspace_premium" },
                                new PricingFeature { Description = "جایگاه برتر در نتایج جستجو",IconName = "workspace_premium" },
                                new PricingFeature { Description = "نمایش در صفحه اصلی" ,IconName = "workspace_premium"},
                                new PricingFeature { Description = "دسترسی به سیستم پیگیری متقاضیان" ,IconName = "workspace_premium"},
                                new PricingFeature { Description = "جستجوی پیشرفته رزومه" ,IconName = "workspace_premium"},
                                new PricingFeature { Description = "اعلان‌های ایمیل",IconName = "workspace_premium" },
                                new PricingFeature { Description = "تحلیل‌های پیشرفته" ,IconName = "workspace_premium"}
                            },
                            ButtonText = "ثبت نام و خرید",
                            Type = "special"
                        },
                        new PricingPlan
                        {
                            Title = "10 آگهی شغلی خاص",
                            Name = "10 آگهی شغلی خاص",
                            Subtitle = "پکیج ویژه بزرگ",
                            Price = 6995000,
                            Currency = "تومان",
                            Duration = 30,
                            DurationUnit = "روز",
                            JobCount = 10,
                            Features = new List<PricingFeature>
                            {
                                new PricingFeature { Description = "انتشار آگهی به مدت 30 روز",IconName = "workspace_premium" },
                                new PricingFeature { Description = "جایگاه برتر در نتایج جستجو" ,IconName = "workspace_premium"},
                                new PricingFeature { Description = "نمایش در صفحه اصلی" ,IconName = "workspace_premium"},
                                new PricingFeature { Description = "دسترسی به سیستم پیگیری متقاضیان" ,IconName = "workspace_premium"},
                                new PricingFeature { Description = "جستجوی پیشرفته رزومه" ,IconName = "workspace_premium"},
                                new PricingFeature { Description = "اعلان‌های ایمیل",IconName = "workspace_premium" },
                                new PricingFeature { Description = "تحلیل‌های پیشرفته" ,IconName = "workspace_premium"},
                                new PricingFeature { Description = "پشتیبانی اولویت‌دار" ,IconName = "workspace_premium"},
                                new PricingFeature { Description = "مدیر حساب اختصاصی",IconName = "workspace_premium" }
                            },
                            ButtonText = "ثبت نام و خرید",
                            Type = "special",
                            IsPopular = true
                        }
                    }
                }
            };
        }

        private static IEnumerable<Feature> GetPreconfiguredFeatures()
        {
            return new List<Feature>
            {
                new Feature
                {
                    Title = "60 Day Job Display",
                    Description = "Your job ads will be visible on our platform for 60 days, giving you more time to find the right candidates.",
                    IconName = "calendar_today",
                    Language = "en"
                },
                new Feature
                {
                    Title = "100% Money Back Guarantee",
                    Description = "If you're not satisfied with our service, we offer a full refund within the first 7 days of your purchase.",
                    IconName = "settings",
                    Language = "en"
                },
                new Feature
                {
                    Title = "20% Discount on First Publication",
                    Description = "New employers get a 20% discount on their first job posting to help them get started with our platform.",
                    IconName = "local_offer",
                    Language = "en",
                },
                new Feature
                {
                    Title = "Resume Management System",
                    Description = "Our advanced resume management system helps you organize and track all applicants efficiently.",
                    IconName = "description",
                    Language = "en"
                },
                new Feature
                {
                    Title = "Invoice and Pre-invoice Generation",
                    Description = "Automatically generate invoices and pre-invoices for your purchases to simplify your accounting process.",
                    IconName = "receipt",
                    Language = "en"
                },
                new Feature
                {
                    Title = "Extensive Employer Support",
                    Description = "Our dedicated support team is available to help employers with any questions or issues they may encounter.",
                    IconName = "support_agent",
                    Language = "en"
                },
                // Farsi features
                new Feature
                {
                    Title = "نمایش آگهی شغلی به مدت 60 روز",
                    Description = "آگهی‌های شغلی شما به مدت 60 روز در پلتفرم ما قابل مشاهده خواهد بود، به شما زمان بیشتری برای یافتن نامزدهای مناسب می‌دهد.",
                    IconName = "calendar_today",
                    Language = "fa"
                },
                new Feature
                {
                    Title = "تضمین بازگشت 100% وجه",
                    Description = "اگر از خدمات ما راضی نیستید، در 7 روز اول خرید خود، مبلغ کامل را مسترد می‌کنیم.",
                    IconName = "settings",
                    Language = "fa"
                },
                new Feature
                {
                    Title = "20% تخفیف در اولین انتشار",
                    Description = "کارفرمایان جدید 20% تخفیف در اولین آگهی شغلی خود دریافت می‌کنند تا بتوانند با پلتفرم ما شروع کنند.",
                    IconName = "local_offer",
                    Language = "fa"
                },
                new Feature
                {
                    Title = "سیستم مدیریت رزومه",
                    Description = "سیستم پیشرفته مدیریت رزومه ما به شما کمک می‌کند تا تمام متقاضیان را به طور کارآمد سازماندهی و پیگیری کنید.",
                    IconName = "description",
                    Language = "fa"
                },
                new Feature
                {
                    Title = "تولید فاکتور و پیش‌فاکتور",
                    Description = "به طور خودکار فاکتورها و پیش‌فاکتورهای خرید شما را تولید می‌کند تا فرآیند حسابداری شما را ساده کند.",
                    IconName = "receipt",
                    Language = "fa"
                },
                new Feature
                {
                    Title = "پشتیبانی گسترده کارفرما",
                    Description = "تیم پشتیبانی اختصاصی ما برای کمک به کارفرمایان در مورد هر سوال یا مشکلی که ممکن است با آن مواجه شوند، در دسترس است.",
                    IconName = "support_agent",
                    Language = "fa"
                }
            };
        }

        private static IEnumerable<MenuItem> GetPreconfiguredMenuItems()
        {
            return new List<MenuItem>
            {
                new MenuItem { Title = "home", Url = "/" },
                new MenuItem { Title = "panel", Url = "panel" },
                new MenuItem { Title = "pricing", Url = "panel/pricing" },
                new MenuItem { Title = "resumeMaker", Url = "resumeMaker" },
                new MenuItem { Title = "jobs", Url = "jobs" },
                new MenuItem { Title = "resume", Url = "resume" },
                new MenuItem { Title = "resume", Url = "resume/create" },
                new MenuItem { Title = "resume", Url = "edit/:id" },
                new MenuItem { Title = "resume", Url = ":id" },
                new MenuItem { Title = "employer-faq", Url = "knowledge/employer-faq/pages/employer-tos" },
                new MenuItem { Title = "job-categories", Url = "job-categories" },
                new MenuItem { Title = "job-categoriesDetail", Url = "job-categories/:slug" },
                new MenuItem { Title = "employer-account-faq", Url = "knowledge/employer-faq/pages/employer-account-faq" },
                new MenuItem { Title = "employer-rules", Url = "knowledge/employer-faq/pages/employer-rules" },
                new MenuItem { Title = "register", Url = "register" },
            };
        }


        //private static IEnumerable<JobCategory> GetPreconfiguredJobCategory()
        //{
        //    return new List<JobCategory>
        //    {
        //        new JobCategory { Name = "Home", Url = "/" },
        //        new JobCategory { Name  = "Pricing", Url = "/pricing" },
        //        new JobCategory { Name  = "About Us", Url = "/about" },
        //        new JobCategory { Name  = "Contact", Url = "/contact" },
        //        new JobCategory { Name  = "Login", Url = "/login" }
        //    };
        //}
    }

}
