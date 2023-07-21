using GrowUp.DataAccess.Data;
using GrowUp.Model;
using GrowUp.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowUp.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {

        private readonly UserManager<IdentityUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _db;


        public DbInitializer(

            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext db)
        {

            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public void Initialize()
        {
            // migrations if they are not appliyed 

            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            // creat rools if they are not created 


            if (!_roleManager.RoleExistsAsync(StaticDetail.Role_Admin).GetAwaiter().GetResult())
            {

                _roleManager.CreateAsync(new IdentityRole(StaticDetail.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(StaticDetail.Role_User_Indi)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(StaticDetail.Role_Employee)).GetAwaiter().GetResult();

                _db.Countries.AddRange(new List<Country> {

                    new Country { CountryCode = "AF", CountryName = "Afghanistan" },
                    new Country { CountryCode = "AL", CountryName = "Albania" },
                    new Country { CountryCode = "DZ", CountryName = "Algeria" },
                    new Country { CountryCode = "AS", CountryName = "American Samoa" },
                    new Country { CountryCode = "AD", CountryName = "Andorra" },
                    new Country { CountryCode = "AO", CountryName = "Angola" },
                    new Country { CountryCode = "AI", CountryName = "Anguilla" },
                    new Country { CountryCode = "AQ", CountryName = "Antarctica" },
                    new Country { CountryCode = "AG", CountryName = "Antigua And Barbuda" },
                    new Country { CountryCode = "AR", CountryName = "Argentina" },
                    new Country { CountryCode = "AM", CountryName = "Armenia" },
                    new Country { CountryCode = "AW", CountryName = "Aruba" },
                    new Country { CountryCode = "AU", CountryName = "Australia" },
                    new Country { CountryCode = "AT", CountryName = "Austria" },
                    new Country { CountryCode = "AZ", CountryName = "Azerbaijan" },
                    new Country { CountryCode = "BS", CountryName = "Bahamas" },
                    new Country { CountryCode = "BH", CountryName = "Bahrain" },
                    new Country { CountryCode = "BD", CountryName = "Bangladesh" },
                    new Country { CountryCode = "BB", CountryName = "Barbados" },
                    new Country { CountryCode = "BY", CountryName = "Belarus" },
                    new Country { CountryCode = "BE", CountryName = "Belgium" },
                    new Country { CountryCode = "BZ", CountryName = "Belize" },
                    new Country { CountryCode = "BJ", CountryName = "Benin" },
                    new Country { CountryCode = "BM", CountryName = "Bermuda" },
                    new Country { CountryCode = "BT", CountryName = "Bhutan" },
                    new Country { CountryCode = "BO", CountryName = "Bolivia" },
                    new Country { CountryCode = "BA", CountryName = "Bosnia And Herzegowina" },
                    new Country { CountryCode = "BW", CountryName = "Botswana" },
                    new Country { CountryCode = "BV", CountryName = "Bouvet Island" },
                    new Country { CountryCode = "BR", CountryName = "Brazil" },
                    new Country { CountryCode = "IO", CountryName = "British Indian Ocean Territory" },
                    new Country { CountryCode = "BN", CountryName = "Brunei Darussalam" },
                    new Country { CountryCode = "BG", CountryName = "Bulgaria" },
                    new Country { CountryCode = "BF", CountryName = "Burkina Faso" },
                    new Country { CountryCode = "BI", CountryName = "Burundi" },
                    new Country { CountryCode = "KH", CountryName = "Cambodia" },
                    new Country { CountryCode = "CM", CountryName = "Cameroon" },
                    new Country { CountryCode = "CA", CountryName = "Canada" },
                    new Country { CountryCode = "CV", CountryName = "Cape Verde" },
                    new Country { CountryCode = "KY", CountryName = "Cayman Islands" },
                    new Country { CountryCode = "CF", CountryName = "Central African Republic" },
                    new Country { CountryCode = "TD", CountryName = "Chad" },
                    new Country { CountryCode = "CL", CountryName = "Chile" },
                    new Country { CountryCode = "CN", CountryName = "China" },
                    new Country { CountryCode = "CX", CountryName = "Christmas Island" },
                    new Country { CountryCode = "CC", CountryName = "Cocos (Keeling) Islands" },
                    new Country { CountryCode = "CO", CountryName = "Colombia" },
                    new Country { CountryCode = "KM", CountryName = "Comoros" },
                    new Country { CountryCode = "CG", CountryName = "Congo" },
                    new Country { CountryCode = "CK", CountryName = "Cook Islands" },
                    new Country { CountryCode = "CR", CountryName = "Costa Rica" },
                    new Country { CountryCode = "CI", CountryName = "Cote D'Ivoire" },
                    new Country { CountryCode = "HR", CountryName = "Croatia (Local Name: Hrvatska)" },
                    new Country { CountryCode = "CU", CountryName = "Cuba" },
                    new Country { CountryCode = "CY", CountryName = "Cyprus" },
                    new Country { CountryCode = "CZ", CountryName = "Czech Republic" },
                    new Country { CountryCode = "DK", CountryName = "Denmark" },
                    new Country { CountryCode = "DJ", CountryName = "Djibouti" },
                    new Country { CountryCode = "DM", CountryName = "Dominica" },
                    new Country { CountryCode = "DO", CountryName = "Dominican Republic" },
                    new Country { CountryCode = "TP", CountryName = "East Timor" },
                    new Country { CountryCode = "EC", CountryName = "Ecuador" },
                    new Country { CountryCode = "EG", CountryName = "Egypt" },
                    new Country { CountryCode = "SV", CountryName = "El Salvador" },
                    new Country { CountryCode = "GQ", CountryName = "Equatorial Guinea" },
                    new Country { CountryCode = "ER", CountryName = "Eritrea" },
                    new Country { CountryCode = "EE", CountryName = "Estonia" },
                    new Country { CountryCode = "ET", CountryName = "Ethiopia" },
                    new Country { CountryCode = "FK", CountryName = "Falkland Islands (Malvinas)" },
                    new Country { CountryCode = "FO", CountryName = "Faroe Islands" },
                    new Country { CountryCode = "FJ", CountryName = "Fiji" },
                    new Country { CountryCode = "FI", CountryName = "Finland" },
                    new Country { CountryCode = "FR", CountryName = "France" },
                    new Country { CountryCode = "GF", CountryName = "French Guiana" },
                    new Country { CountryCode = "PF", CountryName = "French Polynesia" },
                    new Country { CountryCode = "TF", CountryName = "French Southern Territories" },
                    new Country { CountryCode = "GA", CountryName = "Gabon" },
                    new Country { CountryCode = "GM", CountryName = "Gambia" },
                    new Country { CountryCode = "GE", CountryName = "Georgia" },
                    new Country { CountryCode = "DE", CountryName = "Germany" },
                    new Country { CountryCode = "GH", CountryName = "Ghana" },
                    new Country { CountryCode = "GI", CountryName = "Gibraltar" },
                    new Country { CountryCode = "GR", CountryName = "Greece" },
                    new Country { CountryCode = "GL", CountryName = "Greenland" },
                    new Country { CountryCode = "GD", CountryName = "Grenada" },
                    new Country { CountryCode = "GP", CountryName = "Guadeloupe" },
                    new Country { CountryCode = "GU", CountryName = "Guam" },
                    new Country { CountryCode = "GT", CountryName = "Guatemala" },
                    new Country { CountryCode = "GN", CountryName = "Guinea" },
                    new Country { CountryCode = "GW", CountryName = "Guinea-Bissau" },
                    new Country { CountryCode = "GY", CountryName = "Guyana" },
                    new Country { CountryCode = "HT", CountryName = "Haiti" },
                    new Country { CountryCode = "HM", CountryName = "Heard And Mc Donald Islands" },
                    new Country { CountryCode = "VA", CountryName = "Holy See (Vatican City State)" },
                    new Country { CountryCode = "HN", CountryName = "Honduras" },
                    new Country { CountryCode = "HK", CountryName = "Hong Kong" },
                    new Country { CountryCode = "HU", CountryName = "Hungary" },
                    new Country { CountryCode = "IS", CountryName = "Icel And" },
                    new Country { CountryCode = "IN", CountryName = "India" },
                    new Country { CountryCode = "ID", CountryName = "Indonesia" },
                    new Country { CountryCode = "IR", CountryName = "Iran (Islamic Republic Of)" },
                    new Country { CountryCode = "IQ", CountryName = "Iraq" },
                    new Country { CountryCode = "IE", CountryName = "Ireland" },
                    new Country { CountryCode = "IL", CountryName = "Israel" },
                    new Country { CountryCode = "IT", CountryName = "Italy" },
                    new Country { CountryCode = "JM", CountryName = "Jamaica" },
                    new Country { CountryCode = "JP", CountryName = "Japan" },
                    new Country { CountryCode = "JO", CountryName = "Jordan" },
                    new Country { CountryCode = "KZ", CountryName = "Kazakhstan" },
                    new Country { CountryCode = "KE", CountryName = "Kenya" },
                    new Country { CountryCode = "KI", CountryName = "Kiribati" },
                    new Country { CountryCode = "KP", CountryName = "Korea, Dem People'S Republic" },
                    new Country { CountryCode = "KR", CountryName = "Korea, Republic Of" },
                    new Country { CountryCode = "KW", CountryName = "Kuwait" },
                    new Country { CountryCode = "KG", CountryName = "Kyrgyzstan" },
                    new Country { CountryCode = "LA", CountryName = "Lao People'S Dem Republic" },
                    new Country { CountryCode = "LV", CountryName = "Latvia" },
                    new Country { CountryCode = "LB", CountryName = "Lebanon" },
                    new Country { CountryCode = "LS", CountryName = "Lesotho" },
                    new Country { CountryCode = "LR", CountryName = "Liberia" },
                    new Country { CountryCode = "LY", CountryName = "Libyan Arab Jamahiriya" },
                    new Country { CountryCode = "LI", CountryName = "Liechtenstein" },
                    new Country { CountryCode = "LT", CountryName = "Lithuania" },
                    new Country { CountryCode = "LU", CountryName ="Luxembourg" },
                    new Country { CountryCode = "MO", CountryName = "Macau" },
                    new Country { CountryCode = "MK", CountryName = "Macedonia" },
                    new Country { CountryCode = "MG", CountryName = "Madagascar" },
                    new Country { CountryCode = "MW", CountryName = "Malawi" },
                    new Country { CountryCode = "MY", CountryName = "Malaysia" },
                    new Country { CountryCode = "MV", CountryName = "Maldives" },
                    new Country { CountryCode = "ML", CountryName = "Mali" },
                    new Country { CountryCode = "MT", CountryName = "Malta" },
                    new Country { CountryCode = "MH", CountryName = "Marshall Islands" },
                    new Country { CountryCode = "MQ", CountryName = "Martinique" },
                    new Country { CountryCode = "MR", CountryName = "Mauritania" },
                    new Country { CountryCode = "MU", CountryName = "Mauritius" },
                    new Country { CountryCode = "YT", CountryName = "Mayotte" },
                    new Country { CountryCode = "MX", CountryName = "Mexico" },
                    new Country { CountryCode = "FM", CountryName = "Micronesia, Federated States" },
                    new Country { CountryCode = "MD", CountryName = "Moldova, Republic Of" },
                    new Country { CountryCode = "MC", CountryName = "Monaco" },
                    new Country { CountryCode = "MN", CountryName = "Mongolia" },
                    new Country { CountryCode = "MS", CountryName = "Montserrat" },
                    new Country { CountryCode = "MA", CountryName = "Morocco" },
                    new Country { CountryCode = "MZ", CountryName = "Mozambique" },
                    new Country { CountryCode = "MM", CountryName = "Myanmar" },
                    new Country { CountryCode = "NA", CountryName = "Namibia" },
                    new Country { CountryCode = "NR", CountryName = "Nauru" },
                    new Country { CountryCode = "NP", CountryName = "Nepal" },
                    new Country { CountryCode = "NL", CountryName = "Netherlands" },
                    new Country { CountryCode = "AN", CountryName = "Netherlands Ant Illes" },
                    new Country { CountryCode = "NC", CountryName = "New Caledonia" },
                    new Country { CountryCode = "NZ", CountryName = "New Zealand" },
                    new Country { CountryCode = "NI", CountryName = "Nicaragua" },
                    new Country { CountryCode = "NE", CountryName = "Niger" },
                    new Country { CountryCode = "NG", CountryName = "Nigeria" },
                    new Country { CountryCode = "NU", CountryName = "Niue" },
                    new Country { CountryCode = "NF", CountryName = "Norfolk Island" },
                    new Country { CountryCode = "MP", CountryName = "Northern Mariana Islands" },
                    new Country { CountryCode = "NO", CountryName = "Norway" },
                    new Country { CountryCode = "OM", CountryName = "Oman" },
                    new Country { CountryCode = "PK", CountryName = "Pakistan" },
                    new Country { CountryCode = "PW", CountryName = "Palau" },
                    new Country { CountryCode = "PA", CountryName = "Panama" },
                    new Country { CountryCode = "PG", CountryName = "Papua New Guinea" },
                    new Country { CountryCode = "PY", CountryName = "Paraguay" },
                    new Country { CountryCode = "PE", CountryName = "Peru" },
                    new Country { CountryCode = "PH", CountryName = "Philippines" },
                    new Country { CountryCode = "PN", CountryName = "Pitcairn" },
                    new Country { CountryCode = "PL", CountryName = "Poland" },
                    new Country { CountryCode = "PT", CountryName = "Portugal" },
                    new Country { CountryCode = "PR", CountryName = "Puerto Rico" },
                    new Country { CountryCode = "QA", CountryName = "Qatar" },
                    new Country { CountryCode = "RE", CountryName = "Reunion" },
                    new Country { CountryCode = "RO", CountryName = "Romania" },
                    new Country { CountryCode = "RU", CountryName = "Russian Federation" },
                    new Country { CountryCode = "RW", CountryName = "Rwanda" },
                    new Country { CountryCode = "KN", CountryName = "Saint K Itts And Nevis" },
                    new Country { CountryCode = "LC", CountryName = "Saint Lucia" },
                    new Country { CountryCode = "VC", CountryName = "Saint Vincent, The Grenadines" },
                    new Country { CountryCode = "WS", CountryName = "Samoa" },
                    new Country { CountryCode = "SM", CountryName = "San Marino" },
                    new Country { CountryCode = "ST", CountryName = "Sao Tome And Principe" },
                    new Country { CountryCode = "SA", CountryName = "Saudi Arabia" },
                    new Country { CountryCode = "SN", CountryName = "Senegal" },
                    new Country { CountryCode = "SC", CountryName = "Seychelles" },
                    new Country { CountryCode = "SL", CountryName = "Sierra Leone" },
                    new Country { CountryCode = "SG", CountryName = "Singapore" },
                    new Country { CountryCode = "SK", CountryName = "Slovakia (Slovak Republic)" },
                    new Country { CountryCode = "SI", CountryName = "Slovenia" },
                    new Country { CountryCode = "SB", CountryName = "Solomon Islands" },
                    new Country { CountryCode = "SO", CountryName = "Somalia" },
                    new Country { CountryCode = "ZA", CountryName = "South Africa" },
                    new Country { CountryCode = "GS", CountryName = "South Georgia , S Sandwich Is." },
                    new Country { CountryCode = "ES", CountryName = "Spain" },
                    new Country { CountryCode = "LK", CountryName = "Sri Lanka" },
                    new Country { CountryCode = "SH", CountryName = "St. Helena" },
                    new Country { CountryCode = "PM", CountryName = "St. Pierre And Miquelon" },
                    new Country { CountryCode = "SD", CountryName = "Sudan" },
                    new Country { CountryCode = "SR", CountryName = "Suriname" },
                    new Country { CountryCode = "SJ", CountryName = "Svalbard, Jan Mayen Islands" },
                    new Country { CountryCode = "SZ", CountryName = "Sw Aziland" },
                    new Country { CountryCode = "SE", CountryName = "Sweden" },
                    new Country { CountryCode = "CH", CountryName = "Switzerland" },
                    new Country { CountryCode = "SY", CountryName = "Syrian Arab Republic" },
                    new Country { CountryCode = "TW", CountryName = "Taiwan" },
                    new Country { CountryCode = "TJ", CountryName = "Tajikistan" },
                    new Country { CountryCode = "TZ", CountryName = "Tanzania, United Republic Of" },
                    new Country { CountryCode = "TH", CountryName = "Thailand" },
                    new Country { CountryCode = "TG", CountryName = "Togo" },
                    new Country { CountryCode = "TK", CountryName = "Tokelau" },
                    new Country { CountryCode = "TO", CountryName = "Tonga" },
                    new Country { CountryCode = "TT", CountryName = "Trinidad And Tobago" },
                    new Country { CountryCode = "TN", CountryName = "Tunisia" },
                    new Country { CountryCode = "TR", CountryName = "Turkey" },
                    new Country { CountryCode = "TM", CountryName = "Turkmenistan" },
                    new Country { CountryCode = "TC", CountryName = "Turks And Caicos Islands" },
                    new Country { CountryCode = "TV", CountryName = "Tuvalu" },
                    new Country { CountryCode = "UG", CountryName = "Uganda" },
                    new Country { CountryCode = "UA", CountryName = "Ukraine"},
                    new Country { CountryCode = "AE", CountryName = "United Arab Emirates" },
                    new Country { CountryCode = "GB", CountryName = "United Kingdom" },
                    new Country { CountryCode = "US", CountryName = "United States" },
                    new Country { CountryCode = "UM", CountryName = "United States Minor Is." },
                    new Country { CountryCode = "UY", CountryName = "Uruguay" },
                    new Country { CountryCode = "UZ", CountryName = "Uzbekistan" },
                    new Country { CountryCode = "VU", CountryName = "Vanuatu" },
                    new Country { CountryCode = "VE", CountryName = "Venezuela" },
                    new Country { CountryCode = "VN", CountryName = "Viet Nam" },
                    new Country { CountryCode = "VG", CountryName = "Virgin Islands (British)" },
                    new Country { CountryCode = "VI", CountryName = "Virgin Islands (U.S.)" },
                    new Country { CountryCode = "WF", CountryName = "Wallis And Futuna Islands" },
                    new Country { CountryCode = "EH", CountryName = "Western Sahara" },
                    new Country { CountryCode = "YE", CountryName = "Yemen" },
                    new Country { CountryCode = "ZR", CountryName = "Zaire" },
                    new Country { CountryCode = "ZM", CountryName = "Zambia" },
                    new Country { CountryCode = "ZW", CountryName = "Zimbabwe" }

                });
                _db.SaveChanges();



                //if roles are not created then , we will create admin user as well 

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "yassiradmin@gmail.com",
                    Email = "yassiradmin@gmail.com",
                    Name = "Yassir",
                    PhoneNumber = "0656893147",
                    PostalCode = "40000",
                    CountyNameId = 144,

                }, "Admin@123456").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "yassiradmin@gmail.com");

                _userManager.AddToRoleAsync(user, StaticDetail.Role_Admin).GetAwaiter().GetResult();

            }
            return;


        }
    }
}
