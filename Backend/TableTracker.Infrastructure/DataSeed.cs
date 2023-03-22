using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

using TableTracker.Domain.Entities;
using TableTracker.Domain.Enums;
using TableTracker.Infrastructure.Identity;

namespace TableTracker.Infrastructure
{
    public class DataSeed
    {
        private readonly UserManager<TableTrackerIdentityUser> _userManager;
        private readonly RoleManager<TableTrackerIdentityRole> _roleManager;

        public DataSeed(UserManager<TableTrackerIdentityUser> userManger, RoleManager<TableTrackerIdentityRole> roleManager)
        {
            _userManager = userManger;
            _roleManager = roleManager;
        }

        public async Task SeedData(TableDbContext context, IdentityTableDbContext identityContext)
        {

            #region Roles
            string[] roleNames = { "Admin", "Developer", "Manager", "Waiter", "Visitor", "User" };
            IdentityResult roleResult;

            var restaurants = new List<Restaurant>();
            var tableTrackerIdentityUsers = new List<TableTrackerIdentityUser>();
            var cuisines = new List<Cuisine>();
            var franchises = new List<Franchise>();
            var managers = new List<Manager>();
            var layouts = new List<Layout>();
            var visitors = new List<Visitor>();
            var tables = new List<Table>();
            var reservations = new List<Reservation>();
            var restVisitor = new List<RestaurantVisitor>();
            var history = new List<VisitorHistory>();
            var images = new List<Image>();

            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);

                if (!roleExist)
                {
                    roleResult = await _roleManager.CreateAsync(new TableTrackerIdentityRole() { Name = roleName });
                }
            }
            #endregion

            #region IdentityUsers
            if (!identityContext.TableTrackerIdentityUsers.Any())
            {

                for (int i = 0; i < 30; i++)
                {
                    TableTrackerIdentityUser userToAdd;

                    if (i < 10)
                    {
                        userToAdd = new TableTrackerIdentityUser
                        {
                            UserName = $"Generic Boy {i + 1}",
                            Email = $"exampleEmail{i + 1}@service.domain",
                        };

                        tableTrackerIdentityUsers.Add(userToAdd);

                        var result = await _userManager.CreateAsync(userToAdd, "Secret123$");

                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(userToAdd, Enum.GetName(typeof(UserRole), UserRole.Manager));
                        }
                    }
                    else if (i < 20)
                    {
                        userToAdd = new TableTrackerIdentityUser
                        {
                            UserName = $"Juan{i + 1}",
                            Email = $"MehBruh{i + 1}@service.domain",
                        };
                        var result = await _userManager.CreateAsync(userToAdd, "Secret123$");

                        tableTrackerIdentityUsers.Add(userToAdd);


                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(userToAdd, Enum.GetName(typeof(UserRole), UserRole.Visitor));
                        }
                    }
                    else if(i < 30)
                    {
                        userToAdd = new TableTrackerIdentityUser
                        {
                            UserName = $"DefaultChad{i + 1}",
                            Email = $"WaiterClone{i + 1}@idk.com",
                        };
                        var result = await _userManager.CreateAsync(userToAdd, "Secret123$");

                        tableTrackerIdentityUsers.Add(userToAdd);


                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(userToAdd, Enum.GetName(typeof(UserRole), UserRole.Waiter));
                        }
                    }
                }
            }
            #endregion


            #region Cuisine

            if (!context.Cuisines.Any())
            {
                cuisines.Add(new Cuisine { CuisineName = "International" });
                cuisines.Add(new Cuisine { CuisineName = "Ukranian" });
                cuisines.Add(new Cuisine { CuisineName = "Polish" });
                cuisines.Add(new Cuisine { CuisineName = "English" });
                cuisines.Add(new Cuisine { CuisineName = "Italian" });
                cuisines.Add(new Cuisine { CuisineName = "German" });
                cuisines.Add(new Cuisine { CuisineName = "French" });
                cuisines.Add(new Cuisine { CuisineName = "American" });
                cuisines.Add(new Cuisine { CuisineName = "Georgian" });
                cuisines.Add(new Cuisine { CuisineName = "Japanese" });
                cuisines.Add(new Cuisine { CuisineName = "Swedish" });
                cuisines.Add(new Cuisine { CuisineName = "European" });

                await context.AddRangeAsync(cuisines);
            }

            #endregion

            #region Frachise

            if (!context.Franchises.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    franchises.Add(new Franchise { Name = $"{i + 1} franchise" });
                }

                await context.AddRangeAsync(franchises);
            }

            #endregion

            #region Images

            images.Add(new Image { Name = "63f686a1-b0ae-47e7-adbd-4e06708e5294.jpg" });
            images.Add(new Image { Name = "8c9eda00-d482-4a34-9d35-98a7fef67d1f.jpg" });
            images.Add(new Image { Name = "70937ef2-dd49-4c52-b648-9ad422b04699.jpg" });
            images.Add(new Image { Name = "951e2e2b-0b55-4928-a8a5-9f8710d873c7.jpg" });
            images.Add(new Image { Name = "fb3c5bac-2e27-4c21-8ac7-3034d3a139ef.jpg" });
            images.Add(new Image { Name = "f479f4cb-4c2e-43cd-b957-ee9f81677851.jpg" });
            images.Add(new Image { Name = "6efb8a54-8bf8-4d02-9a0d-3ec812c0363a.jpg" });

            await context.AddRangeAsync(images);

            #endregion

            #region Restaurant

            if (!context.Restaurants.Any())
            {
                restaurants.Add(new Restaurant
                {
                    Name = "Baczewski Restaurant",
                    Description = "The Baczewski family has known all over Europe and the world since 1782, when they opened a factory for mass production of vodka. Now the legendary vodka has returned to Lviv...",
                    Discount = Discount.NoDiscount,
                    Franchise = franchises[0],
                    NumberOfTables = 10,
                    PriceRange = 3,
                    Rating = 5,
                    Type = RestaurantType.Restaurant,
                    Cuisines = new[]
                    {
                        cuisines[2],
                        cuisines[11],
                    },
                    MainImage = images[0],
                    DateOfOpening = new DateTime(2022, 01, 01),

                    Address = "Shevska St, 8, Lviv, Lviv Oblast, 79000",
                    Email = "b.reserve@kumpelgroup.com",
                    Phone = "+38 (067) 006 1111",
                    Website = "https://baczewski.com.ua/",
                    Menu = "https://kumpelgroup.com/wp-content/uploads/menu_baczewski_october2020-1.pdf"
                });

                restaurants.Add(new Restaurant
                {
                    Name = "Edison Pub",
                    Description = "Best place for a good evening, good music, nice drinks or deliciuos food! Also good shots.",
                    Discount = Discount.NoDiscount,
                    Franchise = franchises[0],
                    NumberOfTables = 10,
                    PriceRange = 2,
                    Rating = 3,
                    Type = RestaurantType.Restaurant,
                    Cuisines = new[]
                    {
                        cuisines[11],
                    },
                    MainImage = images[1],
                    DateOfOpening = new DateTime(2022, 03, 01),

                    Address = "Valova St, 23, Lviv, Lviv Oblast, 79000",
                    Email = "edisonpublviv@gmail.com",
                    Phone = "+38 (099) 210 7840",
                    Website = "https://www.facebook.com/Edison-Pub-2286943744873179/",
                    Menu = "http://edisonpub.com/uk/menu"
                });

                restaurants.Add(new Restaurant
                {
                    Name = "Teddy Restaurant",
                    Description = "Great and cozy place with open kitchen. Very tasty pasta and big pizza. Delicious meals and really cute teddy bear sitting next to you.",
                    Discount = Discount.NoDiscount,
                    Franchise = franchises[0],
                    NumberOfTables = 10,
                    PriceRange = 3,
                    Rating = 5,
                    Type = RestaurantType.Restaurant,
                    Cuisines = new[]
                    {
                        cuisines[4],
                    },
                    MainImage = images[2],
                    DateOfOpening = new DateTime(2022, 02, 01),

                    Address = "Chaikovs'koho St, 20, Lviv, Lviv Oblast, 79000",
                    Email = "bughalter_cukor@ukr.net",
                    Phone = "+38 (097) 337 9356",
                    Website = "https://teddy.cukor.lviv.ua/",
                    Menu = "https://teddy.cukor.lviv.ua/"
                });

                restaurants.Add(new Restaurant
                {
                    Name = "Sowa",
                    Description = "One of the best places in Lviv to visit. Very tasty food complemented by the cute interior of the cafe.",
                    Discount = Discount.NoDiscount,
                    Franchise = franchises[0],
                    NumberOfTables = 10,
                    PriceRange = 2,
                    Rating = 4,
                    Type = RestaurantType.Restaurant,
                    Cuisines = new[]
                    {
                        cuisines[0],
                        cuisines[11],
                    },
                    MainImage = images[3],
                    DateOfOpening = new DateTime(2022, 01, 02),

                    Address = "Staroievreiska St, 40, Lviv, Lviv Oblast, 79000",
                    Email = "roksolana.bl@gmail.com",
                    Phone = "+38 (098) 634 9046",
                    Website = "https://sowa.choiceqr.com/",
                    Menu = "https://sowa.choiceqr.com/"
                });

                restaurants.Add(new Restaurant
                {
                    Name = "Culinary Studio Kryva Lypa",
                    Description = "All in all fantastic food, great service, very friendly and helpful. Food came on time and timed with each other. Would recommend this restaurant to everyone coming to lviv...",
                    Discount = Discount.NoDiscount,
                    Franchise = franchises[0],
                    NumberOfTables = 10,
                    PriceRange = 3,
                    Rating = 3,
                    Type = RestaurantType.Restaurant,
                    Cuisines = new[]
                    {
                        cuisines[2],
                        cuisines[4],
                        cuisines[11],
                    },
                    MainImage = images[4],
                    DateOfOpening = new DateTime(2022, 02, 02),
                    
                    Address = "Kryva Lypa Passage, 8, Lviv, Lviv Oblast, 79000",
                    Email = "info@kryva-lypa.com",
                    Phone = "+38 (098) 094 8101",
                    Website = "http://kryva-lypa.com.ua/",
                    Menu = "http://kryva-lypa.com.ua/#eat_menu"
                });

                await context.AddRangeAsync(restaurants);
            }

            #endregion

            #region Manager

            if (!context.Managers.Any())
            {
                //TODO: Define Rest find 

                for (int i = 0; i < restaurants.Count; i++)
                {
                    managers.Add(new Manager
                    {
                        Restaurant = restaurants[i],
                        ManagerState = ManagerState.Unocupied,
                        Email = $"exampleEmail{i+1}@service.domain",
                        FullName = $"Generic Boy {i + 1}",
                    });

                    //Restaurant rest = context.Restaurants.Find(restaurants[i].Id);
                    restaurants[i].Manager = managers[^1];

                }

                managers[0].Avatar = images[5];

                await context.AddRangeAsync(managers);
            }

            #endregion

            #region Layout

            if (!context.Layouts.Any())
            {
                //TODO: Define Rest find

                for (int i = 0; i < restaurants.Count; i++)
                {
                    layouts.Add(new Layout { LayoutData = 0, Restaurant = restaurants[i] });

                    //Restaurant rest = context.Restaurants.Find((long) i + 1);
                    restaurants[i].Layout = layouts[^1];

                    //context.Restaurants.Update(restaurants[i]);
                }

                await context.AddRangeAsync(layouts);
            }

            #endregion;

            #region Visitor

            if (!context.Visitors.Any())
            {
                //TODO link IdentityUsers

                for (int i = 0; i < 10; i++)
                {
                    visitors.Add(new Visitor
                    {
                        Email = $"MehBruh{i + 1}@service.domain",
                        FullName = $"Juan{i + 1}",
                        GeneralTrustFactor = 2,
                    });
                }

                visitors[0].Avatar = images[6];

                await context.AddRangeAsync(visitors);
            }

            #endregion

            #region Table

            if (!context.Tables.Any())
            {

                for (int i = 0; i < restaurants.Count * 10; i++)
                {

                    switch (i/(restaurants.Count*2))
                    {
                        case 0:
                            tables.Add(new Table
                            {
                                Floor = i + 1,
                                State = TableState.Unoccupied,
                                NumberOfSeats = 4,
                                TableSize = 10,
                                Number = 1,
                                Restaurant = restaurants[0],
                            });
                            break;

                        case 1:
                            tables.Add(new Table
                            {
                                Floor = i + 1,
                                State = TableState.Unoccupied,
                                NumberOfSeats = 4,
                                TableSize = 10,
                                Number = 1,
                                Restaurant = restaurants[1],
                            });
                            break;

                        case 2:
                            tables.Add(new Table
                            {
                                Floor = i + 1,
                                State = TableState.Unoccupied,
                                NumberOfSeats = 4,
                                TableSize = 10,
                                Number = 1,
                                Restaurant = restaurants[2],
                            });
                            break;

                        case 3:
                            tables.Add(new Table
                            {
                                Floor = i + 1,
                                State = TableState.Unoccupied,
                                NumberOfSeats = 4,
                                TableSize = 10,
                                Number = 1,
                                Restaurant = restaurants[3],
                            });
                            break;

                        case 4:
                            tables.Add(new Table
                            {
                                Floor = i + 1,
                                State = TableState.Unoccupied,
                                NumberOfSeats = 4,
                                TableSize = 10,
                                Number = 1,
                                Restaurant = restaurants[4],
                            });
                            break;

                        default:
                            break;
                    }


                    
                }

                await context.AddRangeAsync(tables);
            }

            #endregion

            #region Reservation

            if (!context.Reservations.Any())
            {
                for (int i = 0; i < restaurants.Count; i++)
                {
                    var dateToPush = DateTime.Today.AddDays(1);
                    TimeSpan ts = new TimeSpan(11, 0, 0);
                    dateToPush = dateToPush.Date + ts;

                    reservations.Add(new Reservation
                    {
                        Date = dateToPush,
                        Table = tables[i],
                        Visitor = visitors[0],
                    });
                }

                await context.AddRangeAsync(reservations);
            }

            #endregion

            #region RestaurantVisitor

            if (!context.RestaurantVisitors.Any())
            {

                for (int i = 0; i < restaurants.Count; i++)
                {
                    restVisitor.Add(new RestaurantVisitor
                    {
                        Restaurant = restaurants[0],
                        AverageMoneySpent = 10,
                        RestaurantRate = 5,
                        TimesVisited = i + 1,
                        Visitor = visitors[i],
                    });
                }

                await context.AddRangeAsync(restVisitor);
            }

            #endregion

            #region VisitorHistory

            if (!context.VisitorHistorys.Any())
            {
                for (int i = 0; i < restaurants.Count; i++)
                {
                    history.Add(new VisitorHistory
                    {
                        DateTime = DateTime.Now,
                        Restaurant = restaurants[0],
                        Visitor = visitors[i],
                    });
                }

               await context.AddRangeAsync(history);
            }

            #endregion

            await context.SaveChangesAsync();

            await identityContext.SaveChangesAsync();
        }
    }
}
