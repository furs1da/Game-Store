using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GameStore.Data.SeedData;


namespace GameStore.Data
{
    public partial class StoreContext : IdentityDbContext<User>
    {
        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = serviceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            string username = "admin";
            string password = "1OBb$^#0^u21!"; // #ruzziaIsTerroristState 
            string roleName = "Admin";

            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
            // if username doesn't exist, create it and add it to role
            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User { UserName = username };
                user.EmailConfirmed = true;
                user.Email = "gamestore.prog3050@gmail.com";

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
        public StoreContext()
        {
        }

        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CreditCard> CreditCards { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<FriendsFamily> FriendsFamilies { get; set; } = null!;
        public virtual DbSet<Game> Games { get; set; } = null!;
        public virtual DbSet<GameFeature> GameFeatures { get; set; } = null!;
        public virtual DbSet<GameImage> GameImages { get; set; } = null!;
        public virtual DbSet<MailingAddress> MailingAddresses { get; set; } = null!;
        public virtual DbSet<Merchandise> Merchandises { get; set; } = null!;
        public virtual DbSet<MerchandiseImage> MerchandiseImages { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Platform> Platforms { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<ReviewImage> ReviewImages { get; set; } = null!;
        public virtual DbSet<ShippingAddress> ShippingAddresses { get; set; } = null!;
        public virtual DbSet<WishList> WishLists { get; set; } = null!;
        public virtual DbSet<PlatformGame> PlatformGames { get; set; } = null!;
        public virtual DbSet<GameFeatureGame> GameFeatureGames { get; set; } = null!;
        public virtual DbSet<GameCategory> GameCategories { get; set; } = null!; 
        public virtual DbSet<CustomerEvent> CustomerEvents { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=PROG_3050;Integrated Security=True; TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<CreditCard>(entity =>
            {
                entity.HasKey(e => e.CreditId)
                    .HasName("PK__CreditCa__C15A9C36A58DE279");

                entity.ToTable("CreditCard");

                entity.Property(e => e.CreditId).HasColumnName("credit_id");

                entity.Property(e => e.CardName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("cardName");

                entity.Property(e => e.CardNumber).HasMaxLength(24).HasColumnName("cardNumber");

                entity.Property(e => e.CustId).HasColumnName("cust_id");

                entity.Property(e => e.ExpirationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expirationDate");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.CreditCards)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKCreditCard326673");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustId)
                    .HasName("PK__Customer__A1B71F90C6489007");

                entity.ToTable("Customer");

                entity.HasIndex(e => e.Email, "UQ__Customer__AB6E6164661522CF")
                    .IsUnique();

                entity.HasIndex(e => e.Nickname, "UQ__Customer__CC6CD17E283CABFD")
                    .IsUnique();

                entity.Property(e => e.CustId).HasColumnName("cust_id");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("dob");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("lastName");

                entity.Property(e => e.MailingAddressId).HasColumnName("mailingAddress_id");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PreferedCategoryId).HasColumnName("preferedCategory_id");

                entity.Property(e => e.PreferedPlatformId).HasColumnName("preferedPlatform_id");

                entity.Property(e => e.RecievePromotion).HasColumnName("recievePromotion");

                entity.Property(e => e.ShippingAddressId).HasColumnName("shippingAddress_id");

                entity.HasOne(d => d.MailingAddress)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.MailingAddressId)
                    .HasConstraintName("FK_Customer_MailingAddress");

                entity.HasOne(d => d.PreferedCategory)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.PreferedCategoryId)
                    .HasConstraintName("FKCustomer578024");

                entity.HasOne(d => d.PreferedPlatform)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.PreferedPlatformId)
                    .HasConstraintName("FKCustomer55737");

                entity.HasOne(d => d.ShippingAddress)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.ShippingAddressId)
                    .HasConstraintName("FK_Customer_ShippingAddress");

                entity.HasMany(d => d.Events)
                    .WithMany(p => p.Customers)
                    .UsingEntity<Dictionary<string, object>>(
                        "CustomerEvent",
                        l => l.HasOne<Event>().WithMany().HasForeignKey("Eventid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FKCustomer_E485607"),
                        r => r.HasOne<Customer>().WithMany().HasForeignKey("Customerid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FKCustomer_E75975"),
                        j =>
                        {
                            j.HasKey("Customerid", "Eventid").HasName("PK__Customer__333907D60F8269FF");

                            j.ToTable("CustomerEvent");
                        });
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK__Employee__1299A861977CC9A6");

                entity.ToTable("Employee");

                entity.Property(e => e.EmpId).HasColumnName("emp_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Duration)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("duration");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<FriendsFamily>(entity =>
            {
                entity.HasKey(e => e.FriendId)
                    .HasName("PK__FriendsF__3FA1E155F831F53C");

                entity.ToTable("FriendsFamily");

                entity.Property(e => e.FriendId).HasColumnName("friend_id");

                entity.Property(e => e.CustId1).HasColumnName("cust_id1");

                entity.Property(e => e.CustId2).HasColumnName("cust_id2");

                entity.Property(e => e.DateConnected)
                    .HasColumnType("datetime")
                    .HasColumnName("dateConnected");

                entity.Property(e => e.IsFamily).HasColumnName("isFamily");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.CustId1Navigation)
                    .WithMany(p => p.FriendsFamilyCustId1Navigations)
                    .HasForeignKey(d => d.CustId1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKFriendsFam820474");

                entity.HasOne(d => d.CustId2Navigation)
                    .WithMany(p => p.FriendsFamilyCustId2Navigations)
                    .HasForeignKey(d => d.CustId2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKFriendsFam820475");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("Game");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Digital).HasColumnName("digital");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.GameStudio).HasColumnName("gameStudio");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("releaseDate");

                entity.HasMany(d => d.Categories)
                    .WithMany(p => p.Games)
                    .UsingEntity<Dictionary<string, object>>(
                        "GameCategory",
                        l => l.HasOne<Category>().WithMany().HasForeignKey("Categoryid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FKGame_Categ786403"),
                        r => r.HasOne<Game>().WithMany().HasForeignKey("Gameid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FKGame_Categ26396"),
                        j =>
                        {
                            j.HasKey("Gameid", "Categoryid").HasName("PK__Game_Cat__0B57EBB7A12BD34A");

                            j.ToTable("GameCategory");
                        });
            });

            modelBuilder.Entity<GameFeature>(entity =>
            {
                entity.HasKey(e => e.FeatureId)
                    .HasName("PK__GameFeat__7906CBD7BF1F29A9");

                entity.ToTable("GameFeature");

                entity.Property(e => e.FeatureId).HasColumnName("feature_id");

                entity.Property(e => e.Feature)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("feature");

                entity.HasMany(d => d.Games)
                    .WithMany(p => p.GameFeatures)
                    .UsingEntity<Dictionary<string, object>>(
                        "GameFeatureGame",
                        l => l.HasOne<Game>().WithMany().HasForeignKey("Gameid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FKGameFeatur387831"),
                        r => r.HasOne<GameFeature>().WithMany().HasForeignKey("GameFeatureid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FKGameFeatur415415"),
                        j =>
                        {
                            j.HasKey("GameFeatureid", "Gameid").HasName("PK__GameFeat__0E2BBB9711BBC777");

                            j.ToTable("GameFeatureGame");
                        });
            });

            modelBuilder.Entity<GameImage>(entity =>
            {
                entity.HasKey(e => e.GameImgId)
                    .HasName("PK__GameImag__132575EA33968AE7");

                entity.ToTable("GameImage");

                entity.Property(e => e.GameImgId).HasColumnName("gameImg_id");

                entity.Property(e => e.Extention)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("extention");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.IsCoverImage).HasColumnName("isCoverImage");

                entity.Property(e => e.Path)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("path");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GameImages)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKGameImage381528");
            });

            modelBuilder.Entity<MailingAddress>(entity =>
            {
                entity.ToTable("MailingAddress");

                entity.Property(e => e.MailingAddressId)
                    .ValueGeneratedNever()
                    .HasColumnName("mailingAddressId");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Merchandise>(entity =>
            {
                entity.HasKey(e => e.MerchId)
                    .HasName("PK__Merchand__1937E9C0E88FCC6F");

                entity.ToTable("Merchandise");

                entity.Property(e => e.MerchId).HasColumnName("merch_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");
            });

            modelBuilder.Entity<MerchandiseImage>(entity =>
            {
                entity.HasKey(e => e.MerchImgId)
                    .HasName("PK__Merchand__4D99D94A9EAE7002");

                entity.ToTable("MerchandiseImage");

                entity.Property(e => e.MerchImgId).HasColumnName("merchImg_id");

                entity.Property(e => e.Extention)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("extention");

                entity.Property(e => e.MerchandiseId).HasColumnName("merchandise_id");

                entity.Property(e => e.Path)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("path");

                entity.HasOne(d => d.Merchandise)
                    .WithMany(p => p.MerchandiseImages)
                    .HasForeignKey(d => d.MerchandiseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKMerchandis483326");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.CustId).HasColumnName("cust_id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.IsShipped).HasColumnName("isShipped");

                entity.Property(e => e.MerchandiseId).HasColumnName("merchandise_id");

                entity.Property(e => e.OrderNo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("order_no");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOrder861201");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOrder547560");

                entity.HasOne(d => d.Merchandise)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MerchandiseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOrder609133");
            });

            modelBuilder.Entity<Platform>(entity =>
            {
                entity.ToTable("Platform");

                entity.Property(e => e.PlatformId).HasColumnName("platform_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasMany(d => d.Games)
                    .WithMany(p => p.Platforms)
                    .UsingEntity<Dictionary<string, object>>(
                        "PlatformGame",
                        l => l.HasOne<Game>().WithMany().HasForeignKey("Gameid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FKPlatform_G103358"),
                        r => r.HasOne<Platform>().WithMany().HasForeignKey("Platformid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FKPlatform_G477683"),
                        j =>
                        {
                            j.HasKey("Platformid", "Gameid").HasName("PK__Platform__B7F882CFAE338AEA");

                            j.ToTable("PlatformGame");
                        });
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.ReviewId).HasColumnName("review_id");

                entity.Property(e => e.CustId).HasColumnName("cust_id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.Rate).HasColumnName("rate");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKReview172984");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKReview486625");
            });

            modelBuilder.Entity<ReviewImage>(entity =>
            {
                entity.HasKey(e => e.ReviewImgId)
                    .HasName("PK__ReviewIm__ADCB53FF60CE66D7");

                entity.ToTable("ReviewImage");

                entity.Property(e => e.ReviewImgId).HasColumnName("reviewImg_id");

                entity.Property(e => e.Extention)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("extention");

                entity.Property(e => e.Path)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("path");

                entity.Property(e => e.ReviewId).HasColumnName("review_id");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.ReviewImages)
                    .HasForeignKey(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKReviewImag856029");
            });

            modelBuilder.Entity<ShippingAddress>(entity =>
            {
                entity.ToTable("ShippingAddress");

                entity.Property(e => e.ShippingAddressId)
                    .ValueGeneratedNever()
                    .HasColumnName("shippingAddressId");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WishList>(entity =>
            {
                entity.HasKey(e => e.WishId)
                    .HasName("PK__WishList__4F227CA0D99D4B73");

                entity.ToTable("WishList");

                entity.Property(e => e.WishId).HasColumnName("wish_id");

                entity.Property(e => e.CustId).HasColumnName("cust_id");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.MerchandiseId).HasColumnName("merchandise_id");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.WishLists)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.WishLists)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Merchandise)
                    .WithMany(p => p.WishLists)
                    .HasForeignKey(d => d.MerchandiseId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // composite primary key for PlatformGame
            modelBuilder.Entity<PlatformGame>()
                .HasKey(pg => new { pg.Platformid, pg.Gameid });

            // one-to-many relationship between Platform and PlatformGame
            modelBuilder.Entity<PlatformGame>()
                .HasOne(pg => pg.Platform)
                .WithMany(p => p.PlatformGames)
                .HasForeignKey(pg => pg.Platformid);

            // one-to-many relationship between Game and PlatformGame
            modelBuilder.Entity<PlatformGame>()
                .HasOne(pg => pg.Game)
                .WithMany(p => p.PlatformGames)
                .HasForeignKey(pg => pg.Gameid);


            // composite primary key for GameFeatureGame
            modelBuilder.Entity<GameFeatureGame>()
                .HasKey(gfg => new { gfg.GameFeatureid, gfg.Gameid });

            // one-to-many relationship between GameFeature and GameFeatureGame
            modelBuilder.Entity<GameFeatureGame>()
                .HasOne(gfg => gfg.GameFeature)
                .WithMany(gf => gf.GameFeatureGames)
                .HasForeignKey(gfg => gfg.GameFeatureid);

            // one-to-many relationship between Game and GameFeatureGame
            modelBuilder.Entity<GameFeatureGame>()
                .HasOne(gfg => gfg.Game)
                .WithMany(g => g.GameFeatureGames)
                .HasForeignKey(gfg => gfg.Gameid);



            // composite primary key for GameCategory
            modelBuilder.Entity<GameCategory>()
                .HasKey(gc => new { gc.Gameid, gc.Categoryid });

            // one-to-many relationship between Category and GameCategory
            modelBuilder.Entity<GameCategory>()
                .HasOne(gc => gc.Category)
                .WithMany(c => c.GameCategories)
                .HasForeignKey(gc => gc.Categoryid);

            // one-to-many relationship between Game and GameCategory
            modelBuilder.Entity<GameCategory>()
                .HasOne(gc => gc.Game)
                .WithMany(g => g.GameCategories)
                .HasForeignKey(gc => gc.Gameid);



            // composite primary key for CustomerEvent
            modelBuilder.Entity<CustomerEvent>()
                .HasKey(ce => new { ce.Customerid, ce.Eventid });

            // one-to-many relationship between Customer and CustomerEvent
            modelBuilder.Entity<CustomerEvent>()
                .HasOne(ce => ce.Customer)
                .WithMany(c => c.CustomerEvents)
                .HasForeignKey(ce => ce.Customerid);

            // one-to-many relationship between Event and CustomerEvent
            modelBuilder.Entity<CustomerEvent>()
                .HasOne(ce => ce.Event)
                .WithMany(g => g.CustomerEvents)
                .HasForeignKey(ce => ce.Eventid);

            OnModelCreatingPartial(modelBuilder);

            modelBuilder.ApplyConfiguration(new SeedPlatform());
            modelBuilder.ApplyConfiguration(new SeedCategory());
            modelBuilder.ApplyConfiguration(new SeedGameFeature());

            modelBuilder.ApplyConfiguration(new SeedGames());

            modelBuilder.ApplyConfiguration(new SeedGameCategory());
            modelBuilder.ApplyConfiguration(new SeedPlatformGame());
            modelBuilder.ApplyConfiguration(new SeedGameFeatureGame());
            modelBuilder.ApplyConfiguration(new SeedEvent());
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
