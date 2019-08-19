using A2ZOnlineStore.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A2ZOnlineStore.Data
{
    public class DbInitializer
    {
        private static Dictionary<string, Category> categories;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    var genresList = new Category[]
                    {
                        new Category { CategoryName = "Clothes", Description="Clothing Products" },
                        new Category { CategoryName = "Shoes", Description="Running Shoes Products" },
                        new Category { CategoryName = "Drinks", Description="Drink Products" }
                    };

                    categories = new Dictionary<string, Category>();

                    foreach (Category genre in genresList)
                    {
                        categories.Add(genre.CategoryName, genre);
                    }
                }

                return categories;
            }
        }


        private static Dictionary<string, Brand> brands;
        public static Dictionary<string, Brand> Brands
        {
            get
            {
                if (brands == null)
                {
                    var genresList = new Brand[]
                    {
                        new Brand { Name = "Summer"},
                        new Brand { Name = "Winter"},
                        new Brand { Name = "Addidas"},
                        new Brand { Name = "Puma"},
                        new Brand { Name = "Nike"},
                        new Brand { Name = "Alcoholic"},
                        new Brand { Name = "Non-Alcoholic"},

                    };

                    brands = new Dictionary<string, Brand>();

                    foreach (Brand genre in genresList)
                    {
                        brands.Add(genre.Name, genre);
                    }
                }

                return brands;
            }
        }

        public static void Seed(AppDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //AppDbContext context =
            //    applicationBuilder.ApplicationServices.GetRequiredService<AppDbContext>();

            context.Database.Migrate();
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(Categories.Select(c => c.Value));
            }

            if (!context.Brands.Any())
            {
                context.Brands.AddRange(Brands.Select(c => c.Value));
            }
            
            if(!roleManager.Roles.Any())
            {
                var roleCreated = roleManager.CreateAsync(new IdentityRole
                {
                    Name = "admin"
                }).Result;
                if(roleCreated.Succeeded)
                {
                    if(!userManager.Users.Any())
                    {
                        var user = new IdentityUser
                        {
                            Email = "admin@atz.com",
                            EmailConfirmed = true,
                            UserName = "Administrator"
                        };
                        var userCreated = userManager.CreateAsync(user, "P@ssw0rd").Result;
                        if(userCreated.Succeeded)
                        {
                            var userroleCreated= userManager.AddToRoleAsync(user, "admin").Result;
                        }
                    }
                }
            }

            if (!context.Products.Any())
            {
                context.AddRange
                (
                    new Product
                    {
                        Name = "World Star",
                        Price = 199.5M,
                        ShortDescription = "Shoes for next century",
                        LongDescription = "Shoes for next century",
                        Brand = Brands["Addidas"],
                        Category = Categories["Shoes"],
                        ImageUrl = "http://imgh.us/beerL_2.jpg",
                        InStock = true,
                        IsPreferredProduct = true,
                        ImageThumbnailUrl = "http://imgh.us/beerS_1.jpeg"
                    },
                    new Product
                    {
                        Name = "White Line",
                        Price = 199.5M,
                        ShortDescription = "will make you world champions",
                        LongDescription = "will make you world champions",
                        Brand = Brands["Puma"],
                        Category = Categories["Shoes"],
                        ImageUrl = "http://imgh.us/beerL_2.jpg",
                        InStock = true,
                        IsPreferredProduct = true,
                        ImageThumbnailUrl = "http://imgh.us/beerS_1.jpeg"
                    },
                    new Product
                    {
                        Name = "Blue Star",
                        Price = 199.5M,
                        ShortDescription = "Lala Land",
                        LongDescription = "Lala Land",
                        Brand = Brands["Nike"],
                        Category = Categories["Shoes"],
                        ImageUrl = "http://imgh.us/beerL_2.jpg",
                        InStock = true,
                        IsPreferredProduct = true,
                    },
                        new Product
                    {
                        Name = "Beer",
                        Price = 7.95M,
                        ShortDescription = "The most widely consumed alcohol",
                        LongDescription = "Beer is the world's oldest[1][2][3] and most widely consumed[4] alcoholic Product; it is the third most popular Product overall, after water and tea.[5] The production of beer is called brewing, which involves the fermentation of starches, mainly derived from cereal grains—most commonly malted barley, although wheat, maize (corn), and rice are widely used.[6] Most beer is flavoured with hops, which add bitterness and act as a natural preservative, though other flavourings such as herbs or fruit may occasionally be included. The fermentation process causes a natural carbonation effect, although this is often removed during processing, and replaced with forced carbonation.[7] Some of humanity's earliest known writings refer to the production and distribution of beer: the Code of Hammurabi included laws regulating beer and beer parlours.",
                        Brand = Brands["Alcoholic"],
                        Category = Categories["Drinks"],
                        ImageUrl = "http://imgh.us/beerL_2.jpg",
                        InStock = true,
                        IsPreferredProduct = true,
                        ImageThumbnailUrl = "http://imgh.us/beerS_1.jpeg"
                    },
                    new Product
                    {
                        Name = "Tea ",
                        Price = 12.95M,
                        ShortDescription = "Made by leaves of the tea plant in hot water.",
                        LongDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                        Brand = Brands["Non-Alcoholic"],
                        Category = Categories["Drinks"],
                        ImageUrl = "http://imgh.us/teaL.jpg",
                        InStock = true,
                        IsPreferredProduct = true,
                        ImageThumbnailUrl = "http://imgh.us/teaS.jpg"
                    },
                    new Product
                    {
                        Name = "Water ",
                        Price = 12.95M,
                        ShortDescription = " It makes up more than half of your body weight ",
                        LongDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                        Brand = Brands["Non-Alcoholic"],
                        Category = Categories["Drinks"],
                        ImageUrl = "http://imgh.us/waterL.jpg",
                        InStock = true,
                        IsPreferredProduct = false,
                        ImageThumbnailUrl = "http://imgh.us/waterS_1.jpg"
                    },
                    new Product
                    {
                        Name = "Coffee ",
                        Price = 12.95M,
                        ShortDescription = " A beverage prepared from coffee beans",
                        LongDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                        Brand = Brands["Non-Alcoholic"],
                        Category = Categories["Drinks"],
                        ImageUrl = "http://imgh.us/coffeeL.jpg",
                        InStock = true,
                        IsPreferredProduct = true,
                        ImageThumbnailUrl = "http://imgh.us/coffeS.jpg"
                    },
                    new Product
                    {
                        Name = "Juice ",
                        Price = 12.95M,
                        ShortDescription = "Naturally contained in fruit or vegetable tissue.",
                        LongDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                        Brand = Brands["Non-Alcoholic"],
                        Category = Categories["Drinks"],
                        ImageUrl = "http://imgh.us/juiceL.jpg",
                        InStock = true,
                        IsPreferredProduct = false,
                        ImageThumbnailUrl = "http://imgh.us/juiceS.jpg"
                    }
                );
            }

            context.SaveChanges();
        }

        
    }
}